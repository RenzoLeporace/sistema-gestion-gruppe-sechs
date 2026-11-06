using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using BE;
using Mapper;

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
                Directory.CreateDirectory(dir);
                return dir;
            }
        }

        private static void MigrarColeccionesLegadas()
        {
            new ClienteMpr().GetAll();
            new ContratoMpr().GetAll();
            new ServicioMpr().GetAll();
            new PersonalMpr().GetAll();
            new VehiculoMpr().GetAll();
            new AsignacionPersonalMpr().GetAll();
            new AsignacionVehiucloMpr().GetAll();
            new InformeOperativoMpr().GetAll();
            new MantenimientoMpr().GetAll();
            new FacturaMpr().GetAll();
            new PagoMpr().GetAll();
            new TipoServicioMpr().GetAll();
            new MprPermiso().ObtenerTodos();
            new RolMpr().ObtenerTodos();
            new UsuarioMapper().ObtenerTodos();
            new BitacoraMapper().ObtenerTodos();
        }

        public IEnumerable<string> EnumerarArchivosDatos()
        {
            MigrarColeccionesLegadas();
            return File.Exists(SqliteStore.DatabasePath) ? new[] { SqliteStore.DatabasePath } : Enumerable.Empty<string>();
        }

        public IEnumerable<string> EnumerarSnapshots() =>
            Directory.EnumerateFiles(BackupDirectory, "BD_Backup_*.sqlite3", SearchOption.TopDirectoryOnly)
                .OrderByDescending(f => f);

        public string CrearSnapshotBackup()
        {
            MigrarColeccionesLegadas();
            var destino = Path.Combine(BackupDirectory, $"BD_Backup_{DateTime.Now:yyyyMMdd-HHmmss}.sqlite3");
            try
            {
                SqliteStore.CrearCopia(destino);
                BLLBitacora.Instance.Registrar(BitacoraTipo.Backup,
                    $"Snapshot creado: {Path.GetFileName(destino)}", "backup", true);
                return destino;
            }
            catch (Exception ex)
            {
                BLLBitacora.Instance.Registrar(BitacoraTipo.Backup, "Fallo al crear snapshot", ex.Message, false);
                throw;
            }
        }

        public void RestaurarDesdeSnapshot(string snapshotPath, bool crearBackupPrevio = true)
        {
            if (string.IsNullOrWhiteSpace(snapshotPath) || !File.Exists(snapshotPath))
                throw new FileNotFoundException("No se encontró el respaldo.", snapshotPath);
            try
            {
                if (crearBackupPrevio)
                {
                    var safety = Path.Combine(BackupDirectory, $"pre-restore-{DateTime.Now:yyyyMMdd-HHmmss}.sqlite3");
                    SqliteStore.CrearCopia(safety);

                }
                SqliteStore.RestaurarCopia(snapshotPath);
                BLLBitacora.Instance.Registrar(BitacoraTipo.Restore,
                    $"Restauración desde snapshot: {Path.GetFileName(snapshotPath)}", "restore", true);
                DatosRestaurados?.Invoke();
            }
            catch (Exception ex)
            {
                BLLBitacora.Instance.Registrar(BitacoraTipo.Restore, "Fallo al restaurar snapshot", ex.Message, false);
                throw;
            }
        }

        public string CrearBackup(string destinoZip)
        {
            if (string.IsNullOrWhiteSpace(destinoZip))
                throw new ArgumentException("Ruta de destino inválida.", nameof(destinoZip));
            MigrarColeccionesLegadas();
            var temp = Path.Combine(Path.GetTempPath(), $"gruppe-sechs-{Guid.NewGuid():N}.sqlite3");
            try
            {
                SqliteStore.CrearCopia(temp);
                Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(destinoZip))!);
                using var fs = new FileStream(destinoZip, FileMode.Create, FileAccess.Write, FileShare.None);
                using var zip = new ZipArchive(fs, ZipArchiveMode.Create);
                zip.CreateEntryFromFile(temp, "Data/gruppe_sechs.sqlite3", CompressionLevel.Optimal);
                BLLBitacora.Instance.Registrar(BitacoraTipo.Backup,
                    $"Backup ZIP creado: {Path.GetFileName(destinoZip)}", "backup", true);
                return destinoZip;
            }
            catch (Exception ex)
            {
                BLLBitacora.Instance.Registrar(BitacoraTipo.Backup, "Fallo al crear backup ZIP", ex.Message, false);
                throw;
            }
            finally
            {
                if (File.Exists(temp)) File.Delete(temp);
            }
        }
    }
}
