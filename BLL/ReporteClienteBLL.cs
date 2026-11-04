using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public class ReporteClienteBLL
    {
        private readonly ClienteMpr _clienteMpr = new ClienteMpr();
        private readonly ContratoMpr _contratoMpr = new ContratoMpr();
        private readonly ServicioMpr _servicioMpr = new ServicioMpr();
        private readonly FacturaMpr _facturaMpr = new FacturaMpr();

        public List<ReporteClienteBE> GenerarReporteClientes()
        {
            return GenerarReporteClientes(DateTime.MinValue, DateTime.MaxValue);
        }

        public List<ReporteClienteBE> GenerarReporteClientes(DateTime desde, DateTime hasta)
        {
            var clientes = _clienteMpr.GetAll();
            var contratos = _contratoMpr.GetAll();
            var serviciosAll = _servicioMpr.GetAll();
            var desdeDate = desde.Date;
            var hastaDate = hasta.Date;

            var servicios = serviciosAll
                .Where(s =>
                {
                    var inicioProp = GetInicioServicio(s).Date;
                    var finProp = GetFinServicio(s).Date;
                    return inicioProp <= hastaDate && finProp >= desdeDate;
                })
                .ToList();

            var facturasAll = _facturaMpr.GetAll();
            var facturas = facturasAll
                .Where(f => f.FechaEmision.Date >= desdeDate && f.FechaEmision.Date <= hastaDate)
                .ToList();

            var resultado = new List<ReporteClienteBE>();

            foreach (var cli in clientes)
            {
                var contratosCli = contratos.Where(c => c.DNI_CUIL_Cliente == cli.DNI_CUIL).ToList();
                var idsContratos = contratosCli.Select(c => c.IdContrato).ToHashSet();

                var serviciosCli = servicios.Where(s => idsContratos.Contains(s.IdContrato)).ToList();
                var facturasCli = facturas.Where(f => f.DNI_CUIL_Cliente == cli.DNI_CUIL).ToList();

                var facturacionAcumulada = facturasCli.Sum(f => f.Total);
                var facturasPagadas = facturasCli.Count(f => f.Estado_Pago);
                var facturasPendientes = facturasCli.Count(f => !f.Estado_Pago);
                var saldoPendiente = facturasCli.Sum(f => f.Saldo_Pendiente);

                var reporte = new ReporteClienteBE(
                    idCliente: cli.IdCliente,
                    nombreCliente: cli.Nombre,
                    dniCuil: cli.DNI_CUIL,
                    serviciosContratados: serviciosCli.Count,
                    facturacionAcumulada: facturacionAcumulada,
                    facturasPagadas: facturasPagadas,
                    facturasPendientes: facturasPendientes,
                    saldoPendiente: saldoPendiente
                );
                resultado.Add(reporte);
            }
            return resultado.OrderByDescending(r => r.FacturacionAcumulada).ToList();
        }
        private DateTime GetInicioServicio(object servicio)
        {
            var prop = servicio.GetType().GetProperty("Fecha_hora_inicio");
            if (prop != null && prop.PropertyType == typeof(DateTime))
                return prop.GetValue(servicio) is DateTime inicio ? inicio : DateTime.MinValue;
            return DateTime.MinValue;
        }
        private DateTime GetFinServicio(object servicio)
        {
            var prop = servicio.GetType().GetProperty("Fecha_hora_fin");
            if (prop != null && prop.PropertyType == typeof(DateTime))
                return prop.GetValue(servicio) is DateTime fin ? fin : DateTime.MaxValue;
            return DateTime.MaxValue;
        }
    }
}
