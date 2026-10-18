namespace UI
{
    partial class PagosYDeudas
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
            dataGridView1 = new DataGridView();
            button1 = new Button();
            txtMontoPagado = new TextBox();
            cbMetodoPago = new ComboBox();
            dtpFechaPagado = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            lblAvisoVencimiento = new Label();
            lblAvisoFacturaSeleccionada = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(698, 253);
            dataGridView1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(437, 275);
            button1.Name = "button1";
            button1.Size = new Size(273, 57);
            button1.TabIndex = 1;
            button1.Text = "Registrar Pago";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // txtMontoPagado
            // 
            txtMontoPagado.Location = new Point(88, 275);
            txtMontoPagado.Name = "txtMontoPagado";
            txtMontoPagado.Size = new Size(125, 27);
            txtMontoPagado.TabIndex = 2;
            // 
            // cbMetodoPago
            // 
            cbMetodoPago.FormattingEnabled = true;
            cbMetodoPago.Location = new Point(88, 308);
            cbMetodoPago.Name = "cbMetodoPago";
            cbMetodoPago.Size = new Size(125, 28);
            cbMetodoPago.TabIndex = 3;
            // 
            // dtpFechaPagado
            // 
            dtpFechaPagado.Location = new Point(275, 293);
            dtpFechaPagado.Name = "dtpFechaPagado";
            dtpFechaPagado.Size = new Size(156, 27);
            dtpFechaPagado.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 278);
            label1.Name = "label1";
            label1.Size = new Size(56, 20);
            label1.TabIndex = 5;
            label1.Text = "Monto:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 312);
            label2.Name = "label2";
            label2.Size = new Size(65, 20);
            label2.TabIndex = 6;
            label2.Text = "Método:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(219, 298);
            label3.Name = "label3";
            label3.Size = new Size(50, 20);
            label3.TabIndex = 7;
            label3.Text = "Fecha:";
            // 
            // lblAvisoVencimiento
            // 
            lblAvisoVencimiento.AutoSize = true;
            lblAvisoVencimiento.Location = new Point(716, 12);
            lblAvisoVencimiento.Name = "lblAvisoVencimiento";
            lblAvisoVencimiento.Size = new Size(91, 20);
            lblAvisoVencimiento.TabIndex = 8;
            lblAvisoVencimiento.Text = "Vencimiento";
            // 
            // lblAvisoFacturaSeleccionada
            // 
            lblAvisoFacturaSeleccionada.AutoSize = true;
            lblAvisoFacturaSeleccionada.Location = new Point(716, 156);
            lblAvisoFacturaSeleccionada.Name = "lblAvisoFacturaSeleccionada";
            lblAvisoFacturaSeleccionada.Size = new Size(97, 20);
            lblAvisoFacturaSeleccionada.TabIndex = 9;
            lblAvisoFacturaSeleccionada.Text = "Seleccionada";
            // 
            // PagosYDeudas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1010, 344);
            Controls.Add(lblAvisoFacturaSeleccionada);
            Controls.Add(lblAvisoVencimiento);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dtpFechaPagado);
            Controls.Add(cbMetodoPago);
            Controls.Add(txtMontoPagado);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Name = "PagosYDeudas";
            Text = "PagosYDeudas";
            Load += PagosYDeudas_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button button1;
        private TextBox txtMontoPagado;
        private ComboBox cbMetodoPago;
        private DateTimePicker dtpFechaPagado;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label lblAvisoVencimiento;
        private Label lblAvisoFacturaSeleccionada;
    }
}