using System.Windows.Forms.DataVisualization.Charting;

namespace UI
{
    partial class ReporteEconomico
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            btnGenerar = new Button();
            gridResumen = new DataGridView();
            chartIngresosGastos = new Chart();
            chartDeuda = new Chart();
            lblDesde = new Label();
            lblHasta = new Label();
            dtpDesde = new DateTimePicker();
            dtpHasta = new DateTimePicker();
            btnAplicarFechas = new Button();
            btnDia = new Button();
            btnSemana = new Button();
            btnMes = new Button();
            ((System.ComponentModel.ISupportInitialize)gridResumen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartIngresosGastos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartDeuda).BeginInit();
            SuspendLayout();
            // 
            // lblDesde
            // 
            lblDesde.Location = new Point(20, 15);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(55, 23);
            lblDesde.Text = "Desde:";
            // 
            // dtpDesde
            // 
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(75, 12);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(110, 27);
            // 
            // lblHasta
            // 
            lblHasta.Location = new Point(195, 15);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(50, 23);
            lblHasta.Text = "Hasta:";
            // 
            // dtpHasta
            // 
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(245, 12);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(110, 27);
            // 
            // btnAplicarFechas
            // 
            btnAplicarFechas.Location = new Point(365, 12);
            btnAplicarFechas.Name = "btnAplicarFechas";
            btnAplicarFechas.Size = new Size(70, 27);
            btnAplicarFechas.Text = "Aplicar";
            btnAplicarFechas.UseVisualStyleBackColor = true;
            btnAplicarFechas.Click += btnAplicarFechas_Click;
            // 
            // btnDia
            // 
            btnDia.Location = new Point(445, 12);
            btnDia.Name = "btnDia";
            btnDia.Size = new Size(55, 27);
            btnDia.Text = "Hoy";
            btnDia.UseVisualStyleBackColor = true;
            btnDia.Click += btnDia_Click;
            // 
            // btnSemana
            // 
            btnSemana.Location = new Point(505, 12);
            btnSemana.Name = "btnSemana";
            btnSemana.Size = new Size(75, 27);
            btnSemana.Text = "Semana";
            btnSemana.UseVisualStyleBackColor = true;
            btnSemana.Click += btnSemana_Click;
            // 
            // btnMes
            // 
            btnMes.Location = new Point(585, 12);
            btnMes.Name = "btnMes";
            btnMes.Size = new Size(55, 27);
            btnMes.Text = "Mes";
            btnMes.UseVisualStyleBackColor = true;
            btnMes.Click += btnMes_Click;
            // 
            // btnGenerar
            // 
            btnGenerar.Location = new Point(655, 10);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new Size(230, 32);
            btnGenerar.TabIndex = 0;
            btnGenerar.Text = "Refrescar";
            btnGenerar.UseVisualStyleBackColor = true;
            btnGenerar.Click += btnGenerar_Click;
            // 
            // gridResumen
            // 
            gridResumen.AllowUserToAddRows = false;
            gridResumen.AllowUserToDeleteRows = false;
            gridResumen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridResumen.ColumnHeadersHeight = 29;
            gridResumen.Location = new Point(20, 50);
            gridResumen.Name = "gridResumen";
            gridResumen.ReadOnly = true;
            gridResumen.RowHeadersWidth = 51;
            gridResumen.Size = new Size(1140, 220);
            gridResumen.TabIndex = 1;
            // 
            // chartIngresosGastos
            // 
            chartIngresosGastos.Location = new Point(20, 280);
            chartIngresosGastos.Name = "chartIngresosGastos";
            chartIngresosGastos.Size = new Size(560, 320);
            chartIngresosGastos.TabIndex = 2;
            chartIngresosGastos.Text = "chartIngresosGastos";
            // 
            // chartDeuda
            // 
            chartDeuda.Location = new Point(600, 280);
            chartDeuda.Name = "chartDeuda";
            chartDeuda.Size = new Size(560, 320);
            chartDeuda.TabIndex = 3;
            chartDeuda.Text = "chartDeuda";
            // 
            // ReporteEconomico
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 621);
            Controls.Add(btnMes);
            Controls.Add(btnSemana);
            Controls.Add(btnDia);
            Controls.Add(btnAplicarFechas);
            Controls.Add(dtpHasta);
            Controls.Add(lblHasta);
            Controls.Add(dtpDesde);
            Controls.Add(lblDesde);
            Controls.Add(chartDeuda);
            Controls.Add(chartIngresosGastos);
            Controls.Add(gridResumen);
            Controls.Add(btnGenerar);
            Name = "ReporteEconomico";
            Text = "Reporte Económico";
            Load += ReporteEconomico_Load;
            ((System.ComponentModel.ISupportInitialize)gridResumen).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartIngresosGastos).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartDeuda).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.DataGridView gridResumen;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartIngresosGastos;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDeuda;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Button btnAplicarFechas;
        private System.Windows.Forms.Button btnDia;
        private System.Windows.Forms.Button btnSemana;
        private System.Windows.Forms.Button btnMes;
    }
}