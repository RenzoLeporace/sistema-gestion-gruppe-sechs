using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public enum BETipoPermiso
    {// 1. GESTIÓN DE CLIENTES Y CONTRATOS
        INFORMACIÓN_DE_CLIENTE,
        CREAR_CONTRATO,
        ACTUALIZAR_CONTRATO,    
        FINALIZAR_CONTRATO,
        CONSULTAR_CONTRATO,

        // 2. GESTIÓN DE PERSONAL DE SEGURIDAD
        ASIGNAR_PERSONAL,
        DISPONIBILIDAD_PERSONAL,
        GESTION_PERSONAL,

        // 3. GESTIÓN DE VEHÍCULOS
        ASIGNAR_VEHICULO,
        ESTADO_VEHICULOS,
        GESTION_VEHICULOS,   

        // 4. GESTIÓN DE SERVICIOS
        CREAR_SERVICIO,
        MONITOREAR_SERVICIO,
        GENERAR_INFORME_SERVICIO,

        // 5. GESTIÓN DE FACTURACIÓN Y REPORTES
        GENERAR_FACTURA,
        PAGOS_Y_DEUDAS,
        REPORTE_CLIENTES,
        REPORTE_RECURSOS,
        REPORTE_ECONÓMICO,

        // 6. GESTIÓN DEL SISTEMA Y SEGURIDAD
        ROLES_Y_PERMISOS,
        MODULO_BACKUP,
        RESTORE,
        BITACORA,
        DASHBOARD
    }
}
