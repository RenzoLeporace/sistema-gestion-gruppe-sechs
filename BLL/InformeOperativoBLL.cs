using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public class InformeOperativoBLL
    {
        private readonly InformeOperativoMpr _informeMpr = new InformeOperativoMpr();
        private readonly ServicioBLL _servicioBll = new ServicioBLL();
        private readonly ServicioMpr _servicioMpr = new ServicioMpr();
        private readonly AsignacionPersonalMpr _asigPersMpr = new AsignacionPersonalMpr();
        private readonly AsignacionVehiculoMpr _asigVehMpr = new AsignacionVehiculoMpr();

        public List<ServicioBE> ListarFinalizadosSinInforme(DateTime? ahora = null)
        {
            var now = ahora ?? DateTime.Now;
            return _servicioBll.Listar()
                .Where(s => s.Fecha_hora_fin <= now)
                .Where(s => !s.IdInformeOperativo.HasValue || s.IdInformeOperativo.Value <= 0)
                .OrderByDescending(s => s.Fecha_hora_fin)
                .ToList();
        }
        public bool ValidarServicioListoParaInforme(ServicioBE s, out string motivo)
        {
            if (s == null) { motivo = "Servicio no encontrado."; return false; }
            if (s.Fecha_hora_fin > DateTime.Now)
            {
                motivo = "El servicio aún no ha finalizado.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(s.TipoServicio))
            {
                motivo = "Información del servicio incompleta: tipo de servicio.";
                return false;
            }
            var tipo = (s.TipoServicio ?? "").Trim().ToUpperInvariant();
            var asignadosPer = _asigPersMpr.GetByServicio(s.IdServicio).Count;
            var asignadosVeh = _asigVehMpr.GetByServicio(s.IdServicio).Count;
            var tienePersonal = (s.PersonalNecesario > 0) || (asignadosPer > 0);
            if (!tienePersonal)
            {
                motivo = "Información de recursos incompleta: no hay personal registrado para el servicio.";
                return false;
            }
            var requiereVehiculo = tipo == "TRANSPORTE";
            var tieneVehiculo = (s.VehiculosNecesarios > 0) || (asignadosVeh > 0);
            if (requiereVehiculo && !tieneVehiculo)
            {
                motivo = "Información de recursos incompleta: no hay vehículos registrados para el servicio de TRANSPORTE.";
                return false;
            }

            motivo = "";
            return true;
        }
        public InformeOperativoBE GenerarInforme(int idServicio, string incidencias, string resultado, string observaciones)
        {
            var servicio = _servicioMpr.GetById(idServicio)
                ?? throw new InvalidOperationException("Servicio no encontrado.");
            if (servicio.IdInformeOperativo.HasValue && servicio.IdInformeOperativo.Value > 0)
                throw new InvalidOperationException("El servicio ya tiene un informe asociado.");
            if (!ValidarServicioListoParaInforme(servicio, out var motivo))
                throw new InvalidOperationException(motivo);
            var nuevo = new InformeOperativoBE(
                idInforme: 0,
                idServicio: servicio.IdServicio,
                incidencias: incidencias?.Trim() ?? "",
                observaciones: observaciones?.Trim() ?? "",
                resultado: resultado?.Trim() ?? ""
            );
            var agregado = _informeMpr.Add(nuevo);
            servicio.IdInformeOperativo = agregado.IdInforme;
            _servicioMpr.Update(servicio);
            return agregado;
        }
    }
}
