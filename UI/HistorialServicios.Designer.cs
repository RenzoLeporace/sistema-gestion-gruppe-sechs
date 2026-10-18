namespace UI
{
    partial class HistorialServicios
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
            label1 = new Label();
            btnConsHisServicios = new Button();
            txtIdServicioCons = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(358, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(430, 188);
            dataGridView1.TabIndex = 14;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(19, 43);
            label1.Name = "label1";
            label1.Size = new Size(125, 20);
            label1.TabIndex = 13;
            label1.Text = "DNI/CUIT Cliente:";
            // 
            // btnConsHisServicios
            // 
            btnConsHisServicios.Location = new Point(70, 114);
            btnConsHisServicios.Name = "btnConsHisServicios";
            btnConsHisServicios.Size = new Size(171, 65);
            btnConsHisServicios.TabIndex = 12;
            btnConsHisServicios.Text = "Consultar Historial";
            btnConsHisServicios.UseVisualStyleBackColor = true;
            btnConsHisServicios.Click += btnConsHisServicios_Click;
            // 
            // txtIdServicioCons
            // 
            txtIdServicioCons.Location = new Point(162, 40);
            txtIdServicioCons.Name = "txtIdServicioCons";
            txtIdServicioCons.Size = new Size(171, 27);
            txtIdServicioCons.TabIndex = 11;
            // 
            // HistorialServicios
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 224);
            Controls.Add(dataGridView1);
            Controls.Add(label1);
            Controls.Add(btnConsHisServicios);
            Controls.Add(txtIdServicioCons);
            Name = "HistorialServicios";
            Text = "HistorialServicios";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Label label1;
        private Button btnConsHisServicios;
        private TextBox txtIdServicioCons;
    }
}