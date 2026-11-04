using System;

namespace BE
{
    public class MantenimientoBE
    {

        public MantenimientoBE() { }

        public double Costo { get; set; }
        public DateTime Fecha { get; set; }
        public int IdMantenimiento { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        public int IdVehiculo { get; set; }

        public MantenimientoBE(double costo, DateTime fecha, int idMantenimiento, string estado, string descripcion)
        {
            Costo = costo;
            Fecha = fecha;
            IdMantenimiento = idMantenimiento;
            Estado = estado;
            Descripcion = descripcion;
        }
    }
}
