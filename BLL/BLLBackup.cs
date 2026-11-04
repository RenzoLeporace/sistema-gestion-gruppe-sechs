using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Linq;
using BE;

namespace BLL
{
    public sealed class BLLBackup
    {
        private static readonly Lazy<BLLBackup> _inst = new(() => new BLLBackup());
        public static BLLBackup Instance => _inst.Value;
        private BLLBackup() { }
        public event Action? DatosRestaurados;
        public string DataDirectory => AppDomain.CurrentDomain.BaseDirectory;
        public string BackupDirectory
        {
            get
            {
                var dir = Path.Combine(DataDirectory, "BACKUP");
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                return dir;
            }
        }
        public IEnumerable<string> EnumerarArchivosDatos()
        {
            var baseDir = DataDirectory;
            if (!Directory.Exists(baseDir)) return Enumerable.Empty<string>();

            var rootXml = Directory.EnumerateFiles(baseDir, "*.xml", SearchOption.TopDirectoryOnly);
            var dataDir = Path.Combine(baseDir, "Data");
            var dataXml = Directory.Exists(dataDir)
                ? Directory.EnumerateFiles(dataDir, "*.xml", SearchOption.AllDirectories)
                : Enumerable.Empty<string>();
            var all = rootXml.Concat(dataXml);
            return all.Where(f =>
            {
                var name = Path.GetFileName(f);
                return !name.StartsWith("BD_Backup_", StringComparison.OrdinalIgnoreCase);
            });
        }
        public IEnumerable<string> EnumerarSnapshots()
        {
            var dir = BackupDirectory;
            return Directory.EnumerateFiles(dir, "BD_Backup_*.xml", SearchOption.TopDirectoryOnly)
                            .OrderByDescending(f => f);
        }
        public string CrearSnapshotBackup()
        {
            var files = EnumerarArchivosDatos().ToList();
            if (!files.Any())
                throw new InvalidOperationException("No se encontraron archivos de datos (.xml) para respaldar.");
            var fileName = $"BD_Backup_{DateTime.Now:dd-MM-yyyy HH-mm-ss}.xml";
            fileName = fileName.Replace(':', '-');
            var destino = Path.Combine(BackupDirectory, fileName);

            try
            {
                var root = new XElement("BackupSnapshot",
                    new XAttribute("Fecha", DateTime.Now.ToString("O")),
                    new XAttribute("Origen", DataDirectory));

                foreach (var file in files)
                {
                    var bytes = File.ReadAllBytes(file);
                    var contentB64 = Convert.ToBase64String(bytes);
                    var relPath = Path.GetRelativePath(DataDirectory, file);
                    root.Add(new XElement("File",
                        new XAttribute("Name", Path.GetFileName(file)),
                        new XAttribute("Path", relPath),
                        contentB64));
                }
                var doc = new XDocument(root);
                doc.Save(destino);

                BLLBitacora.Instance.Registrar(BitacoraTipo.Backup,
                    $"Snapshot creado: {Path.GetFileName(destino)}",
                    "backup", true);

                return destino;
            }
            catch (Exception ex)
            {
                BLLBitacora.Instance.Registrar(BitacoraTipo.Backup,
                    "Fallo al crear snapshot", ex.Message, false);
                throw;
            }
        }
        public void RestaurarDesdeSnapshot(string snapshotPath, bool crearBackupPrevio = true)
        {
            if (string.IsNullOrWhiteSpace(snapshotPath) || !File.Exists(snapshotPath))
                throw new FileNotFoundException("No se encontró el archivo de snapshot.", snapshotPath);
            try
            {
                if (crearBackupPrevio)
                {
                    var safetyZip = Path.Combine(BackupDirectory, $"pre-restore-{DateTime.Now:yyyyMMdd-HHmmss}.zip");
                    CrearBackup(safetyZip);
                }
                var doc = XDocument.Load(snapshotPath);
                var root = doc.Root;
                if (root == null || root.Name != "BackupSnapshot")
                    throw new InvalidOperationException("Formato de snapshot inválido.");
                var files = root.Elements("File").ToList();
                if (!files.Any())
                    throw new InvalidOperationException("El snapshot no contiene datos.");
                foreach (var f in files)
                {
                    var pathAttr = f.Attribute("Path");
                    var nameAttr = f.Attribute("Name");
                    string? relativePath = pathAttr?.Value;
                    if (string.IsNullOrWhiteSpace(relativePath))
                        relativePath = nameAttr?.Value ?? throw new InvalidOperationException("Entrada de archivo inválida.");
                    var destFile = Path.Combine(DataDirectory, relativePath);
                    var destDir = Path.GetDirectoryName(destFile);
                    if (!string.IsNullOrEmpty(destDir) && !Directory.Exists(destDir))
                        Directory.CreateDirectory(destDir);
                    var b64 = f.Value.Trim();
                    var bytes = Convert.FromBase64String(b64);
                    File.WriteAllBytes(destFile, bytes);
                }
                BLLBitacora.Instance.Registrar(BitacoraTipo.Restore,
                    $"Restauración desde snapshot: {Path.GetFileName(snapshotPath)}",
                    "restore", true);
                DatosRestaurados?.Invoke();
            }
            catch (Exception ex)
            {
                BLLBitacora.Instance.Registrar(BitacoraTipo.Restore,
                    "Fallo al restaurar snapshot", ex.Message, false);
                throw;
            }
        }
        public string CrearBackup(string destinoZip)
        {
            if (string.IsNullOrWhiteSpace(destinoZip))
                throw new ArgumentException("Ruta de destino inválida.", nameof(destinoZip));

            var files = EnumerarArchivosDatos().ToList();
            if (!files.Any())
                throw new InvalidOperationException("No se encontraron archivos de datos (.xml) para respaldar.");
            try
            {
                var dir = Path.GetDirectoryName(destinoZip);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                using var fs = new FileStream(destinoZip, FileMode.Create, FileAccess.Write, FileShare.None);
                using var zip = new ZipArchive(fs, ZipArchiveMode.Create);
                foreach (var file in files)
                {
                    var relPath = Path.GetRelativePath(DataDirectory, file);
                    relPath = relPath.Replace('\\', '/');
                    zip.CreateEntryFromFile(file, relPath, CompressionLevel.Optimal);
                }
                var entry = zip.CreateEntry("backup_info.txt");
                using var writer = new StreamWriter(entry.Open());
                writer.WriteLine($"Fecha: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                writer.WriteLine($"Origen: {DataDirectory}");
                writer.WriteLine($"Archivos: {files.Count}");
                BLLBitacora.Instance.Registrar(BitacoraTipo.Backup,
                    $"Backup ZIP creado: {Path.GetFileName(destinoZip)}", "backup", true);
                return destinoZip;
            }
            catch (Exception ex)
            {
                BLLBitacora.Instance.Registrar(BitacoraTipo.Backup,
                    "Fallo al crear backup ZIP", ex.Message, false);
                throw;
            }
        }
    }
}