# Sistema de Gestión Gruppe Sechs

Aplicación de escritorio en C# y Windows Forms para gestionar las operaciones de una empresa de seguridad. Centraliza clientes, contratos, servicios, personal, vehículos y facturación en una misma interfaz.

## Funcionalidades

- Registrar clientes y gestionar contratos y servicios.
- Consultar disponibilidad de personal y vehículos, asignar recursos y registrar mantenimientos.
- Generar facturas en PDF, registrar pagos y consultar saldos pendientes.
- Consultar informes operativos y reportes de clientes, recursos e ingresos.
- Administrar usuarios, roles y permisos, consultar la bitácora y realizar copias de respaldo.

## Ejecutar en Windows

Necesitás el SDK de .NET especificado en [`global.json`](global.json). El proyecto usa Windows Forms y almacena los datos localmente en archivos XML.

1. Abrí [`Sistema Gestion Gruppe Sechs.sln`](Sistema%20Gestion%20Gruppe%20Sechs.sln) en Visual Studio con la carga de trabajo de escritorio de .NET.
2. Seleccioná `UI` como proyecto de inicio y ejecutalo. La aplicación comienza en la pantalla de inicio de sesión.
3. Para el primer acceso, usá el procedimiento de acceso del equipo.

Desde la raíz del repositorio también podés compilar y ejecutar:

```powershell
dotnet build "Sistema Gestion Gruppe Sechs.sln"
dotnet run --project UI/UI.csproj
```

## Estructura

| Directorio | Responsabilidad |
| --- | --- |
| [`UI/`](UI/) | Formularios de Windows y navegación. |
| [`BLL/`](BLL/) | Reglas de negocio y coordinación de operaciones. |
| [`BE/`](BE/) | Entidades de negocio. |
| [`Mapper/`](Mapper/) | Lectura y escritura de datos XML. |
| [`Seguridad/`](Seguridad/) | Utilidades de seguridad. |
| [`tests/IntegrationChecks/`](tests/IntegrationChecks/) | Comprobaciones de clientes, personal, contratos, facturas, pagos y respaldos. |

## Comprobaciones de integración

En Windows, ejecutá:

```powershell
dotnet run --project tests/IntegrationChecks/IntegrationChecks.csproj
```

El ejecutable de pruebas reinicia la carpeta `Data` de su propio directorio de salida. Usalo con datos de prueba.
