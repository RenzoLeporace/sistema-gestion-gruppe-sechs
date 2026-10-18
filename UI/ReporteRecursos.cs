using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BLL;

namespace UI
{
    public partial class ReporteRecursos : Form
    {
        private readonly ReporteRecursosBLL _bll = new ReporteRecursosBLL();

        public ReporteRecursos()
        {
            InitializeComponent();
        }

        private void ReporteRecursos_Load(object sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Today.AddDays(-30);
            dtpHasta.Value = DateTime.Today;
            GenerarConRango();
        }

        private void btnReporteRecursos_Click(object sender, EventArgs e) => GenerarConRango();
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
                var rep = _bll.Generar(desde, hasta);

                if (rep.Items == null || rep.Items.Count == 0)
                {
                    gridRecursos.DataSource = null;
                    LimpiarCharts();
                    MessageBox.Show("No hay asignaciones para el rango.", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var moneda = CultureInfo.GetCultureInfo("es-AR");
                var vista = rep.Items.Select(i => new
                {
                    i.Tipo,
                    i.Id,
                    Recurso = i.Identificador,
                    Horas = Math.Round(i.HorasAsignadas, 2),
                    i.ServiciosParticipados,
                    IngresoEstimado = i.IngresoEstimado.ToString("C", moneda),
                    Rango = $"{desde:dd/MM/yyyy} - {hasta:dd/MM/yyyy}"
                }).ToList();
                gridRecursos.DataSource = null;
                gridRecursos.DataSource = vista;
                ConfigurarChartHoras(rep);
                ConfigurarChartDistribucion(rep);
                ConfigurarChartRentabilidad(rep);
                if (!rep.DatosCompletos)
                {
                    MessageBox.Show(
                        "Datos incompletos en asignaciones (rango). Reporte parcial.",
                        "Alerta de datos",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al generar reporte", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCharts()
        {
            foreach (var ch in new[] { chartHoras, chartDistribucion, chartRentabilidad })
            {
                if (ch == null) continue;
                ch.Series.Clear();
                ch.ChartAreas.Clear();
                ch.Titles.Clear();
                ch.Legends.Clear();
            }
        }

        private void ConfigurarChartHoras(BE.ReporteRecursosBE rep)
        {
            chartHoras.Series.Clear();
            chartHoras.ChartAreas.Clear();
            var area = new ChartArea("Horas");
            area.AxisX.Interval = 1;
            area.AxisX.Title = "Recurso";
            area.AxisY.Title = "Horas Asignadas";
            area.AxisY.LabelStyle.Format = "N2";
            chartHoras.ChartAreas.Add(area);

            var serie = new Series("Horas")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
                Label = "#VALY{N2}"
            };

            foreach (var item in rep.Items.OrderByDescending(x => x.HorasAsignadas).Take(12))
            {
                var label = item.Tipo == "Personal"
                    ? Trunc(item.Identificador, 10)
                    : "Veh " + Trunc(item.Identificador, 8);
                serie.Points.AddXY(label, item.HorasAsignadas);
            }
            chartHoras.Series.Add(serie);
            chartHoras.Titles.Clear();
            chartHoras.Titles.Add("Top recursos por horas (Rango)");
        }

        private void ConfigurarChartDistribucion(BE.ReporteRecursosBE rep)
        {
            chartDistribucion.Series.Clear();
            chartDistribucion.ChartAreas.Clear();
            var area = new ChartArea("Distribucion");
            chartDistribucion.ChartAreas.Add(area);
            var serie = new Series("Horas")
            {
                ChartType = SeriesChartType.Pie
            };
            serie.Points.AddXY("Personal", rep.TotalHorasPersonal);
            serie.Points.AddXY("Vehículos", rep.TotalHorasVehiculos);
            serie.Label = "#VALX: #PERCENT{P1}";
            chartDistribucion.Series.Add(serie);
            chartDistribucion.Titles.Clear();
            chartDistribucion.Titles.Add("Distribución de Horas (Rango)");
            chartDistribucion.Legends.Clear();
            chartDistribucion.Legends.Add(new Legend("Leyenda"));
        }

        private void ConfigurarChartRentabilidad(BE.ReporteRecursosBE rep)
        {
            chartRentabilidad.Series.Clear();
            chartRentabilidad.ChartAreas.Clear();
            var area = new ChartArea("Rentabilidad");
            area.AxisX.Interval = 1;
            area.AxisX.Title = "Tipo";
            area.AxisY.Title = "Ingreso Estimado (ARS)";
            chartRentabilidad.ChartAreas.Add(area);
            var serie = new Series("Ingreso")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true
            };
            serie.Points.AddXY("Personal", rep.TotalIngresosPersonalEstimado);
            serie.Points.AddXY("Vehículos", rep.TotalIngresosVehiculosEstimado);
            serie.Points.AddXY("Total", rep.RentabilidadEstimado);
            chartRentabilidad.Series.Add(serie);
            chartRentabilidad.Titles.Clear();
            chartRentabilidad.Titles.Add("Ingresos Estimados (Rango)");
        }

        private static string Trunc(string v, int max)
        {
            if (string.IsNullOrWhiteSpace(v)) return "";
            return v.Length <= max ? v : v.Substring(0, max) + "...";
        }
    }
}
