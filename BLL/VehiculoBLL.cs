using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public class VehiculoBLL
    {
        private readonly VehiculoMpr _mpr = new VehiculoMpr();
        private readonly AsignacionVehiucloMpr _asigMpr = new AsignacionVehiucloMpr();
        public const int EstadoMantenimiento = 0;
        public const int EstadoDisponible = 1;
        public const int EstadoAsignado = 2;
        public List<VehiculoBE> Listar() => _mpr.GetAll();
        public VehiculoBE? BuscarPorId(int id) => _mpr.GetById(id);
        public List<VehiculoBE> Buscar(string criterio)
        {
            var q = (criterio ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(q)) return Listar();

            return Listar()
                .Where(v =>
                    (!string.IsNullOrEmpty(v.Patente) && v.Patente.Contains(q, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(v.Modelo) && v.Modelo.Contains(q, StringComparison.OrdinalIgnoreCase)))
                .OrderBy(v => v.Patente)
                .ToList();
        }
        public List<VehiculoBE> ListarDisponibles()
        {
            return Listar().Where(v => v.EstadoOperativo == EstadoDisponible).OrderByDescending(v => v.Anio).ToList();
        }
        public List<VehiculoBE> ListarEnMantenimiento()
        {
            return Listar().Where(v => v.EstadoOperativo == EstadoMantenimiento).OrderByDescending(v => v.Anio).ToList();
        }
        public void MarcarEstado(int idVehiculo, int nuevoEstado)
        {
            var v = _mpr.GetById(idVehiculo);
            if (v == null) return;
            v.EstadoOperativo = nuevoEstado;
            _mpr.Update(v);
        }
        public VehiculoBE Agregar(string patente, string modelo, int anio, string nivelBlindaje)
        {
            patente = (patente ?? "").Trim();
            modelo = (modelo ?? "").Trim();
            nivelBlindaje = (nivelBlindaje ?? "").Trim();
            ValidarCampos(patente, modelo, anio);
            if (_mpr.GetByPatente(patente) != null)
                throw new InvalidOperationException("La patente ya existe.");
            var nuevo = new VehiculoBE
            {
                Patente = patente,
                Modelo = modelo,
                Anio = anio,
                Nivel_Blindaje = nivelBlindaje,
                EstadoOperativo = EstadoDisponible
            };
            return _mpr.Add(nuevo);
        }
        public void Editar(int idVehiculo, string patente, string modelo, int anio, string nivelBlindaje)
        {
            var actual = _mpr.GetById(idVehiculo) ?? throw new InvalidOperationException("Vehículo no encontrado.");
            patente = (patente ?? "").Trim();
            modelo = (modelo ?? "").Trim();
            nivelBlindaje = (nivelBlindaje ?? "").Trim();
            ValidarCampos(patente, modelo, anio);
            var otroConPatente = _mpr.GetByPatente(patente);
            if (otroConPatente != null && otroConPatente.IdVehiculo != idVehiculo)
                throw new InvalidOperationException("La patente ya pertenece a otro vehículo.");
            actual.Patente = patente;
            actual.Modelo = modelo;
            actual.Anio = anio;
            actual.Nivel_Blindaje = nivelBlindaje;
            _mpr.Update(actual);
        }
        public void Eliminar(int idVehiculo)
        {
            var v = _mpr.GetById(idVehiculo) ?? throw new InvalidOperationException("Vehículo no encontrado.");
            if (v.EstadoOperativo == EstadoAsignado)
                throw new InvalidOperationException("No se puede eliminar un vehículo asignado a un servicio.");
            var now = DateTime.Now;
            var asignaciones = _asigMpr.GetByVehiculo(idVehiculo);
            var activa = asignaciones.Any(a => a.Inicio <= now && now <= a.Fin);
            if (activa)
                throw new InvalidOperationException("El vehículo tiene una asignación activa.");
            _mpr.Delete(idVehiculo);
        }
        private static void ValidarCampos(string patente, string modelo, int anio)
        {
            if (string.IsNullOrWhiteSpace(patente)) throw new ArgumentException("La patente es obligatoria.");
            if (string.IsNullOrWhiteSpace(modelo)) throw new ArgumentException("El modelo es obligatorio.");
            var year = DateTime.Now.Year + 1;
            if (anio < 1980 || anio > year) throw new ArgumentException($"El año debe estar entre 1980 y {year}.");
        }
    }
}
