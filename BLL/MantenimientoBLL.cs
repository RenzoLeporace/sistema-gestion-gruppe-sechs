using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public class MantenimientoBLL
    {
        private readonly MantenimientoMpr _mpr = new MantenimientoMpr();
        private readonly VehiculoBLL _vehiculoBll = new VehiculoBLL();

        public List<MantenimientoBE> Listar() => _mpr.GetAll();

        public MantenimientoBE RegistrarIngresoMantenimiento(int idVehiculo, string patente, double costo = 50_000)
        {
            var desc = $"Mantenimiento de vehículo ID {idVehiculo} - {patente}";
            var mant = new MantenimientoBE(
                costo: costo,
                fecha: DateTime.Now,
                idMantenimiento: 0,
                estado: "Abierto",
                descripcion: desc
            )
            {
                IdVehiculo = idVehiculo
            };
            var agregado = _mpr.Add(mant);
            _vehiculoBll.MarcarEstado(idVehiculo, VehiculoBLL.EstadoMantenimiento);
            AppEvents.NotifyEconomiaActualizada();
            return agregado;
        }

        public void CerrarUltimoMantenimiento(int idVehiculo, double costo)
        {
            var lista = _mpr.GetByVehiculo(idVehiculo);
            var ultimo = lista
                .Where(m => m.Estado == "Abierto")
                .OrderByDescending(m => m.Fecha)
                .FirstOrDefault();

            if (ultimo == null)
                throw new InvalidOperationException("No hay mantenimientos abiertos para este vehículo.");

            ultimo.Costo = costo;
            ultimo.Estado = "Cerrado";
            _mpr.Update(ultimo);

            _vehiculoBll.MarcarEstado(idVehiculo, VehiculoBLL.EstadoDisponible);
        }

        public void CerrarUltimoMantenimiento(int idVehiculo)
        {
            var abiertos = _mpr.GetByVehiculo(idVehiculo)
                .Where(m => string.Equals(m.Estado, "Abierto", StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(m => m.Fecha)
                .ToList();

            var abierto = abiertos.FirstOrDefault();

            if (abierto != null)
            {
                abierto.Estado = "Cerrado";
                _mpr.Update(abierto);
            }
            _vehiculoBll.MarcarEstado(idVehiculo, VehiculoBLL.EstadoDisponible);
            AppEvents.NotifyEconomiaActualizada();
        }
    }
}
