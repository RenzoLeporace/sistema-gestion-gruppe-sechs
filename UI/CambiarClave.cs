using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;
using BLL;

namespace UI
{
    /// <summary>
    /// Pide una contraseña nueva al iniciar sesión cuando el usuario debe cambiar la que tiene,
    /// como la contraseña inicial de ADMIN.
    /// </summary>
    public class CambiarClave : Form
    {
        private readonly string _claveActual;
        private TextBox txtNueva;
        private TextBox txtConfirmar;
        private Button btnGuardar;
        private Button btnCancelar;

        public CambiarClave(string claveActual)
        {
            _claveActual = claveActual;
            Text = "Cambiar contraseña";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(392, 228);
            InitializeComponents();
            AcceptButton = btnGuardar;
            CancelButton = btnCancelar;
        }

        [MemberNotNull(nameof(txtNueva), nameof(txtConfirmar), nameof(btnGuardar), nameof(btnCancelar))]
        private void InitializeComponents()
        {
            var lblAviso = new Label
            {
                Text = $"Por seguridad, elija una contraseña nueva de al menos {BLLUsuario.LongitudMinimaClave} caracteres para continuar.",
                Left = 16,
                Top = 16,
                Width = 360,
                Height = 40
            };
            var lblNueva = new Label { Text = "Contraseña nueva:", Left = 16, Top = 64, AutoSize = true };
            txtNueva = new TextBox { Left = 16, Top = 86, Width = 360, UseSystemPasswordChar = true };
            var lblConfirmar = new Label { Text = "Repetir contraseña:", Left = 16, Top = 120, AutoSize = true };
            txtConfirmar = new TextBox { Left = 16, Top = 142, Width = 360, UseSystemPasswordChar = true };
            btnGuardar = new Button { Text = "Guardar", Left = 200, Width = 84, Top = 186 };
            btnCancelar = new Button { Text = "Cancelar", Left = 300, Width = 76, Top = 186, DialogResult = DialogResult.Cancel };
            btnGuardar.Click += BtnGuardar_Click;
            Controls.AddRange(new Control[] { lblAviso, lblNueva, txtNueva, lblConfirmar, txtConfirmar, btnGuardar, btnCancelar });
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            if (!string.Equals(txtNueva.Text, txtConfirmar.Text, StringComparison.Ordinal))
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmar.Clear();
                txtConfirmar.Focus();
                return;
            }
            try
            {
                BLLUsuario.Instance.CambiarClave(_claveActual, txtNueva.Text);
                MessageBox.Show("La contraseña se actualizó correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNueva.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar la contraseña: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
