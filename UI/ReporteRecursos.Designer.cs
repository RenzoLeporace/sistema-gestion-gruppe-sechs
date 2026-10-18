using System.Windows.Forms.DataVisualization.Charting;

namespace UI
{
    partial class ReporteRecursos
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
            btnReporteRecursos = new Button();
            gridRecursos = new DataGridView();
            chartHoras = new Chart();
            chartDistribucion = new Chart();
            chartRentabilidad = new Chart();
            lblDesde = new Label();
            lblHasta = new Label();
            dtpDesde = new DateTimePicker();
            dtpHasta = new DateTimePicker();
            btnAplicarFechas = new Button();
            btnDia = new Button();
            btnSemana = new Button();
            btnMes = new Button();
            ((System.ComponentModel.ISupportInitialize)gridRecursos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartHoras).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartDistribucion).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartRentabilidad).BeginInit();
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
            // btnReporteRecursos
            // 
            btnReporteRecursos.Location = new Point(650, 10);
            btnReporteRecursos.Name = "btnReporteRecursos";
            btnReporteRecursos.Size = new Size(230, 32);
            btnReporteRecursos.TabIndex = 0;
            btnReporteRecursos.Text = "Refrescar";
            btnReporteRecursos.UseVisualStyleBackColor = true;
            btnReporteRecursos.Click += btnReporteRecursos_Click;
            // 
            // gridRecursos
            // 
            gridRecursos.AllowUserToAddRows = false;
            gridRecursos.AllowUserToDeleteRows = false;
            gridRecursos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridRecursos.ColumnHeadersHeight = 29;
            gridRecursos.Location = new Point(20, 50);
            gridRecursos.Name = "gridRecursos";
            gridRecursos.ReadOnly = true;
            gridRecursos.RowHeadersWidth = 51;
            gridRecursos.Size = new Size(1140, 250);
            gridRecursos.TabIndex = 1;
            // 
            // chartHoras
            // 
            chartHoras.Location = new Point(20, 310);
            chartHoras.Name = "chartHoras";
            chartHoras.Size = new Size(360, 270);
            chartHoras.TabIndex = 2;
            // 
            // chartDistribucion
            // 
            chartDistribucion.Location = new Point(410, 310);
            chartDistribucion.Name = "chartDistribucion";
            chartDistribucion.Size = new Size(360, 270);
            chartDistribucion.TabIndex = 3;
            // 
            // chartRentabilidad
            // 
            chartRentabilidad.Location = new Point(800, 310);
            chartRentabilidad.Name = "chartRentabilidad";
            chartRentabilidad.Size = new Size(360, 270);
            chartRentabilidad.TabIndex = 4;
            // 
            // ReporteRecursos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 611);
            Controls.Add(btnMes);
            Controls.Add(btnSemana);
            Controls.Add(btnDia);
            Controls.Add(btnAplicarFechas);
            Controls.Add(dtpHasta);
            Controls.Add(lblHasta);
            Controls.Add(dtpDesde);
            Controls.Add(lblDesde);
            Controls.Add(chartRentabilidad);
            Controls.Add(chartDistribucion);
            Controls.Add(chartHoras);
            Controls.Add(gridRecursos);
            Controls.Add(btnReporteRecursos);
            Name = "ReporteRecursos";
            Text = "Reporte de Recursos";
            Load += ReporteRecursos_Load;
            ((System.ComponentModel.ISupportInitialize)gridRecursos).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartHoras).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartDistribucion).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartRentabilidad).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnReporteRecursos;
        private System.Windows.Forms.DataGridView gridRecursos;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartHoras;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDistribucion;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRentabilidad;
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