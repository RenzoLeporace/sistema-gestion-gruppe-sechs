using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BLL;
using BE;

namespace UI
{
    public partial class GestionPersonal : Form
    {
        private readonly PersonalBLL _bll = new PersonalBLL();
        private readonly TipoServicioBLL _tipoSrvBll = new TipoServicioBLL();
        private bool _suppressDialogs = false;
        private string _pendingInitialMessage = null;

        public GestionPersonal()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
            this.AcceptButton = btnBuscarPes;
            CargarTiposDeServicioEnCombo();
        }
        private void GestionPersonal_Load_1(object sender, EventArgs e)
        {
            _suppressDialogs = true;
            _pendingInitialMessage = null;

            RefrescarGrilla(_bll.Listar());
            LimpiarFormulario();

            _suppressDialogs = false;

            if (!string.IsNullOrEmpty(_pendingInitialMessage))
            {
                MessageBox.Show(_pendingInitialMessage, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _pendingInitialMessage = null;
            }
        }
        private void CargarTiposDeServicioEnCombo()
        {
            var permitidos = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "ESCOLTA","EVENTO","TRANSPORTE"
            };

            var tipos = _tipoSrvBll.Listar()
                .Where(t => permitidos.Contains(t.Descripcion ?? ""))
                .OrderBy(t => t.Descripcion)
                .ToList();
            if (tipos.Count == 0)
            {
                tipos = new List<TipoServicioBE>
                {
                    new TipoServicioBE{ IdTipoServicio = 1, Descripcion = "ESCOLTA", TarifaBase = 0},
                    new TipoServicioBE{ IdTipoServicio = 2, Descripcion = "EVENTO", TarifaBase = 0},
                    new TipoServicioBE{ IdTipoServicio = 3, Descripcion = "TRANSPORTE", TarifaBase = 0},
                };
            }
            comboBox1.DataSource = tipos;
            comboBox1.DisplayMember = "Descripcion";
            comboBox1.ValueMember = "IdTipoServicio";
            comboBox1.SelectedIndex = -1;
        }
        private void btnBuscarPes_Click_1(object sender, EventArgs e)
        {
            var criterio = txtBuscaPers.Text?.Trim();
            var lista = _bll.BuscarPorCriterio(criterio);
            if (lista.Count == 0)
            {
                RefrescarGrilla(new List<PersonalBE>());
                LimpiarFormulario();
                var msg = "No se encontraron resultados. Ingrese los datos y presione Agregar para dar de alta.";
                if (!_suppressDialogs)
                    MessageBox.Show(msg, "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (string.IsNullOrEmpty(_pendingInitialMessage))
                    _pendingInitialMessage = msg;
            }
            else
            {
                RefrescarGrilla(lista);
            }
        }
        private void btnAgregarPers_Click_1(object sender, EventArgs e)
        {
            try
            {
                var nombre = txtNombrePers.Text?.Trim();
                var dni = txtDNIPers.Text?.Trim();
                var especialidad = (comboBox1.SelectedItem as TipoServicioBE)?.Descripcion;
                var habs = ObtenerHabilitacionesSeleccionadas();

                _bll.Agregar(nombre, dni, especialidad, habs, disponible: true, datos: LeerDatosAdicionales());
                RefrescarGrilla(_bll.Listar());
                SeleccionarPorDni(dni);
                MessageBox.Show("Personal agregado correctamente.", "Alta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnModificarPers_Click_1(object sender, EventArgs e)
        {
            try
            {
                var seleccionado = GetSeleccionado();
                if (seleccionado == null)
                {
                    MessageBox.Show("Seleccione un registro en la grilla para modificar.", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var nombre = txtNombrePers.Text?.Trim();
                var dni = txtDNIPers.Text?.Trim();
                var especialidad = (comboBox1.SelectedItem as TipoServicioBE)?.Descripcion;
                var habs = ObtenerHabilitacionesSeleccionadas();
                _bll.Actualizar(seleccionado.IdPersonal, nombre, dni, especialidad, habs, disponible: seleccionado.Estado_Disponibilidad, datos: LeerDatosAdicionales());
                RefrescarGrilla(_bll.Listar());
                SeleccionarPorId(seleccionado.IdPersonal);
                MessageBox.Show("Datos actualizados correctamente.", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al modificar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEliminarPers_Click_1(object sender, EventArgs e)
        {
            var seleccionado = GetSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un registro en la grilla para eliminar.", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var dr = MessageBox.Show($"¿Confirma eliminar a '{seleccionado.Nombre}' (DNI {seleccionado.DNI})?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes) return;
            try
            {
                _bll.Eliminar(seleccionado.IdPersonal);
                RefrescarGrilla(_bll.Listar());
                LimpiarFormulario();
                MessageBox.Show("Registro eliminado.", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var sel = GetSeleccionado();
            if (sel == null) return;
            CargarFormularioDesde(sel);
        }
        private void RefrescarGrilla(List<PersonalBE> datos)
        {
            dataGridView1.DataSource = null;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = datos;
        }
        private PersonalBE GetSeleccionado()
        {
            if (dataGridView1.CurrentRow?.DataBoundItem is PersonalBE p)
                return p;
            return null;
        }
        private void CargarFormularioDesde(PersonalBE p)
        {
            txtNombrePers.Text = p.Nombre;
            txtDNIPers.Text = p.DNI;
            SeleccionarTipoServicioPorDescripcion(p.Especialidad ?? string.Empty);
            var habs = (p.Habilitaciones ?? string.Empty)
                .Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(h => h.Trim().ToUpperInvariant())
                .ToHashSet();
            checkBox1.Checked = habs.Contains("PERMISO DE ARMAS");
            checkBox2.Checked = habs.Contains("PRIMEROS AUXILIOS");
            checkBox3.Checked = habs.Contains("CARNET DE CONDUCIR") || habs.Any(h => h.Contains("CARNET") || h.Contains("CONDUC"));
            txtCorreoPers.Text = p.Correo ?? string.Empty;
            txtTelefonoPers.Text = p.Telefono ?? string.Empty;
            txtTurnosPers.Text = p.TurnosDisponibles ?? string.Empty;
            txtCapacitacionesPers.Text = p.Capacitaciones ?? string.Empty;
            dtpVencimientoArmas.Checked = p.VencimientoLicenciaArmas.HasValue;
            if (p.VencimientoLicenciaArmas.HasValue)
                dtpVencimientoArmas.Value = p.VencimientoLicenciaArmas.Value;
        }
        private void SeleccionarTipoServicioPorDescripcion(string desc)
        {
            if (string.IsNullOrWhiteSpace(desc))
            {
                comboBox1.SelectedIndex = -1;
                return;
            }
            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                if (comboBox1.Items[i] is TipoServicioBE ts &&
                    string.Equals(ts.Descripcion, desc, StringComparison.OrdinalIgnoreCase))
                {
                    comboBox1.SelectedIndex = i;
                    return;
                }
            }
            comboBox1.SelectedIndex = -1;
        }
        private void LimpiarFormulario()
        {
            txtNombrePers.Text = string.Empty;
            txtDNIPers.Text = string.Empty;
            comboBox1.SelectedIndex = -1;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            txtCorreoPers.Clear();
            txtTelefonoPers.Clear();
            txtTurnosPers.Clear();
            txtCapacitacionesPers.Clear();
            dtpVencimientoArmas.Checked = false;
        }
        private PersonalBLL.DatosAdicionalesPersonal LeerDatosAdicionales()
        {
            return new PersonalBLL.DatosAdicionalesPersonal
            {
                Correo = txtCorreoPers.Text,
                Telefono = txtTelefonoPers.Text,
                TurnosDisponibles = txtTurnosPers.Text,
                Capacitaciones = txtCapacitacionesPers.Text,
                VencimientoLicenciaArmas = dtpVencimientoArmas.Checked ? dtpVencimientoArmas.Value.Date : null
            };
        }
        private List<string> ObtenerHabilitacionesSeleccionadas()
        {
            var res = new List<string>();
            if (checkBox1.Checked) res.Add("Permiso de Armas");
            if (checkBox2.Checked) res.Add("Primeros Auxilios");
            if (checkBox3.Checked) res.Add("Carnet de Conducir");
            return res;
        }
        private void SeleccionarPorDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni)) return;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.DataBoundItem is PersonalBE p && string.Equals(p.DNI, dni, StringComparison.OrdinalIgnoreCase))
                {
                    dataGridView1.ClearSelection();
                    row.Selected = true;
                    dataGridView1.CurrentCell = row.Cells[0];
                    break;
                }
            }
        }
        private void SeleccionarPorId(int id)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.DataBoundItem is PersonalBE p && p.IdPersonal == id)
                {
                    dataGridView1.ClearSelection();
                    row.Selected = true;
                    dataGridView1.CurrentCell = row.Cells[0];
                    break;
                }
            }
        }
    }
}
