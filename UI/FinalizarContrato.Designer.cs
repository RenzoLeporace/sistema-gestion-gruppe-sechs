namespace UI
{
    partial class FinalizarContrato
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
            label1 = new Label();
            btnFinContrato = new Button();
            txtIdContratoFin = new TextBox();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(19, 43);
            label1.Name = "label1";
            label1.Size = new Size(89, 20);
            label1.TabIndex = 9;
            label1.Text = "ID Contrato:";
            // 
            // btnFinContrato
            // 
            btnFinContrato.Location = new Point(70, 114);
            btnFinContrato.Name = "btnFinContrato";
            btnFinContrato.Size = new Size(171, 65);
            btnFinContrato.TabIndex = 8;
            btnFinContrato.Text = "Finalizar Contrato";
            btnFinContrato.UseVisualStyleBackColor = true;
            btnFinContrato.Click += btnFinContrato_Click;
            // 
            // txtIdContratoFin
            // 
            txtIdContratoFin.Location = new Point(162, 40);
            txtIdContratoFin.Name = "txtIdContratoFin";
            txtIdContratoFin.Size = new Size(171, 27);
            txtIdContratoFin.TabIndex = 7;
            txtIdContratoFin.TextChanged += txtIdContratoFin_TextChanged;
            txtIdContratoFin.KeyDown += txtIdContratoFin_KeyDown;
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
            dataGridView1.TabIndex = 10;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // FinalizarContrato
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 222);
            Controls.Add(dataGridView1);
            Controls.Add(label1);
            Controls.Add(btnFinContrato);
            Controls.Add(txtIdContratoFin);
            Name = "FinalizarContrato";
            Text = "FinalizarContrato";
            Load += FinalizarContrato_Load_1;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFinContrato;
        private System.Windows.Forms.TextBox txtIdContratoFin;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}