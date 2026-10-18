using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public class AsignacionVehiculoBLL
    {
        private readonly AsignacionVehiucloMpr _asigMpr = new AsignacionVehiucloMpr();
        private readonly ServicioBLL _servicioBll = new ServicioBLL();
        private readonly VehiculoBLL _vehiculoBll = new VehiculoBLL();

        public int ContarAsignados(int idServicio)
        {
            return _asigMpr.GetByServicio(idServicio).Count;
        }

        public void SincronizarDisponibilidad(DateTime? ahora = null)
        {
            var now = ahora ?? DateTime.Now;
            var asignaciones = _asigMpr.GetAll();
            var vehiculos = _vehiculoBll.Listar();
            foreach (var v in vehiculos)
            {
                var ocupado = asignaciones.Any(a =>
                    a.IdVehiculo == v.IdVehiculo &&
                    a.Inicio <= now &&
                    now <= a.Fin &&
                    a.IdVehiculo > 0);
                var estadoDeseado = ocupado ? VehiculoBLL.EstadoAsignado : VehiculoBLL.EstadoDisponible;
                if (v.EstadoOperativo == VehiculoBLL.EstadoMantenimiento && !ocupado)
                    continue;
                if (v.EstadoOperativo != estadoDeseado)
                    _vehiculoBll.MarcarEstado(v.IdVehiculo, estadoDeseado);
            }
        }

        private bool TieneSolapamientoVehiculo(int idVehiculo, DateTime inicio, DateTime fin)
        {
            return _asigMpr.GetAll().Any(a =>
                a.IdVehiculo == idVehiculo &&
                a.Inicio < fin &&
                inicio < a.Fin);
        }

        public List<ServicioBE> ListarServiciosQueRequierenVehiculos(DateTime? ahora = null, bool incluirFuturos = true)
        {
            SincronizarDisponibilidad(ahora);
            var now = ahora ?? DateTime.Now;

            var servicios = _servicioBll.Listar();
            IEnumerable<ServicioBE> candidatos = incluirFuturos
                ? servicios.Where(s => s.Fecha_hora_fin >= now)
                : servicios.Where(s => s.Fecha_hora_inicio <= now && now <= s.Fecha_hora_fin);
            return candidatos
                .Where(s => s.VehiculosNecesarios > ContarAsignados(s.IdServicio))
                .OrderBy(s => s.Fecha_hora_inicio)
                .ToList();
        }

        public List<VehiculoBE> ListarVehiculosDisponibles()
        {
            SincronizarDisponibilidad();
            return _vehiculoBll.ListarDisponibles();
        }

        public List<VehiculoBE> ListarSugeridosEnMantenimiento()
        {
            return _vehiculoBll.ListarEnMantenimiento();
        }

        public void Asignar(int idServicio, int idVehiculo)
        {
            var servicio = _servicioBll.Listar().FirstOrDefault(s => s.IdServicio == idServicio)
                ?? throw new InvalidOperationException("Servicio no encontrado.");
            var vehiculo = _vehiculoBll.BuscarPorId(idVehiculo)
                ?? throw new InvalidOperationException("Vehículo no encontrado.");
            if (TieneSolapamientoVehiculo(idVehiculo, servicio.Fecha_hora_inicio, servicio.Fecha_hora_fin))
                throw new InvalidOperationException("El vehículo ya tiene otra asignación que se solapa en el mismo horario.");

            SincronizarDisponibilidad();
            if (vehiculo.EstadoOperativo != VehiculoBLL.EstadoDisponible)
                throw new InvalidOperationException("El vehículo seleccionado no está disponible.");

            var yaAsignado = _asigMpr.GetByServicio(idServicio).Any(a => a.IdVehiculo == idVehiculo);
            if (yaAsignado)
                throw new InvalidOperationException("El vehículo ya está asignado a este servicio.");

            var asignados = ContarAsignados(idServicio);
            if (servicio.VehiculosNecesarios - asignados <= 0)
                throw new InvalidOperationException("El servicio ya no requiere más vehículos.");

            var asignacion = new AsignacionVehiculoBE(
                idServicio: servicio.IdServicio,
                idVehiculo: vehiculo.IdVehiculo,
                inicio: servicio.Fecha_hora_inicio,
                fin: servicio.Fecha_hora_fin
            );
            _asigMpr.Add(asignacion);
            _vehiculoBll.MarcarEstado(vehiculo.IdVehiculo, VehiculoBLL.EstadoAsignado);
        }

        public void AsignarDesdeMantenimiento(int idServicio, int idVehiculo)
        {
            var v = _vehiculoBll.BuscarPorId(idVehiculo)
                ?? throw new InvalidOperationException("Vehículo no encontrado.");
            if (v.EstadoOperativo != VehiculoBLL.EstadoMantenimiento)
                throw new InvalidOperationException("El vehículo no está en mantenimiento.");
            _vehiculoBll.MarcarEstado(idVehiculo, VehiculoBLL.EstadoDisponible);

            var servicio = _servicioBll.Listar().First(s => s.IdServicio == idServicio);
            if (TieneSolapamientoVehiculo(idVehiculo, servicio.Fecha_hora_inicio, servicio.Fecha_hora_fin))
                throw new InvalidOperationException("El vehículo tiene otra asignación solapada.");

            Asignar(idServicio, idVehiculo);
        }

        public List<AsignacionVehiculoBE> ListarAsignaciones()
        {
            return _asigMpr.GetAll();
        }
    }
}
