using System;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BLL;

namespace UI
{
    public partial class ReporteEconomico : Form
    {
        private readonly ReporteEconomicoBLL _bll = new ReporteEconomicoBLL();

        public ReporteEconomico()
        {
            InitializeComponent();
        }

        private void ReporteEconomico_Load(object sender, EventArgs e)
        {
            AppEvents.EconomiaActualizada += OnEconomiaActualizada;
            dtpDesde.Value = DateTime.Today.AddDays(-30);
            dtpHasta.Value = DateTime.Today;
            GenerarConRango();
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            AppEvents.EconomiaActualizada -= OnEconomiaActualizada;
            base.OnFormClosed(e);
        }
        private void OnEconomiaActualizada(object? sender, EventArgs e)
        {
            if (IsDisposed) return;
            if (InvokeRequired)
                BeginInvoke(new Action(GenerarConRango));
            else
                GenerarConRango();
        }

        private void btnGenerar_Click(object sender, EventArgs e) => GenerarConRango();
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
                var rep = _bll.GenerarReporteEconomico(desde, hasta);
                var moneda = CultureInfo.GetCultureInfo("es-AR");
                var ingresosBase = rep.IngresosTotales > 0 ? rep.IngresosTotales : rep.CobradoCalculado;
                var rentabilidadCalc = ingresosBase - rep.GastosTotales;
                var margenCalc = ingresosBase > 0 ? (rentabilidadCalc / ingresosBase) * 100.0 : 0.0;

                var rows = new[]
                {
                    new
                    {
                        rep.Titulo,
                        Rango = $"{desde:dd/MM/yyyy} - {hasta:dd/MM/yyyy}",
                        FacturacionTotal = rep.FacturacionTotal.ToString("C", moneda),
                        Cobrado = rep.CobradoCalculado.ToString("C", moneda),
                        Gastos = rep.GastosTotales.ToString("C", moneda),
                        Rentabilidad = rentabilidadCalc.ToString("C", moneda),
                        Margen = margenCalc.ToString("0.##' %'"),
                        DeudaPendiente = rep.DeudaPendienteClientes.ToString("C", moneda),
                        DatosGastos = rep.GastosDisponibles ? "Completos" : "No disponibles"
                    }
                };
                gridResumen.DataSource = null;
                gridResumen.DataSource = rows;

                ConfigurarChartIngresosGastos(rep);
                ConfigurarChartDeuda(rep);

                if (!rep.GastosDisponibles)
                {
                    MessageBox.Show("No se encontraron datos de gastos en el rango seleccionado.",
                        "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al generar reporte", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarChartIngresosGastos(BE.ReporteEconomicoBE rep)
        {
            chartIngresosGastos.Series.Clear();
            chartIngresosGastos.ChartAreas.Clear();
            var area = new ChartArea("IngresosGastos")
            {
                AxisX = { Interval = 1, Title = "Concepto" },
                AxisY = { Title = "Monto (ARS)", Minimum = 0 }
            };
            chartIngresosGastos.ChartAreas.Add(area);
            var serie = new Series("Monto")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
                LabelFormat = "C0"
            };

            var ingresosBase = rep.IngresosTotales > 0 ? rep.IngresosTotales : rep.CobradoCalculado;
            var rentabilidadCalc = ingresosBase - rep.GastosTotales;

            serie.Points.AddXY("Facturación Total", rep.FacturacionTotal);
            serie.Points.AddXY("Cobrado", rep.CobradoCalculado);
            serie.Points.AddXY("Gastos", rep.GastosTotales);
            serie.Points.AddXY("Rentabilidad", rentabilidadCalc);

            chartIngresosGastos.Series.Add(serie);
            chartIngresosGastos.Titles.Clear();
            chartIngresosGastos.Titles.Add("Facturación, Cobros y Rentabilidad (Rango)");
        }

        private void ConfigurarChartDeuda(BE.ReporteEconomicoBE rep)
        {
            chartDeuda.Series.Clear();
            chartDeuda.ChartAreas.Clear();
            var area = new ChartArea("Deuda");
            chartDeuda.ChartAreas.Add(area);

            var totalCobrado = rep.CobradoCalculado;
            var deuda = rep.DeudaPendienteClientes;
            if (totalCobrado <= 0 && deuda <= 0)
            {
                chartDeuda.Titles.Clear();
                chartDeuda.Titles.Add("Sin datos para graficar");
                return;
            }
            var serie = new Series("Estado de Cobranza")
            {
                ChartType = SeriesChartType.Pie
            };
            serie.Points.AddXY("Cobrado", totalCobrado);
            serie.Points.AddXY("Pendiente", deuda);
            serie.Label = "#VALX: #PERCENT{P1}";
            chartDeuda.Series.Add(serie);
            chartDeuda.Titles.Clear();
            chartDeuda.Titles.Add("Cobrado vs Deuda (Rango)");
            chartDeuda.Legends.Clear();
            chartDeuda.Legends.Add(new Legend("Leyenda"));
        }
    }
}
