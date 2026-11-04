using System;
using System.Linq;
using System.Windows.Forms;
using BLL;
using BE;
using Microsoft.VisualBasic;

namespace UI
{
    public partial class GestionVehiculos : Form
    {
        private readonly VehiculoBLL _vehiculoBll = new VehiculoBLL();
        private readonly MantenimientoBLL _mantBll = new MantenimientoBLL();
        private VehiculoBE? _seleccionado;

        private double _precioMantenimiento = 50000;

        public GestionVehiculos()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoGenerateColumns = true;

            dataGridView1.SelectionChanged += (_, __) => OnSeleccionChanged();
            dataGridView1.CellClick += (_, __) => OnSeleccionChanged();
            dataGridView1.CellDoubleClick += (_, __) => OnSeleccionChanged();
        }

        private void GestionVehiculos_Load(object sender, EventArgs e)
        {
            CargarListaInicial();
        }

        private void CargarListaInicial()
        {
            LlenarGrilla(_vehiculoBll.Listar());
            LimpiarFormulario();
        }

        private void LlenarGrilla(System.Collections.Generic.List<VehiculoBE> lista)
        {
            var vista = lista.Select(v => new
            {
                v.IdVehiculo,
                v.Patente,
                v.Modelo,
                v.Anio,
                Blindaje = v.Nivel_Blindaje,
                Estado = v.EstadoOperativo switch
                {
                    VehiculoBLL.EstadoDisponible => "Disponible",
                    VehiculoBLL.EstadoAsignado => "Asignado",
                    VehiculoBLL.EstadoMantenimiento => "Mantenimiento",
                    _ => "Desconocido"
                }
            })
            .OrderBy(x => x.Patente)
            .ToList();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = vista;
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[0].Selected = true;
                OnSeleccionChanged();
            }
        }

        private void OnSeleccionChanged()
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.IsNewRow)
            {
                _seleccionado = null;
                return;
            }

            object? rawId = null;
            if (dataGridView1.CurrentRow.Cells["IdVehiculo"] != null)
                rawId = dataGridView1.CurrentRow.Cells["IdVehiculo"].Value;
            else
                rawId = dataGridView1.CurrentRow.Cells[0].Value;

            if (rawId == null) { _seleccionado = null; return; }

            if (!int.TryParse(rawId.ToString(), out var id))
            {
                _seleccionado = null;
                return;
            }

            _seleccionado = _vehiculoBll.BuscarPorId(id);
            if (_seleccionado == null) return;

            txtPatente.Text = _seleccionado.Patente;
            txtModelo.Text = _seleccionado.Modelo;
            txtAnio.Text = _seleccionado.Anio.ToString();
            txtBlindaje.Text = _seleccionado.Nivel_Blindaje ?? "";
            chkMantenimiento.Checked = _seleccionado.EstadoOperativo == VehiculoBLL.EstadoMantenimiento;
        }

        private void Buscar()
        {
            var criterio = txtPatenteTipo.Text?.Trim() ?? "";
            var lista = _vehiculoBll.Buscar(criterio);
            LlenarGrilla(lista);
            if (lista.Count == 0)
            {
                MessageBox.Show("No se encontraron vehículos con el criterio ingresado.\nPuede utilizar 'Agregar' para registrar uno nuevo.",
                    "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormularioParaAlta(criterio);
            }
        }

        private void Agregar()
        {
            try
            {
                if (!int.TryParse(txtAnio.Text.Trim(), out var anio))
                {
                    MessageBox.Show("Año inválido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var nuevo = _vehiculoBll.Agregar(
                    patente: txtPatente.Text,
                    modelo: txtModelo.Text,
                    anio: anio,
                    nivelBlindaje: txtBlindaje.Text
                );

                if (chkMantenimiento.Checked)
                {
                    _mantBll.RegistrarIngresoMantenimiento(nuevo.IdVehiculo, nuevo.Patente);
                }

                MessageBox.Show($"Vehículo agregado (ID: {nuevo.IdVehiculo}).", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarListaInicial();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo agregar el vehículo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Modificar()
        {
            if (_seleccionado == null)
            {
                MessageBox.Show("Seleccione un vehículo de la lista.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                if (!int.TryParse(txtAnio.Text.Trim(), out var anio))
                {
                    MessageBox.Show("Año inválido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var estadoAnterior = _seleccionado.EstadoOperativo;

                _vehiculoBll.Editar(
                    idVehiculo: _seleccionado.IdVehiculo,
                    patente: txtPatente.Text,
                    modelo: txtModelo.Text,
                    anio: anio,
                    nivelBlindaje: txtBlindaje.Text
                );

                if (chkMantenimiento.Checked && estadoAnterior != VehiculoBLL.EstadoMantenimiento)
                {
                    _mantBll.RegistrarIngresoMantenimiento(_seleccionado.IdVehiculo, _seleccionado.Patente);
                }
                else if (!chkMantenimiento.Checked && estadoAnterior == VehiculoBLL.EstadoMantenimiento)
                {
                    var precio = PedirPrecioMantenimiento();
                    if (precio == null)
                    {
                        chkMantenimiento.Checked = true;
                        _vehiculoBll.MarcarEstado(_seleccionado.IdVehiculo, VehiculoBLL.EstadoMantenimiento);
                    }
                    else
                    {
                        try
                        {
                            _mantBll.CerrarUltimoMantenimiento(_seleccionado.IdVehiculo, precio.Value);
                        }
                        catch
                        {
                            _vehiculoBll.MarcarEstado(_seleccionado.IdVehiculo, VehiculoBLL.EstadoDisponible);
                        }
                    }
                }

                MessageBox.Show("Vehículo actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var lista = _vehiculoBll.Listar();
                LlenarGrilla(lista);
                var fila = dataGridView1.Rows.Cast<DataGridViewRow>()
                    .FirstOrDefault(r => int.TryParse(r.Cells["IdVehiculo"].Value?.ToString(), out var id) && id == _seleccionado.IdVehiculo);
                if (fila != null)
                {
                    fila.Selected = true;
                    OnSeleccionChanged();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo actualizar el vehículo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Eliminar()
        {
            if (_seleccionado == null)
            {
                MessageBox.Show("Seleccione un vehículo de la lista.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var confirm = MessageBox.Show($"¿Confirma eliminar el vehículo {_seleccionado.Patente}?",
                "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;
            try
            {
                _vehiculoBll.Eliminar(_seleccionado.IdVehiculo);
                MessageBox.Show("Vehículo eliminado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarListaInicial();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo eliminar el vehículo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            _seleccionado = null;
            txtPatente.Clear();
            txtModelo.Clear();
            txtAnio.Clear();
            txtBlindaje.Clear();
            chkMantenimiento.Checked = false;
        }

        private void LimpiarFormularioParaAlta(string criterio)
        {
            LimpiarFormulario();
            if (!string.IsNullOrWhiteSpace(criterio))
                txtPatente.Text = criterio.ToUpperInvariant();
        }

        private void btnBuscarVeh_Click(object sender, EventArgs e) => Buscar();
        private void btnEliminarVeh_Click(object sender, EventArgs e) => Eliminar();
        private void btnAgregarVeh_Click(object sender, EventArgs e) => Agregar();
        private void btnModificarVeh_Click(object sender, EventArgs e) => Modificar();
        private double? PedirPrecioMantenimiento()
        {
            var input = Interaction.InputBox(
                "Ingrese el precio del mantenimiento:",
                "Precio de mantenimiento",
                _precioMantenimiento.ToString("0")
            );

            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            if (!double.TryParse(input, out var precio) || precio < 0)
            {
                MessageBox.Show("Ingrese un precio de mantenimiento válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            _precioMantenimiento = precio;
            return precio;
        }
    }
}
