using System;

namespace BE
{
    public enum BitacoraTipo
    {
        Backup,
        Restore,
        Info,
        Error
    }

    public enum BitacoraResultado
    {
        Exito,
        Fallo
    }

    public class BitacoraEntrada
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int UsuarioId { get; set; }
        public string Usuario { get; set; } = "SYSTEM";
        public BitacoraTipo Tipo { get; set; } = BitacoraTipo.Info;
        public BitacoraResultado Resultado { get; set; } = BitacoraResultado.Exito;
        public string Mensaje { get; set; } = string.Empty;
        public string Detalle { get; set; } = string.Empty;
    }
}