using System;
using System.Linq;
using System.Windows.Forms;
using BLL;
using BE;

namespace UI
{
    public partial class Backup : Form
    {
        public Backup()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            label1.Text = "Historial Backups:";
            CargarHistorialBackups();
            CargarUsuarioActual();
        }
        private void CargarUsuarioActual()
        {
            var u = BLLUsuario.Instance.CurrentUser;
            textBox1.Text = u?.Id.ToString() ?? "0";
            textBox2.Text = u?.Nombre ?? "SYSTEM";
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
        private void CargarHistorialBackups()
        {
            try
            {
                var backups = BLLBitacora.Instance.ObtenerTodos()
                    .Where(b => b.Tipo == BitacoraTipo.Backup)
                    .Select(b => new
                    {
                        b.Fecha,
                        Detalle = b.Detalle,
                        b.UsuarioId,
                        b.Id
                    })
                    .ToList();
                ConfigurarColumnas();
                dataGridView1.DataSource = backups;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando historial: {ex.Message}", "Error",
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
        private void btnHacerBack_Click(object sender, EventArgs e)
        {
            try
            {
                var path = BLLBackup.Instance.CrearSnapshotBackup();
                MessageBox.Show($"Backup creado:\n{path}", "Backup",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarHistorialBackups();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fallo al crear backup:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnLimpiarBack_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            dataGridView1.ClearSelection();
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
