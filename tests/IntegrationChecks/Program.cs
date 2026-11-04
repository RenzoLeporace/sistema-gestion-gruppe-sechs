using BE;
using BLL;
using Mapper;

var data = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
if (Directory.Exists(data)) Directory.Delete(data, recursive: true);

var checks = 0;
void Check(bool condition, string name)
{
    if (!condition) throw new Exception($"Falló: {name}");
    Console.WriteLine($"OK: {name}");
    checks++;
}

var clientes = new ClienteBLL();
var cliente = clientes.Alta("Cliente prueba", "Calle 123", "1122334455", "test@example.com", "30123456");
Check(clientes.BuscarPorDni("30123456")?.IdCliente == cliente.IdCliente, "alta y lectura de cliente");
try { clientes.Alta("Duplicado", "Calle 123", "1122334455", "test@example.com", "30123456"); throw new Exception("Aceptó DNI duplicado"); }
catch (InvalidOperationException) { Check(true, "bloqueo de cliente duplicado"); }
clientes.Actualizar("Cliente actualizado", "Calle 456", "1122334455", "test@example.com", "30123456");
Check(clientes.BuscarPorDni("30123456")?.Nombre == "Cliente actualizado", "actualización de cliente");

var personalBll = new PersonalBLL();
var datos = new PersonalBLL.DatosAdicionalesPersonal
{
    Correo = "guardia@example.com", Telefono = "1133344455", TurnosDisponibles = "Noche",
    VencimientoLicenciaArmas = DateTime.Today.AddYears(1), Capacitaciones = "RCP"
};
var personal = personalBll.Agregar("Guardia", "32123456", "ESCOLTA", ["Permiso de Armas"], datos: datos);
Check(personalBll.BuscarPorId(personal.IdPersonal)?.Correo == datos.Correo, "datos de contacto de personal persistidos");
Check(personalBll.BuscarPorId(personal.IdPersonal)?.VencimientoLicenciaArmas == datos.VencimientoLicenciaArmas, "licencia de armas persistida");
personalBll.MarcarDisponibilidad(personal.IdPersonal, false);
Check(personalBll.BuscarPorId(personal.IdPersonal)?.TurnosDisponibles == "Noche", "cambio de disponibilidad conserva datos adicionales");
try { personalBll.Agregar("Otro", "33123456", "ESCOLTA", [], datos: datos); throw new Exception("Aceptó fecha sin permiso"); }
catch (ArgumentException) { Check(true, "validación de licencia de armas"); }
personalBll.Actualizar(personal.IdPersonal, "Guardia", "32123456", "ESCOLTA", [], false,
    new PersonalBLL.DatosAdicionalesPersonal { Correo = "nuevo@example.com" });
Check(personalBll.BuscarPorId(personal.IdPersonal)?.VencimientoLicenciaArmas == null, "limpieza de licencia al quitar permiso");

var inicio = DateTime.Now.AddHours(24);
var contrato = new ContratoBLL().CrearContrato(cliente.DNI_CUIL, "ESCOLTA", inicio, inicio.AddHours(12), 500000, "Un guardia");
Check(new ContratoBLL().BuscarPorId(contrato.IdContrato) != null, "alta y lectura de contrato");
var servicio = new ServicioBLL().CrearServicio(contrato.IdContrato, "ESCOLTA", inicio, inicio.AddHours(12), 1, 0);
Check(new ServicioBLL().Listar().Any(s => s.IdServicio == servicio.IdServicio), "alta y lectura de servicio");

// El flujo de facturación exige servicios finalizados. Se adelantan sólo los datos aislados de la prueba.
var servicioMpr = new ServicioMpr();
var facturable = servicioMpr.GetById(servicio.IdServicio) ?? throw new Exception("Falló: servicio de prueba no encontrado");
facturable.Fecha_hora_inicio = DateTime.Now.AddHours(-13);
facturable.Fecha_hora_fin = DateTime.Now.AddHours(-1);
servicioMpr.Update(facturable);
var facturacion = new FacturaBLL();
Check(facturacion.Previsualizar(contrato.IdContrato).Total > 0, "previsualización de factura");
var factura = facturacion.GenerarFactura(contrato.IdContrato);
var facturaMpr = new FacturaMpr();
Check(File.Exists(factura.RutaDocumento), "emisión de PDF de factura");
Check(facturaMpr.GetAll().Single().RutaDocumento == factura.RutaDocumento, "ruta de PDF persistida en factura");
var pagoBll = new PagoBLL();
pagoBll.RegistrarPago(factura.IdFactura, factura.Total / 2, DateTime.Now, "Efectivo");
Check(facturaMpr.GetAll().Single().Saldo_Pendiente < factura.Total, "pago parcial persistido");
try { pagoBll.RegistrarPago(factura.IdFactura, factura.Total, DateTime.Now, "Efectivo"); throw new Exception("Aceptó un sobrepago"); }
catch (InvalidOperationException) { Check(true, "rechazo de pago mayor al saldo"); }
pagoBll.RegistrarPago(factura.IdFactura, facturaMpr.GetAll().Single().Saldo_Pendiente, DateTime.Now, "Efectivo");
Check(facturaMpr.GetAll().Single().Estado_Pago, "pago completo persistido");
Check(!pagoBll.ListarFacturasPendientesOrdenadas().Any(), "listado de pendientes actualizado");

var snapshot = BLLBackup.Instance.CrearSnapshotBackup();
Check(File.Exists(snapshot), "creación de respaldo XML");
Check(BLLBackup.Instance.EnumerarSnapshots().Contains(snapshot), "respaldo visible para restauración");

Console.WriteLine($"{checks} comprobaciones superadas.");
