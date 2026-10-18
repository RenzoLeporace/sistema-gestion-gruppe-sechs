using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public class PersonalBLL
    {
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
        public PersonalBE Agregar(string nombre, string dni, string especialidad, IEnumerable<string> habilitaciones, bool disponible = true)
        {
            ValidarDatos(nombre, dni, especialidad);

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

            return _mpr.Add(nuevo);
        }
        public void Actualizar(int idPersonal, string nombre, string dni, string especialidad, IEnumerable<string> habilitaciones, bool disponible)
        {
            ValidarDatos(nombre, dni, especialidad);

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
    }
}
