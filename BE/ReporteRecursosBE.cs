using System;
using System.Collections.Generic;

namespace BE
{
    public class ReporteRecursosBE : ReporteBE
    {
        public double TotalHorasPersonal { get; set; }
        public double TotalHorasVehiculos { get; set; }
        public double TotalIngresosPersonalEstimado { get; set; }
        public double TotalIngresosVehiculosEstimado { get; set; }
        public double RentabilidadEstimado => Math.Round(TotalIngresosPersonalEstimado + TotalIngresosVehiculosEstimado, 2);

        public bool DatosCompletos { get; set; }
        public bool TieneErroresCriticos { get; set; }
        public List<string> Errores { get; set; } = new();

        public List<ReporteRecursoItemBE> Items { get; set; } = new();

        public ReporteRecursosBE() : base(DateTime.Now, "Reporte de Recursos") { }
    }
    public class ReporteRecursoItemBE
    {
        public string Tipo { get; set; }
        public int Id { get; set; }
        public string Identificador { get; set; }
        public double HorasAsignadas { get; set; }
        public int ServiciosParticipados { get; set; }
        public double IngresoEstimado { get; set; }
    }
}
