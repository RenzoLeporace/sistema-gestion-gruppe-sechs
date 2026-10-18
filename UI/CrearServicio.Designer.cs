namespace UI
{
    partial class CrearServicio
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            btnCrearServicio = new Button();
            txtVehiculos = new TextBox();
            label4 = new Label();
            label5 = new Label();
            txtPersonal = new TextBox();
            label6 = new Label();
            txtIdContrato = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtTipoServicioContrato = new TextBox();
            label3 = new Label();
            txtFechaServicioContrato = new TextBox();
            txtRecursosContrato = new TextBox();
            SuspendLayout();
            // 
            // btnCrearServicio
            // 
            btnCrearServicio.Location = new Point(79, 391);
            btnCrearServicio.Name = "btnCrearServicio";
            btnCrearServicio.Size = new Size(157, 47);
            btnCrearServicio.TabIndex = 0;
            btnCrearServicio.Text = "Crear Servicio";
            btnCrearServicio.UseVisualStyleBackColor = true;
            btnCrearServicio.Click += btnCrearServicio_Click;
            // 
            // txtVehiculos
            // 
            txtVehiculos.Location = new Point(149, 333);
            txtVehiculos.Name = "txtVehiculos";
            txtVehiculos.Size = new Size(125, 27);
            txtVehiculos.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 278);
            label4.Name = "label4";
            label4.Size = new Size(137, 20);
            label4.TabIndex = 9;
            label4.Text = "Personal Necesario:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 336);
            label5.Name = "label5";
            label5.Size = new Size(74, 20);
            label5.TabIndex = 10;
            label5.Text = "Vehiculos:";
            // 
            // txtPersonal
            // 
            txtPersonal.Location = new Point(149, 275);
            txtPersonal.Name = "txtPersonal";
            txtPersonal.Size = new Size(125, 27);
            txtPersonal.TabIndex = 5;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 20);
            label6.Name = "label6";
            label6.Size = new Size(89, 20);
            label6.TabIndex = 11;
            label6.Text = "ID Contrato:";
            // 
            // txtIdContrato
            // 
            txtIdContrato.Location = new Point(149, 17);
            txtIdContrato.Name = "txtIdContrato";
            txtIdContrato.Size = new Size(157, 27);
            txtIdContrato.TabIndex = 1;
            txtIdContrato.Leave += txtIdContrato_Leave;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 89);
            label1.Name = "label1";
            label1.Size = new Size(121, 20);
            label1.TabIndex = 6;
            label1.Text = "Tipo De Servicio:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 187);
            label2.Name = "label2";
            label2.Size = new Size(139, 20);
            label2.TabIndex = 7;
            label2.Text = "Detalles y Recursos:";
            // 
            // txtTipoServicioContrato
            // 
            txtTipoServicioContrato.Location = new Point(149, 82);
            txtTipoServicioContrato.Name = "txtTipoServicioContrato";
            txtTipoServicioContrato.ReadOnly = true;
            txtTipoServicioContrato.Size = new Size(157, 27);
            txtTipoServicioContrato.TabIndex = 13;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 147);
            label3.Name = "label3";
            label3.Size = new Size(106, 20);
            label3.TabIndex = 14;
            label3.Text = "Fecha Servicio:";
            // 
            // txtFechaServicioContrato
            // 
            txtFechaServicioContrato.Location = new Point(149, 140);
            txtFechaServicioContrato.Name = "txtFechaServicioContrato";
            txtFechaServicioContrato.ReadOnly = true;
            txtFechaServicioContrato.Size = new Size(157, 27);
            txtFechaServicioContrato.TabIndex = 15;
            // 
            // txtRecursosContrato
            // 
            txtRecursosContrato.Location = new Point(12, 210);
            txtRecursosContrato.Multiline = true;
            txtRecursosContrato.Name = "txtRecursosContrato";
            txtRecursosContrato.ReadOnly = true;
            txtRecursosContrato.Size = new Size(294, 47);
            txtRecursosContrato.TabIndex = 16;
            // 
            // CrearServicio
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(338, 450);
            Controls.Add(txtRecursosContrato);
            Controls.Add(txtFechaServicioContrato);
            Controls.Add(label3);
            Controls.Add(txtTipoServicioContrato);
            Controls.Add(txtIdContrato);
            Controls.Add(label6);
            Controls.Add(txtPersonal);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtVehiculos);
            Controls.Add(btnCrearServicio);
            Name = "CrearServicio";
            Text = "CrearServicio";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCrearServicio;
        private TextBox txtVehiculos;
        private Label label4;
        private Label label5;
        private TextBox txtPersonal;
        private Label label6;
        private TextBox txtIdContrato;
        private Label label1;
        private Label label2;
        private TextBox txtTipoServicioContrato;
        private Label label3;
        private TextBox txtFechaServicioContrato;
        private TextBox txtRecursosContrato;
    }
}