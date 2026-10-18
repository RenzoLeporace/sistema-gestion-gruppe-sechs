using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public class AsignacionPersonalBLL
    {
        private readonly AsignacionPersonalMpr _asigMpr = new AsignacionPersonalMpr();
        private readonly ServicioBLL _servicioBll = new ServicioBLL();
        private readonly PersonalBLL _personalBll = new PersonalBLL();

        public int ContarAsignados(int idServicio)
        {
            return _asigMpr.GetByServicio(idServicio).Count;
        }

        public void SincronizarDisponibilidad(DateTime? ahora = null)
        {
            var now = ahora ?? DateTime.Now;
            var asignaciones = _asigMpr.GetAll();
            var personas = _personalBll.Listar();

            foreach (var p in personas)
            {
                var ocupado = asignaciones.Any(a =>
                    a.IdPersonal == p.IdPersonal &&
                    a.Inicio <= now &&
                    now <= a.Fin);

                var deseadoDisponible = !ocupado;
                if (p.Estado_Disponibilidad != deseadoDisponible)
                {
                    _personalBll.MarcarDisponibilidad(p.IdPersonal, deseadoDisponible);
                }
            }
        }
        private bool TieneSolapamientoPersonal(int idPersonal, DateTime inicio, DateTime fin)
        {
            return _asigMpr.GetAll().Any(a =>
                a.IdPersonal == idPersonal &&
                a.Inicio < fin &&
                inicio < a.Fin);
        }

        public List<ServicioBE> ListarServiciosQueRequierenPersonal(DateTime? ahora = null)
        {
            SincronizarDisponibilidad(ahora);

            var now = ahora ?? DateTime.Now;
            var margenPasado = now.AddHours(-24);
            var servicios = _servicioBll.Listar()
                .Where(s => s.Fecha_hora_fin >= margenPasado)
                .Where(s =>
                {
                    var asignados = ContarAsignados(s.IdServicio);
                    if (s.PersonalNecesario <= 0)
                        return asignados == 0;
                    return s.PersonalNecesario > asignados;
                })
                .OrderBy(s => s.Fecha_hora_inicio)
                .ToList();

            return servicios;
        }

        public List<PersonalBE> ListarDisponiblesPorEspecialidad(string tipoServicio)
        {
            SincronizarDisponibilidad();

            var tipo = (tipoServicio ?? string.Empty).Trim().ToUpperInvariant();
            return _personalBll.Listar()
                .Where(p => p.Estado_Disponibilidad)
                .Where(p => string.Equals((p.Especialidad ?? string.Empty).Trim().ToUpperInvariant(), tipo, StringComparison.OrdinalIgnoreCase))
                .OrderBy(p => p.Nombre)
                .ToList();
        }

        public List<PersonalBE> ListarRecomendadosPorHabilitaciones(string tipoServicio)
        {
            SincronizarDisponibilidad();

            var tipo = (tipoServicio ?? string.Empty).Trim().ToUpperInvariant();

            bool Cumple(PersonalBE p)
            {
                var toks = (p.Habilitaciones ?? string.Empty)
                    .Split(new[] { '|', ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(t => t.Trim().ToUpperInvariant());

                return tipo switch
                {
                    "TRANSPORTE" => toks.Any(t => t.Contains("CARNET") || t.Contains("CONDUC")),
                    "ESCOLTA" => toks.Any(t => t.Contains("ARMA")),
                    "EVENTO" => toks.Any(t => t.Contains("AUXILIO") || t.Contains("PRIMER") || t.Contains("RCP")),
                    _ => false
                };
            }
            return _personalBll.Listar()
                .Where(p => p.Estado_Disponibilidad)
                .Where(Cumple)
                .OrderBy(p => p.Nombre)
                .ToList();
        }

        public void Asignar(int idServicio, int idPersonal)
        {
            var servicio = _servicioBll.Listar().FirstOrDefault(s => s.IdServicio == idServicio)
                ?? throw new InvalidOperationException("Servicio no encontrado.");
            var persona = _personalBll.BuscarPorId(idPersonal)
                ?? throw new InvalidOperationException("Personal no encontrado.");
            if (TieneSolapamientoPersonal(idPersonal, servicio.Fecha_hora_inicio, servicio.Fecha_hora_fin))
                throw new InvalidOperationException("El personal ya tiene otra asignación que se solapa en el mismo horario.");

            SincronizarDisponibilidad();

            if (!persona.Estado_Disponibilidad)
                throw new InvalidOperationException("El personal seleccionado no está disponible en este momento.");

            var yaAsignado = _asigMpr.GetByServicio(idServicio).Any(a => a.IdPersonal == idPersonal);
            if (yaAsignado)
                throw new InvalidOperationException("El personal ya está asignado a este servicio.");

            var asignados = ContarAsignados(idServicio);
            var restantes = servicio.PersonalNecesario - asignados;
            if (servicio.PersonalNecesario > 0 && restantes <= 0)
                throw new InvalidOperationException("El servicio ya no requiere más personal.");

            var tipo = (servicio.TipoServicio ?? string.Empty).Trim().ToUpperInvariant();
            var especialidadMatch = string.Equals((persona.Especialidad ?? string.Empty).Trim().ToUpperInvariant(), tipo, StringComparison.OrdinalIgnoreCase);
            var habilitacionMatch = !especialidadMatch && ListarRecomendadosPorHabilitaciones(tipo).Any(p => p.IdPersonal == persona.IdPersonal);

            if (!especialidadMatch && !habilitacionMatch)
                throw new InvalidOperationException("El personal no cumple la especialidad ni habilitaciones requeridas.");

            var asignacion = new AsignacionPersonalBE(
                idServicio: servicio.IdServicio,
                idPersonal: persona.IdPersonal,
                inicio: servicio.Fecha_hora_inicio,
                fin: servicio.Fecha_hora_fin,
                especialidadAsignacion: persona.Especialidad,
                criterio: especialidadMatch ? "Especialidad" : "Habilitacion"
            );

            _asigMpr.Add(asignacion);
            _personalBll.MarcarDisponibilidad(persona.IdPersonal, disponible: false);
        }

        public List<AsignacionPersonalBE> ListarAsignaciones()
        {
            return _asigMpr.GetAll();
        }
    }
}
