using System;

namespace BE
{
    public class AsignacionPersonalBE
    {
        public DateTime Fecha { get; set; }
        public int IdAsignacionPersonal { get; set; }
        public double ValorPersonal { get; set; }

        public int IdServicio { get; set; }
        public int IdPersonal { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public string EspecialidadAsignacion { get; set; }
        public string Criterio { get; set; }

        public AsignacionPersonalBE() { }

        public AsignacionPersonalBE(DateTime fecha, int idAsignacionPersonal, double valorPersonal)
        {
            Fecha = fecha;
            IdAsignacionPersonal = idAsignacionPersonal;
            ValorPersonal = valorPersonal;
        }

        public AsignacionPersonalBE(int idServicio, int idPersonal, DateTime inicio, DateTime fin, string especialidadAsignacion, string criterio)
        {
            IdServicio = idServicio;
            IdPersonal = idPersonal;
            Inicio = inicio;
            Fin = fin;
            EspecialidadAsignacion = especialidadAsignacion;
            Fecha = DateTime.Now;
        }
    }
}
