using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Xml.Serialization;
using Microsoft.Data.Sqlite;

namespace Mapper
{
    // Cada colección conserva registros independientes en SQLite. Los XML anteriores
    // sólo se consultan una vez para migrar datos existentes sin perderlos.
    public static class SqliteStore
    {
        private static readonly JsonSerializerOptions JsonOptions = CrearOpciones();
        public static string DatabasePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "gruppe_sechs.sqlite3");

        private static JsonSerializerOptions CrearOpciones()
        {
            var resolver = new DefaultJsonTypeInfoResolver();
            resolver.Modifiers.Add(info =>
            {
                foreach (var property in info.Properties.ToArray())
                    if (property.AttributeProvider?.IsDefined(typeof(XmlIgnoreAttribute), true) == true)
                        info.Properties.Remove(property);
            });
            return new JsonSerializerOptions { TypeInfoResolver = resolver, PropertyNameCaseInsensitive = true };
        }

        private static SqliteConnection Abrir()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(DatabasePath)!);
            var connection = new SqliteConnection(new SqliteConnectionStringBuilder
            {
                DataSource = DatabasePath,
                Mode = SqliteOpenMode.ReadWriteCreate,
                Pooling = false
            }.ToString());
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "PRAGMA busy_timeout=5000; CREATE TABLE IF NOT EXISTS records (collection TEXT NOT NULL, ordinal INTEGER NOT NULL, document TEXT NOT NULL, PRIMARY KEY(collection, ordinal)); CREATE TABLE IF NOT EXISTS migrations (collection TEXT PRIMARY KEY);";
            command.ExecuteNonQuery();
            return connection;
        }

        private static string Coleccion(string fileName) => Path.GetFileNameWithoutExtension(fileName).ToLowerInvariant();

        public static List<T> Leer<T>(string fileName)
        {
            using var connection = Abrir();
            var collection = Coleccion(fileName);
            using var count = connection.CreateCommand();
            count.CommandText = "SELECT COUNT(*) FROM migrations WHERE collection = $collection";
            count.Parameters.AddWithValue("$collection", collection);
            if ((long)count.ExecuteScalar()! == 0)
            {
                if (File.Exists(fileName)) MigrarXml<T>(connection, collection, fileName);
                MarcarMigrada(connection, collection);
            }

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT document FROM records WHERE collection = $collection ORDER BY ordinal";
            command.Parameters.AddWithValue("$collection", collection);
            using var reader = command.ExecuteReader();
            var result = new List<T>();
            while (reader.Read())
                result.Add(JsonSerializer.Deserialize<T>(reader.GetString(0), JsonOptions)
                    ?? throw new InvalidDataException($"Registro inválido en {collection}."));
            return result;
        }

        private static void MigrarXml<T>(SqliteConnection connection, string collection, string fileName)
        {
            if (new FileInfo(fileName).Length == 0) return;
            List<T> oldData;
            using (var stream = File.OpenRead(fileName))
                oldData = (new XmlSerializer(typeof(List<T>)).Deserialize(stream) as List<T>)
                    ?? throw new InvalidDataException($"No se pudo migrar {fileName}.");
            GuardarEnConexion(connection, collection, oldData);
        }

        public static void Guardar<T>(string fileName, List<T> data)
        {
            using var connection = Abrir();
            var collection = Coleccion(fileName);
            GuardarEnConexion(connection, collection, data ?? new List<T>());
            MarcarMigrada(connection, collection);
        }

        private static void MarcarMigrada(SqliteConnection connection, string collection)
        {
            using var command = connection.CreateCommand();
            command.CommandText = "INSERT OR IGNORE INTO migrations(collection) VALUES($collection)";
            command.Parameters.AddWithValue("$collection", collection);
            command.ExecuteNonQuery();
        }

        private static void GuardarEnConexion<T>(SqliteConnection connection, string collection, List<T> data)
        {
            using var transaction = connection.BeginTransaction();
            using (var delete = connection.CreateCommand())
            {
                delete.Transaction = transaction;
                delete.CommandText = "DELETE FROM records WHERE collection = $collection";
                delete.Parameters.AddWithValue("$collection", collection);
                delete.ExecuteNonQuery();
            }
            for (var i = 0; i < data.Count; i++)
            {
                using var insert = connection.CreateCommand();
                insert.Transaction = transaction;
                insert.CommandText = "INSERT INTO records(collection, ordinal, document) VALUES($collection, $ordinal, $document)";
                insert.Parameters.AddWithValue("$collection", collection);
                insert.Parameters.AddWithValue("$ordinal", i);
                insert.Parameters.AddWithValue("$document", JsonSerializer.Serialize(data[i], JsonOptions));
                insert.ExecuteNonQuery();
            }
            transaction.Commit();
        }

        public static void CrearCopia(string destination)
        {
            using var source = Abrir();
            Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(destination))!);
            using var target = new SqliteConnection(new SqliteConnectionStringBuilder { DataSource = destination, Pooling = false }.ToString());
            target.Open();
            source.BackupDatabase(target);
        }

        public static void RestaurarCopia(string sourcePath)
        {
            if (!File.Exists(sourcePath)) throw new FileNotFoundException("No se encontró el respaldo.", sourcePath);
            using var source = new SqliteConnection(new SqliteConnectionStringBuilder { DataSource = sourcePath, Mode = SqliteOpenMode.ReadOnly, Pooling = false }.ToString());
            source.Open();
            using (var check = source.CreateCommand())
            {
                check.CommandText = "PRAGMA integrity_check";
                if ((string?)check.ExecuteScalar() != "ok") throw new InvalidDataException("El respaldo SQLite está dañado.");
            }
            using var target = Abrir();
            source.BackupDatabase(target);
        }
    }
}
