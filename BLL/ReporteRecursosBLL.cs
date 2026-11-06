using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public class ReporteRecursosBLL
    {
        private readonly AsignacionPersonalMpr _asigPersMpr = new AsignacionPersonalMpr();
        private readonly AsignacionVehiculoMpr _asigVehMpr = new AsignacionVehiculoMpr();
        private readonly PersonalBLL _personalBll = new PersonalBLL();
        private readonly VehiculoBLL _vehiculoBll = new VehiculoBLL();
        private readonly ServicioMpr _servicioMpr = new ServicioMpr();

        public ReporteRecursosBE Generar()
        {
            return Generar(DateTime.MinValue, DateTime.MaxValue);
        }

        public ReporteRecursosBE Generar(DateTime desde, DateTime hasta)
        {
            var reporte = new ReporteRecursosBE();
            var servicios = _servicioMpr.GetAll();
            var asignPersAll = _asigPersMpr.GetAll();
            var asignVehAll = _asigVehMpr.GetAll();
            desde = desde.Date;
            hasta = hasta.Date.AddDays(1).AddTicks(-1);
            var asignPers = asignPersAll.Where(a => a.Inicio <= hasta && a.Fin >= desde).ToList();
            var asignVeh = asignVehAll.Where(a => a.Inicio <= hasta && a.Fin >= desde).ToList();
            bool erroresCriticos = false;
            foreach (var asig in asignPers)
            {
                DateTime finReal = (asig.Fin == default || asig.Fin == DateTime.MinValue) ? DateTime.Now : asig.Fin;
                if (asig.Inicio != default && asig.Fin != default && asig.Inicio >= asig.Fin)
                {
                    erroresCriticos = true;
                    reporte.Errores.Add($"Rango inválido asignación personal IdPersonal={asig.IdPersonal} Servicio={asig.IdServicio} Inicio={asig.Inicio} Fin={asig.Fin}");
                    continue;
                }
                var per = _personalBll.BuscarPorId(asig.IdPersonal);
                var serv = servicios.FirstOrDefault(s => s.IdServicio == asig.IdServicio);
                if (per == null || serv == null)
                {
                    reporte.Errores.Add($"Asignación personal ignorada (referencia faltante) IdPersonal={asig.IdPersonal} Servicio={asig.IdServicio}");
                    continue;
                }
                var inicioClip = asig.Inicio < desde ? desde : asig.Inicio;
                var finClip = finReal > hasta ? hasta : finReal;
                var horas = (finClip - inicioClip).TotalHours;
                if (horas <= 0) continue;
                double baseSueldo = (horas / 12.0) * FacturaBLL.TarifaBasePersonal;
                int cantHabs = 0;
                if (!string.IsNullOrWhiteSpace(per.Habilitaciones))
                {
                    cantHabs = per.Habilitaciones.Split(new[] { '|', ',', ';' }, StringSplitOptions.RemoveEmptyEntries).Length;
                }
                double plusHabs = cantHabs * FacturaBLL.TarifaHabilitacion;

                double ingresoEstimado = baseSueldo + plusHabs;
                var item = reporte.Items.FirstOrDefault(i => i.Tipo == "Personal" && i.Id == per.IdPersonal);
                if (item == null)
                {
                    item = new ReporteRecursoItemBE
                    {
                        Tipo = "Personal",
                        Id = per.IdPersonal,
                        Identificador = per.Nombre,
                        HorasAsignadas = 0,
                        ServiciosParticipados = 0,
                        IngresoEstimado = 0
                    };
                    reporte.Items.Add(item);
                }
                item.HorasAsignadas += horas;
                item.ServiciosParticipados += 1;
                item.IngresoEstimado += ingresoEstimado;
                reporte.TotalHorasPersonal += horas;
                reporte.TotalIngresosPersonalEstimado += ingresoEstimado;
            }
            foreach (var asig in asignVeh)
            {
                if (asig.Inicio != default && asig.Fin != default && asig.Inicio >= asig.Fin)
                {
                    erroresCriticos = true;
                    reporte.Errores.Add($"Rango inválido asignación vehículo IdVehiculo={asig.IdVehiculo} Servicio={asig.IdServicio} Inicio={asig.Inicio} Fin={asig.Fin}");
                    continue;
                }

                var veh = _vehiculoBll.BuscarPorId(asig.IdVehiculo);
                var serv = servicios.FirstOrDefault(s => s.IdServicio == asig.IdServicio);
                if (veh == null || serv == null)
                {
                    reporte.Errores.Add($"Asignación vehículo ignorada (referencia faltante) IdVehiculo={asig.IdVehiculo} Servicio={asig.IdServicio}");
                    continue;
                }

                var inicioClip = asig.Inicio < desde ? desde : asig.Inicio;
                var finClip = asig.Fin > hasta ? hasta : asig.Fin;
                var horas = (finClip - inicioClip).TotalHours;
                if (horas <= 0) continue;

                bool esCaudales =
                    (!string.IsNullOrWhiteSpace(veh.Modelo) && veh.Modelo.Contains("CAUDAL", StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrWhiteSpace(veh.Nivel_Blindaje) && veh.Nivel_Blindaje.Contains("CAUDAL", StringComparison.OrdinalIgnoreCase));

                double ingresoEstimado = esCaudales ? FacturaBLL.TarifaCamionCaudales : FacturaBLL.TarifaBlindadoNormal;

                var item = reporte.Items.FirstOrDefault(i => i.Tipo == "Vehículo" && i.Id == veh.IdVehiculo);
                if (item == null)
                {
                    item = new ReporteRecursoItemBE
                    {
                        Tipo = "Vehículo",
                        Id = veh.IdVehiculo,
                        Identificador = veh.Patente,
                        HorasAsignadas = 0,
                        ServiciosParticipados = 0,
                        IngresoEstimado = 0
                    };
                    reporte.Items.Add(item);
                }
                item.HorasAsignadas += horas;
                item.ServiciosParticipados += 1;
                item.IngresoEstimado += ingresoEstimado;
                reporte.TotalHorasVehiculos += horas;
                reporte.TotalIngresosVehiculosEstimado += ingresoEstimado;
            }

            reporte.TieneErroresCriticos = erroresCriticos;
            reporte.DatosCompletos = !erroresCriticos;
            reporte.Items = reporte.Items.OrderByDescending(i => i.HorasAsignadas).ToList();
            return reporte;
        }
    }
}