using System;
using System.Linq;
using System.Windows.Forms;
using BLL;
using Mapper;

namespace UI
{
    public partial class HistorialServicios : Form
    {
        private readonly ClienteBLL _clienteBll = new ClienteBLL();
        private readonly ContratoBLL _contratoBll = new ContratoBLL();
        private readonly ServicioBLL _servicioBll = new ServicioBLL();
        private readonly AsignacionPersonalMpr _asigPersMpr = new AsignacionPersonalMpr();
        private readonly AsignacionVehiucloMpr _asigVehMpr = new AsignacionVehiucloMpr();
        private readonly InformeOperativoMpr _informeMpr = new InformeOperativoMpr();
        private readonly FacturaMpr _facturaMpr = new FacturaMpr();

        public HistorialServicios()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.ReadOnly = true;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.AcceptButton = btnConsHisServicios;
        }

        private void Consultar()
        {
            var criterio = (txtIdServicioCons.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(criterio))
            {
                MessageBox.Show("Ingrese DNI/CUIT o nombre del cliente.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var cliente = _clienteBll.BuscarPorDni(criterio);
            if (cliente == null)
            {
                var candidatos = _clienteBll.Listar()
                    .Where(c => !string.IsNullOrWhiteSpace(c.Nombre) &&
                                c.Nombre.Contains(criterio, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                if (candidatos.Count == 1)
                    cliente = candidatos.First();
                else if (candidatos.Count > 1)
                {
                    var nombres = string.Join(Environment.NewLine, candidatos.Select(c => $"- {c.Nombre} ({c.DNI_CUIL})"));
                    MessageBox.Show($"Se encontraron múltiples clientes, refine la búsqueda:\n{nombres}",
                        "Múltiples coincidencias", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            if (cliente == null)
            {
                MessageBox.Show("Cliente no encontrado. Intente nuevamente.", "Sin resultados",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = null;
                return;
            }

            var contratos = _contratoBll.Listar()
                .Where(c => string.Equals(c.DNI_CUIL_Cliente, cliente.DNI_CUIL, StringComparison.OrdinalIgnoreCase))
                .ToList();
            if (contratos.Count == 0)
            {
                dataGridView1.DataSource = null;
                MessageBox.Show("El cliente no posee contratos registrados.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var idsContrato = contratos.Select(c => c.IdContrato).ToHashSet();
            var servicios = _servicioBll.Listar()
                .Where(s => idsContrato.Contains(s.IdContrato))
                .OrderByDescending(s => s.Fecha_hora_inicio)
                .ToList();
            if (servicios.Count == 0)
            {
                dataGridView1.DataSource = null;
                MessageBox.Show("El cliente no posee servicios asociados a sus contratos.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var informesDict = _informeMpr.GetAll()
                .ToDictionary(i => i.IdInforme, i => i);
            var facturasPorContrato = _facturaMpr.GetAll()
                .GroupBy(f => f.IdContrato)
                .ToDictionary(g => g.Key, g => g.ToList());

            var vista = servicios.Select(s =>
            {
                var contrato = contratos.First(c => c.IdContrato == s.IdContrato);
                var persAsign = _asigPersMpr.GetByServicio(s.IdServicio).Count;
                var vehAsign = _asigVehMpr.GetByServicio(s.IdServicio).Count;

                string estadoCalc =
                    DateTime.Now < s.Fecha_hora_inicio ? "Programado" :
                    DateTime.Now > s.Fecha_hora_fin ? "Finalizado" :
                    "En ejecución";

                informesDict.TryGetValue(s.IdInformeOperativo.GetValueOrDefault(), out var informe);
                facturasPorContrato.TryGetValue(s.IdContrato, out var facturas);

                return new
                {
                    s.IdServicio,
                    s.IdContrato,
                    Tipo = s.TipoServicio,
                    Inicio = s.Fecha_hora_inicio,
                    Fin = s.Fecha_hora_fin,
                    Estado = estadoCalc,
                    Personal = $"{persAsign}/{s.PersonalNecesario}",
                    Vehiculos = $"{vehAsign}/{s.VehiculosNecesarios}",
                    CostoEstimadoContrato = contrato.Costo_Estimado,
                    FacturasContrato = facturas == null ? "Sin factura" : string.Join(", ", facturas.Select(f => $"#{f.IdFactura}")),
                    TotalFacturadoContrato = facturas?.Sum(f => f.Total) ?? 0,
                    SaldoPendienteContrato = facturas?.Sum(f => f.Saldo_Pendiente) ?? 0,
                    TieneInforme = informe != null ? "Sí" : "No",
                    IdInforme = informe?.IdInforme,
                    Incidencias = informe?.Incidencias,
                    ObservacionesInforme = informe?.Observaciones,
                    ResultadoInforme = informe?.Resultado,
                    FechaInforme = informe?.FechaCreacion
                };
            }).ToList();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = vista;
        }

        private void btnConsHisServicios_Click(object sender, EventArgs e)
        {
            Consultar();
        }
    }
}
