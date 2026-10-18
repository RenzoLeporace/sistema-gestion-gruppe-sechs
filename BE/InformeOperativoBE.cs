using System;

namespace BE
{
    public class InformeOperativoBE
    {
        public int IdInforme { get; set; }
        public int IdServicio { get; set; }
        public string Incidencias { get; set; }
        public string Observaciones { get; set; }
        public string Resultado { get; set; }
        public DateTime FechaCreacion { get; set; }

        public InformeOperativoBE() { }

        public InformeOperativoBE(int idInforme, int idServicio, string incidencias, string observaciones, string resultado)
        {
            IdInforme = idInforme;
            IdServicio = idServicio;
            Incidencias = incidencias;
            Observaciones = observaciones;
            Resultado = resultado;
            FechaCreacion = DateTime.Now;
        }
    }
}
