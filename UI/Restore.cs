using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BLL;

namespace UI
{
    public partial class Restore : Form
    {
        public Restore()
        {
            InitializeComponent();
        }
        private void Restore_Load(object sender, EventArgs e)
        {
            BLLBackup.Instance.DatosRestaurados -= OnDatosRestaurados;
            BLLBackup.Instance.DatosRestaurados += OnDatosRestaurados;
        }
        private void OnDatosRestaurados()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(OnDatosRestaurados));
                return;
            }

            MessageBox.Show("Restauración completada. La aplicación se reiniciará para reflejar los datos.",
                "Restore", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Restart();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            label1.Text = "Backups Disponibles Para realizar Restore:";
            CargarSnapshots();
            CargarUsuarioActual();
        }

        private void CargarUsuarioActual()
        {
            var u = BLLUsuario.Instance.CurrentUser;
            textBox1.Text = u?.Id.ToString() ?? "0";
            textBox2.Text = u?.Nombre ?? "SYSTEM";
        }
        private void CargarSnapshots()
        {
            try
            {
                listBox1.Items.Clear();
                var snaps = BLLBackup.Instance.EnumerarSnapshots();
                foreach (var s in snaps)
                    listBox1.Items.Add(Path.GetFileName(s));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando backups: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnHacerRest_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un backup.", "Restore",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var fileName = listBox1.SelectedItem.ToString()!;
            var fullPath = Path.Combine(BLLBackup.Instance.BackupDirectory, fileName);
            try
            {
                BLLBackup.Instance.RestaurarDesdeSnapshot(fullPath, crearBackupPrevio: true);
                MessageBox.Show("Restore completado correctamente.", "Restore",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fallo al restaurar:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnLimpiarRest_Click(object sender, EventArgs e)
        {
            listBox1.ClearSelected();
            textBox1.Clear();
            textBox2.Clear();
        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            CargarSnapshots();
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
