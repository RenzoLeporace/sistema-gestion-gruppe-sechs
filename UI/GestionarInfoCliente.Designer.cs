namespace UI
{
    partial class GestionarInfoCliente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtDNICUITCliente = new TextBox();
            label1 = new Label();
            BtnAltaCliente = new Button();
            dataGridView1 = new DataGridView();
            txtNombreCliente = new TextBox();
            txtDireccionCliente = new TextBox();
            txtTelefonoCliente = new TextBox();
            txtCorreoCliente = new TextBox();
            BtnActualizarCliente = new Button();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            BtnEliminarCliente = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // txtDNICUITCliente
            // 
            txtDNICUITCliente.Location = new Point(110, 45);
            txtDNICUITCliente.Name = "txtDNICUITCliente";
            txtDNICUITCliente.Size = new Size(125, 27);
            txtDNICUITCliente.TabIndex = 0;
            txtDNICUITCliente.TextChanged += txtDNICUITCliente_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 48);
            label1.Name = "label1";
            label1.Size = new Size(75, 20);
            label1.TabIndex = 1;
            label1.Text = "DNI/CUIT:";
            // 
            // BtnAltaCliente
            // 
            BtnAltaCliente.Location = new Point(32, 342);
            BtnAltaCliente.Name = "BtnAltaCliente";
            BtnAltaCliente.Size = new Size(203, 29);
            BtnAltaCliente.TabIndex = 2;
            BtnAltaCliente.Text = "Alta Nuevo Cliente";
            BtnAltaCliente.UseVisualStyleBackColor = true;
            BtnAltaCliente.Click += BtnAltaCliente_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(269, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(519, 426);
            dataGridView1.TabIndex = 3;
            // 
            // txtNombreCliente
            // 
            txtNombreCliente.Location = new Point(110, 100);
            txtNombreCliente.Name = "txtNombreCliente";
            txtNombreCliente.Size = new Size(125, 27);
            txtNombreCliente.TabIndex = 4;
            // 
            // txtDireccionCliente
            // 
            txtDireccionCliente.Location = new Point(110, 160);
            txtDireccionCliente.Name = "txtDireccionCliente";
            txtDireccionCliente.Size = new Size(125, 27);
            txtDireccionCliente.TabIndex = 5;
            // 
            // txtTelefonoCliente
            // 
            txtTelefonoCliente.Location = new Point(110, 229);
            txtTelefonoCliente.Name = "txtTelefonoCliente";
            txtTelefonoCliente.Size = new Size(125, 27);
            txtTelefonoCliente.TabIndex = 6;
            // 
            // txtCorreoCliente
            // 
            txtCorreoCliente.Location = new Point(110, 291);
            txtCorreoCliente.Name = "txtCorreoCliente";
            txtCorreoCliente.Size = new Size(125, 27);
            txtCorreoCliente.TabIndex = 7;
            // 
            // BtnActualizarCliente
            // 
            BtnActualizarCliente.Location = new Point(32, 377);
            BtnActualizarCliente.Name = "BtnActualizarCliente";
            BtnActualizarCliente.Size = new Size(203, 29);
            BtnActualizarCliente.TabIndex = 8;
            BtnActualizarCliente.Text = "Actualizar Información";
            BtnActualizarCliente.UseVisualStyleBackColor = true;
            BtnActualizarCliente.Click += BtnActualizarCliente_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 103);
            label2.Name = "label2";
            label2.Size = new Size(67, 20);
            label2.TabIndex = 9;
            label2.Text = "Nombre:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 163);
            label3.Name = "label3";
            label3.Size = new Size(75, 20);
            label3.TabIndex = 10;
            label3.Text = "Dirección:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 232);
            label4.Name = "label4";
            label4.Size = new Size(70, 20);
            label4.TabIndex = 11;
            label4.Text = "Teléfono:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 294);
            label5.Name = "label5";
            label5.Size = new Size(57, 20);
            label5.TabIndex = 12;
            label5.Text = "Correo:";
            // 
            // BtnEliminarCliente
            // 
            BtnEliminarCliente.Location = new Point(32, 409);
            BtnEliminarCliente.Name = "BtnEliminarCliente";
            BtnEliminarCliente.Size = new Size(203, 29);
            BtnEliminarCliente.TabIndex = 13;
            BtnEliminarCliente.Text = "Baja Cliente";
            BtnEliminarCliente.UseVisualStyleBackColor = true;
            BtnEliminarCliente.Click += BtnEliminarCliente_Click;
            // 
            // GestionarInfoCliente
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(BtnEliminarCliente);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(BtnActualizarCliente);
            Controls.Add(txtCorreoCliente);
            Controls.Add(txtTelefonoCliente);
            Controls.Add(txtDireccionCliente);
            Controls.Add(txtNombreCliente);
            Controls.Add(dataGridView1);
            Controls.Add(BtnAltaCliente);
            Controls.Add(label1);
            Controls.Add(txtDNICUITCliente);
            Name = "GestionarInfoCliente";
            Text = "GestionarInfoCliente";
            Load += GestionarInfoCliente_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtDNICUITCliente;
        private Label label1;
        private Button BtnAltaCliente;
        private DataGridView dataGridView1;
        private TextBox txtNombreCliente;
        private TextBox txtDireccionCliente;
        private TextBox txtTelefonoCliente;
        private TextBox txtCorreoCliente;
        private Button BtnActualizarCliente;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button BtnEliminarCliente;
    }
}