using System.Windows.Forms.DataVisualization.Charting;

namespace UI
{
    partial class ReporteCliente
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
            btnReporteClientes = new Button();
            gridResumen = new DataGridView();
            panel1 = new Panel();
            chartFacturacion = new Chart();
            chartServicios = new Chart();
            chartPagos = new Chart();
            lblDesde = new Label();
            lblHasta = new Label();
            dtpDesde = new DateTimePicker();
            dtpHasta = new DateTimePicker();
            btnAplicarFechas = new Button();
            btnDia = new Button();
            btnSemana = new Button();
            btnMes = new Button();
            ((System.ComponentModel.ISupportInitialize)gridResumen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartFacturacion).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartServicios).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartPagos).BeginInit();
            SuspendLayout();
            // 
            // lblDesde
            // 
            lblDesde.Location = new Point(20, 15);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(50, 23);
            lblDesde.Text = "Desde:";
            // 
            // dtpDesde
            // 
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(75, 12);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(105, 27);
            // 
            // lblHasta
            // 
            lblHasta.Location = new Point(190, 15);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(50, 23);
            lblHasta.Text = "Hasta:";
            // 
            // dtpHasta
            // 
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(245, 12);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(105, 27);
            // 
            // btnAplicarFechas
            // 
            btnAplicarFechas.Location = new Point(355, 12);
            btnAplicarFechas.Name = "btnAplicarFechas";
            btnAplicarFechas.Size = new Size(65, 27);
            btnAplicarFechas.Text = "Aplicar";
            btnAplicarFechas.UseVisualStyleBackColor = true;
            btnAplicarFechas.Click += btnAplicarFechas_Click;
            // 
            // btnDia
            // 
            btnDia.Location = new Point(425, 12);
            btnDia.Name = "btnDia";
            btnDia.Size = new Size(50, 27);
            btnDia.Text = "Hoy";
            btnDia.UseVisualStyleBackColor = true;
            btnDia.Click += btnDia_Click;
            // 
            // btnSemana
            // 
            btnSemana.Location = new Point(480, 12);
            btnSemana.Name = "btnSemana";
            btnSemana.Size = new Size(75, 27);
            btnSemana.Text = "Semana";
            btnSemana.UseVisualStyleBackColor = true;
            btnSemana.Click += btnSemana_Click;
            // 
            // btnMes
            // 
            btnMes.Location = new Point(560, 12);
            btnMes.Name = "btnMes";
            btnMes.Size = new Size(55, 27);
            btnMes.Text = "Mes";
            btnMes.UseVisualStyleBackColor = true;
            btnMes.Click += btnMes_Click;
            // 
            // btnReporteClientes
            // 
            btnReporteClientes.Location = new Point(625, 10);
            btnReporteClientes.Name = "btnReporteClientes";
            btnReporteClientes.Size = new Size(220, 32);
            btnReporteClientes.TabIndex = 0;
            btnReporteClientes.Text = "Refrescar";
            btnReporteClientes.UseVisualStyleBackColor = true;
            btnReporteClientes.Click += btnReporteClientes_Click;
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
            gridResumen.Size = new Size(1140, 168);
            gridResumen.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.Location = new Point(20, 230);
            panel1.Name = "panel1";
            panel1.Size = new Size(1140, 250);
            panel1.TabIndex = 2;
            // 
            // chartFacturacion
            // 
            chartFacturacion.Location = new Point(0, 0);
            chartFacturacion.Name = "chartFacturacion";
            chartFacturacion.Size = new Size(360, 250);
            chartFacturacion.TabIndex = 3;
            // 
            // chartServicios
            // 
            chartServicios.Location = new Point(380, 0);
            chartServicios.Name = "chartServicios";
            chartServicios.Size = new Size(360, 250);
            chartServicios.TabIndex = 4;
            // 
            // chartPagos
            // 
            chartPagos.Location = new Point(760, 0);
            chartPagos.Name = "chartPagos";
            chartPagos.Size = new Size(360, 250);
            chartPagos.TabIndex = 5;
            // 
            // panel charts
            // 
            panel1.Controls.Add(chartFacturacion);
            panel1.Controls.Add(chartServicios);
            panel1.Controls.Add(chartPagos);
            // 
            // ReporteCliente
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 495);
            Controls.Add(btnMes);
            Controls.Add(btnSemana);
            Controls.Add(btnDia);
            Controls.Add(btnAplicarFechas);
            Controls.Add(dtpHasta);
            Controls.Add(lblHasta);
            Controls.Add(dtpDesde);
            Controls.Add(lblDesde);
            Controls.Add(panel1);
            Controls.Add(gridResumen);
            Controls.Add(btnReporteClientes);
            Name = "ReporteCliente";
            Text = "Reporte de Clientes";
            Load += ReporteCliente_Load;
            ((System.ComponentModel.ISupportInitialize)gridResumen).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartFacturacion).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartServicios).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartPagos).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnReporteClientes;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartFacturacion;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartServicios;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPagos;
        private System.Windows.Forms.DataGridView gridResumen;
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