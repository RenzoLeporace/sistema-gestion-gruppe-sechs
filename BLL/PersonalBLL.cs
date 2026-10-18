using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public class PersonalBLL
    {
        public sealed class DatosAdicionalesPersonal
        {
            public string Correo { get; set; } = string.Empty;
            public string Telefono { get; set; } = string.Empty;
            public string TurnosDisponibles { get; set; } = string.Empty;
            public DateTime? VencimientoLicenciaArmas { get; set; }
            public string Capacitaciones { get; set; } = string.Empty;
        }
        private readonly PersonalMpr _mpr = new PersonalMpr();

        private static readonly HashSet<string> EspecialidadesValidas = new(StringComparer.OrdinalIgnoreCase)
        {
            "ESCOLTA", "EVENTO", "TRANSPORTE"
        };
        public List<PersonalBE> Listar()
        {
            return _mpr.GetAll().OrderBy(p => p.Nombre).ToList();
        }
        public List<PersonalBE> BuscarPorCriterio(string criterio)
        {
            criterio = criterio?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(criterio))
                return Listar();

            return _mpr.GetAll()
                .Where(p => (p.Nombre ?? string.Empty).Contains(criterio, StringComparison.OrdinalIgnoreCase)
                         || (p.DNI ?? string.Empty).Contains(criterio, StringComparison.OrdinalIgnoreCase))
                .OrderBy(p => p.Nombre)
                .ToList();
        }
        public PersonalBE BuscarPorDniExacto(string dni)
        {
            dni = (dni ?? string.Empty).Trim();
            return _mpr.GetAll().FirstOrDefault(p => string.Equals(p.DNI, dni, StringComparison.OrdinalIgnoreCase));
        }
        public PersonalBE BuscarPorId(int idPersonal)
        {
            return _mpr.GetAll().FirstOrDefault(p => p.IdPersonal == idPersonal);
        }
        public PersonalBE Agregar(string nombre, string dni, string especialidad, IEnumerable<string> habilitaciones, bool disponible = true, DatosAdicionalesPersonal datos = null)
        {
            ValidarDatos(nombre, dni, especialidad);
            ValidarDatosAdicionales(datos, habilitaciones);

            if (BuscarPorDniExacto(dni) != null)
                throw new InvalidOperationException("El DNI ya existe, no se puede duplicar.");

            var habStr = string.Join("|", (habilitaciones ?? Array.Empty<string>()).Where(h => !string.IsNullOrWhiteSpace(h)).Distinct(StringComparer.OrdinalIgnoreCase));

            var nuevo = new PersonalBE
            {
                Nombre = nombre.Trim(),
                DNI = dni.Trim(),
                Especialidad = especialidad.Trim().ToUpperInvariant(),
                Habilitaciones = habStr,
                Estado_Disponibilidad = disponible
            };
            AplicarDatosAdicionales(nuevo, datos);

            return _mpr.Add(nuevo);
        }
        public void Actualizar(int idPersonal, string nombre, string dni, string especialidad, IEnumerable<string> habilitaciones, bool disponible, DatosAdicionalesPersonal datos = null)
        {
            ValidarDatos(nombre, dni, especialidad);
            ValidarDatosAdicionales(datos, habilitaciones);

            var todos = _mpr.GetAll();
            var existente = todos.FirstOrDefault(p => p.IdPersonal == idPersonal)
                ?? throw new InvalidOperationException("No se encontró el personal a actualizar.");

            var duplicado = todos.Any(p => p.IdPersonal != idPersonal && string.Equals(p.DNI, dni?.Trim(), StringComparison.OrdinalIgnoreCase));
            if (duplicado)
                throw new InvalidOperationException("El DNI ya está utilizado por otro registro.");

            existente.Nombre = nombre.Trim();
            existente.DNI = dni.Trim();
            existente.Especialidad = especialidad.Trim().ToUpperInvariant();
            existente.Habilitaciones = string.Join("|", (habilitaciones ?? Array.Empty<string>()).Where(h => !string.IsNullOrWhiteSpace(h)).Distinct(StringComparer.OrdinalIgnoreCase));
            existente.Estado_Disponibilidad = disponible;
            AplicarDatosAdicionales(existente, datos);

            _mpr.Update(existente);
        }
        public void Eliminar(int idPersonal)
        {
            _mpr.Delete(idPersonal);
        }
        public void MarcarDisponibilidad(int idPersonal, bool disponible)
        {
            var p = BuscarPorId(idPersonal) ?? throw new InvalidOperationException("Personal no encontrado.");
            Actualizar(p.IdPersonal, p.Nombre, p.DNI, p.Especialidad, (p.Habilitaciones ?? string.Empty).Split('|', StringSplitOptions.RemoveEmptyEntries), disponible);
        }
        private static void ValidarDatos(string nombre, string dni, string especialidad)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(dni))
                throw new ArgumentException("El DNI es obligatorio.");
            if (!dni.All(char.IsDigit))
                throw new ArgumentException("El DNI debe ser numérico.");
            if (string.IsNullOrWhiteSpace(especialidad) || !EspecialidadesValidas.Contains(especialidad.Trim().ToUpperInvariant()))
                throw new ArgumentException("Especialidad inválida. Debe ser: ESCOLTA, EVENTO o TRANSPORTE.");
        }
        private static void ValidarDatosAdicionales(DatosAdicionalesPersonal datos, IEnumerable<string> habilitaciones)
        {
            if (datos == null) return;
            if (!string.IsNullOrWhiteSpace(datos.Correo) &&
                !System.Net.Mail.MailAddress.TryCreate(datos.Correo.Trim(), out _))
                throw new ArgumentException("El correo electrónico no es válido.");
            if (datos.VencimientoLicenciaArmas.HasValue &&
                !(habilitaciones ?? Array.Empty<string>()).Any(h => string.Equals(h, "Permiso de Armas", StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException("La fecha de vencimiento requiere permiso de armas.");
        }
        private static void AplicarDatosAdicionales(PersonalBE personal, DatosAdicionalesPersonal datos)
        {
            if (datos == null) return;
            personal.Correo = datos.Correo?.Trim() ?? string.Empty;
            personal.Telefono = datos.Telefono?.Trim() ?? string.Empty;
            personal.TurnosDisponibles = datos.TurnosDisponibles?.Trim() ?? string.Empty;
            personal.VencimientoLicenciaArmas = datos.VencimientoLicenciaArmas;
            personal.Capacitaciones = datos.Capacitaciones?.Trim() ?? string.Empty;
        }
    }
}
