using System;

namespace BE    
{
    public class VehiculoBE
    {
        public int IdVehiculo { get; set; }
        public string Patente { get; set; }
        public string Modelo { get; set; }
        public int EstadoOperativo { get; set; }
        public int Anio { get; set; }
        public string Nivel_Blindaje { get; set; }

        public VehiculoBE() { }

        public VehiculoBE(int idVehiculo, string patente, string modelo, int estadoOperativo, int anio, string nivel_Blindaje)
        {
            IdVehiculo = idVehiculo;
            Patente = patente;
            Modelo = modelo;
            EstadoOperativo = estadoOperativo;
            Anio = anio;
            Nivel_Blindaje = nivel_Blindaje;
        }
    }
}
