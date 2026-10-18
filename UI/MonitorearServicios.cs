using System;
using System.Linq;
using System.Windows.Forms;
using BLL;

namespace UI
{
    public partial class MonitorearServicios : Form
    {
        private readonly System.Windows.Forms.Timer _timer;
        private readonly ServicioBLL _servicioBll = new ServicioBLL();

        public MonitorearServicios()
        {
            InitializeComponent();
            _timer = new System.Windows.Forms.Timer { Interval = 10000 };
            _timer.Tick += (_, __) => Refrescar();
            FormClosed += (_, __) => _timer?.Stop();
        }
        private void MonitorearServicios_Load_1(object sender, EventArgs e)
        {
            Refrescar();
            _timer.Start();
        }
        private void Refrescar()
        {
            var ahora = DateTime.Now;
            var servicios = _servicioBll.Listar();
            if (servicios.Count == 0)
            {
                dataGridView1.DataSource = null;
                return;
            }
            var lista = servicios
                .Select(s =>
                {
                    var durSeg = (s.Fecha_hora_fin - s.Fecha_hora_inicio).TotalSeconds;
                    var transSeg = (ahora - s.Fecha_hora_inicio).TotalSeconds;
                    double progreso = durSeg <= 0 ? 1.0 : Math.Clamp(transSeg / durSeg, 0, 1);

                    string estadoCalc =
                        ahora < s.Fecha_hora_inicio ? "Programado" :
                        ahora > s.Fecha_hora_fin ? "Vencido" :
                        "En ejecución";
                    string incidencia =
                        estadoCalc == "Vencido" ? "Tiempo excedido, pendiente de cierre" :
                        estadoCalc == "Programado" ? "Aún no inicia" :
                        "";
                    bool activoTemporalmente = estadoCalc == "En ejecución";
                    bool futuro = estadoCalc == "Programado";

                    return new
                    {
                        s.IdServicio,
                        s.IdContrato,
                        s.TipoServicio,
                        Inicio = s.Fecha_hora_inicio,
                        Fin = s.Fecha_hora_fin,
                        Estado = estadoCalc,
                        Progreso = $"{(int)(progreso * 100)}%",
                        Personal = s.PersonalNecesario,
                        Vehiculos = s.VehiculosNecesarios,
                        Incidencia = incidencia,
                        Activo = activoTemporalmente,
                        Futuro = futuro
                    };
                })
                .Where(x => x.Estado != "Vencido")
                .OrderByDescending(x => x.Activo)
                .ThenBy(x => x.Inicio)
                .ToList();
            dataGridView1.DataSource = null;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = lista;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var estado = row.Cells["Estado"]?.Value?.ToString();
                if (estado == "En ejecución")
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.Honeydew;
                else if (estado == "Programado")
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.AliceBlue;
            }
        }
    }
}
