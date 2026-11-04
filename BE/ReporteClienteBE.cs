using System;

namespace BE
{
    public class ReporteClienteBE : ReporteBE
    {
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public string DNI_CUIL { get; set; } = string.Empty;

        public int ServiciosContratados { get; set; }
        public double FacturacionAcumulada { get; set; }

        public int FacturasPagadas { get; set; }
        public int FacturasPendientes { get; set; }
        public double SaldoPendiente { get; set; }
        public double PorcentajePagado { get; set; }

        public ReporteClienteBE() : base(DateTime.Now, "Reporte de Clientes") { }

        public ReporteClienteBE(int idCliente, string nombreCliente, string dniCuil,
                                int serviciosContratados, double facturacionAcumulada,
                                int facturasPagadas, int facturasPendientes,
                                double saldoPendiente)
            : base(DateTime.Now, "Reporte de Clientes")
        {
            IdCliente = idCliente;
            NombreCliente = nombreCliente;
            DNI_CUIL = dniCuil;
            ServiciosContratados = serviciosContratados;
            FacturacionAcumulada = Math.Round(facturacionAcumulada, 2);
            FacturasPagadas = facturasPagadas;
            FacturasPendientes = facturasPendientes;
            SaldoPendiente = Math.Round(saldoPendiente, 2);
            PorcentajePagado = FacturacionAcumulada <= 0
                ? 0
                : Math.Round(((FacturacionAcumulada - SaldoPendiente) / FacturacionAcumulada) * 100, 2);
        }
    }
}
