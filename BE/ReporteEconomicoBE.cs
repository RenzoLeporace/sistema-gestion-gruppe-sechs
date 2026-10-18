using System;

namespace BE
{
    public class ReporteEconomicoBE : ReporteBE
    {
        public double IngresosTotales { get; set; }
        public double GastosTotales { get; set; }
        public double Rentabilidad { get; set; }
        public double MargenRentabilidad { get; set; }
        public double DeudaPendienteClientes { get; set; }
        public bool GastosDisponibles { get; set; }
        public double CobradoCalculado { get; set; }
        public double FacturacionTotal { get; set; }

        public ReporteEconomicoBE() : base(DateTime.Now, "Reporte Económico") { }

        public ReporteEconomicoBE(double ingresosTotales,
                                  double gastosTotales,
                                  double rentabilidad,
                                  double margenRentabilidad,
                                  double deudaPendienteClientes,
                                  bool gastosDisponibles)
            : base(DateTime.Now, "Reporte Económico")
        {
            IngresosTotales = Math.Round(ingresosTotales, 2);
            GastosTotales = Math.Round(gastosTotales, 2);
            Rentabilidad = Math.Round(rentabilidad, 2);
            MargenRentabilidad = Math.Round(margenRentabilidad, 2);
            DeudaPendienteClientes = Math.Round(deudaPendienteClientes, 2);
            GastosDisponibles = gastosDisponibles;
            CobradoCalculado = 0;
            FacturacionTotal = 0;
        }
    }
}
