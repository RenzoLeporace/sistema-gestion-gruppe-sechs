namespace UI
{
    partial class DisponibilidadPersonal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
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
            dataGridView1 = new DataGridView();
            lblEspecialidad = new Label();
            cbEspecialidad = new ComboBox();
            lblEstado = new Label();
            cbEstado = new ComboBox();
            lblMomento = new Label();
            dtpMomento = new DateTimePicker();
            btnRefrescar = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 46);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(976, 402);
            dataGridView1.TabIndex = 0;
            // 
            // lblEspecialidad
            // 
            lblEspecialidad.AutoSize = true;
            lblEspecialidad.Location = new Point(12, 15);
            lblEspecialidad.Name = "lblEspecialidad";
            lblEspecialidad.Size = new Size(96, 20);
            lblEspecialidad.TabIndex = 1;
            lblEspecialidad.Text = "Especialidad:";
            // 
            // cbEspecialidad
            // 
            cbEspecialidad.DropDownStyle = ComboBoxStyle.DropDownList;
            cbEspecialidad.Location = new Point(113, 12);
            cbEspecialidad.Name = "cbEspecialidad";
            cbEspecialidad.Size = new Size(170, 28);
            cbEspecialidad.TabIndex = 2;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Location = new Point(302, 15);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(57, 20);
            lblEstado.TabIndex = 3;
            lblEstado.Text = "Estado:";
            // 
            // cbEstado
            // 
            cbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cbEstado.Location = new Point(365, 12);
            cbEstado.Name = "cbEstado";
            cbEstado.Size = new Size(140, 28);
            cbEstado.TabIndex = 4;
            // 
            // lblMomento
            // 
            lblMomento.AutoSize = true;
            lblMomento.Location = new Point(537, 15);
            lblMomento.Name = "lblMomento";
            lblMomento.Size = new Size(77, 20);
            lblMomento.TabIndex = 7;
            lblMomento.Text = "Momento:";
            // 
            // dtpMomento
            // 
            dtpMomento.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpMomento.Format = DateTimePickerFormat.Custom;
            dtpMomento.Location = new Point(620, 12);
            dtpMomento.Name = "dtpMomento";
            dtpMomento.Size = new Size(169, 27);
            dtpMomento.TabIndex = 8;
            // 
            // btnRefrescar
            // 
            btnRefrescar.Location = new Point(828, 11);
            btnRefrescar.Name = "btnRefrescar";
            btnRefrescar.Size = new Size(160, 29);
            btnRefrescar.TabIndex = 9;
            btnRefrescar.Text = "Refrescar";
            btnRefrescar.UseVisualStyleBackColor = true;
            btnRefrescar.Click += btnRefrescar_Click;
            // 
            // DisponibilidadPersonal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 460);
            Controls.Add(btnRefrescar);
            Controls.Add(dtpMomento);
            Controls.Add(lblMomento);
            Controls.Add(cbEstado);
            Controls.Add(lblEstado);
            Controls.Add(cbEspecialidad);
            Controls.Add(lblEspecialidad);
            Controls.Add(dataGridView1);
            Name = "DisponibilidadPersonal";
            Text = "Disponibilidad de Personal";
            Load += DisponibilidadPersonal_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Label lblEspecialidad;
        private ComboBox cbEspecialidad;
        private Label lblEstado;
        private ComboBox cbEstado;
        private Label lblMomento;
        private DateTimePicker dtpMomento;
        private Button btnRefrescar;
    }
}