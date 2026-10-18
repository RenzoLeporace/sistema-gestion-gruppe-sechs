using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using BLL;
using BE;
using Mapper;

namespace UI
{
    public partial class FinalizarContrato : Form
    {
        private readonly ContratoBLL _contratoBll;
        private readonly ServicioMpr _servicioMpr;
        private ContratoBE _seleccionado;

        public FinalizarContrato()
        {
            InitializeComponent();
            _contratoBll = new ContratoBLL();
            _servicioMpr = new ServicioMpr();
        }
        private void FinalizarContrato_Load_1(object sender, EventArgs e)
        {
            RefrescarGrid(_contratoBll.ListarActivos());
        }
        private void txtIdContratoFin_TextChanged(object sender, EventArgs e)
        {
        }
        private void txtIdContratoFin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                BuscarPorId();
            }
        }
        private void BuscarPorId()
        {
            var texto = txtIdContratoFin.Text.Trim();
            if (!int.TryParse(texto, out int id))
            {
                MessageBox.Show("Ingrese un ID numérico válido.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var contrato = _contratoBll.BuscarPorId(id);
            if (contrato == null)
            {
                MessageBox.Show("Contrato no encontrado.", "Resultado",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _seleccionado = null;
                return;
            }
            if (string.Equals(contrato.Estado, "Finalizado", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("El contrato ya está finalizado.", "Estado",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _seleccionado = null;
                return;
            }
            _seleccionado = contrato;
            SeleccionarFilaPorId(id);
            MostrarResumen(contrato);
        }
        private void MostrarResumen(ContratoBE c)
        {
            var resumen =
                $"ID: {c.IdContrato}\nCliente DNI/CUIT: {c.DNI_CUIL_Cliente}\nServicio: {c.Tipo_Servicio}\nFecha Inicio: {c.Fecha_Inicio:d}\nFecha Fin: {c.Fecha_Fin:d}\nCosto: {c.Costo_Estimado:F2}\nRecursos: {c.Recursos_Estimados}\nEstado: {c.Estado}";
            MessageBox.Show(resumen, "Resumen del Contrato",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void RefrescarGrid(System.Collections.Generic.IEnumerable<ContratoBE> lista)
        {
            dataGridView1.DataSource = null;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = lista.Select(c => new
            {
                c.IdContrato,
                c.DNI_CUIL_Cliente,
                c.Tipo_Servicio,
                c.Fecha_Inicio,
                c.Fecha_Fin,
                c.Costo_Estimado,
                c.Recursos_Estimados
            }).ToList();
            var todosServicios = _servicioMpr.GetAll() ?? new System.Collections.Generic.List<ServicioBE>();
            var ahora = DateTime.Now;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!(row.Cells["IdContrato"].Value is int id)) continue;
                var servicios = todosServicios
                    .Where(s => s != null && s.IdServicio > 0 && s.IdContrato == id)
                    .ToList();

                if (servicios.Count == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
                    continue;
                }

                bool hayFinalizado = servicios.Any(s =>
                    (s.Fecha_hora_fin != default && s.Fecha_hora_fin <= ahora)
                    || string.Equals((s.Estado ?? string.Empty).Trim(), "Finalizado", StringComparison.OrdinalIgnoreCase)
                    || (s.IdInformeOperativo.HasValue && s.IdInformeOperativo.Value > 0)
                );

                if (hayFinalizado)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }
        private void SeleccionarFilaPorId(int id)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["IdContrato"].Value is int valor && valor == id)
                {
                    row.Selected = true;
                    dataGridView1.CurrentCell = row.Cells[0];
                    break;
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SeleccionarDesdeGrid(e.RowIndex);
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SeleccionarDesdeGrid(e.RowIndex);
        }
        private void SeleccionarDesdeGrid(int rowIndex)
        {
            if (rowIndex < 0) return;
            var fila = dataGridView1.Rows[rowIndex];
            if (fila.Cells["IdContrato"].Value is int id)
            {
                var contrato = _contratoBll.BuscarPorId(id);
                if (contrato == null) return;
                if (string.Equals(contrato.Estado, "Finalizado", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("El contrato ya está finalizado.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _seleccionado = null;
                    return;
                }
                _seleccionado = contrato;
                txtIdContratoFin.Text = contrato.IdContrato.ToString();
                MostrarResumen(contrato);
            }
        }
        private void btnFinContrato_Click(object sender, EventArgs e)
        {
            if (_seleccionado == null)
            {
                BuscarPorId();
                if (_seleccionado == null) return;
            }
            var mensajeConfirmacion =
                $"¿Confirma finalizar el contrato ID {_seleccionado.IdContrato}?\nServicio: {_seleccionado.Tipo_Servicio}\nCliente: {_seleccionado.DNI_CUIL_Cliente}\nEstado actual: {_seleccionado.Estado}";
            var r = MessageBox.Show(mensajeConfirmacion, "Confirmar Finalización",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r != DialogResult.Yes) return;
            try
            {
                _contratoBll.FinalizarContrato(_seleccionado.IdContrato);
                MessageBox.Show("Contrato finalizado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _seleccionado = null;
                RefrescarGrid(_contratoBll.ListarActivos());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
