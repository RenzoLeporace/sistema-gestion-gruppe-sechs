using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace UI
{
    partial class DashBoard
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView gridResumen;
        private Chart chartEcoBarras;
        private Chart chartDeuda;
        private Chart chartClientes;
        private Chart chartRecursos;
        private Button btnRefrescar;

        private DateTimePicker dtpDesde;
        private DateTimePicker dtpHasta;
        private Button btnAplicarFechas;
        private Label lblDesde;
        private Label lblHasta;
        private Button btnDia;
        private Button btnSemana;
        private Button btnMes;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            gridResumen = new DataGridView();
            btnRefrescar = new Button();
            dtpDesde = new DateTimePicker();
            dtpHasta = new DateTimePicker();
            btnAplicarFechas = new Button();
            lblDesde = new Label();
            lblHasta = new Label();
            btnDia = new Button();
            btnSemana = new Button();
            btnMes = new Button();
            chartEcoBarras = new Chart();
            chartDeuda = new Chart();
            chartClientes = new Chart();
            chartRecursos = new Chart();
            ((System.ComponentModel.ISupportInitialize)gridResumen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartEcoBarras).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartDeuda).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartClientes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartRecursos).BeginInit();
            SuspendLayout();
            // 
            // gridResumen
            // 
            gridResumen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gridResumen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridResumen.ColumnHeadersHeight = 29;
            gridResumen.Location = new Point(12, 70);
            gridResumen.Name = "gridResumen";
            gridResumen.ReadOnly = true;
            gridResumen.RowHeadersWidth = 51;
            gridResumen.Size = new Size(980, 100);
            gridResumen.TabIndex = 0;
            // 
            // chartEcoBarras
            // 
            chartEcoBarras.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            chartEcoBarras.Location = new Point(12, 180);
            chartEcoBarras.Name = "chartEcoBarras";
            chartEcoBarras.Size = new Size(480, 200);
            chartEcoBarras.TabIndex = 20;
            chartEcoBarras.Text = "chartEcoBarras";
            // 
            // chartDeuda
            // 
            chartDeuda.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            chartDeuda.Location = new Point(512, 180);
            chartDeuda.Name = "chartDeuda";
            chartDeuda.Size = new Size(480, 200);
            chartDeuda.TabIndex = 21;
            chartDeuda.Text = "chartDeuda";
            // 
            // chartClientes
            // 
            chartClientes.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            chartClientes.Location = new Point(12, 390);
            chartClientes.Name = "chartClientes";
            chartClientes.Size = new Size(480, 200);
            chartClientes.TabIndex = 22;
            chartClientes.Text = "chartClientes";
            // 
            // chartRecursos
            // 
            chartRecursos.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            chartRecursos.Location = new Point(512, 390);
            chartRecursos.Name = "chartRecursos";
            chartRecursos.Size = new Size(480, 200);
            chartRecursos.TabIndex = 23;
            chartRecursos.Text = "chartRecursos";
            // 
            // btnRefrescar
            // 
            btnRefrescar.Location = new Point(900, 29);
            btnRefrescar.Name = "btnRefrescar";
            btnRefrescar.Size = new Size(92, 32);
            btnRefrescar.TabIndex = 5;
            btnRefrescar.Text = "Refrescar";
            btnRefrescar.UseVisualStyleBackColor = true;
            btnRefrescar.Click += btnRefrescar_Click;
            // 
            // dtpDesde
            // 
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(77, 5);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(110, 27);
            dtpDesde.TabIndex = 6;
            // 
            // dtpHasta
            // 
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(263, 5);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(110, 27);
            dtpHasta.TabIndex = 7;
            // 
            // btnAplicarFechas
            // 
            btnAplicarFechas.Location = new Point(423, 32);
            btnAplicarFechas.Name = "btnAplicarFechas";
            btnAplicarFechas.Size = new Size(85, 27);
            btnAplicarFechas.TabIndex = 8;
            btnAplicarFechas.Text = "Aplicar";
            btnAplicarFechas.UseVisualStyleBackColor = true;
            btnAplicarFechas.Click += btnAplicarFechas_Click;
            // 
            // lblDesde
            // 
            lblDesde.Location = new Point(12, 10);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(59, 24);
            lblDesde.TabIndex = 13;
            lblDesde.Text = "Desde:";
            // 
            // lblHasta
            // 
            lblHasta.Location = new Point(205, 10);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(63, 24);
            lblHasta.TabIndex = 12;
            lblHasta.Text = "Hasta:";
            // 
            // btnDia
            // 
            btnDia.Location = new Point(576, 32);
            btnDia.Name = "btnDia";
            btnDia.Size = new Size(70, 27);
            btnDia.TabIndex = 9;
            btnDia.Text = "Hoy";
            btnDia.UseVisualStyleBackColor = true;
            btnDia.Click += btnDia_Click;
            // 
            // btnSemana
            // 
            btnSemana.Location = new Point(652, 32);
            btnSemana.Name = "btnSemana";
            btnSemana.Size = new Size(75, 27);
            btnSemana.TabIndex = 10;
            btnSemana.Text = "Semana";
            btnSemana.UseVisualStyleBackColor = true;
            btnSemana.Click += btnSemana_Click;
            // 
            // btnMes
            // 
            btnMes.Location = new Point(733, 32);
            btnMes.Name = "btnMes";
            btnMes.Size = new Size(70, 27);
            btnMes.TabIndex = 11;
            btnMes.Text = "Mes";
            btnMes.UseVisualStyleBackColor = true;
            btnMes.Click += btnMes_Click;
            // 
            // DashBoard
            // 
            ClientSize = new Size(1004, 641);
            Controls.Add(chartRecursos);
            Controls.Add(chartClientes);
            Controls.Add(chartDeuda);
            Controls.Add(chartEcoBarras);
            Controls.Add(btnMes);
            Controls.Add(btnSemana);
            Controls.Add(btnDia);
            Controls.Add(btnAplicarFechas);
            Controls.Add(dtpHasta);
            Controls.Add(lblHasta);
            Controls.Add(dtpDesde);
            Controls.Add(lblDesde);
            Controls.Add(btnRefrescar);
            Controls.Add(gridResumen);
            Name = "DashBoard";
            Text = "Dashboard";
            Load += DashBoard_Load;
            ((System.ComponentModel.ISupportInitialize)gridResumen).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartEcoBarras).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartDeuda).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartClientes).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartRecursos).EndInit();
            ResumeLayout(false);
        }
    }
}