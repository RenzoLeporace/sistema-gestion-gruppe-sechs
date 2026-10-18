
namespace UI
{
    partial class Login
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnCancelar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblUsuario = new Label();
            txtUsuario = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            btnLogin = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(24, 24);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(62, 20);
            lblUsuario.TabIndex = 0;
            lblUsuario.Text = "Usuario:";
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(24, 48);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(240, 27);
            txtUsuario.TabIndex = 1;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(24, 88);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(86, 20);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "Contraseña:";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(24, 112);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(240, 27);
            txtPassword.TabIndex = 3;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(24, 156);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(94, 32);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Entrar";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click_1;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(170, 156);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(94, 32);
            btnCancelar.TabIndex = 5;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(292, 210);
            Controls.Add(btnCancelar);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(txtUsuario);
            Controls.Add(lblUsuario);
            Name = "Login";
            Text = "Login";
            Load += Login_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}