using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using BE;
using Mapper;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace BLL
{
    public class FacturaBLL
    {
        public const double TarifaBaseServicio = 300_000;
        public const double TarifaBasePersonal = 50_000;
        public const double TarifaHabilitacion = 10_000;
        public const double TarifaBlindadoNormal = 100_000;
        public const double TarifaCamionCaudales = 150_000;
        public const double PorcentajeIVA = 0.21;

        private readonly FacturaMpr _facturaMpr = new FacturaMpr();
        private readonly ContratoBLL _contratoBll = new ContratoBLL();
        private readonly ClienteBLL _clienteBll = new ClienteBLL();
        private readonly ServicioBLL _servicioBll = new ServicioBLL();
        private readonly AsignacionPersonalMpr _asigPersMpr = new AsignacionPersonalMpr();
        private readonly AsignacionVehiculoMpr _asigVehMpr = new AsignacionVehiculoMpr();
        private readonly PersonalBLL _personalBll = new PersonalBLL();
        private readonly VehiculoBLL _vehiculoBll = new VehiculoBLL();

        public class PrevisualizacionFactura
        {
            public int IdContrato { get; set; }
            public string Cliente { get; set; } = string.Empty;
            public string DNI { get; set; } = string.Empty;
            public int ServiciosFacturables { get; set; }
            public double Subtotal { get; set; }
            public double Impuestos { get; set; }
            public double Total { get; set; }
            public List<string> Notas { get; set; } = new();
        }

        public List<ContratoBE> ListarContratosPendientesDeFacturar(DateTime? ahora = null)
        {
            var now = ahora ?? DateTime.Now;
            var contratos = _contratoBll.Listar();
            var servicios = _servicioBll.Listar();
            var idsPorPeriodo = contratos.Where(c => c.Fecha_Fin <= now).Select(c => c.IdContrato);
            var idsPorServicios = servicios.Where(s => s.Fecha_hora_fin <= now).Select(s => s.IdContrato);
            var candidatosIds = idsPorPeriodo.Concat(idsPorServicios).Distinct().ToList();

            return contratos
                .Where(c => candidatosIds.Contains(c.IdContrato))
                .Where(c => !_facturaMpr.GetByContrato(c.IdContrato).Any())
                .OrderByDescending(c => c.Fecha_Fin)
                .ToList();
        }
        public PrevisualizacionFactura Previsualizar(int idContrato, DateTime? ahora = null)
        {
            var now = ahora ?? DateTime.Now;
            var contrato = _contratoBll.BuscarPorId(idContrato) ?? throw new InvalidOperationException("Contrato no encontrado.");
            var cliente = _clienteBll.BuscarPorDni(contrato.DNI_CUIL_Cliente)
                ?? throw new InvalidOperationException("Datos de cliente incompletos.");

            var servicios = _servicioBll.Listar()
                .Where(s => s.IdContrato == contrato.IdContrato && s.Fecha_hora_fin <= now)
                .ToList();

            if (servicios.Count == 0)
                throw new InvalidOperationException("No hay servicios finalizados para facturar.");

            double subtotal = 0;
            var notas = new List<string>();

            foreach (var s in servicios)
            {
                double itemServicio = TarifaBaseServicio;
                var asignacionesP = _asigPersMpr.GetByServicio(s.IdServicio);
                double duracionHoras = (s.Fecha_hora_fin - s.Fecha_hora_inicio).TotalHours;

                foreach (var asig in asignacionesP)
                {
                    var per = _personalBll.BuscarPorId(asig.IdPersonal);
                    if (per == null) continue;
                    double pagoBase = (duracionHoras / 12.0) * TarifaBasePersonal;
                    int cantHabs = string.IsNullOrWhiteSpace(per.Habilitaciones) ? 0 :
                        per.Habilitaciones.Split(new[] { '|', ',', ';' }, StringSplitOptions.RemoveEmptyEntries).Length;

                    double plusHabs = cantHabs * TarifaHabilitacion;

                    itemServicio += (pagoBase + plusHabs);
                }
                var asignacionesV = _asigVehMpr.GetByServicio(s.IdServicio);
                foreach (var av in asignacionesV)
                {
                    var v = _vehiculoBll.BuscarPorId(av.IdVehiculo);
                    if (v == null) continue;

                    bool esCaudales = (v.Modelo?.Contains("CAUDAL", StringComparison.OrdinalIgnoreCase) ?? false) ||
                                     (v.Nivel_Blindaje?.Contains("CAUDAL", StringComparison.OrdinalIgnoreCase) ?? false);

                    itemServicio += esCaudales ? TarifaCamionCaudales : TarifaBlindadoNormal;
                }

                subtotal += itemServicio;
            }

            var impuestos = Math.Round(subtotal * PorcentajeIVA, 2);
            var total = Math.Round(subtotal + impuestos, 2);

            return new PrevisualizacionFactura
            {
                IdContrato = contrato.IdContrato,
                Cliente = cliente.Nombre,
                DNI = cliente.DNI_CUIL,
                ServiciosFacturables = servicios.Count,
                Subtotal = Math.Round(subtotal, 2),
                Impuestos = impuestos,
                Total = total,
                Notas = notas
            };
        }
        public FacturaBE GenerarFactura(int idContrato)
        {
            var prev = Previsualizar(idContrato);
            var contrato = _contratoBll.BuscarPorId(idContrato)
                ?? throw new InvalidOperationException("Contrato no encontrado.");
            var cliente = _clienteBll.BuscarPorDni(contrato.DNI_CUIL_Cliente)
                ?? throw new InvalidOperationException("Cliente no encontrado.");

            var factura = new FacturaBE(
                idFactura: 0,
                idContrato: contrato.IdContrato,
                dni: cliente.DNI_CUIL,
                nombreCliente: cliente.Nombre,
                fechaEmision: DateTime.Now,
                fechaVencimiento: DateTime.Now.AddDays(15),
                subtotal: prev.Subtotal,
                impuestos: prev.Impuestos,
                total: prev.Total,
                estadoPago: false,
                saldoPendiente: prev.Total,
                rutaDocumento: ""
            );

            var agregada = _facturaMpr.Add(factura);
            agregada.RutaDocumento = EmitirDocumentoPdf(agregada, contrato);
            var todas = _facturaMpr.GetAll();
            var idx = todas.FindIndex(f => f.IdFactura == agregada.IdFactura);
            if (idx >= 0)
            {
                todas[idx] = agregada;
                _facturaMpr.GuardarTodos(todas);
            }

            return agregada;
        }
        private static string Moneda(double v) => v.ToString("C", CultureInfo.GetCultureInfo("es-AR"));
        private string EmitirDocumentoPdf(FacturaBE factura, ContratoBE contrato)
        {
            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Docs", "Facturas");
            Directory.CreateDirectory(dir);
            var ruta = Path.Combine(dir, $"Factura_{factura.IdFactura:000000}.pdf");

            var cliente = _clienteBll.BuscarPorDni(contrato.DNI_CUIL_Cliente);
            var servicios = _servicioBll.Listar()
                .Where(s => s.IdContrato == contrato.IdContrato && s.Fecha_hora_fin <= DateTime.Now)
                .OrderBy(s => s.Fecha_hora_inicio).ToList();

            using var fs = new FileStream(ruta, FileMode.Create);
            using var doc = new Document(PageSize.A4, 36, 36, 36, 36);
            PdfWriter.GetInstance(doc, fs);
            doc.Open();
            var fTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            var fSubtitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            var fSeccion = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
            var fTexto = FontFactory.GetFont(FontFactory.HELVETICA, 9);
            var fTextoBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9);
            doc.Add(new Paragraph($"Factura #{factura.IdFactura:000000}", fTitulo));
            doc.Add(new Paragraph($"Fecha emisión: {factura.FechaEmision:dd/MM/yyyy}   Vencimiento: {factura.FechaVencimiento:dd/MM/yyyy}", fTexto));
            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph("Cliente", fSubtitulo));
            doc.Add(new Paragraph($"Nombre: {cliente?.Nombre}", fTexto));
            doc.Add(new Paragraph($"DNI/CUIT: {cliente?.DNI_CUIL}", fTexto));
            doc.Add(new Paragraph($"Dirección: {cliente?.Direccion ?? "No especificada"}", fTexto));
            doc.Add(new Paragraph($"Contacto: {cliente?.Correo ?? "S/D"} - {cliente?.Telefono ?? "S/D"}", fTexto));
            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph("Contrato", fSubtitulo));
            doc.Add(new Paragraph($"ID: {contrato.IdContrato}  Tipo: {contrato.Tipo_Servicio}", fTexto));
            doc.Add(new Paragraph($"Inicio: {contrato.Fecha_Inicio:dd/MM/yyyy HH:mm}  Fin: {contrato.Fecha_Fin:dd/MM/yyyy HH:mm}", fTexto));
            var primerS = servicios.FirstOrDefault();
            int cantP = primerS != null ? _asigPersMpr.GetByServicio(primerS.IdServicio).Count : 0;
            int cantV = primerS != null ? _asigVehMpr.GetByServicio(primerS.IdServicio).Count : 0;
            doc.Add(new Paragraph($"Recursos estimados: {cantP} {(cantP == 1 ? "HOMBRE" : "HOMBRES")}. {(cantV > 0 ? cantV + " VEHICULOS" : "SIN VEHICULOS")}.", fTexto));
            doc.Add(new Paragraph(" "));

            var tabla = new PdfPTable(new float[] { 10, 40, 20, 10, 10, 20 }) { WidthPercentage = 100 };
            string[] headers = { "Servicio", "Periodo", "Tipo", "Dotación", "Vehículos", "Importe" };

            foreach (var h in headers)
            {
                var cell = new PdfPCell(new Phrase(h, fSeccion))
                {
                    BackgroundColor = new BaseColor(240, 240, 240),
                    Padding = 5
                };
                tabla.AddCell(cell);
            }

            foreach (var s in servicios)
            {
                double subItem = TarifaBaseServicio;
                double hs = (s.Fecha_hora_fin - s.Fecha_hora_inicio).TotalHours;

                var asigP = _asigPersMpr.GetByServicio(s.IdServicio);
                foreach (var ap in asigP)
                {
                    var p = _personalBll.BuscarPorId(ap.IdPersonal);
                    int hbs = p?.Habilitaciones?.Split('|', ',', ';').Length ?? 0;
                    subItem += ((hs / 12.0) * TarifaBasePersonal) + (hbs * TarifaHabilitacion);
                }

                var asigV = _asigVehMpr.GetByServicio(s.IdServicio);
                foreach (var av in asigV)
                {
                    var v = _vehiculoBll.BuscarPorId(av.IdVehiculo);
                    bool caud = v?.Modelo?.Contains("CAUDAL", StringComparison.OrdinalIgnoreCase) ?? false;
                    subItem += caud ? TarifaCamionCaudales : TarifaBlindadoNormal;
                }

                tabla.AddCell(new PdfPCell(new Phrase($"#{s.IdServicio}", fTexto)) { Padding = 5 });
                tabla.AddCell(new PdfPCell(new Phrase($"{s.Fecha_hora_inicio:dd/MM/yyyy HH:mm} - {s.Fecha_hora_fin:dd/MM/yyyy HH:mm}", fTexto)) { Padding = 5 });
                tabla.AddCell(new PdfPCell(new Phrase(s.TipoServicio, fTexto)) { Padding = 5 });
                tabla.AddCell(new PdfPCell(new Phrase(asigP.Count.ToString(), fTexto)) { Padding = 5 });
                tabla.AddCell(new PdfPCell(new Phrase(asigV.Count.ToString(), fTexto)) { Padding = 5 });
                tabla.AddCell(new PdfPCell(new Phrase(Moneda(subItem), fTexto)) { Padding = 5 });
            }
            doc.Add(tabla);
            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph($"Subtotal: {Moneda(factura.Subtotal)}", fTexto));
            doc.Add(new Paragraph($"Impuestos (IVA 21%): {Moneda(factura.Impuestos)}", fTexto));
            doc.Add(new Paragraph($"Total: {Moneda(factura.Total)}", fSubtitulo));

            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph(" "));
            var pPie = new Paragraph("Documento emitido automáticamente. Valores expresados en ARS.", fTexto);
            doc.Add(pPie);

            doc.Close();
            return ruta;
        }
    }
}
