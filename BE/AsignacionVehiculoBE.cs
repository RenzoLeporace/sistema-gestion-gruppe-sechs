using System;

namespace BE
{
    public class AsignacionVehiculoBE
    {
        public DateTime FechaHora { get; set; }
        public int IdAsignacionVehiculo { get; set; }
        public double ValorVehiculo { get; set; }

        public int IdServicio { get; set; }
        public int IdVehiculo { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }

        public AsignacionVehiculoBE() { }

        public AsignacionVehiculoBE(DateTime fechahora, int idAsignacionVehiculo, double valorVehiculo)
        {
            FechaHora = fechahora;
            IdAsignacionVehiculo = idAsignacionVehiculo;
            ValorVehiculo = valorVehiculo;
        }

        public AsignacionVehiculoBE(int idServicio, int idVehiculo, DateTime inicio, DateTime fin)
        {
            IdServicio = idServicio;
            IdVehiculo = idVehiculo;
            Inicio = inicio;
            Fin = fin;
            FechaHora = DateTime.Now;
        }
    }
}
