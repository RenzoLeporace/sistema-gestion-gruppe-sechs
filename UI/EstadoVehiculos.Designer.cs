namespace UI
{
    partial class EstadoVehiculos
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
            lblTitulo = new Label();
            lblEstado = new Label();
            cmbEstado = new ComboBox();
            lblBlindaje = new Label();
            cmbBlindaje = new ComboBox();
            lblTipo = new Label();
            txtTipoModelo = new TextBox();
            btnRefrescar = new Button();
            dataGridView1 = new DataGridView();
            lblResumenTotal = new Label();
            lblResumenDisponible = new Label();
            lblResumenAsignado = new Label();
            lblResumenMantenimiento = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitulo.Location = new Point(12, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(247, 25);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Control de Estado de Flota";
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Location = new Point(12, 47);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(57, 20);
            lblEstado.TabIndex = 1;
            lblEstado.Text = "Estado:";
            // 
            // cmbEstado
            // 
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Location = new Point(73, 43);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(151, 28);
            cmbEstado.TabIndex = 2;
            // 
            // lblBlindaje
            // 
            lblBlindaje.AutoSize = true;
            lblBlindaje.Location = new Point(242, 47);
            lblBlindaje.Name = "lblBlindaje";
            lblBlindaje.Size = new Size(66, 20);
            lblBlindaje.TabIndex = 3;
            lblBlindaje.Text = "Blindaje:";
            // 
            // cmbBlindaje
            // 
            cmbBlindaje.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBlindaje.FormattingEnabled = true;
            cmbBlindaje.Location = new Point(314, 43);
            cmbBlindaje.Name = "cmbBlindaje";
            cmbBlindaje.Size = new Size(151, 28);
            cmbBlindaje.TabIndex = 4;
            // 
            // lblTipo
            // 
            lblTipo.AutoSize = true;
            lblTipo.Location = new Point(481, 47);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(100, 20);
            lblTipo.TabIndex = 5;
            lblTipo.Text = "Tipo/Modelo:";
            // 
            // txtTipoModelo
            // 
            txtTipoModelo.Location = new Point(582, 43);
            txtTipoModelo.Name = "txtTipoModelo";
            txtTipoModelo.PlaceholderText = "Ej: SUV, VAN, Ranger";
            txtTipoModelo.Size = new Size(206, 27);
            txtTipoModelo.TabIndex = 6;
            // 
            // btnRefrescar
            // 
            btnRefrescar.Location = new Point(705, 9);
            btnRefrescar.Name = "btnRefrescar";
            btnRefrescar.Size = new Size(83, 29);
            btnRefrescar.TabIndex = 7;
            btnRefrescar.Text = "Refrescar";
            btnRefrescar.UseVisualStyleBackColor = true;
            btnRefrescar.Click += btnRefrescar_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 124);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(776, 314);
            dataGridView1.TabIndex = 8;
            // 
            // lblResumenTotal
            // 
            lblResumenTotal.AutoSize = true;
            lblResumenTotal.Location = new Point(12, 86);
            lblResumenTotal.Name = "lblResumenTotal";
            lblResumenTotal.Size = new Size(55, 20);
            lblResumenTotal.TabIndex = 9;
            lblResumenTotal.Text = "Total: -";
            // 
            // lblResumenDisponible
            // 
            lblResumenDisponible.AutoSize = true;
            lblResumenDisponible.Location = new Point(137, 86);
            lblResumenDisponible.Name = "lblResumenDisponible";
            lblResumenDisponible.Size = new Size(94, 20);
            lblResumenDisponible.TabIndex = 10;
            lblResumenDisponible.Text = "Disponible: -";
            // 
            // lblResumenAsignado
            // 
            lblResumenAsignado.AutoSize = true;
            lblResumenAsignado.Location = new Point(292, 86);
            lblResumenAsignado.Name = "lblResumenAsignado";
            lblResumenAsignado.Size = new Size(85, 20);
            lblResumenAsignado.TabIndex = 11;
            lblResumenAsignado.Text = "Asignado: -";
            // 
            // lblResumenMantenimiento
            // 
            lblResumenMantenimiento.AutoSize = true;
            lblResumenMantenimiento.Location = new Point(433, 86);
            lblResumenMantenimiento.Name = "lblResumenMantenimiento";
            lblResumenMantenimiento.Size = new Size(123, 20);
            lblResumenMantenimiento.TabIndex = 12;
            lblResumenMantenimiento.Text = "Mantenimiento: -";
            // 
            // EstadoVehiculos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblResumenMantenimiento);
            Controls.Add(lblResumenAsignado);
            Controls.Add(lblResumenDisponible);
            Controls.Add(lblResumenTotal);
            Controls.Add(dataGridView1);
            Controls.Add(btnRefrescar);
            Controls.Add(txtTipoModelo);
            Controls.Add(lblTipo);
            Controls.Add(cmbBlindaje);
            Controls.Add(lblBlindaje);
            Controls.Add(cmbEstado);
            Controls.Add(lblEstado);
            Controls.Add(lblTitulo);
            Name = "EstadoVehiculos";
            Text = "Estado de Vehículos";
            Load += EstadoVehiculos_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblEstado;
        private ComboBox cmbEstado;
        private Label lblBlindaje;
        private ComboBox cmbBlindaje;
        private Label lblTipo;
        private TextBox txtTipoModelo;
        private Button btnRefrescar;
        private DataGridView dataGridView1;
        private Label lblResumenTotal;
        private Label lblResumenDisponible;
        private Label lblResumenAsignado;
        private Label lblResumenMantenimiento;
    }
}