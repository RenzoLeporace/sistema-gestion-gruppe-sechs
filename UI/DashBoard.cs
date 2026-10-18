using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BLL;

namespace UI
{
    public partial class DashBoard : Form
    {
        private readonly ReporteEconomicoBLL _ecoBll = new ReporteEconomicoBLL();
        private readonly ReporteClienteBLL _cliBll = new ReporteClienteBLL();
        private readonly ReporteRecursosBLL _recBll = new ReporteRecursosBLL();

        public DashBoard()
        {
            InitializeComponent();
        }

        private void DashBoard_Load(object sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Today.AddDays(-30);
            dtpHasta.Value = DateTime.Today;
            AppEvents.EconomiaActualizada += OnEconomiaActualizada;
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

        private void btnAplicarFechas_Click(object sender, EventArgs e) => GenerarConRango();
        private void btnRefrescar_Click(object sender, EventArgs e) => GenerarConRango();
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
                var eco = _ecoBll.GenerarReporteEconomico(desde, hasta) ?? new BE.ReporteEconomicoBE();
                var clientes = _cliBll.GenerarReporteClientes(desde, hasta) ?? new System.Collections.Generic.List<BE.ReporteClienteBE>();
                var rec = _recBll.Generar(desde, hasta) ?? new BE.ReporteRecursosBE();
                if (gridResumen == null || chartEcoBarras == null || chartDeuda == null ||
                    chartClientes == null || chartRecursos == null)
                {
                    MessageBox.Show("Alguno de los controles del dashboard es null.",
                        "Controles faltantes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                CargarResumen(eco, clientes, rec, desde, hasta);
                ConfigurarEcoBarras(eco);
                ConfigurarDeuda(eco);
                ConfigurarClientes(clientes);
                ConfigurarRecursos(rec);
                gridResumen.Refresh();
                chartEcoBarras.Invalidate();
                chartDeuda.Invalidate();
                chartClientes.Invalidate();
                chartRecursos.Invalidate();

                if (!eco.GastosDisponibles)
                {
                    MessageBox.Show("No hay datos de gastos en el rango seleccionado.",
                        "Datos de gastos faltantes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (rec.TieneErroresCriticos)
                {
                    var detalles = rec.Errores.Count > 0
                        ? "\nDetalle:\n - " + string.Join("\n - ", rec.Errores.Take(5))
                        : "";
                    MessageBox.Show("Asignaciones con rangos inválidos (en el período)." + detalles,
                        "Datos de recursos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al generar dashboard", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarResumen(BE.ReporteEconomicoBE eco,
                                   System.Collections.Generic.List<BE.ReporteClienteBE> clientes,
                                   BE.ReporteRecursosBE rec,
                                   DateTime desde, DateTime hasta)
        {
            var moneda = CultureInfo.GetCultureInfo("es-AR");
            var topCliente = clientes.Count > 0
                ? clientes.OrderByDescending(c => c.FacturacionAcumulada).First()
                : null;
            var ingresosBase = eco.IngresosTotales > 0 ? eco.IngresosTotales : eco.CobradoCalculado;
            var rentabilidadCalc = ingresosBase - eco.GastosTotales;
            var margenCalc = ingresosBase > 0 ? (rentabilidadCalc / ingresosBase) * 100.0 : 0.0;
            var minutosPersonal = Math.Round(rec.TotalHorasPersonal * 60, 0);
            var minutosVehiculos = Math.Round(rec.TotalHorasVehiculos * 60, 0);

            var fila = new[]
            {
                new
                {
                    FechaGeneracion = DateTime.Now,
                    Rango = $"{desde:dd/MM/yyyy} - {hasta:dd/MM/yyyy}",
                    FacturacionTotal = eco.FacturacionTotal.ToString("C", moneda),
                    Ingresos = ingresosBase.ToString("C", moneda),
                    Gastos = eco.GastosTotales.ToString("C", moneda),
                    Rentabilidad = rentabilidadCalc.ToString("C", moneda),
                    Margen = margenCalc.ToString("0.##' %'"),
                    DeudaPendiente = eco.DeudaPendienteClientes.ToString("C", moneda),
                    Cobrado = eco.CobradoCalculado.ToString("C", moneda),
                    Clientes = clientes.Count,
                    ServiciosTotales = clientes.Sum(c => c.ServiciosContratados),
                    TopCliente = topCliente?.NombreCliente ?? "-",
                    FacturacionTop = (topCliente?.FacturacionAcumulada ?? 0).ToString("C", moneda),
                    MinutosPersonal = minutosPersonal,
                    MinutosVehiculos = minutosVehiculos
                }
            };
            gridResumen.DataSource = null;
            gridResumen.DataSource = fila;
        }

        private void ConfigurarEcoBarras(BE.ReporteEconomicoBE eco)
        {
            if (eco == null) return;
            chartEcoBarras.Series.Clear();
            chartEcoBarras.ChartAreas.Clear();
            var area = new ChartArea("Eco")
            {
                AxisX = { Interval = 1, Title = "Concepto" },
                AxisY = { Title = "Monto (ARS)", Minimum = 0 }
            };
            chartEcoBarras.ChartAreas.Add(area);

            var serie = new Series("Montos")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
                LabelFormat = "C0"
            };

            var ingresosBase = eco.IngresosTotales > 0 ? eco.IngresosTotales : eco.CobradoCalculado;
            var rentabilidadCalc = ingresosBase - eco.GastosTotales;

            serie.Points.AddXY("Facturación Total", eco.FacturacionTotal);
            serie.Points.AddXY("Cobrado", eco.CobradoCalculado);
            serie.Points.AddXY("Gastos", eco.GastosTotales);
            serie.Points.AddXY("Rentabilidad", rentabilidadCalc);

            chartEcoBarras.Series.Add(serie);
            chartEcoBarras.Titles.Clear();
            chartEcoBarras.Titles.Add("Facturación, Cobros y Rentabilidad (Rango)");
        }

        private void ConfigurarDeuda(BE.ReporteEconomicoBE eco)
        {
            if (eco == null) return;
            chartDeuda.Series.Clear();
            chartDeuda.ChartAreas.Clear();
            var area = new ChartArea("Deuda");
            chartDeuda.ChartAreas.Add(area);

            var serie = new Series("Cobranza") { ChartType = SeriesChartType.Pie };
            serie.Points.AddXY("Cobrado", eco.CobradoCalculado);
            serie.Points.AddXY("Pendiente", eco.DeudaPendienteClientes);
            serie.Label = "#VALX: #PERCENT{P1}";
            chartDeuda.Series.Add(serie);
            chartDeuda.Titles.Clear();
            chartDeuda.Titles.Add("Cobrado vs Deuda (Rango)");
            chartDeuda.Legends.Clear();
            chartDeuda.Legends.Add(new Legend("Leyenda"));
        }

        private void ConfigurarClientes(System.Collections.Generic.List<BE.ReporteClienteBE> clientes)
        {
            chartClientes.Series.Clear();
            chartClientes.ChartAreas.Clear();
            var area = new ChartArea("Clientes") { AxisX = { Interval = 1, Title = "Cliente" }, AxisY = { Title = "Facturación (ARS)" } };
            chartClientes.ChartAreas.Add(area);
            var serie = new Series("Facturación") { ChartType = SeriesChartType.Column, IsValueShownAsLabel = true };
            foreach (var c in clientes.OrderByDescending(c => c.FacturacionAcumulada).Take(10))
                serie.Points.AddXY(Trunc(c.NombreCliente, 12), c.FacturacionAcumulada);
            chartClientes.Series.Add(serie);
            chartClientes.Titles.Clear();
            chartClientes.Titles.Add("Top 10 Clientes (Rango)");
        }

        private void ConfigurarRecursos(BE.ReporteRecursosBE rep)
        {
            chartRecursos.Series.Clear();
            chartRecursos.ChartAreas.Clear();
            var area = new ChartArea("Recursos");
            chartRecursos.ChartAreas.Add(area);
            var serie = new Series("Horas") { ChartType = SeriesChartType.Pie };
            serie.Points.AddXY("Personal", rep.TotalHorasPersonal);
            serie.Points.AddXY("Vehículos", rep.TotalHorasVehiculos);
            serie.Label = "#VALX: #PERCENT{P1}";
            chartRecursos.Series.Add(serie);
            chartRecursos.Titles.Clear();
            chartRecursos.Titles.Add("Distribución Horas (Rango)");
            chartRecursos.Legends.Clear();
            chartRecursos.Legends.Add(new Legend("Leyenda"));
        }

        private static string Trunc(string v, int max) =>
            string.IsNullOrWhiteSpace(v) ? "" : (v.Length <= max ? v : v.Substring(0, max) + "...");

    }
}
