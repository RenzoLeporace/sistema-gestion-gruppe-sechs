namespace UI
{
    partial class GestionVehiculos
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
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            btnModificarVeh = new Button();
            btnAgregarVeh = new Button();
            txtModelo = new TextBox();
            txtBlindaje = new TextBox();
            txtAnio = new TextBox();
            txtPatente = new TextBox();
            btnEliminarVeh = new Button();
            btnBuscarVeh = new Button();
            txtPatenteTipo = new TextBox();
            dataGridView1 = new DataGridView();
            chkMantenimiento = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(319, 348);
            label5.Name = "label5";
            label5.Size = new Size(104, 20);
            label5.TabIndex = 29;
            label5.Text = "Nivel Blindaje:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(319, 269);
            label4.Name = "label4";
            label4.Size = new Size(64, 20);
            label4.TabIndex = 28;
            label4.Text = "Modelo:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 344);
            label3.Name = "label3";
            label3.Size = new Size(39, 20);
            label3.TabIndex = 27;
            label3.Text = "Año:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 269);
            label2.Name = "label2";
            label2.Size = new Size(61, 20);
            label2.TabIndex = 26;
            label2.Text = "Patente:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 54);
            label1.Name = "label1";
            label1.Size = new Size(97, 20);
            label1.TabIndex = 25;
            label1.Text = "Patente/Tipo:";
            // 
            // btnModificarVeh
            // 
            btnModificarVeh.Location = new Point(652, 320);
            btnModificarVeh.Name = "btnModificarVeh";
            btnModificarVeh.Size = new Size(94, 29);
            btnModificarVeh.TabIndex = 24;
            btnModificarVeh.Text = "Modificar";
            btnModificarVeh.UseVisualStyleBackColor = true;
            btnModificarVeh.Click += btnModificarVeh_Click;
            // 
            // btnAgregarVeh
            // 
            btnAgregarVeh.Location = new Point(652, 264);
            btnAgregarVeh.Name = "btnAgregarVeh";
            btnAgregarVeh.Size = new Size(94, 29);
            btnAgregarVeh.TabIndex = 23;
            btnAgregarVeh.Text = "Agregar";
            btnAgregarVeh.UseVisualStyleBackColor = true;
            btnAgregarVeh.Click += btnAgregarVeh_Click;
            // 
            // txtModelo
            // 
            txtModelo.Location = new Point(432, 266);
            txtModelo.Name = "txtModelo";
            txtModelo.Size = new Size(125, 27);
            txtModelo.TabIndex = 22;
            // 
            // txtBlindaje
            // 
            txtBlindaje.Location = new Point(432, 341);
            txtBlindaje.Name = "txtBlindaje";
            txtBlindaje.Size = new Size(125, 27);
            txtBlindaje.TabIndex = 21;
            // 
            // txtAnio
            // 
            txtAnio.Location = new Point(85, 341);
            txtAnio.Name = "txtAnio";
            txtAnio.Size = new Size(125, 27);
            txtAnio.TabIndex = 20;
            // 
            // txtPatente
            // 
            txtPatente.Location = new Point(85, 266);
            txtPatente.Name = "txtPatente";
            txtPatente.Size = new Size(125, 27);
            txtPatente.TabIndex = 19;
            // 
            // btnEliminarVeh
            // 
            btnEliminarVeh.Location = new Point(82, 172);
            btnEliminarVeh.Name = "btnEliminarVeh";
            btnEliminarVeh.Size = new Size(125, 29);
            btnEliminarVeh.TabIndex = 18;
            btnEliminarVeh.Text = "Eliminar";
            btnEliminarVeh.UseVisualStyleBackColor = true;
            btnEliminarVeh.Click += btnEliminarVeh_Click;
            // 
            // btnBuscarVeh
            // 
            btnBuscarVeh.Location = new Point(82, 111);
            btnBuscarVeh.Name = "btnBuscarVeh";
            btnBuscarVeh.Size = new Size(125, 29);
            btnBuscarVeh.TabIndex = 17;
            btnBuscarVeh.Text = "Buscar";
            btnBuscarVeh.UseVisualStyleBackColor = true;
            btnBuscarVeh.Click += btnBuscarVeh_Click;
            // 
            // txtPatenteTipo
            // 
            txtPatenteTipo.Location = new Point(134, 51);
            txtPatenteTipo.Name = "txtPatenteTipo";
            txtPatenteTipo.Size = new Size(125, 27);
            txtPatenteTipo.TabIndex = 16;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(299, 22);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(489, 214);
            dataGridView1.TabIndex = 15;
            // 
            // chkMantenimiento
            // 
            chkMantenimiento.AutoSize = true;
            chkMantenimiento.Location = new Point(85, 380);
            chkMantenimiento.Name = "chkMantenimiento";
            chkMantenimiento.Size = new Size(152, 24);
            chkMantenimiento.TabIndex = 30;
            chkMantenimiento.Text = "En mantenimiento";
            chkMantenimiento.UseVisualStyleBackColor = true;
            // 
            // GestionVehiculos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 420);
            Controls.Add(chkMantenimiento);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnModificarVeh);
            Controls.Add(btnAgregarVeh);
            Controls.Add(txtModelo);
            Controls.Add(txtBlindaje);
            Controls.Add(txtAnio);
            Controls.Add(txtPatente);
            Controls.Add(btnEliminarVeh);
            Controls.Add(btnBuscarVeh);
            Controls.Add(txtPatenteTipo);
            Controls.Add(dataGridView1);
            Name = "GestionVehiculos";
            Text = "GestionVehiculos";
            Load += GestionVehiculos_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button btnModificarVeh;
        private Button btnAgregarVeh;
        private TextBox txtModelo;
        private TextBox txtBlindaje;
        private TextBox txtAnio;
        private TextBox txtPatente;
        private Button btnEliminarVeh;
        private Button btnBuscarVeh;
        private TextBox txtPatenteTipo;
        private DataGridView dataGridView1;
        private CheckBox chkMantenimiento;
    }
}