using System;
using System.Linq;
using System.Windows.Forms;
using BLL;
using BE;

namespace UI
{
    public partial class Bitacora : Form
    {
        public Bitacora()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
            CargarBitacora(null);
        }

        private void ConfigurarColumnas()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Fecha",
                HeaderText = "Fecha Registro",
                Width = 120,
                DefaultCellStyle = { Format = "d/M/yyyy HH:mm" }
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Detalle",
                HeaderText = "Detalle",
                Width = 90
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UsuarioId",
                HeaderText = "Id Usuario",
                Width = 70
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "Id",
                Width = 50
            });
        }
        private void CargarBitacora(BitacoraTipo? filtro)
        {
            try
            {
                var entries = BLLBitacora.Instance.ObtenerTodos();
                if (filtro.HasValue)
                    entries = entries.Where(e => e.Tipo == filtro.Value);

                var data = entries.Select(e => new
                {
                    e.Fecha,
                    Detalle = e.Detalle,
                    e.UsuarioId,
                    e.Id
                }).ToList();

                ConfigurarColumnas();
                dataGridView1.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando bitácora: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DataGridView1_SelectionChanged(object? sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow?.DataBoundItem is null) return;
            dynamic row = dataGridView1.CurrentRow.DataBoundItem;
            textBox1.Text = Convert.ToString(row.UsuarioId);
            var u = BLLUsuario.Instance.GetUsuarios().FirstOrDefault(x => x.Id == (int)row.UsuarioId);
            textBox2.Text = u?.Nombre ?? "SYSTEM";
        }
        private void btnBit_Click(object sender, EventArgs e)
        {
            BitacoraTipo? filtro = null;
            if (radBackups.Checked) filtro = BitacoraTipo.Backup;
            else if (radRestore.Checked) filtro = BitacoraTipo.Restore;
            CargarBitacora(filtro);
        }
        private void radBackups_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                CargarBitacora(BitacoraTipo.Backup);
        }
        private void radRestore_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                CargarBitacora(BitacoraTipo.Restore);
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
