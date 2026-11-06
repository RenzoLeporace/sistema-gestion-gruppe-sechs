using System.Text;
using BE;
using BLL;
using Mapper;
using System.Xml.Serialization;
using UI;
using Seguridad;

var data = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
if (Directory.Exists(data)) Directory.Delete(data, recursive: true);

var checks = 0;
void Check(bool condition, string name)
{
    if (!condition) throw new Exception($"Falló: {name}");
    Console.WriteLine($"OK: {name}");
    checks++;
}

var legacyPath = Path.Combine(data, "legacy.xml");
Directory.CreateDirectory(data);
using (var legacy = File.Create(legacyPath))
    new XmlSerializer(typeof(List<ClienteBE>)).Serialize(legacy,
        new List<ClienteBE> { new(7, "Legado", "legacy@example.invalid", "00000007", "0000000000", "Calle 1") });
Check(XMLHelper.Leer<ClienteBE>(legacyPath).Single().Nombre == "Legado", "migración inicial desde XML a SQLite");
XMLHelper.Guardar(legacyPath, new List<ClienteBE>());
Check(XMLHelper.Leer<ClienteBE>(legacyPath).Count == 0, "un borrado no reimporta XML antiguo");
Check(File.Exists(SqliteStore.DatabasePath), "base de datos SQLite creada");
// Usuario de prueba con clave generada en ejecución; se migra desde el formato legado.
var claveLegada = Guid.NewGuid().ToString("N");
var claveNueva = Guid.NewGuid().ToString("N");
string FormatoAnterior(string clave) => Convert.ToBase64String(Encoding.Unicode.GetBytes(clave));
var usuariosXml = Path.Combine(data, "usuarios.xml");
Directory.CreateDirectory(data);
File.WriteAllText(usuariosXml, $"""
    <?xml version="1.0"?>
    <ArrayOfUsuarioDto xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
      <UsuarioDto><Id>2</Id><Nombre>operador-prueba</Nombre><PasswordEncrypted>{FormatoAnterior(claveLegada)}</PasswordEncrypted><RolesIds /><Bloqueado>false</Bloqueado><Activo>true</Activo><PermisosIds /></UsuarioDto>
    </ArrayOfUsuarioDto>
    """);
var usuarios = BLLUsuario.Instance;
Check(usuarios.GetUsuarios().All(u => HashContrasena.EsHash(u.PasswordHash)), "contraseñas anteriores migradas a hash");
Check(XMLHelper.Leer<UsuarioMapper.UsuarioDto>(usuariosXml).All(u => !string.IsNullOrEmpty(u.PasswordHash)), "contraseñas activas guardadas como hash en SQLite");
Check(usuarios.ValidarCredenciales("operador-prueba", claveLegada), "inicio de sesión con contraseña migrada");
Check(!usuarios.ValidarCredenciales("operador-prueba", claveNueva), "rechazo de contraseña incorrecta");
try { usuarios.CambiarClave(claveLegada, "corta"); throw new Exception("Aceptó una contraseña corta"); }
catch (ArgumentException) { Check(true, "rechazo de contraseña corta"); }
try { usuarios.CambiarClave(claveNueva, claveNueva); throw new Exception("Aceptó una contraseña actual incorrecta"); }
catch (ArgumentException) { Check(true, "cambio de contraseña exige la actual"); }
usuarios.CambiarClave(claveLegada, claveNueva);
Check(!usuarios.ValidarCredenciales("operador-prueba", claveLegada) && usuarios.ValidarCredenciales("operador-prueba", claveNueva), "solo vale la contraseña nueva");
var hashUno = HashContrasena.Calcular(claveNueva);
var hashDos = HashContrasena.Calcular(claveNueva);
Check(hashUno != hashDos && HashContrasena.Verificar(claveNueva, hashUno) && HashContrasena.Verificar(claveNueva, hashDos), "sal distinta para cada hash");
usuarios.Logout();
var rolMpr = new RolMpr();
var rol = new BERol { Nombre = "Administrador de prueba", Descripcion = "Acceso de prueba" };
rolMpr.Guardar(rol);
var usuarioMpr = new UsuarioMapper();
var usuarioConRol = usuarioMpr.ObtenerTodos().Single(u => u.Nombre == "operador-prueba");
usuarioConRol.Roles.Add(rol);
usuarioMpr.Guardar(usuarioConRol);
Check(usuarioMpr.ObtenerTodos().Single(u => u.Nombre == "operador-prueba").Roles.Single().Nombre == rol.Nombre, "usuarios y roles persistidos en SQLite");


var clientes = new ClienteBLL();
var cliente = clientes.Alta("Cliente prueba", "Calle 123", "0000000000", "client@example.invalid", "00000001");
Check(clientes.BuscarPorDni("00000001")?.IdCliente == cliente.IdCliente, "alta y lectura de cliente");
try { clientes.Alta("Duplicado", "Calle 123", "0000000000", "client@example.invalid", "00000001"); throw new Exception("Aceptó DNI duplicado"); }
catch (InvalidOperationException) { Check(true, "bloqueo de cliente duplicado"); }
clientes.Actualizar("Cliente actualizado", "Calle 456", "0000000000", "client@example.invalid", "00000001");
Check(clientes.BuscarPorDni("00000001")?.Nombre == "Cliente actualizado", "actualización de cliente");

var personalBll = new PersonalBLL();
var datos = new PersonalBLL.DatosAdicionalesPersonal
{
    Correo = "staff@example.invalid", Telefono = "0000000000", TurnosDisponibles = "Noche",
    VencimientoLicenciaArmas = DateTime.Today.AddYears(1), Capacitaciones = "RCP"
};
var personal = personalBll.Agregar("Guardia", "00000002", "ESCOLTA", ["Permiso de Armas"], datos: datos);
Check(personalBll.BuscarPorId(personal.IdPersonal)?.Correo == datos.Correo, "datos de contacto de personal persistidos");
Check(personalBll.BuscarPorId(personal.IdPersonal)?.VencimientoLicenciaArmas == datos.VencimientoLicenciaArmas, "licencia de armas persistida");
personalBll.MarcarDisponibilidad(personal.IdPersonal, false);
Check(personalBll.BuscarPorId(personal.IdPersonal)?.TurnosDisponibles == "Noche", "cambio de disponibilidad conserva datos adicionales");
try { personalBll.Agregar("Otro", "00000003", "ESCOLTA", [], datos: datos); throw new Exception("Aceptó fecha sin permiso"); }
catch (ArgumentException) { Check(true, "validación de licencia de armas"); }
personalBll.Actualizar(personal.IdPersonal, "Guardia", "00000002", "ESCOLTA", [], false,
    new PersonalBLL.DatosAdicionalesPersonal { Correo = "updated@example.invalid" });
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
Check(File.Exists(snapshot), "creación de respaldo SQLite");
Check(BLLBackup.Instance.EnumerarSnapshots().Contains(snapshot), "respaldo visible para restauración");
clientes.Actualizar("Cambio posterior", "Calle 456", "0000000000", "client@example.invalid", "00000001");
BLLBackup.Instance.RestaurarDesdeSnapshot(snapshot, crearBackupPrevio: false);
Check(clientes.BuscarPorDni("00000001")?.Nombre == "Cliente actualizado", "restauración recupera estado de cliente");

Exception? uiError = null;
var uiThread = new Thread(() =>
{
    try
    {
        using var main = new Form1();
        using var staff = new GestionPersonal();
        using var history = new HistorialServicios();
        using var security = new RolesYpermisos();
        if (main.Controls.Count == 0 || staff.Controls.Count == 0 || history.Controls.Count == 0 || security.Controls.OfType<Panel>().Count() < 4)
            throw new Exception("Formulario sin controles");
        if (main.Controls.OfType<PictureBox>().Single().Image == null)
            throw new Exception("Falta el logo de inicio");
    }
    catch (Exception ex) { uiError = ex; }
});
uiThread.SetApartmentState(ApartmentState.STA);
uiThread.Start();
uiThread.Join();
if (uiError != null) throw new Exception("Falló la apertura de formularios", uiError);
Check(true, "apertura de formularios principal, personal, historial y roles");

Console.WriteLine($"{checks} comprobaciones superadas.");


