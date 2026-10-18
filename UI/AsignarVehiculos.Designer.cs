namespace UI
{
    partial class AsignarVehiculos
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
            btnAsignarServicioV = new Button();
            dataGridView2 = new DataGridView();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(334, 236);
            label2.Name = "label2";
            label2.Size = new Size(153, 20);
            label2.TabIndex = 9;
            label2.Text = "Vehículos Disponibles";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(324, 23);
            label1.Name = "label1";
            label1.Size = new Size(157, 20);
            label1.TabIndex = 8;
            label1.Text = "Servicios Sin Vehículos";
            // 
            // btnAsignarServicioV
            // 
            btnAsignarServicioV.Location = new Point(279, 442);
            btnAsignarServicioV.Name = "btnAsignarServicioV";
            btnAsignarServicioV.Size = new Size(233, 58);
            btnAsignarServicioV.TabIndex = 7;
            btnAsignarServicioV.Text = "Asignar A Servicio";
            btnAsignarServicioV.UseVisualStyleBackColor = true;
            btnAsignarServicioV.Click += btnAsignarServicioV_Click;
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(12, 259);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.ReadOnly = true;
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.Size = new Size(776, 167);
            dataGridView2.TabIndex = 6;
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
            dataGridView1.Size = new Size(776, 167);
            dataGridView1.TabIndex = 5;
            // 
            // AsignarVehiculos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 510);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnAsignarServicioV);
            Controls.Add(dataGridView2);
            Controls.Add(dataGridView1);
            Name = "AsignarVehiculos";
            Text = "AsignarVehiculos";
            Load += AsignarVehiculos_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private Label label1;
        private Button btnAsignarServicioV;
        private DataGridView dataGridView2;
        private DataGridView dataGridView1;
    }
}