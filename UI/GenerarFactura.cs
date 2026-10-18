using System;
using System.Linq;
using System.Windows.Forms;
using BLL;

namespace UI
{
    public partial class GenerarFactura : Form
    {
        private readonly FacturaBLL _facturaBll = new FacturaBLL();
        private readonly ContratoBLL _contratoBll = new ContratoBLL();

        public GenerarFactura()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionChanged += (_, __) => PrevisualizarSeleccion();
            dataGridView2.ReadOnly = true;
            dataGridView2.AutoGenerateColumns = true;
        }
        private void GenerarFactura_Load(object sender, EventArgs e)
        {
            CargarContratosPendientes();
        }
        private void CargarContratosPendientes()
        {
            try
            {
                var lista = _facturaBll.ListarContratosPendientesDeFacturar();
                var vista = lista.Select(c => new
                {
                    c.IdContrato,
                    c.DNI_CUIL_Cliente,
                    c.Tipo_Servicio,
                    Inicio = c.Fecha_Inicio,
                    Fin = c.Fecha_Fin,
                    c.Costo_Estimado,
                    c.Estado
                }).ToList();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = vista;
                dataGridView2.DataSource = null;
                btnGenerarFactura.Enabled = false;
                if (vista.Count == 0)
                {
                    MessageBox.Show("No hay contratos con período o servicios finalizados pendientes de facturar.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[0].Selected = true;
                    dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
                    PrevisualizarSeleccion();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar contratos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int? ContratoSeleccionadoId()
        {
            if (dataGridView1.CurrentRow == null) return null;
            var row = dataGridView1.CurrentRow.DataBoundItem;
            if (row == null) return null;
            try
            {
                dynamic d = row;
                return (int)d.IdContrato;
            }
            catch { return null; }
        }
        private void PrevisualizarSeleccion()
        {
            var id = ContratoSeleccionadoId();
            if (id == null)
            {
                dataGridView2.DataSource = null;
                btnGenerarFactura.Enabled = false;
                return;
            }
            try
            {
                var prev = _facturaBll.Previsualizar(id.Value);
                var vista = new[]
                {
                    new
                    {
                        prev.IdContrato,
                        prev.Cliente,
                        prev.DNI,
                        prev.ServiciosFacturables,
                        Subtotal = prev.Subtotal,
                        Impuestos = prev.Impuestos,
                        Total = prev.Total
                    }
                };
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = vista;
                btnGenerarFactura.Enabled = true;
            }
            catch (Exception ex)
            {
                dataGridView2.DataSource = null;
                btnGenerarFactura.Enabled = false;
                MessageBox.Show(ex.Message, "No se puede calcular la factura", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void Generar()
        {
            var id = ContratoSeleccionadoId();
            if (id == null)
            {
                MessageBox.Show("Seleccione un contrato.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var factura = _facturaBll.GenerarFactura(id.Value);
                MessageBox.Show($"Factura generada (ID: {factura.IdFactura}).\nArchivo: {factura.RutaDocumento}",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarContratosPendientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al generar la factura", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnGenerarFactura_Click(object sender, EventArgs e)
        {
            Generar();
        }
    }
}
