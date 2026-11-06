using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BLL;
using BE;

namespace UI
{
    public partial class GestionarInfoCliente : Form
    {
        private readonly ClienteBLL _clienteBll;
        private ClienteBE? _clienteActual;
        public GestionarInfoCliente()
        {
            InitializeComponent();
            ModernTheme.Apply(this);
            _clienteBll = new ClienteBLL();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
        }
        private void GestionarInfoCliente_Load(object sender, EventArgs e)
        {
            CargarGrid();
            LimpiarCampos(paraAlta: true);
        }
        private void txtDNICUITCliente_TextChanged(object sender, EventArgs e)
        {
            var dni = txtDNICUITCliente.Text.Trim();
            if (string.IsNullOrWhiteSpace(dni))
            {
                LimpiarCampos(paraAlta: true);
                return;
            }

            _clienteActual = _clienteBll.BuscarPorDni(dni);
            if (_clienteActual != null)
            {
                MapearAFormulario(_clienteActual);
                BtnAltaCliente.Enabled = false;
                BtnActualizarCliente.Enabled = true;
            }
            else
            {
                LimpiarCampos(paraAlta: true);
            }
        }
        private void BtnAltaCliente_Click(object sender, EventArgs e)
        {
            try
            {
                var nuevo = _clienteBll.Alta(
                    txtNombreCliente.Text,
                    txtDireccionCliente.Text,
                    txtTelefonoCliente.Text,
                    txtCorreoCliente.Text,
                    txtDNICUITCliente.Text
                );
                MessageBox.Show($"Cliente registrado correctamente. ID asignado: {nuevo.IdCliente}", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _clienteActual = nuevo;
                CargarGrid();
                MapearAFormulario(_clienteActual);
                BtnAltaCliente.Enabled = false;
                BtnActualizarCliente.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en alta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnActualizarCliente_Click(object sender, EventArgs e)
        {
            if (_clienteActual == null)
            {
                MessageBox.Show("No hay cliente cargado para actualizar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                _clienteBll.Actualizar(
                    txtNombreCliente.Text,
                    txtDireccionCliente.Text,
                    txtTelefonoCliente.Text,
                    txtCorreoCliente.Text,
                    txtDNICUITCliente.Text
                );

                MessageBox.Show("Información actualizada correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _clienteActual = _clienteBll.BuscarPorDni(txtDNICUITCliente.Text);
                CargarGrid();
                if (_clienteActual != null) MapearAFormulario(_clienteActual);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en actualización", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MapearAFormulario(ClienteBE cli)
        {
            txtNombreCliente.Text = cli.Nombre;
            txtDireccionCliente.Text = cli.Direccion;
            txtTelefonoCliente.Text = cli.Telefono;
            txtCorreoCliente.Text = cli.Correo;
            txtDNICUITCliente.Text = cli.DNI_CUIL;
        }
        private void LimpiarCampos(bool paraAlta)
        {
            if (paraAlta)
            {
                _clienteActual = null;
                txtNombreCliente.Text = "";
                txtDireccionCliente.Text = "";
                txtTelefonoCliente.Text = "";
                txtCorreoCliente.Text = "";
                BtnAltaCliente.Enabled = true;
                BtnActualizarCliente.Enabled = false;
            }
        }
        private void CargarGrid()
        {
            var lista = _clienteBll.Listar();
            dataGridView1.DataSource = null;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = lista
                .Select(c => new
                {
                    c.IdCliente,
                    c.Nombre,
                    c.DNI_CUIL,
                    c.Telefono,
                    c.Correo,
                    c.Direccion
                }).ToList();
            dataGridView1.ClearSelection();
        }

        private void DataGridView1_SelectionChanged(object? sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                LimpiarCampos(paraAlta: true);
                return;
            }
            var cell = dataGridView1.CurrentRow.Cells["DNI_CUIL"];
            var dni = cell?.Value?.ToString()?.Trim();
            if (string.IsNullOrWhiteSpace(dni))
            {
                LimpiarCampos(paraAlta: true);
                return;
            }
            _clienteActual = _clienteBll.BuscarPorDni(dni);
            if (_clienteActual != null)
            {
                MapearAFormulario(_clienteActual);
                BtnAltaCliente.Enabled = false;
                BtnActualizarCliente.Enabled = true;
            }
            else
            {
                LimpiarCampos(paraAlta: true);
            }
        }

        private void BtnEliminarCliente_Click(object sender, EventArgs e)
        {
            if (_clienteActual == null)
            {
                MessageBox.Show("No hay cliente seleccionado para eliminar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"¿Seguro que desea eliminar al cliente {_clienteActual.Nombre}?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                _clienteBll.Eliminar(_clienteActual.IdCliente);

                MessageBox.Show("Cliente eliminado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos(paraAlta: true);
                CargarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al eliminar",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
