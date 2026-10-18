using System;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BLL;

namespace UI
{
    public partial class ReporteCliente : Form
    {
        private readonly ReporteClienteBLL _reporteClienteBll = new ReporteClienteBLL();

        public ReporteCliente()
        {
            InitializeComponent();
        }

        private void ReporteCliente_Load(object sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Today.AddDays(-30);
            dtpHasta.Value = DateTime.Today;
            GenerarConRango();
        }

        private void btnReporteClientes_Click(object sender, EventArgs e) => GenerarConRango();
        private void btnAplicarFechas_Click(object sender, EventArgs e) => GenerarConRango();
        private void btnDia_Click(object sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Today;
            dtpHasta.Value = DateTime.Today;
            Generar(DateTime.Today, DateTime.Today);
        }
        private void btnSemana_Click(object sender, EventArgs e)
        {
            var hasta = DateTime.Today;
            var desde = hasta.AddDays(-6);
            dtpDesde.Value = desde;
            dtpHasta.Value = hasta;
            Generar(desde, hasta);
        }
        private void btnMes_Click(object sender, EventArgs e)
        {
            var hasta = DateTime.Today;
            var desde = new DateTime(hasta.Year, hasta.Month, 1);
            dtpDesde.Value = desde;
            dtpHasta.Value = hasta;
            Generar(desde, hasta);
        }

        private void GenerarConRango()
        {
            var desde = dtpDesde.Value.Date;
            var hasta = dtpHasta.Value.Date;
            if (desde > hasta)
            {
                MessageBox.Show("La fecha 'Desde' no puede ser mayor que 'Hasta'.", "Rango inválido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Generar(desde, hasta);
        }

        private void Generar(DateTime desde, DateTime hasta)
        {
            try
            {
                var datos = _reporteClienteBll.GenerarReporteClientes(desde, hasta)
                           ?? new System.Collections.Generic.List<BE.ReporteClienteBE>();

                var vista = datos.Select(r => new
                {
                    r.IdCliente,
                    r.NombreCliente,
                    r.DNI_CUIL,
                    r.ServiciosContratados,
                    Facturacion = r.FacturacionAcumulada,
                    r.FacturasPagadas,
                    r.FacturasPendientes,
                    r.SaldoPendiente,
                    r.PorcentajePagado,
                    Rango = $"{desde:dd/MM/yyyy} - {hasta:dd/MM/yyyy}"
                }).ToList();
                gridResumen.DataSource = null;
                gridResumen.DataSource = vista;
                ConfigurarChartFacturacion(datos);
                ConfigurarChartServicios(datos);
                ConfigurarChartPagos(datos);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al generar reporte", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarChartFacturacion(System.Collections.Generic.List<BE.ReporteClienteBE> datos)
        {
            chartFacturacion.Series.Clear();
            chartFacturacion.ChartAreas.Clear();
            var area = new ChartArea("Facturacion");
            area.AxisX.Interval = 1;
            area.AxisX.Title = "Cliente";
            area.AxisY.Title = "Facturación (ARS)";
            chartFacturacion.ChartAreas.Add(area);
            var serie = new Series("Facturación")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true
            };
            foreach (var r in datos.OrderByDescending(x => x.FacturacionAcumulada).Take(15))
                serie.Points.AddXY(TruncNombre(r.NombreCliente), r.FacturacionAcumulada);
            chartFacturacion.Series.Add(serie);
            chartFacturacion.Titles.Clear();
            chartFacturacion.Titles.Add("Facturación por Cliente (Rango)");
        }

        private void ConfigurarChartServicios(System.Collections.Generic.List<BE.ReporteClienteBE> datos)
        {
            chartServicios.Series.Clear();
            chartServicios.ChartAreas.Clear();
            var area = new ChartArea("Servicios");
            area.AxisX.Interval = 1;
            area.AxisX.Title = "Cliente";
            area.AxisY.Title = "Servicios Contratados";
            chartServicios.ChartAreas.Add(area);
            var serie = new Series("Servicios")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true
            };
            foreach (var r in datos.OrderByDescending(x => x.ServiciosContratados).Take(15))
                serie.Points.AddXY(TruncNombre(r.NombreCliente), r.ServiciosContratados);
            chartServicios.Series.Add(serie);
            chartServicios.Titles.Clear();
            chartServicios.Titles.Add("Servicios por Cliente (Rango)");
        }

        private void ConfigurarChartPagos(System.Collections.Generic.List<BE.ReporteClienteBE> datos)
        {
            chartPagos.Series.Clear();
            chartPagos.ChartAreas.Clear();
            var area = new ChartArea("Pagos");
            chartPagos.ChartAreas.Add(area);
            var totalFact = datos.Sum(d => d.FacturacionAcumulada);
            var saldoPend = datos.Sum(d => d.SaldoPendiente);
            var totalPagado = totalFact - saldoPend;

            var serie = new Series("Estado Pagos")
            {
                ChartType = SeriesChartType.Pie
            };
            serie.Points.AddXY("Pagado", totalPagado);
            serie.Points.AddXY("Pendiente", saldoPend);
            serie.Label = "#VALX: #PERCENT{P1}";
            chartPagos.Series.Add(serie);
            chartPagos.Titles.Clear();
            chartPagos.Titles.Add("Estado Global de Pagos (Rango)");
            chartPagos.Legends.Clear();
            chartPagos.Legends.Add(new Legend("Legend"));
        }

        private static string TruncNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return "";
            return nombre.Length <= 12 ? nombre : nombre.Substring(0, 12) + "...";
        }
    }
}
