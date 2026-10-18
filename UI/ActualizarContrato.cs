using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using BLL;
using BE;

namespace UI
{
    public partial class ActualizarContrato : Form
    {
        private readonly ContratoBLL _contratoBll;
        private ContratoBE _contratoSeleccionado;

        public ActualizarContrato()
        {
            InitializeComponent();
            _contratoBll = new ContratoBLL();
        }
        private void ActualizarContrato_Load(object sender, EventArgs e)
        {
            dTpNuevaDuración.MinDate = DateTime.Today;
            RefrescarGrid(_contratoBll.ListarModificables());
            LimpiarDetalle();
        }
        private void btnBuscarContrato_Click(object sender, EventArgs e)
        {
            BuscarYMostrar();
        }
        private void txtIdContrato_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                BuscarYMostrar();
            }
        }
        private void BuscarYMostrar()
        {
            var criterio = txtIdContrato.Text.Trim();
            if (string.IsNullOrWhiteSpace(criterio))
            {
                MessageBox.Show("Ingrese ID de contrato o DNI/CUIT.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var modificables = _contratoBll.ListarModificables();
            RefrescarGrid(modificables);

            if (int.TryParse(criterio, out int id))
            {
                var contrato = _contratoBll.BuscarPorId(id);
                if (contrato == null || string.Equals(contrato.Estado, "Finalizado", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Contrato no encontrado o ya finalizado.", "Resultado",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LimpiarDetalle();
                }
                else if (!_contratoBll.PuedeSerModificado(contrato))
                {
                    var ahora = DateTime.Now;
                    var tipo = contrato.Tipo_Servicio ?? "<null>";
                    var tipoRaw = tipo;
                    var detalles =
                        $"ID: {contrato.IdContrato}\n" +
                        $"Tipo_Servicio (literal): '{tipoRaw}' (length={tipoRaw.Length})\n" +
                        $"Fecha_Inicio: {contrato.Fecha_Inicio:O}\n" +
                        $"Fecha_Fin:   {contrato.Fecha_Fin:O}\n" +
                        $"Ahora:       {ahora:O}\n" +
                        $"Resultado PuedeSerModificado: {_contratoBll.PuedeSerModificado(contrato)}\n\n" +
                        "Razón probable: servicio en ejecución con servicio asignado, o comienza en <12h.";

                    MessageBox.Show("El contrato no está disponible para modificar.\n\nDetalles:\n" + detalles, "Resultado",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    LimpiarDetalle();
                }
                else
                {
                    CargarDetalle(contrato);
                    SeleccionarFilaPorId(id);
                }
            }
            else
            {
                var modificablesIds = modificables.Select(m => m.IdContrato).ToHashSet();
                var lista = _contratoBll.BuscarPorDni(criterio)
                    .Where(c => modificablesIds.Contains(c.IdContrato))
                    .ToList();
                if (lista.Count == 0)
                {
                    MessageBox.Show("No se encontraron contratos modificables para el DNI/CUIT.", "Resultado",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LimpiarDetalle();
                }
                else
                {
                    MessageBox.Show("Seleccione un contrato modificable en la lista para editar.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var fila = dataGridView1.Rows[e.RowIndex];
            if (fila.Cells["IdContrato"]?.Value is int id)
            {
                var contrato = _contratoBll.BuscarPorId(id);
                if (contrato != null && _contratoBll.PuedeSerModificado(contrato))
                {
                    CargarDetalle(contrato);
                }
            }
        }
        private void CargarDetalle(ContratoBE contrato)
        {
            _contratoSeleccionado = contrato;
            txtIdContrato.Text = contrato.IdContrato.ToString();
            dTpNuevaDuración.Value = contrato.Fecha_Fin < dTpNuevaDuración.MinDate ? DateTime.Today : contrato.Fecha_Fin;
            txtNuevosRecursos.Text = contrato.Recursos_Estimados;
            txtNuevoCostoEs.Text = contrato.Costo_Estimado.ToString("F2");
        }
        private void LimpiarDetalle()
        {
            _contratoSeleccionado = null;
            txtNuevosRecursos.Text = "";
            txtNuevoCostoEs.Text = "";
            dTpNuevaDuración.Value = DateTime.Today;
        }
        private void RefrescarGrid(System.Collections.Generic.IEnumerable<ContratoBE> contratos)
        {
            dataGridView1.DataSource = null;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = contratos
                .Where(c => !string.Equals(c.Estado, "Finalizado", StringComparison.OrdinalIgnoreCase))
                .Select(c => new
                {
                    c.IdContrato,
                    c.DNI_CUIL_Cliente,
                    c.Tipo_Servicio,
                    c.Fecha_Inicio,
                    c.Fecha_Fin,
                    c.Costo_Estimado,
                    c.Recursos_Estimados,
                    c.Estado
                }).ToList();
        }
        private void SeleccionarFilaPorId(int idContrato)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["IdContrato"].Value is int id && id == idContrato)
                {
                    row.Selected = true;
                    dataGridView1.CurrentCell = row.Cells[0];
                    break;
                }
            }
        }
        private void btnActualizarContrato_Click(object sender, EventArgs e)
        {
            if (_contratoSeleccionado == null)
            {
                MessageBox.Show("Debe seleccionar un contrato activo.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (!decimal.TryParse(txtNuevoCostoEs.Text?.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out var baseCost) || baseCost <= 0)
                {
                    MessageBox.Show("Ingrese un costo válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var nuevoCostoConIva = Math.Round(baseCost * 1.21m, 2);

                _contratoBll.ActualizarContrato(
                    _contratoSeleccionado.IdContrato,
                    (double)nuevoCostoConIva,
                    txtNuevosRecursos.Text,
                    dTpNuevaDuración.Value
                );

                MessageBox.Show("Contrato actualizado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                var actualizado = _contratoBll.BuscarPorId(_contratoSeleccionado.IdContrato);
                RefrescarGrid(_contratoBll.ListarModificables());
                if (actualizado != null && !string.Equals(actualizado.Estado, "Finalizado", StringComparison.OrdinalIgnoreCase))
                {
                    CargarDetalle(actualizado);
                    SeleccionarFilaPorId(actualizado.IdContrato);
                }
                else
                {
                    LimpiarDetalle();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
