using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public class ReporteEconomicoBLL
    {
        private readonly FacturaMpr _facturaMpr = new FacturaMpr();
        private readonly PagoMpr _pagoMpr = new PagoMpr();
        private readonly MantenimientoMpr _mantenimientoMpr = new MantenimientoMpr();
        private readonly AsignacionPersonalMpr _asigPersMpr = new AsignacionPersonalMpr();
        private readonly PersonalBLL _personalBll = new PersonalBLL();

        public ReporteEconomicoBE GenerarReporteEconomico(DateTime desde, DateTime hasta)
        {
            desde = desde.Date;
            hasta = hasta.Date.AddDays(1).AddTicks(-1);

            var facturas = (_facturaMpr.GetAll() ?? new List<FacturaBE>())
                .Where(f => f.FechaEmision >= desde && f.FechaEmision <= hasta).ToList();

            var pagos = (_pagoMpr.GetAll() ?? new List<PagoBE>())
                .Where(p => p.Fecha_Pago >= desde && p.Fecha_Pago <= hasta).ToList();

            var facturacionTotal = facturas.Sum(f => f.Total);
            var cobradoCalculado = facturas.Sum(f => Math.Max(0, f.Total - f.Saldo_Pendiente));
            var ingresosDesdePagos = pagos.Sum(p => p.Monto_Pagado);
            var ingresos = Math.Max(ingresosDesdePagos, cobradoCalculado);

            var gastosMantenimiento = (_mantenimientoMpr.GetAll() ?? new List<MantenimientoBE>())
                .Where(m => m.Fecha >= desde && m.Fecha <= hasta)
                .Sum(m => m.Costo);

            double gastosSueldos = 0;
            var asignaciones = (_asigPersMpr.GetAll() ?? new List<AsignacionPersonalBE>())
                .Where(a => a.Inicio <= hasta && (a.Fin >= desde || a.Fin == default))
                .ToList();

            foreach (var asig in asignaciones)
            {
                var per = _personalBll.BuscarPorId(asig.IdPersonal);
                if (per == null) continue;
                DateTime finReal = (asig.Fin == default || asig.Fin == DateTime.MinValue) ? DateTime.Now : asig.Fin;
                var inicioClip = asig.Inicio < desde ? desde : asig.Inicio;
                var finClip = finReal > hasta ? hasta : finReal;

                double horasEnRango = (finClip - inicioClip).TotalHours;
                if (horasEnRango <= 0) continue;
                int cantHabilitaciones = 0;
                if (!string.IsNullOrWhiteSpace(per.Habilitaciones))
                {
                    cantHabilitaciones = per.Habilitaciones
                        .Split(new[] { '|', ',', ';' }, StringSplitOptions.RemoveEmptyEntries).Count();
                }
                double pagoBasePersonal = (horasEnRango / 12.0) * FacturaBLL.TarifaBasePersonal;
                double plusHabilitaciones = cantHabilitaciones * FacturaBLL.TarifaHabilitacion;
                gastosSueldos += (pagoBasePersonal + plusHabilitaciones);
            }

            var gastosTotales = gastosMantenimiento + gastosSueldos;
            var deudaPendiente = facturas.Sum(f => f.Saldo_Pendiente);
            var rentabilidad = ingresos - gastosTotales;
            var margen = ingresos > 0 ? (rentabilidad / ingresos) * 100.0 : 0.0;

            var rep = new ReporteEconomicoBE(ingresos, gastosTotales, rentabilidad, margen, deudaPendiente, true)
            {
                CobradoCalculado = Math.Round(cobradoCalculado, 2),
                FacturacionTotal = Math.Round(facturacionTotal, 2)
            };

            return rep;
        }
    }
}