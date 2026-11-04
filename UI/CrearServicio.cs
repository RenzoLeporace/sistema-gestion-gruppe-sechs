using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using BE;

namespace UI
{
    public partial class CrearServicio : Form
    {
        private readonly ServicioBLL _servicioBll = new ServicioBLL();
        private readonly ContratoBLL _contratoBll = new ContratoBLL();
        private readonly ClienteBLL _clienteBll = new ClienteBLL();

        private ContratoBE? _contratoActual;
        private DateTime _inicioServicio;
        private DateTime _finServicio;
        private Label? _lblClienteInfo;

        public CrearServicio()
        {
            InitializeComponent();
            txtPersonal.Text = "1";
            txtVehiculos.Text = "0";
        }
        private void txtIdContrato_Leave(object sender, EventArgs e)
        {
            if (!int.TryParse(txtIdContrato.Text.Trim(), out var id) || id <= 0)
            {
                LimpiarContrato();
                return;
            }
            var c = _contratoBll.BuscarPorId(id);
            if (c == null)
            {
                MessageBox.Show("Contrato no encontrado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LimpiarContrato();
                return;
            }
            if (string.Equals(c.Estado, "Finalizado", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("El contrato está finalizado. No se pueden crear servicios.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LimpiarContrato();
                return;
            }
            AplicarDatosContrato(c);
        }

        private void AplicarDatosContrato(ContratoBE c)
        {
            _contratoActual = c;
            txtTipoServicioContrato.Text = c.Tipo_Servicio ?? "";
            txtFechaServicioContrato.Text =
                $"{c.Fecha_Inicio:dd/MM/yyyy HH:mm} - {c.Fecha_Fin:dd/MM/yyyy HH:mm}";
            txtRecursosContrato.Text = c.Recursos_Estimados ?? "";
            var inicioBase = c.Fecha_Inicio;
            var finBase = c.Fecha_Fin;
            if (finBase <= inicioBase) finBase = inicioBase.AddHours(8);
            _inicioServicio = inicioBase;
            _finServicio = finBase;

            MostrarInfoCliente(c);
        }

        private void MostrarInfoCliente(ContratoBE contrato)
        {
            EnsureClienteLabel();

            if (string.IsNullOrWhiteSpace(contrato?.DNI_CUIL_Cliente))
            {
                _lblClienteInfo.Text = "";
                _lblClienteInfo.Visible = false;
                return;
            }

            var cliente = _clienteBll.BuscarPorDni(contrato.DNI_CUIL_Cliente);
            if (cliente != null)
            {
                _lblClienteInfo.Text = $"{cliente.Nombre} - {cliente.Telefono}";
                _lblClienteInfo.Visible = true;
            }
            else
            {
                _lblClienteInfo.Text = "Cliente: datos no disponibles";
                _lblClienteInfo.Visible = true;
            }
        }

        [MemberNotNull(nameof(_lblClienteInfo))]
        private void EnsureClienteLabel()
        {
            if (_lblClienteInfo != null) return;

            _lblClienteInfo = new Label
            {
                Name = "lblClienteInfo",
                AutoSize = true,
                ForeColor = Color.DarkBlue,
                Visible = false,
                Font = this.Font
            };

            try
            {
                var x = txtIdContrato.Location.X;
                var y = txtIdContrato.Location.Y + txtIdContrato.Height + 6;
                _lblClienteInfo.Location = new Point(x, y);
            }
            catch
            {
                _lblClienteInfo.Location = new Point(10, 10);
            }

            Controls.Add(_lblClienteInfo);
            _lblClienteInfo.BringToFront();
        }

        private void btnCrearServicio_Click(object sender, EventArgs e)
        {
            try
            {
                if (_contratoActual == null)
                {
                    MessageBox.Show("Primero ingrese un ID de contrato válido.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!int.TryParse(txtPersonal.Text.Trim(), out var personal) || personal < 0)
                {
                    MessageBox.Show("Cantidad de personal inválida.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!int.TryParse(txtVehiculos.Text.Trim(), out var vehiculos) || vehiculos < 0)
                {
                    MessageBox.Show("Cantidad de vehículos inválida.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (_inicioServicio < DateTime.Now)
                {
                    MessageBox.Show("La fecha y hora de inicio no pueden ser pasadas.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (_finServicio <= _inicioServicio)
                {
                    MessageBox.Show("La fecha y hora de fin deben ser posteriores al inicio.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var servicio = _servicioBll.CrearServicio(
                    idContrato: _contratoActual.IdContrato,
                    tipoServicio: _contratoActual.Tipo_Servicio,
                    inicio: _inicioServicio,
                    fin: _finServicio,
                    personalNecesario: personal,
                    vehiculosNecesarios: vehiculos,
                    observacion: ""
                );
                MessageBox.Show($"Servicio creado (ID: {servicio.IdServicio}). Recursos reservados.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarTodo();
                BLL.AppEvents.NotifyEconomiaActualizada();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al crear servicio",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimpiarContrato()
        {
            _contratoActual = null;
            txtTipoServicioContrato.Clear();
            txtFechaServicioContrato.Clear();
            txtRecursosContrato.Clear();
            txtPersonal.Text = "1";
            txtVehiculos.Text = "0";
            ClearClienteLabel();
        }

        private void ClearClienteLabel()
        {
            if (_lblClienteInfo == null) return;
            _lblClienteInfo.Text = "";
            _lblClienteInfo.Visible = false;
        }

        private void LimpiarTodo()
        {
            txtIdContrato.Clear();
            LimpiarContrato();
        }
    }
}
