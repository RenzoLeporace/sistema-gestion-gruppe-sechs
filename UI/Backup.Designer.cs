namespace UI
{
    partial class Backup
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
            label2 = new Label();
            label3 = new Label();
            btnHacerBack = new Button();
            BtnLimpiarBack = new Button();
            BtnExit = new Button();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(428, 188);
            dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 217);
            label1.Name = "label1";
            label1.Size = new Size(317, 20);
            label1.TabIndex = 1;
            label1.Text = "Información del Usuario que realizo el Backup:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 263);
            label2.Name = "label2";
            label2.Size = new Size(27, 20);
            label2.TabIndex = 2;
            label2.Text = "ID:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 305);
            label3.Name = "label3";
            label3.Size = new Size(62, 20);
            label3.TabIndex = 3;
            label3.Text = "Usuario:";
            // 
            // btnHacerBack
            // 
            btnHacerBack.Location = new Point(12, 344);
            btnHacerBack.Name = "btnHacerBack";
            btnHacerBack.Size = new Size(94, 59);
            btnHacerBack.TabIndex = 4;
            btnHacerBack.Text = "Realizar Backup";
            btnHacerBack.UseVisualStyleBackColor = true;
            btnHacerBack.Click += btnHacerBack_Click;
            // 
            // BtnLimpiarBack
            // 
            BtnLimpiarBack.Location = new Point(181, 344);
            BtnLimpiarBack.Name = "BtnLimpiarBack";
            BtnLimpiarBack.Size = new Size(94, 59);
            BtnLimpiarBack.TabIndex = 5;
            BtnLimpiarBack.Text = "Limpiar Campos";
            BtnLimpiarBack.UseVisualStyleBackColor = true;
            BtnLimpiarBack.Click += BtnLimpiarBack_Click;
            // 
            // BtnExit
            // 
            BtnExit.Location = new Point(346, 344);
            BtnExit.Name = "BtnExit";
            BtnExit.Size = new Size(94, 59);
            BtnExit.TabIndex = 6;
            BtnExit.Text = "Exit";
            BtnExit.UseVisualStyleBackColor = true;
            BtnExit.Click += BtnExit_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(77, 302);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(254, 27);
            textBox2.TabIndex = 17;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(45, 260);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(286, 27);
            textBox1.TabIndex = 16;
            // 
            // Backup
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(452, 416);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(BtnExit);
            Controls.Add(BtnLimpiarBack);
            Controls.Add(btnHacerBack);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Name = "Backup";
            Text = "Backup";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnHacerBack;
        private Button BtnLimpiarBack;
        private Button BtnExit;
        private TextBox textBox2;
        private TextBox textBox1;
    }
}