using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using BLL;

namespace UI
{
    public partial class PagosYDeudas : Form
    {
        private readonly PagoBLL _pagoBll = new PagoBLL();
        private readonly FacturaBLL _facturaBll = new FacturaBLL();

        public PagosYDeudas()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
        }

        private void PagosYDeudas_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            CargarMetodosPago();
            CargarFacturasPendientes();
        }

        private void ConfigurarGrid()
        {
            dataGridView1.AutoGenerateColumns = true;
        }

        private void CargarMetodosPago()
        {
            cbMetodoPago.Items.Clear();
            cbMetodoPago.Items.AddRange(new object[]
            {
                "Efectivo",
                "Transferencia",
                "Tarjeta de crédito",
                "Tarjeta de débito"
            });
            cbMetodoPago.SelectedIndex = 0;
            dtpFechaPagado.Value = DateTime.Now;
        }

        private void CargarFacturasPendientes()
        {
            var lista = _pagoBll.ListarFacturasPendientesOrdenadas();
            var vista = lista.Select(f => new
            {
                f.IdFactura,
                f.NombreCliente,
                f.DNI_CUIL_Cliente,
                Emision = f.FechaEmision,
                Vencimiento = f.FechaVencimiento,
                Total = f.Total,
                SaldoPendiente = f.Saldo_Pendiente,
                Recargo = f.RecargoVencimiento,
                Vencida = f.Vencida
            }).ToList();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = vista;

            ActualizarLabelVencidas(lista);

            if (vista.Count > 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[0].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
            }
        }

        private void ActualizarLabelVencidas(System.Collections.Generic.List<BE.FacturaBE> facturas)
        {
            var vencidas = facturas.Where(f => f.Vencida).Select(f => f.IdFactura).ToList();
            lblAvisoVencimiento.Text = vencidas.Count == 0
                ? ""
                : "Facturas vencidas: " + string.Join(", ", vencidas);
        }

        private int? FacturaSeleccionadaId()
        {
            if (dataGridView1.CurrentRow == null) return null;
            var bound = dataGridView1.CurrentRow.DataBoundItem;
            if (bound == null) return null;
            try
            {
                dynamic d = bound;
                return (int)d.IdFactura;
            }
            catch { return null; }
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var id = FacturaSeleccionadaId();
            if (id == null)
            {
                lblAvisoFacturaSeleccionada.Text = "";
                return;
            }
            var factura = _pagoBll.ObtenerPorId(id.Value);
            if (factura != null && factura.Vencida)
            {
                lblAvisoFacturaSeleccionada.Text = $"Factura {factura.IdFactura}: venció (recargo 10% aplicado)";
            }
            else
            {
                lblAvisoFacturaSeleccionada.Text = "";
            }
        }

        private void RegistrarPago()
        {
            var id = FacturaSeleccionadaId();
            if (id == null)
            {
                MessageBox.Show("Seleccione una factura.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!double.TryParse(txtMontoPagado.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out var monto) || monto <= 0)
            {
                MessageBox.Show("Ingrese un monto válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMontoPagado.Focus();
                return;
            }
            var metodo = cbMetodoPago.SelectedItem?.ToString() ?? string.Empty;
            var fecha = dtpFechaPagado.Value;
            try
            {
                var factura = _pagoBll.RegistrarPago(id.Value, monto, fecha, metodo);

                var msg = factura.Estado_Pago
                    ? "Pago registrado. La factura quedó saldada."
                    : $"Pago registrado. Saldo restante: {factura.Saldo_Pendiente.ToString("C", CultureInfo.GetCultureInfo("es-AR"))}.";
                MessageBox.Show(msg, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMontoPagado.Clear();
                cbMetodoPago.SelectedIndex = 0;
                dtpFechaPagado.Value = DateTime.Now;
                CargarFacturasPendientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo registrar el pago", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegistrarPago();
        }
    }
}
