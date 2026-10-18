using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BLL;
using BE;

namespace UI
{
    public partial class AsignarPersonal : Form
    {
        private readonly AsignacionPersonalBLL _asigBll = new AsignacionPersonalBLL();
        private readonly ServicioBLL _servicioBll = new ServicioBLL();
        private bool _suppressDialogs = false;
        private string _pendingInitialMessage = null;

        public AsignarPersonal()
        {
            InitializeComponent();
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void AsignarPersonal_Load(object sender, EventArgs e)
        {
            CargarServicios();
        }

        private void CargarServicios()
        {
            var servicios = _asigBll.ListarServiciosQueRequierenPersonal();

            var lista = servicios
                .Select(s =>
                {
                    var asignados = _asigBll.ContarAsignados(s.IdServicio);
                    var requeridos = s.PersonalNecesario;
                    var restantes = Math.Max(0, requeridos - asignados);
                    return new
                    {
                        s.IdServicio,
                        s.IdContrato,
                        TipoServicio = s.TipoServicio,
                        Inicio = s.Fecha_hora_inicio,
                        Fin = s.Fecha_hora_fin,
                        Requeridos = requeridos,
                        Asignados = asignados,
                        Restantes = restantes
                    };
                })
                .Where(x => x.Asignados == 0 || x.Restantes > 0)
                .OrderBy(x => x.Inicio)
                .ToList();
            _suppressDialogs = true;
            _pendingInitialMessage = null;
            dataGridView1.SelectionChanged -= DataGridView1_SelectionChanged;
            dataGridView1.DataSource = null;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = lista;
            dataGridView2.DataSource = null;

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.ClearSelection();
                var row0 = dataGridView1.Rows[0];
                row0.Selected = true;
                dataGridView1.CurrentCell = row0.Cells[0];
                OnServicioSeleccionChanged();
            }
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
            _suppressDialogs = false;
            if (!string.IsNullOrEmpty(_pendingInitialMessage))
            {
                MessageBox.Show(_pendingInitialMessage, "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _pendingInitialMessage = null;
            }
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            OnServicioSeleccionChanged();
        }

        private void OnServicioSeleccionChanged()
        {
            var sel = GetServicioSeleccionado();
            if (sel == null)
            {
                dataGridView2.DataSource = null;
                return;
            }

            CargarPersonalParaServicio(sel.Value.TipoServicio);
        }

        private void CargarPersonalParaServicio(string tipoServicio)
        {
            var candidatos = _asigBll.ListarDisponiblesPorEspecialidad(tipoServicio);

            if (candidatos.Count == 0)
            {
                var recomendados = _asigBll.ListarRecomendadosPorHabilitaciones(tipoServicio);
                if (recomendados.Count == 0)
                {
                    var msg = "No hay personal disponible con la especialidad requerida ni habilitado como alternativa.";
                    if (!_suppressDialogs)
                        MessageBox.Show(msg, "Sin candidatos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else if (string.IsNullOrEmpty(_pendingInitialMessage))
                        _pendingInitialMessage = msg;
                }
                else
                {
                    var msg = "No hay personal con la especialidad requerida. Se muestran alternativas por habilitaciones.";
                    if (!_suppressDialogs)
                        MessageBox.Show(msg, "Recomendaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else if (string.IsNullOrEmpty(_pendingInitialMessage))
                        _pendingInitialMessage = msg;
                }
                candidatos = recomendados;
            }

            var datos = candidatos.Select(p => new
            {
                p.IdPersonal,
                p.Nombre,
                p.DNI,
                Especialidad = p.Especialidad,
                Habilitaciones = p.Habilitaciones,
                Disponible = p.Estado_Disponibilidad
            }).ToList();

            dataGridView2.DataSource = null;
            dataGridView2.AutoGenerateColumns = true;
            dataGridView2.DataSource = datos;
        }

        private (int IdServicio, string TipoServicio)? GetServicioSeleccionado()
        {
            if (dataGridView1.CurrentRow?.DataBoundItem is null) return null;

            dynamic row = dataGridView1.CurrentRow.DataBoundItem;
            int idServicio = row.IdServicio;
            string tipo = row.TipoServicio;
            return (idServicio, tipo);
        }

        private int? GetPersonalSeleccionadoId()
        {
            if (dataGridView2.CurrentRow?.DataBoundItem is null) return null;

            dynamic row = dataGridView2.CurrentRow.DataBoundItem;
            int idPersonal = row.IdPersonal;
            return idPersonal;
        }

        private void btnAsignarServicio_Click_1(object sender, EventArgs e)
        {
            var servicioSel = GetServicioSeleccionado();
            if (servicioSel == null)
            {
                MessageBox.Show("Seleccione un servicio.", "Asignar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var idPersonal = GetPersonalSeleccionadoId();
            if (idPersonal == null)
            {
                MessageBox.Show("Seleccione una persona disponible.", "Asignar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                _asigBll.Asignar(servicioSel.Value.IdServicio, idPersonal.Value);
                MessageBox.Show("Personal asignado exitosamente.", "Asignar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarServicios();
                ReseleccionarServicio(servicioSel.Value.IdServicio);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al asignar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReseleccionarServicio(int idServicio)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                dynamic item = row.DataBoundItem;
                if (item.IdServicio == idServicio)
                {
                    row.Selected = true;
                    dataGridView1.CurrentCell = row.Cells[0];
                    _suppressDialogs = true;
                    OnServicioSeleccionChanged();
                    _suppressDialogs = false;
                    return;
                }
            }
            dataGridView2.DataSource = null;
        }
    }
}
