namespace UI
{
    partial class CrearContrato
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
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtAsociarCliente = new TextBox();
            cboxTipoServiciocontrato = new ComboBox();
            txtCostoEstimado = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label4 = new Label();
            label5 = new Label();
            button1 = new Button();
            label3 = new Label();
            label6 = new Label();
            dtpServicioFin = new DateTimePicker();
            dtrFechaServ = new DateTimePicker();
            txtRecursosMinimos = new TextBox();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            lbl7 = new Label();
            SuspendLayout();
            // 
            // txtAsociarCliente
            // 
            txtAsociarCliente.Location = new Point(189, 44);
            txtAsociarCliente.Name = "txtAsociarCliente";
            txtAsociarCliente.Size = new Size(151, 27);
            txtAsociarCliente.TabIndex = 0;
            txtAsociarCliente.TextChanged += txtAsociarCliente_TextChanged;
            // 
            // cboxTipoServiciocontrato
            // 
            cboxTipoServiciocontrato.FormattingEnabled = true;
            cboxTipoServiciocontrato.Location = new Point(189, 127);
            cboxTipoServiciocontrato.Name = "cboxTipoServiciocontrato";
            cboxTipoServiciocontrato.Size = new Size(151, 28);
            cboxTipoServiciocontrato.TabIndex = 1;
            cboxTipoServiciocontrato.SelectedIndexChanged += cboxTipoServiciocontrato_SelectedIndexChanged;
            // 
            // txtCostoEstimado
            // 
            txtCostoEstimado.Location = new Point(189, 349);
            txtCostoEstimado.Name = "txtCostoEstimado";
            txtCostoEstimado.Size = new Size(151, 27);
            txtCostoEstimado.TabIndex = 3;
            txtCostoEstimado.TextChanged += txtCostoEstimado_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 47);
            label1.Name = "label1";
            label1.Size = new Size(125, 20);
            label1.TabIndex = 5;
            label1.Text = "DNI/CUIT Cliente:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(21, 130);
            label2.Name = "label2";
            label2.Size = new Size(121, 20);
            label2.TabIndex = 6;
            label2.Text = "Tipo De Servicio:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(21, 352);
            label4.Name = "label4";
            label4.Size = new Size(116, 20);
            label4.TabIndex = 8;
            label4.Text = "Costo Estimado:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 430);
            label5.Name = "label5";
            label5.Size = new Size(139, 20);
            label5.TabIndex = 9;
            label5.Text = "Detalles y Recursos:";
            // 
            // button1
            // 
            button1.Location = new Point(97, 476);
            button1.Name = "button1";
            button1.Size = new Size(153, 52);
            button1.TabIndex = 10;
            button1.Text = "Crear Contrato";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(21, 253);
            label3.Name = "label3";
            label3.Size = new Size(73, 20);
            label3.TabIndex = 14;
            label3.Text = "Fecha Fin:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(21, 188);
            label6.Name = "label6";
            label6.Size = new Size(90, 20);
            label6.TabIndex = 13;
            label6.Text = "Fecha Inicio:";
            // 
            // dtpServicioFin
            // 
            dtpServicioFin.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpServicioFin.Format = DateTimePickerFormat.Custom;
            dtpServicioFin.Location = new Point(189, 248);
            dtpServicioFin.Name = "dtpServicioFin";
            dtpServicioFin.ShowUpDown = true;
            dtpServicioFin.Size = new Size(157, 27);
            dtpServicioFin.TabIndex = 12;
            // 
            // dtrFechaServ
            // 
            dtrFechaServ.CustomFormat = "dd/MM/yyyy HH:mm";
            dtrFechaServ.Format = DateTimePickerFormat.Custom;
            dtrFechaServ.Location = new Point(189, 183);
            dtrFechaServ.Name = "dtrFechaServ";
            dtrFechaServ.ShowUpDown = true;
            dtrFechaServ.Size = new Size(157, 27);
            dtrFechaServ.TabIndex = 11;
            // 
            // txtRecursosMinimos
            // 
            txtRecursosMinimos.Location = new Point(166, 415);
            txtRecursosMinimos.Multiline = true;
            txtRecursosMinimos.Name = "txtRecursosMinimos";
            txtRecursosMinimos.Size = new Size(174, 49);
            txtRecursosMinimos.TabIndex = 17;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(21, 298);
            label7.Name = "label7";
            label7.Size = new Size(138, 20);
            label7.TabIndex = 19;
            label7.Text = "Tarifa Base: 300.000";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(166, 298);
            label8.Name = "label8";
            label8.Size = new Size(163, 20);
            label8.TabIndex = 20;
            label8.Text = "Tarifa Vehiculo: 100.000";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(62, 318);
            label9.Name = "label9";
            label9.Size = new Size(188, 20);
            label9.TabIndex = 21;
            label9.Text = "Tarifa Personal 12hs: 50.000";
            // 
            // lbl7
            // 
            lbl7.AutoSize = true;
            lbl7.Location = new Point(21, 383);
            lbl7.Name = "lbl7";
            lbl7.Size = new Size(0, 20);
            lbl7.TabIndex = 22;
            // 
            // CrearContrato
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(376, 540);
            Controls.Add(lbl7);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(txtRecursosMinimos);
            Controls.Add(label3);
            Controls.Add(label6);
            Controls.Add(dtpServicioFin);
            Controls.Add(dtrFechaServ);
            Controls.Add(button1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtCostoEstimado);
            Controls.Add(cboxTipoServiciocontrato);
            Controls.Add(txtAsociarCliente);
            Name = "CrearContrato";
            Text = "CrearContrato";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtAsociarCliente;
        private ComboBox cboxTipoServiciocontrato;
        private TextBox txtCostoEstimado;
        private Label label1;
        private Label label2;
        private Label label4;
        private Label label5;
        private Button button1;
        private Label label3;
        private Label label6;
        private DateTimePicker dtpServicioFin;
        private DateTimePicker dtrFechaServ;
        private TextBox txtRecursosMinimos;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label lbl7;
    }
}