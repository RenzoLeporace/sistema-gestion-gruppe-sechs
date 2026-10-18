namespace UI
{
    partial class GenerarInformeOperativo
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
            label2 = new Label();
            label1 = new Label();
            dataGridView2 = new DataGridView();
            dataGridView1 = new DataGridView();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            btnGInforme = new Button();
            Incidencias = new Label();
            label3 = new Label();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(334, 210);
            label2.Name = "label2";
            label2.Size = new Size(154, 20);
            label2.TabIndex = 13;
            label2.Text = "Servicio Seleccionado";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(324, 16);
            label1.Name = "label1";
            label1.Size = new Size(145, 20);
            label1.TabIndex = 12;
            label1.Text = "Servicios Finalizados";
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(12, 233);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.ReadOnly = true;
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.Size = new Size(776, 95);
            dataGridView2.TabIndex = 11;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 39);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(776, 157);
            dataGridView1.TabIndex = 10;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(344, 363);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(125, 27);
            textBox1.TabIndex = 14;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(12, 363);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(125, 27);
            textBox2.TabIndex = 15;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(663, 363);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(125, 27);
            textBox3.TabIndex = 16;
            // 
            // btnGInforme
            // 
            btnGInforme.Location = new Point(324, 404);
            btnGInforme.Name = "btnGInforme";
            btnGInforme.Size = new Size(164, 42);
            btnGInforme.TabIndex = 17;
            btnGInforme.Text = "Generar Informe";
            btnGInforme.UseVisualStyleBackColor = true;
            btnGInforme.Click += btnGInforme_Click;
            // 
            // Incidencias
            // 
            Incidencias.AutoSize = true;
            Incidencias.Location = new Point(33, 340);
            Incidencias.Name = "Incidencias";
            Incidencias.Size = new Size(82, 20);
            Incidencias.TabIndex = 18;
            Incidencias.Text = "Incidencias";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(363, 340);
            label3.Name = "label3";
            label3.Size = new Size(81, 20);
            label3.TabIndex = 19;
            label3.Text = "Resultados";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(673, 340);
            label4.Name = "label4";
            label4.Size = new Size(105, 20);
            label4.TabIndex = 20;
            label4.Text = "Observaciones";
            // 
            // GenerarInformeOperativo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 458);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(Incidencias);
            Controls.Add(btnGInforme);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dataGridView2);
            Controls.Add(dataGridView1);
            Name = "GenerarInformeOperativo";
            Text = "GenerarInformeOperativo";
            Load += GenerarInformeOperativo_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private Label label1;
        private DataGridView dataGridView2;
        private DataGridView dataGridView1;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Button btnGInforme;
        private Label Incidencias;
        private Label label3;
        private Label label4;
    }
}