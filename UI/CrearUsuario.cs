using System;
using System.Linq;
using System.Windows.Forms;
using BLL;
using BE;

namespace UI
{
    public class CrearUsuario : Form
    {
        private TextBox txtNombre;
        private TextBox txtPassword;
        private CheckedListBox clbRoles;
        private Button btnGuardar;
        private Button btnCancelar;
        public CrearUsuario()
        {
            Text = "Crear Usuario";
            Width = 420;
            Height = 360;
            StartPosition = FormStartPosition.CenterParent;
            InitializeComponents();
            Load += CrearUsuario_Load;
        }
        private void InitializeComponents()
        {
            var lblNombre = new Label { Text = "Usuario:", Left = 16, Top = 20, AutoSize = true };
            txtNombre = new TextBox { Left = 16, Top = 44, Width = 360 };
            var lblPass = new Label { Text = "Contraseña:", Left = 16, Top = 80, AutoSize = true };
            txtPassword = new TextBox { Left = 16, Top = 104, Width = 360, UseSystemPasswordChar = true };
            btnGuardar = new Button { Text = "Guardar", Left = 200, Width = 84, Top = 280 };
            btnCancelar = new Button { Text = "Cancelar", Left = 300, Width = 76, Top = 280 };
            btnGuardar.Click += BtnGuardar_Click;
            btnCancelar.Click += (_, __) => { DialogResult = DialogResult.Cancel; Close(); };
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblPass);
            Controls.Add(txtPassword);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
        }
        private void CrearUsuario_Load(object? sender, EventArgs e)
        {
        }
        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            var current = BLLUsuario.Instance.CurrentUser;
            var esAdmin =
                current != null &&
                (
                    string.Equals(current.Nombre, "ADMIN", StringComparison.OrdinalIgnoreCase)
                    || (current.Roles?.Any(r => string.Equals(r.Nombre, "Admin", StringComparison.OrdinalIgnoreCase)) == true)
                    || BLLManagerSeguridad.Instance.UsuarioTienePermiso(current, "GESTION_USUARIOS")
                );
            if (!esAdmin)
            {
                var usuarioActual = current?.Nombre ?? "Ninguno";
                var rolesActuales = current?.Roles != null && current.Roles.Any()
                    ? string.Join(", ", current.Roles.Select(r => r.Nombre))
                    : "Ninguno";
                MessageBox.Show($"Solo el usuario ADMIN puede crear usuarios y asignar roles.\nUsuario actual: {usuarioActual}\nRoles: {rolesActuales}", "Permiso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var nombre = txtNombre.Text?.Trim() ?? string.Empty;
            var pass = txtPassword.Text ?? string.Empty;
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Ingrese usuario y contraseña.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var nuevo = new BEUsuario { Nombre = nombre };
                var creado = BLLUsuario.Instance.CrearUsuario(nuevo, pass);         
                MessageBox.Show("Usuario creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}