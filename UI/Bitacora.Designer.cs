namespace UI
{
    partial class Bitacora
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
            BtnExit = new Button();
            btnBit = new Button();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            dataGridView1 = new DataGridView();
            radBackups = new RadioButton();
            radRestore = new RadioButton();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // BtnExit
            // 
            BtnExit.Location = new Point(309, 344);
            BtnExit.Name = "BtnExit";
            BtnExit.Size = new Size(94, 59);
            BtnExit.TabIndex = 13;
            BtnExit.Text = "Exit";
            BtnExit.UseVisualStyleBackColor = true;
            BtnExit.Click += BtnExit_Click;
            // 
            // btnBit
            // 
            btnBit.Location = new Point(12, 344);
            btnBit.Name = "btnBit";
            btnBit.Size = new Size(94, 59);
            btnBit.TabIndex = 11;
            btnBit.Text = "Recargar Bitacora";
            btnBit.UseVisualStyleBackColor = true;
            btnBit.Click += btnBit_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 305);
            label3.Name = "label3";
            label3.Size = new Size(62, 20);
            label3.TabIndex = 10;
            label3.Text = "Usuario:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 263);
            label2.Name = "label2";
            label2.Size = new Size(27, 20);
            label2.TabIndex = 9;
            label2.Text = "ID:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 217);
            label1.Name = "label1";
            label1.Size = new Size(373, 20);
            label1.TabIndex = 8;
            label1.Text = "Información del Usuario que realizo el Backup/Restore:";
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
            dataGridView1.Size = new Size(391, 188);
            dataGridView1.TabIndex = 7;
            // 
            // radBackups
            // 
            radBackups.AutoSize = true;
            radBackups.Location = new Point(134, 344);
            radBackups.Name = "radBackups";
            radBackups.Size = new Size(87, 24);
            radBackups.TabIndex = 14;
            radBackups.TabStop = true;
            radBackups.Text = "Backups.";
            radBackups.UseVisualStyleBackColor = true;
            radBackups.CheckedChanged += radBackups_CheckedChanged;
            // 
            // radRestore
            // 
            radRestore.AutoSize = true;
            radRestore.Location = new Point(134, 379);
            radRestore.Name = "radRestore";
            radRestore.Size = new Size(83, 24);
            radRestore.TabIndex = 15;
            radRestore.TabStop = true;
            radRestore.Text = "Restore.";
            radRestore.UseVisualStyleBackColor = true;
            radRestore.CheckedChanged += radRestore_CheckedChanged;
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
            // Bitacora
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(424, 420);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(radRestore);
            Controls.Add(radBackups);
            Controls.Add(BtnExit);
            Controls.Add(btnBit);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Name = "Bitacora";
            Text = "Bitacora";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnExit;
        private Button btnBit;
        private Label label3;
        private Label label2;
        private Label label1;
        private DataGridView dataGridView1;
        private RadioButton radBackups;
        private RadioButton radRestore;
        private TextBox textBox2;
        private TextBox textBox1;
    }
}