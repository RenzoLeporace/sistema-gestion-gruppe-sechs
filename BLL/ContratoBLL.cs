using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public class ContratoBLL
    {
        private readonly ContratoMpr _contratoMpr = new ContratoMpr();
        private readonly ClienteBLL _clienteBll = new ClienteBLL();
        private readonly TipoServicioBLL _tipoServicioBll = new TipoServicioBLL();

        public List<ContratoBE> Listar()
        {
            return _contratoMpr.GetAll();
        }

        public List<ContratoBE> ListarActivos()
        {
            return _contratoMpr.GetAll()
                .Where(c => !string.Equals(c.Estado, "Finalizado", StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<ContratoBE> ListarModificables()
        {
            var ahora = DateTime.Now;
            return _contratoMpr.GetAll()
                .Where(c => PuedeSerModificado(c, ahora))
                .ToList();
        }

        public bool PuedeSerModificado(ContratoBE contrato)
        {
            return PuedeSerModificado(contrato, DateTime.Now);
        }

        private bool PuedeSerModificado(ContratoBE c, DateTime ahora)
        {
            if (c == null) return false;
            if (string.Equals(c.Estado, "Finalizado", StringComparison.OrdinalIgnoreCase))
                return false;
            if (ahora >= c.Fecha_Inicio && ahora <= c.Fecha_Fin)
                return false;

            if (c.Fecha_Inicio > ahora && (c.Fecha_Inicio - ahora).TotalHours <= 12)
                return false;

            return true;
        }

        public ContratoBE BuscarPorId(int id)
        {
            return _contratoMpr.GetAll().FirstOrDefault(c => c.IdContrato == id);
        }

        public List<ContratoBE> BuscarPorDni(string dni)
        {
            return _contratoMpr.GetAll().Where(c => c.DNI_CUIL_Cliente == dni).ToList();
        }
        public ContratoBE CrearContrato(string dniCuilCliente, string tipoServicio, DateTime fechaInicio,
                                        DateTime fechaFin, double costoEstimado, string recursosStr)
        {
            if (string.IsNullOrWhiteSpace(dniCuilCliente))
                throw new ArgumentException("Debe ingresar DNI/CUIT del cliente.");
            var cliente = _clienteBll.BuscarPorDni(dniCuilCliente);
            if (cliente == null)
                throw new InvalidOperationException("El cliente no existe.");
            if (string.IsNullOrWhiteSpace(tipoServicio))
                throw new ArgumentException("Debe seleccionar un tipo de servicio.");
            var tipoServ = _tipoServicioBll.BuscarPorDescripcion(tipoServicio);
            if (tipoServ == null)
                throw new InvalidOperationException("Tipo de servicio inválido.");
            if (fechaInicio < DateTime.Now)
                throw new ArgumentException("La fecha y hora de inicio no pueden ser pasadas.");
            if (fechaFin <= fechaInicio)
                throw new ArgumentException("La fecha y hora de fin deben ser posteriores al inicio.");
            if (costoEstimado <= 0)
                throw new ArgumentException("Costo estimado inválido.");
            if (string.IsNullOrWhiteSpace(recursosStr))
                throw new ArgumentException("Recursos estimados inválidos (debe describirse, ej: 'tres hombres y un auto').");
            var contrato = new ContratoBE(
                idContrato: 0,
                idCliente: cliente.IdCliente,
                dniCuilCliente: cliente.DNI_CUIL,
                costoEstimado: costoEstimado,
                fechaInicio: fechaInicio,
                fechaFin: fechaFin,
                estado: "Activo",
                tipoServicio: tipoServ.Descripcion,
                recursosEstimados: recursosStr.Trim()
            );
            return _contratoMpr.Add(contrato);
        }
        public void ActualizarContrato(int idContrato, double nuevoCosto, string nuevosRecursosStr, DateTime nuevaFechaFin)
        {
            var contrato = BuscarPorId(idContrato);
            if (contrato == null)
                throw new InvalidOperationException("Contrato no encontrado.");
            if (string.Equals(contrato.Estado, "Finalizado", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("No se puede actualizar un contrato finalizado.");

            var ahora = DateTime.Now;
            bool enEjecucion = ahora >= contrato.Fecha_Inicio && ahora <= contrato.Fecha_Fin;
            bool comienzaEn12hs = contrato.Fecha_Inicio > ahora && (contrato.Fecha_Inicio - ahora).TotalHours <= 12;

            if (enEjecucion)
                throw new InvalidOperationException("No se puede actualizar un contrato cuyo servicio está en ejecución.");
            if (comienzaEn12hs)
                throw new InvalidOperationException("No se puede actualizar un contrato cuyo servicio comienza dentro de las próximas 12 horas.");
            if (nuevoCosto <= 0)
                throw new ArgumentException("Costo estimado inválido.");
            if (string.IsNullOrWhiteSpace(nuevosRecursosStr))
                throw new ArgumentException("Descripción de recursos inválida.");
            if (nuevaFechaFin < contrato.Fecha_Inicio)
                throw new ArgumentException("La nueva fecha/hora no puede ser anterior al inicio.");

            contrato.Costo_Estimado = nuevoCosto;
            contrato.Recursos_Estimados = nuevosRecursosStr.Trim();
            contrato.Fecha_Fin = nuevaFechaFin;

            var contratos = _contratoMpr.GetAll();
            var index = contratos.FindIndex(c => c.IdContrato == contrato.IdContrato);
            if (index >= 0)
            {
                contratos[index] = contrato;
                _contratoMpr.GuardarTodos(contratos);
            }
        }

        public bool FinalizarContrato(int idContrato)
        {
            var contrato = BuscarPorId(idContrato);
            if (contrato == null)
                throw new InvalidOperationException("Contrato no encontrado.");
            if (string.Equals(contrato.Estado, "Finalizado", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("El contrato ya está finalizado.");
            if (contrato.Fecha_Fin > DateTime.Now)
                contrato.Fecha_Fin = DateTime.Now;
            contrato.Estado = "Finalizado";
            var contratos = _contratoMpr.GetAll();
            var index = contratos.FindIndex(c => c.IdContrato == contrato.IdContrato);
            if (index >= 0)
            {
                contratos[index] = contrato;
                _contratoMpr.GuardarTodos(contratos);
            }
            return true;
        }
    }
}
