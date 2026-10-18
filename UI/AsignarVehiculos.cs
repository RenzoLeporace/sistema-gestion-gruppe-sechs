using System;
using System.Linq;
using System.Windows.Forms;
using BLL;
using BE;

namespace UI
{
    public partial class AsignarVehiculos : Form
    {
        private readonly AsignacionVehiculoBLL _asigVehBll = new AsignacionVehiculoBLL();
        private readonly VehiculoBLL _vehiculoBll = new VehiculoBLL();
        private bool _suppressDialogs = false;
        private string _pendingInitialMessage = null;

        public AsignarVehiculos()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect = false;
            dataGridView2.ReadOnly = true;
            dataGridView2.AutoGenerateColumns = true;
        }

        private void DataGridView1_SelectionChanged(object? sender, EventArgs e)
        {
            CargarVehiculosDisponibles();
        }

        private void AsignarVehiculos_Load(object sender, EventArgs e)
        {
            CargarServicios();
        }
        private void CargarServicios()
        {
            var servicios = _asigVehBll.ListarServiciosQueRequierenVehiculos();
            var vista = servicios.Select(s => new
            {
                s.IdServicio,
                s.IdContrato,
                s.TipoServicio,
                Inicio = s.Fecha_hora_inicio,
                Fin = s.Fecha_hora_fin,
                Requiere = s.VehiculosNecesarios
            }).ToList();
            _suppressDialogs = true;
            _pendingInitialMessage = null;
            dataGridView1.SelectionChanged -= DataGridView1_SelectionChanged;

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = vista;

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.ClearSelection();
                var firstRow = dataGridView1.Rows[0];
                firstRow.Selected = true;
                dataGridView1.CurrentCell = firstRow.Cells[0];
                CargarVehiculosDisponibles();
            }
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
            _suppressDialogs = false;
            if (!string.IsNullOrEmpty(_pendingInitialMessage))
            {
                MessageBox.Show(_pendingInitialMessage, "Sin disponibles", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _pendingInitialMessage = null;
            }
        }
        private int? ServicioSeleccionadoId()
        {
            if (dataGridView1.CurrentRow == null) return null;
            return Convert.ToInt32(dataGridView1.CurrentRow.Cells["IdServicio"].Value);
        }
        private void CargarVehiculosDisponibles()
        {
            var disponibles = _asigVehBll.ListarVehiculosDisponibles();
            if (disponibles.Count == 0)
            {
                var msg = "No hay vehículos disponibles.\nSe mostrarán sugerencias desde mantenimiento.";
                if (!_suppressDialogs)
                {
                    MessageBox.Show(msg, "Sin disponibles", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (string.IsNullOrEmpty(_pendingInitialMessage))
                {
                    _pendingInitialMessage = msg;
                }

                var sugeridos = _asigVehBll.ListarSugeridosEnMantenimiento();
                var vistaSug = sugeridos.Select(v => new
                {
                    v.IdVehiculo,
                    v.Patente,
                    v.Modelo,
                    v.Anio,
                    v.Nivel_Blindaje,
                    Estado = "Mantenimiento"
                }).ToList();
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = vistaSug;
            }
            else
            {
                var vista = disponibles.Select(v => new
                {
                    v.IdVehiculo,
                    v.Patente,
                    v.Modelo,
                    v.Anio,
                    v.Nivel_Blindaje,
                    Estado = "Disponible"
                }).ToList();
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = vista;
            }
        }
        private void AsignarSeleccion()
        {
            var idServ = ServicioSeleccionadoId();
            if (idServ == null)
            {
                MessageBox.Show("Seleccione un servicio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dataGridView2.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un vehículo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var idVeh = Convert.ToInt32(dataGridView2.CurrentRow.Cells["IdVehiculo"].Value);
            var estado = dataGridView2.CurrentRow.Cells["Estado"].Value?.ToString() ?? "";
            try
            {
                if (estado == "Mantenimiento")
                {
                    var ok = MessageBox.Show(
                        "El vehículo está en mantenimiento. ¿Desea asignarlo con prioridad (saldrá de mantenimiento)?",
                        "Asignación prioritaria",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (ok == DialogResult.Yes)
                        _asigVehBll.AsignarDesdeMantenimiento(idServ.Value, idVeh);
                    else
                        return;
                }
                else
                {
                    _asigVehBll.Asignar(idServ.Value, idVeh);
                }
                MessageBox.Show("Vehículo asignado exitosamente al servicio.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarServicios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo asignar el vehículo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAsignarServicioV_Click(object sender, EventArgs e)
        {
            AsignarSeleccion();
        }
    }
}
