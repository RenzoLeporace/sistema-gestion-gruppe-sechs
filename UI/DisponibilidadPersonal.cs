using System;
using System.Linq;
using System.Windows.Forms;
using BLL;
using BE;

namespace UI
{
    public partial class DisponibilidadPersonal : Form
    {
        private readonly PersonalBLL _personalBll = new PersonalBLL();
        private readonly AsignacionPersonalBLL _asigBll = new AsignacionPersonalBLL();
        private readonly TipoServicioBLL _tipoBll = new TipoServicioBLL();

        public DisponibilidadPersonal()
        {
            InitializeComponent();
        }

        private void DisponibilidadPersonal_Load(object sender, EventArgs e)
        {
            CargarFiltros();
            dtpMomento.Value = DateTime.Now;
            Refrescar();
            cbEspecialidad.SelectedIndexChanged += (_, __) => Refrescar();
            cbEstado.SelectedIndexChanged += (_, __) => Refrescar();
            dtpMomento.ValueChanged += (_, __) => Refrescar();
        }
        private void CargarFiltros()
        {
            var especialidades = _tipoBll.Listar()
                .Select(t => t.Descripcion?.Trim())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(s => s)
                .ToList();
            cbEspecialidad.Items.Clear();
            cbEspecialidad.Items.Add("Todas");
            foreach (var e in especialidades) cbEspecialidad.Items.Add(e);
            cbEspecialidad.SelectedIndex = 0;
            cbEstado.Items.Clear();
            cbEstado.Items.Add("Todos");
            cbEstado.Items.Add("Disponible");
            cbEstado.Items.Add("Asignado");
            cbEstado.SelectedIndex = 0;

        }
        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            Refrescar();
        }
        private void Refrescar()
        {
            var momento = dtpMomento.Value;
            var personas = _personalBll.Listar();
            var asignaciones = _asigBll.ListarAsignaciones();

            string TurnoDesde(DateTime inicio)
            {
                var h = inicio.TimeOfDay;
                if (h >= TimeSpan.FromHours(6) && h < TimeSpan.FromHours(14)) return "Mañana";
                if (h >= TimeSpan.FromHours(14) && h < TimeSpan.FromHours(22)) return "Tarde";
                return "Noche";
            }
            var datos = personas
                .Select(p =>
                {
                    var asignAct = asignaciones
                        .Where(a => a.IdPersonal == p.IdPersonal && a.Inicio <= momento && momento <= a.Fin)
                        .OrderBy(a => a.Inicio)
                        .FirstOrDefault();
                    var ocupado = asignAct != null;
                    var estado = ocupado ? "Asignado" : "Disponible";
                    var disponibleDesde = ocupado ? asignAct.Fin : momento;

                    var proxima = asignaciones
                        .Where(a => a.IdPersonal == p.IdPersonal && a.Inicio > momento)
                        .OrderBy(a => a.Inicio)
                        .FirstOrDefault();
                    var turno = ocupado ? TurnoDesde(asignAct.Inicio) : "Sin turno";
                    return new
                    {
                        p.IdPersonal,
                        p.Nombre,
                        p.DNI,
                        Especialidad = p.Especialidad,
                        Estado = estado,
                        AsignacionFin = asignAct?.Fin,
                    };
                })
                .ToList();
            var esp = cbEspecialidad.SelectedItem?.ToString();
            if (!string.IsNullOrWhiteSpace(esp) && !string.Equals(esp, "Todas", StringComparison.OrdinalIgnoreCase))
                datos = datos.Where(d => string.Equals(d.Especialidad?.Trim(), esp, StringComparison.OrdinalIgnoreCase)).ToList();
            var est = cbEstado.SelectedItem?.ToString();
            if (!string.IsNullOrWhiteSpace(est) && !string.Equals(est, "Todos", StringComparison.OrdinalIgnoreCase))
                datos = datos.Where(d => string.Equals(d.Estado, est, StringComparison.OrdinalIgnoreCase)).ToList();
            dataGridView1.DataSource = null;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = datos
                .OrderBy(d => d.Especialidad)
                .ThenBy(d => d.Nombre)
                .ToList();
        }
    }
}
