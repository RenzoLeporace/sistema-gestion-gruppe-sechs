using System;
using System.Globalization;
using System.Windows.Forms;
using BLL;

namespace UI
{
    public partial class CrearContrato : Form
    {
        private readonly ContratoBLL _contratoBll;
        private readonly TipoServicioBLL _tipoServicioBll;
        private readonly ClienteBLL _clienteBll;
        public CrearContrato()
        {
            InitializeComponent();
            _contratoBll = new ContratoBLL();
            _tipoServicioBll = new TipoServicioBLL();
            _clienteBll = new ClienteBLL();
            CargarTiposServicio();
            ConfigurarControles();
        }
        private void ConfigurarControles()
        {
            var ahora = DateTime.Now;
            dtrFechaServ.MinDate = ahora;
            dtpServicioFin.MinDate = ahora;
            dtrFechaServ.Value = ahora;
            dtpServicioFin.Value = ahora.AddHours(1);

            dtrFechaServ.ValueChanged += (_, __) =>
            {
                dtpServicioFin.MinDate = dtrFechaServ.Value;
                if (dtpServicioFin.Value < dtrFechaServ.Value)
                    dtpServicioFin.Value = dtrFechaServ.Value.AddHours(1);
            };
        }
        private void CargarTiposServicio()
        {
            cboxTipoServiciocontrato.Items.Clear();
            foreach (var ts in _tipoServicioBll.Listar())
                cboxTipoServiciocontrato.Items.Add(ts.Descripcion);

            if (cboxTipoServiciocontrato.Items.Count > 0)
                cboxTipoServiciocontrato.SelectedIndex = 0;
        }
        private void txtAsociarCliente_TextChanged(object sender, EventArgs e)
        {
            var dni = txtAsociarCliente.Text.Trim();
            if (dni.Length >= 7)
            {
                var cli = _clienteBll.BuscarPorDni(dni);
                txtAsociarCliente.BackColor = cli != null ? System.Drawing.Color.Honeydew : System.Drawing.Color.MistyRose;
            }
            else
            {
                txtAsociarCliente.BackColor = System.Drawing.Color.White;
            }
        }
        private void txtCostoEstimado_TextChanged(object sender, EventArgs e)
        {
            ActualizarCostoConIva();
        }

        private void ActualizarCostoConIva()
        {
            var texto = txtCostoEstimado.Text.Trim();
            if (string.IsNullOrWhiteSpace(texto))
            {
                lbl7.Text = "";
                return;
            }
            if (!decimal.TryParse(texto, NumberStyles.Number, CultureInfo.CurrentCulture, out var baseCost))
            {
                lbl7.Text = "Valor inválido";
                return;
            }

            var conIva = Math.Round(baseCost * 1.21m, 2);
            lbl7.Text = $"Costo + IVA (21%): {conIva.ToString("C", CultureInfo.GetCultureInfo("es-AR"))}";
        }

        private void cboxTipoServiciocontrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboxTipoServiciocontrato.SelectedItem != null)
            {
                var tarifa = _tipoServicioBll.ObtenerTarifaBase(cboxTipoServiciocontrato.SelectedItem.ToString() ?? string.Empty);
                if (tarifa > 0 && string.IsNullOrWhiteSpace(txtCostoEstimado.Text))
                    txtCostoEstimado.PlaceholderText = tarifa.ToString("F2");
                if (string.IsNullOrWhiteSpace(txtCostoEstimado.Text))
                {
                    if (decimal.TryParse(txtCostoEstimado.PlaceholderText, NumberStyles.Number, CultureInfo.InvariantCulture, out var baseCost))
                    {
                        var conIva = Math.Round(baseCost * 1.21m, 2);
                        lbl7.Text = $"Costo + IVA (21%): {conIva.ToString("C", CultureInfo.GetCultureInfo("es-AR"))}";
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var dni = txtAsociarCliente.Text.Trim();
                var tipoServicio = cboxTipoServiciocontrato.SelectedItem?.ToString();
                var fechaInicio = dtrFechaServ.Value;
                var fechaFin = dtpServicioFin.Value;
                string fuenteCosto = string.IsNullOrWhiteSpace(txtCostoEstimado.Text)
                    ? txtCostoEstimado.PlaceholderText
                    : txtCostoEstimado.Text;

                if (!decimal.TryParse(fuenteCosto, NumberStyles.Number, CultureInfo.CurrentCulture, out var baseCost))
                {
                    MessageBox.Show("Ingrese un costo estimado válido.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCostoEstimado.Focus();
                    return;
                }

                var costoConIva = Math.Round(baseCost * 1.21m, 2);
                var recursos = txtRecursosMinimos.Text;

                if (_clienteBll.BuscarPorDni(dni) == null)
                {
                    MessageBox.Show("El DNI/CUIT no corresponde a un cliente registrado.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var nuevo = _contratoBll.CrearContrato(dni, tipoServicio ?? string.Empty, fechaInicio, fechaFin, (double)costoConIva, recursos);
                MessageBox.Show($"Contrato creado correctamente. ID: {nuevo.IdContrato}\nCosto con IVA: {costoConIva.ToString("C", CultureInfo.GetCultureInfo("es-AR"))}",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormulario();
                lbl7.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al crear contrato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimpiarFormulario()
        {
            txtCostoEstimado.Text = "";
            txtRecursosMinimos.Text = "";
            var ahora = DateTime.Now;
            dtrFechaServ.Value = ahora;
            dtpServicioFin.Value = ahora.AddHours(1);
            lbl7.Text = "";
        }
    }
}
