namespace UI
{
    partial class GestionPersonal
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
            txtBuscaPers = new TextBox();
            btnBuscarPes = new Button();
            btnEliminarPers = new Button();
            txtNombrePers = new TextBox();
            txtDNIPers = new TextBox();
            btnAgregarPers = new Button();
            btnModificarPers = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            comboBox1 = new ComboBox();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox3 = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(299, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(489, 214);
            dataGridView1.TabIndex = 0;
            // 
            // txtBuscaPers
            // 
            txtBuscaPers.Location = new Point(134, 41);
            txtBuscaPers.Name = "txtBuscaPers";
            txtBuscaPers.Size = new Size(125, 27);
            txtBuscaPers.TabIndex = 1;
            // 
            // btnBuscarPes
            // 
            btnBuscarPes.Location = new Point(82, 101);
            btnBuscarPes.Name = "btnBuscarPes";
            btnBuscarPes.Size = new Size(125, 29);
            btnBuscarPes.TabIndex = 2;
            btnBuscarPes.Text = "Buscar";
            btnBuscarPes.UseVisualStyleBackColor = true;
            btnBuscarPes.Click += btnBuscarPes_Click_1;
            // 
            // btnEliminarPers
            // 
            btnEliminarPers.Location = new Point(82, 162);
            btnEliminarPers.Name = "btnEliminarPers";
            btnEliminarPers.Size = new Size(125, 29);
            btnEliminarPers.TabIndex = 3;
            btnEliminarPers.Text = "Eliminar";
            btnEliminarPers.UseVisualStyleBackColor = true;
            btnEliminarPers.Click += btnEliminarPers_Click_1;
            // 
            // txtNombrePers
            // 
            txtNombrePers.Location = new Point(85, 256);
            txtNombrePers.Name = "txtNombrePers";
            txtNombrePers.Size = new Size(125, 27);
            txtNombrePers.TabIndex = 4;
            // 
            // txtDNIPers
            // 
            txtDNIPers.Location = new Point(85, 331);
            txtDNIPers.Name = "txtDNIPers";
            txtDNIPers.Size = new Size(125, 27);
            txtDNIPers.TabIndex = 5;
            // 
            // btnAgregarPers
            // 
            btnAgregarPers.Location = new Point(652, 254);
            btnAgregarPers.Name = "btnAgregarPers";
            btnAgregarPers.Size = new Size(94, 29);
            btnAgregarPers.TabIndex = 8;
            btnAgregarPers.Text = "Agregar";
            btnAgregarPers.UseVisualStyleBackColor = true;
            btnAgregarPers.Click += btnAgregarPers_Click_1;
            // 
            // btnModificarPers
            // 
            btnModificarPers.Location = new Point(652, 329);
            btnModificarPers.Name = "btnModificarPers";
            btnModificarPers.Size = new Size(94, 29);
            btnModificarPers.TabIndex = 9;
            btnModificarPers.Text = "Modificar";
            btnModificarPers.UseVisualStyleBackColor = true;
            btnModificarPers.Click += btnModificarPers_Click_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 44);
            label1.Name = "label1";
            label1.Size = new Size(99, 20);
            label1.TabIndex = 10;
            label1.Text = "Nombre/DNI:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 259);
            label2.Name = "label2";
            label2.Size = new Size(67, 20);
            label2.TabIndex = 11;
            label2.Text = "Nombre:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 334);
            label3.Name = "label3";
            label3.Size = new Size(38, 20);
            label3.TabIndex = 12;
            label3.Text = "DNI:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(319, 259);
            label4.Name = "label4";
            label4.Size = new Size(96, 20);
            label4.TabIndex = 13;
            label4.Text = "Especialidad:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(319, 338);
            label5.Name = "label5";
            label5.Size = new Size(107, 20);
            label5.TabIndex = 14;
            label5.Text = "Habilitaciones:";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(430, 256);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 15;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(445, 306);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(150, 24);
            checkBox1.TabIndex = 16;
            checkBox1.Text = "Permiso de Armas";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(445, 365);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(145, 24);
            checkBox2.TabIndex = 17;
            checkBox2.Text = "Primeros Auxilios";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(445, 335);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(137, 24);
            checkBox3.TabIndex = 18;
            checkBox3.Text = "Carnet Conducir";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // GestionPersonal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 392);
            Controls.Add(checkBox3);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(comboBox1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnModificarPers);
            Controls.Add(btnAgregarPers);
            Controls.Add(txtDNIPers);
            Controls.Add(txtNombrePers);
            Controls.Add(btnEliminarPers);
            Controls.Add(btnBuscarPes);
            Controls.Add(txtBuscaPers);
            Controls.Add(dataGridView1);
            Name = "GestionPersonal";
            Text = "GestionPersonal";
            Load += GestionPersonal_Load_1;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private TextBox txtBuscaPers;
        private Button btnBuscarPes;
        private Button btnEliminarPers;
        private TextBox txtNombrePers;
        private TextBox txtDNIPers;
        private Button btnAgregarPers;
        private Button btnModificarPers;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private ComboBox comboBox1;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
    }
}