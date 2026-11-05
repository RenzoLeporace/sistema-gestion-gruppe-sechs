# Sistema de Gestión Gruppe Sechs

[![Compilación y chequeos](https://github.com/RenzoLeporace/sistema-gestion-gruppe-sechs/actions/workflows/ci.yml/badge.svg)](https://github.com/RenzoLeporace/sistema-gestion-gruppe-sechs/actions/workflows/ci.yml)

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
3. Ingresá con el usuario `ADMIN` y la contraseña `ADMIN`. En el primer acceso, el sistema te pide elegir una contraseña nueva de al menos 8 caracteres.

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
| [`Seguridad/`](Seguridad/) | Hash de contraseñas con PBKDF2. |
| [`tests/IntegrationChecks/`](tests/IntegrationChecks/) | Comprobaciones de contraseñas, clientes, personal, contratos, facturas, pagos y respaldos. |

## Seguridad

- Las contraseñas se guardan como hash PBKDF2-HMAC-SHA256, con 600.000 iteraciones y una sal aleatoria por contraseña. No se pueden recuperar: para validar un inicio de sesión se vuelve a calcular el hash y se compara.
- Las contraseñas guardadas por versiones anteriores, que solo estaban en Base64, se convierten a hash al abrir la aplicación o al iniciar sesión con ellas.
- Una contraseña nueva debe tener al menos 8 caracteres y no puede ser igual al nombre de usuario. Si un usuario tiene como contraseña su propio nombre, como la cuenta inicial `ADMIN`, debe cambiarla al ingresar.

## Comprobaciones de integración

En Windows, ejecutá:

```powershell
dotnet run --project tests/IntegrationChecks/IntegrationChecks.csproj
```

El ejecutable de pruebas reinicia la carpeta `Data` de su propio directorio de salida. Usalo con datos de prueba.
