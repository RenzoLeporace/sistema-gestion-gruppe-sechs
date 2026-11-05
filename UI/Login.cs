using System;
using System.Windows.Forms;
using BLL;

namespace UI
{
    public partial class Login : Form
    {
        private readonly BLLUsuario _bllUser = BLLUsuario.Instance;

        public Login()
        {
            InitializeComponent();
        }
        private void Login_Load(object sender, EventArgs e)
        {

        }
        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            var usuario = txtUsuario.Text?.Trim();
            var password = txtPassword.Text ?? string.Empty;
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Ingrese usuario y contraseña.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                if (_bllUser.ValidarCredenciales(usuario, password))
                {
                    if (_bllUser.CurrentUser?.DebeCambiarClave == true && !PedirClaveNueva(password))
                    {
                        _bllUser.Logout();
                        txtPassword.Clear();
                        txtPassword.Focus();
                        return;
                    }
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar credenciales: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool PedirClaveNueva(string claveActual)
        {
            using var frm = new CambiarClave(claveActual);
            if (frm.ShowDialog(this) == DialogResult.OK) return true;
            MessageBox.Show("Debe cambiar la contraseña para ingresar al sistema.", "Cambio de contraseña", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}