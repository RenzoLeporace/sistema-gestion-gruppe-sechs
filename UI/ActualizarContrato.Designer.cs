namespace UI
{
    partial class ActualizarContrato
    {
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
            txtIdContrato = new TextBox();
            btnBuscarContrato = new Button();
            txtNuevosRecursos = new TextBox();
            dTpNuevaDuración = new DateTimePicker();
            txtNuevoCostoEs = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            btnActualizarContrato = new Button();
            labelHint = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(397, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(391, 417);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // txtIdContrato
            // 
            txtIdContrato.Location = new Point(191, 51);
            txtIdContrato.Name = "txtIdContrato";
            txtIdContrato.Size = new Size(171, 27);
            txtIdContrato.TabIndex = 1;
            txtIdContrato.KeyDown += txtIdContrato_KeyDown;
            // 
            // btnBuscarContrato
            // 
            btnBuscarContrato.Location = new Point(99, 125);
            btnBuscarContrato.Name = "btnBuscarContrato";
            btnBuscarContrato.Size = new Size(171, 29);
            btnBuscarContrato.TabIndex = 2;
            btnBuscarContrato.Text = "Buscar";
            btnBuscarContrato.UseVisualStyleBackColor = true;
            btnBuscarContrato.Click += btnBuscarContrato_Click;
            // 
            // txtNuevosRecursos
            // 
            txtNuevosRecursos.Location = new Point(191, 282);
            txtNuevosRecursos.Name = "txtNuevosRecursos";
            txtNuevosRecursos.Size = new Size(171, 27);
            txtNuevosRecursos.TabIndex = 3;
            // 
            // dTpNuevaDuración
            // 
            dTpNuevaDuración.Location = new Point(191, 200);
            dTpNuevaDuración.Name = "dTpNuevaDuración";
            dTpNuevaDuración.Size = new Size(171, 27);
            dTpNuevaDuración.TabIndex = 4;
            // 
            // txtNuevoCostoEs
            // 
            txtNuevoCostoEs.Location = new Point(191, 369);
            txtNuevoCostoEs.Name = "txtNuevoCostoEs";
            txtNuevoCostoEs.Size = new Size(171, 27);
            txtNuevoCostoEs.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(48, 54);
            label1.Name = "label1";
            label1.Size = new Size(106, 20);
            label1.TabIndex = 6;
            label1.Text = "ID CONTRATO:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(21, 205);
            label2.Name = "label2";
            label2.Size = new Size(119, 20);
            label2.TabIndex = 7;
            label2.Text = "Nueva Fecha Fin:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(21, 285);
            label3.Name = "label3";
            label3.Size = new Size(142, 20);
            label3.TabIndex = 8;
            label3.Text = "Recursos Estimados:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(21, 372);
            label4.Name = "label4";
            label4.Size = new Size(116, 20);
            label4.TabIndex = 9;
            label4.Text = "Costo Estimado:";
            // 
            // btnActualizarContrato
            // 
            btnActualizarContrato.Location = new Point(99, 402);
            btnActualizarContrato.Name = "btnActualizarContrato";
            btnActualizarContrato.Size = new Size(171, 29);
            btnActualizarContrato.TabIndex = 10;
            btnActualizarContrato.Text = "Actualizar";
            btnActualizarContrato.UseVisualStyleBackColor = true;
            btnActualizarContrato.Click += btnActualizarContrato_Click;
            // 
            // labelHint
            // 
            labelHint.AutoSize = true;
            labelHint.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
            labelHint.Location = new Point(48, 78);
            labelHint.Name = "labelHint";
            labelHint.Size = new Size(157, 19);
            labelHint.TabIndex = 11;
            labelHint.Text = "Ingrese un ID numérico.";
            // 
            // ActualizarContrato
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 441);
            Controls.Add(labelHint);
            Controls.Add(btnActualizarContrato);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtNuevoCostoEs);
            Controls.Add(dTpNuevaDuración);
            Controls.Add(txtNuevosRecursos);
            Controls.Add(btnBuscarContrato);
            Controls.Add(txtIdContrato);
            Controls.Add(dataGridView1);
            Name = "ActualizarContrato";
            Text = "ActualizarContrato";
            Load += ActualizarContrato_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtIdContrato;
        private System.Windows.Forms.Button btnBuscarContrato;
        private System.Windows.Forms.TextBox txtNuevosRecursos;
        private System.Windows.Forms.DateTimePicker dTpNuevaDuración;
        private System.Windows.Forms.TextBox txtNuevoCostoEs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnActualizarContrato;
        private System.Windows.Forms.Label labelHint;
    }
}