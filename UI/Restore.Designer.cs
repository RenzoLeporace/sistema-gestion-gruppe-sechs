namespace UI
{
    partial class Restore
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
            btnHacerRest = new Button();
            btnRecargar = new Button();
            BtnExit = new Button();
            label1 = new Label();
            listBox1 = new ListBox();
            label2 = new Label();
            label3 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            BtnLimpiarRest = new Button();
            SuspendLayout();
            // 
            // btnHacerRest
            // 
            btnHacerRest.Location = new Point(12, 245);
            btnHacerRest.Name = "btnHacerRest";
            btnHacerRest.Size = new Size(110, 50);
            btnHacerRest.TabIndex = 2;
            btnHacerRest.Text = "Realizar Restore";
            btnHacerRest.UseVisualStyleBackColor = true;
            btnHacerRest.Click += btnHacerRest_Click;
            // 
            // btnRecargar
            // 
            btnRecargar.Location = new Point(134, 245);
            btnRecargar.Name = "btnRecargar";
            btnRecargar.Size = new Size(110, 50);
            btnRecargar.TabIndex = 3;
            btnRecargar.Text = "Recargar Datos";
            btnRecargar.UseVisualStyleBackColor = true;
            btnRecargar.Click += btnRecargar_Click;
            // 
            // BtnExit
            // 
            BtnExit.Location = new Point(252, 245);
            BtnExit.Name = "BtnExit";
            BtnExit.Size = new Size(110, 50);
            BtnExit.TabIndex = 4;
            BtnExit.Text = "Exit";
            BtnExit.UseVisualStyleBackColor = true;
            BtnExit.Click += BtnExit_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(287, 20);
            label1.TabIndex = 0;
            label1.Text = "Backups Disponibles Para realizar Restore:";
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(12, 32);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(350, 204);
            listBox1.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 310);
            label2.Name = "label2";
            label2.Size = new Size(27, 20);
            label2.TabIndex = 5;
            label2.Text = "ID:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 350);
            label3.Name = "label3";
            label3.Size = new Size(62, 20);
            label3.TabIndex = 6;
            label3.Text = "Usuario:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(45, 307);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(317, 27);
            textBox1.TabIndex = 7;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(80, 347);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(282, 27);
            textBox2.TabIndex = 8;
            // 
            // BtnLimpiarRest
            // 
            BtnLimpiarRest.Location = new Point(12, 384);
            BtnLimpiarRest.Name = "BtnLimpiarRest";
            BtnLimpiarRest.Size = new Size(110, 50);
            BtnLimpiarRest.TabIndex = 9;
            BtnLimpiarRest.Text = "Limpiar Campos";
            BtnLimpiarRest.UseVisualStyleBackColor = true;
            BtnLimpiarRest.Click += BtnLimpiarRest_Click;
            // 
            // Restore
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(374, 448);
            Controls.Add(BtnLimpiarRest);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(BtnExit);
            Controls.Add(btnRecargar);
            Controls.Add(btnHacerRest);
            Controls.Add(listBox1);
            Controls.Add(label1);
            Name = "Restore";
            Text = "Restore";
            Load += Restore_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnHacerRest;
        private Button btnRecargar;
        private Button BtnExit;
        private Label label1;
        private ListBox listBox1;
        private Label label2;
        private Label label3;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button BtnLimpiarRest;
    }
}