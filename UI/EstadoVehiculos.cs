using System;
using System.Linq;
using System.Windows.Forms;
using BLL;

namespace UI
{
    public partial class EstadoVehiculos : Form
    {
        private readonly VehiculoBLL _vehiculoBll = new VehiculoBLL();
        private readonly AsignacionVehiculoBLL _asigVehiculoBll = new AsignacionVehiculoBLL();

        public EstadoVehiculos()
        {
            InitializeComponent();
            cmbEstado.SelectedIndexChanged += (_, __) => AplicarFiltros();
            cmbBlindaje.SelectedIndexChanged += (_, __) => AplicarFiltros();
            txtTipoModelo.TextChanged += (_, __) => AplicarFiltros();
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
        }
        private void EstadoVehiculos_Load(object sender, EventArgs e)
        {
            Iniciar();
        }
        private void Iniciar()
        {
            try
            {
                _asigVehiculoBll.SincronizarDisponibilidad();
            }
            catch
            {
            }
            cmbEstado.Items.Clear();
            cmbEstado.Items.Add("Todos");
            cmbEstado.Items.Add("Disponible");
            cmbEstado.Items.Add("Asignado");
            cmbEstado.Items.Add("Mantenimiento");
            if (cmbEstado.SelectedIndex < 0) cmbEstado.SelectedIndex = 0;

            var blindajes = _vehiculoBll.Listar()
                .Select(v => (v.Nivel_Blindaje ?? "").Trim())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(s => s)
                .ToList();
            cmbBlindaje.Items.Clear();
            cmbBlindaje.Items.Add("Todos");
            foreach (var b in blindajes) cmbBlindaje.Items.Add(b);
            if (cmbBlindaje.SelectedIndex < 0) cmbBlindaje.SelectedIndex = 0;
            RefrescarResumen();
            AplicarFiltros();
        }
        private static string EstadoToText(int estado) => estado switch
        {
            VehiculoBLL.EstadoDisponible => "Disponible",
            VehiculoBLL.EstadoAsignado => "Asignado",
            VehiculoBLL.EstadoMantenimiento => "Mantenimiento",
            _ => "Desconocido"
        };
        private void RefrescarResumen()
        {
            var all = _vehiculoBll.Listar();
            var total = all.Count;
            var disp = all.Count(v => v.EstadoOperativo == VehiculoBLL.EstadoDisponible);
            var asig = all.Count(v => v.EstadoOperativo == VehiculoBLL.EstadoAsignado);
            var mant = all.Count(v => v.EstadoOperativo == VehiculoBLL.EstadoMantenimiento);

            lblResumenTotal.Text = $"Total: {total}";
            lblResumenDisponible.Text = $"Disponible: {disp}";
            lblResumenAsignado.Text = $"Asignado: {asig}";
            lblResumenMantenimiento.Text = $"Mantenimiento: {mant}";
        }
        private void AplicarFiltros()
        {
            var estadoSel = (cmbEstado.SelectedItem?.ToString() ?? "Todos").Trim();
            var blindajeSel = (cmbBlindaje.SelectedItem?.ToString() ?? "Todos").Trim();
            var tipoTexto = (txtTipoModelo.Text ?? "").Trim();
            var query = _vehiculoBll.Listar().AsEnumerable();
            if (!string.Equals(estadoSel, "Todos", StringComparison.OrdinalIgnoreCase))
            {
                var objetivo = estadoSel;
                query = query.Where(v => string.Equals(EstadoToText(v.EstadoOperativo), objetivo, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.Equals(blindajeSel, "Todos", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(v => string.Equals((v.Nivel_Blindaje ?? "").Trim(), blindajeSel, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrWhiteSpace(tipoTexto))
            {
                query = query.Where(v =>
                    (!string.IsNullOrEmpty(v.Modelo) && v.Modelo.Contains(tipoTexto, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(v.Patente) && v.Patente.Contains(tipoTexto, StringComparison.OrdinalIgnoreCase)));
            }

            var asignaciones = _asigVehiculoBll.ListarAsignaciones();
            var now = DateTime.Now;

            var vista = query
                .Select(v =>
                {
                    var proxima = asignaciones
                        .Where(a => a.IdVehiculo == v.IdVehiculo && a.Inicio > now)
                        .OrderBy(a => a.Inicio)
                        .FirstOrDefault();
                    return new
                    {
                        v.IdVehiculo,
                        v.Patente,
                        v.Modelo,
                        v.Anio,
                        Blindaje = v.Nivel_Blindaje,
                        Estado = EstadoToText(v.EstadoOperativo),
                        ProximaAsignacion = proxima?.Inicio
                    };
                })
                .OrderBy(x => x.Estado)
                .ThenBy(x => x.ProximaAsignacion.HasValue ? 0 : 1)
                .ThenBy(x => x.ProximaAsignacion)
                .ThenByDescending(x => x.Anio)
                .ThenBy(x => x.Modelo)
                .ToList();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = vista;
        }
        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            Iniciar();
        }
    }
}
