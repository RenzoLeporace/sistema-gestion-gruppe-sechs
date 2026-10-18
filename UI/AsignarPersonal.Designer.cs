namespace UI
{
    partial class AsignarPersonal
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
            dataGridView2 = new DataGridView();
            btnAsignarServicio = new Button();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
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
            dataGridView1.Size = new Size(776, 167);
            dataGridView1.TabIndex = 0;
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(12, 252);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.ReadOnly = true;
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.Size = new Size(776, 167);
            dataGridView2.TabIndex = 1;
            // 
            // btnAsignarServicio
            // 
            btnAsignarServicio.Location = new Point(279, 435);
            btnAsignarServicio.Name = "btnAsignarServicio";
            btnAsignarServicio.Size = new Size(233, 58);
            btnAsignarServicio.TabIndex = 2;
            btnAsignarServicio.Text = "Asignar A Servicio";
            btnAsignarServicio.UseVisualStyleBackColor = true;
            btnAsignarServicio.Click += btnAsignarServicio_Click_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(324, 16);
            label1.Name = "label1";
            label1.Size = new Size(150, 20);
            label1.TabIndex = 3;
            label1.Text = "Servicios Sin Personal";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(334, 229);
            label2.Name = "label2";
            label2.Size = new Size(140, 20);
            label2.TabIndex = 4;
            label2.Text = "Personal Disponible";
            // 
            // AsignarPersonal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 505);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnAsignarServicio);
            Controls.Add(dataGridView2);
            Controls.Add(dataGridView1);
            Name = "AsignarPersonal";
            Text = "AsignarPersonal";
            Load += AsignarPersonal_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private Button btnAsignarServicio;
        private Label label1;
        private Label label2;
    }
}