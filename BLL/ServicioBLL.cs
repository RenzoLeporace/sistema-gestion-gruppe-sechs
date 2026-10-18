using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public class ServicioBLL
    {
        private readonly ServicioMpr _servicioMpr = new ServicioMpr();
        private readonly ContratoBLL _contratoBll = new ContratoBLL();
        private readonly AsignacionPersonalMpr _asigPersMpr = new AsignacionPersonalMpr();
        private readonly AsignacionVehiucloMpr _asigVehMpr = new AsignacionVehiucloMpr();
        private readonly TipoServicioBLL _tipoServBll = new TipoServicioBLL();
        public List<ServicioBE> Listar() => _servicioMpr.GetAll();
        public ServicioBE CrearServicio(int idContrato, string tipoServicio, DateTime inicio, DateTime fin,
                                        int personalNecesario, int vehiculosNecesarios, string observacion = "")
        {
            if (idContrato <= 0) throw new ArgumentException("ID de contrato inválido.");
            var contrato = _contratoBll.BuscarPorId(idContrato);
            if (contrato == null) throw new InvalidOperationException("Contrato no encontrado.");
            if (string.Equals(contrato.Estado, "Finalizado", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("No se puede crear un servicio para un contrato finalizado.");
            if (string.IsNullOrWhiteSpace(tipoServicio))
                throw new ArgumentException("Debe seleccionar un tipo de servicio.");
            var ts = _tipoServBll.BuscarPorDescripcion(tipoServicio.Trim());
            if (ts == null) throw new ArgumentException("Tipo de servicio inválido o no registrado.");
            var permitidos = new[] { "ESCOLTA", "EVENTO", "TRANSPORTE" };
            if (!permitidos.Contains((ts.Descripcion ?? "").Trim().ToUpperInvariant()))
                throw new ArgumentException("Tipo de servicio inválido. Use: ESCOLTA, EVENTO o TRANSPORTE.");
            if (inicio < DateTime.Today) throw new ArgumentException("La fecha de inicio no puede ser pasada.");
            if (fin <= inicio) throw new ArgumentException("La fecha/hora de fin debe ser posterior al inicio.");
            if (personalNecesario < 0) throw new ArgumentException("Personal necesario inválido.");
            if (vehiculosNecesarios < 0) throw new ArgumentException("Vehículos necesarios inválidos.");
            int vehiculosNecesariosFinal = vehiculosNecesarios;
            if (string.Equals((ts.Descripcion ?? "").Trim(), "TRANSPORTE", StringComparison.OrdinalIgnoreCase)
                && vehiculosNecesariosFinal < 1)
            {
                vehiculosNecesariosFinal = 1;
            }

            var servicio = new ServicioBE(
                estado: "Planificado",
                idServicio: 0,
                valorServicio: observacion?.Trim() ?? "",
                fecha_hora_inicio: inicio,
                fecha_hora_fin: fin,
                idContrato: idContrato,
                tipoServicio: ts.Descripcion,
                personalNecesario: personalNecesario,
                vehiculosNecesarios: vehiculosNecesariosFinal
            );
            var creado = _servicioMpr.Add(servicio);
            if (personalNecesario > 0)
            {
                _asigPersMpr.Add(new AsignacionPersonalBE(inicio, 0, personalNecesario));
            }
            if (vehiculosNecesariosFinal > 0)
            {
                _asigVehMpr.Add(new AsignacionVehiculoBE(inicio, 0, vehiculosNecesariosFinal));
            }
            AppEvents.NotifyEconomiaActualizada();

            return creado;
        }
    }
}
