using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BLL;
using BE;

namespace UI
{
    public partial class GenerarInformeOperativo : Form
    {
        private readonly InformeOperativoBLL _informeBll = new InformeOperativoBLL();
        private List<ServicioBE> _fuente = new();
        private ServicioBE? _seleccionado;
        private bool _suppressDialogs = false;
        private string? _pendingInitialMessage = null;

        public GenerarInformeOperativo()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.ReadOnly = true;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
            dataGridView2.AutoGenerateColumns = true;
            dataGridView2.ReadOnly = true;
            dataGridView2.MultiSelect = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void DataGridView1_SelectionChanged(object? sender, EventArgs e)
        {
            OnSeleccionChanged();
        }

        private void GenerarInformeOperativo_Load(object sender, EventArgs e)
        {
            CargarServicios();
        }
        private void CargarServicios()
        {
            _fuente = _informeBll.ListarFinalizadosSinInforme();

            var vista = _fuente.Select(s => new
            {
                s.IdServicio,
                s.IdContrato,
                s.TipoServicio,
                Inicio = s.Fecha_hora_inicio,
                Fin = s.Fecha_hora_fin,
                s.PersonalNecesario,
                s.VehiculosNecesarios
            }).ToList();

            _seleccionado = null;
            dataGridView2.DataSource = null;
            btnGInforme.Enabled = false;
            _suppressDialogs = true;
            _pendingInitialMessage = null;
            dataGridView1.SelectionChanged -= DataGridView1_SelectionChanged;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = vista;

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.ClearSelection();
                var firstRow = dataGridView1.Rows[0];
                firstRow.Selected = true;
                dataGridView1.CurrentCell = firstRow.Cells[0];
                OnSeleccionChanged();
            }
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
            _suppressDialogs = false;
            if (!string.IsNullOrEmpty(_pendingInitialMessage))
            {
                MessageBox.Show(_pendingInitialMessage, "Información incompleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _pendingInitialMessage = null;
            }
        }
        private void OnSeleccionChanged()
        {
            var id = GetIdServicioSeleccionado();
            if (id == null)
            {
                _seleccionado = null;
                dataGridView2.DataSource = null;
                btnGInforme.Enabled = false;
                return;
            }
            _seleccionado = _fuente.FirstOrDefault(x => x.IdServicio == id.Value);
            if (_seleccionado == null)
            {
                dataGridView2.DataSource = null;
                btnGInforme.Enabled = false;
                return;
            }
            var detalle = new[]
            {
                new
                {
                    _seleccionado.IdServicio,
                    _seleccionado.IdContrato,
                    _seleccionado.TipoServicio,
                    Inicio = _seleccionado.Fecha_hora_inicio,
                    Fin = _seleccionado.Fecha_hora_fin,
                    _seleccionado.PersonalNecesario,
                    _seleccionado.VehiculosNecesarios
                }
            };
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = detalle;
            if (!_informeBll.ValidarServicioListoParaInforme(_seleccionado, out var motivo))
            {
                btnGInforme.Enabled = false;
                var mensaje = $"No es posible generar el informe.\n{motivo}\n\nCancele y complete la información faltante.";
                if (_suppressDialogs)
                {
                    if (string.IsNullOrEmpty(_pendingInitialMessage))
                        _pendingInitialMessage = mensaje;
                }
                else
                {
                    MessageBox.Show(mensaje, "Información incompleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                btnGInforme.Enabled = true;
            }
        }
        private int? GetIdServicioSeleccionado()
        {
            if (dataGridView1.CurrentRow?.DataBoundItem == null) return null;
            dynamic row = dataGridView1.CurrentRow.DataBoundItem;
            try
            {
                int id = row.IdServicio;
                return id;
            }
            catch
            {
                return null;
            }
        }
        private void Generar()
        {
            if (_seleccionado == null)
            {
                MessageBox.Show("Seleccione un servicio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var incidencias = textBox2.Text;
            var resultados = textBox1.Text;
            var observaciones = textBox3.Text;
            try
            {
                var informe = _informeBll.GenerarInforme(_seleccionado.IdServicio, incidencias, resultados, observaciones);

                MessageBox.Show($"Informe generado (ID: {informe.IdInforme}) y asociado al servicio {informe.IdServicio}.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear(); textBox2.Clear(); textBox3.Clear();
                CargarServicios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo generar el informe", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnGInforme_Click(object sender, EventArgs e)
        {
            Generar();
        }
    }
}
