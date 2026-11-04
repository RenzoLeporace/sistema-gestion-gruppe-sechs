using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public class BLLManagerSeguridad
    {
        private static readonly Lazy<BLLManagerSeguridad> _instance = new(() => new BLLManagerSeguridad());
        public static BLLManagerSeguridad Instance => _instance.Value;

        private List<BERol> _roles = new();
        private List<BEPermisoSimple> _permisos = new();

        private readonly RolMpr _rolMpr = new RolMpr();
        private readonly MprPermiso _permMpr = new MprPermiso();

        private BLLManagerSeguridad()
        {
            _permisos = _permMpr.ObtenerTodos() ?? new List<BEPermisoSimple>();
            _roles = _rolMpr.ObtenerTodos() ?? new List<BERol>();
            var allTipos = Enum.GetValues(typeof(BETipoPermiso)).Cast<BETipoPermiso>();
            var anyAdded = false;
            foreach (var tp in allTipos)
            {
                if (!_permisos.Any(p => p.Permiso == tp))
                {
                    var nuevo = new BEPermisoSimple
                    {
                        Id = 0,
                        Nombre = tp.ToString(),
                        Permiso = tp
                    };
                    _permMpr.Guardar(nuevo);
                    anyAdded = true;
                }
            }
            if (anyAdded)
            {
                _permisos = _permMpr.ObtenerTodos() ?? new List<BEPermisoSimple>();
            }
            if (!_roles.Any())
            {
                var admin = new BERol
                {
                    Id = 0,
                    Nombre = "Admin",
                    Descripcion = "Rol administrador con todos los permisos",
                    Permisos = new List<BEPermisoSimple>(_permisos)
                };
                _rolMpr.Guardar(admin);
                _roles = _rolMpr.ObtenerTodos() ?? new List<BERol>();
            }
        }
        public IEnumerable<BERol> GetRoles() => _rolMpr.ObtenerTodos() ?? new List<BERol>();
        public IEnumerable<BEPermisoSimple> GetPermisos() => _permMpr.ObtenerTodos() ?? new List<BEPermisoSimple>();

        public bool UsuarioTienePermiso(BEUsuario? usuario, BETipoPermiso permiso)
        {
            if (usuario == null) return false;
            var directos = (usuario.Permisos ?? new List<BEComponente>())
                .SelectMany(FlattenPermisosDeComponente)
                .Any(p => p.Permiso == permiso);
            if (directos) return true;
            var porRol = (usuario.Roles ?? new List<BERol>())
                .SelectMany(PermisosDeRol)
                .Any(p => p.Permiso == permiso);

            return porRol;
        }
        public bool UsuarioTienePermiso(BEUsuario? usuario, string permisoCodigo)
        {
            if (usuario == null) return false;
            if (string.IsNullOrWhiteSpace(permisoCodigo)) return false;
            if (Enum.TryParse<BETipoPermiso>(permisoCodigo, true, out var permisoEnum))
            {
                return UsuarioTienePermiso(usuario, permisoEnum);
            }
            bool tieneDirectoPorNombre = (usuario.Permisos ?? new List<BEComponente>())
                .SelectMany(FlattenPermisosDeComponente)
                .Any(p => string.Equals(p.Nombre, permisoCodigo, StringComparison.OrdinalIgnoreCase));
            if (tieneDirectoPorNombre) return true;

            return (usuario.Roles ?? new List<BERol>())
                .SelectMany(PermisosDeRol)
                .Any(p => string.Equals(p.Nombre, permisoCodigo, StringComparison.OrdinalIgnoreCase));
        }
        private static IEnumerable<BEPermisoSimple> PermisosDeRol(BERol rol)
        {
            if (rol == null) return Enumerable.Empty<BEPermisoSimple>();
            return (rol.Hijos ?? Array.Empty<BEComponente>())
                .SelectMany(FlattenPermisosDeComponente)
                .GroupBy(p => p.Permiso)
                .Select(g => g.First());
        }
        private static IEnumerable<BEPermisoSimple> FlattenPermisosDeComponente(BEComponente c)
        {
            if (c == null) yield break;

            if (c is BEPermisoSimple ps)
            {
                yield return ps;
                yield break;
            }
            foreach (var h in c.Hijos ?? Array.Empty<BEComponente>())
            {
                foreach (var inner in FlattenPermisosDeComponente(h))
                    yield return inner;
            }
        }
        public BERol CrearRol(BERol rol)
        {
            if (rol == null) throw new ArgumentNullException(nameof(rol));
            rol.Id = 0;
            _rolMpr.Guardar(rol);
            _roles = _rolMpr.ObtenerTodos() ?? new List<BERol>();
            return rol;
        }
        public BERol? ModificarRol(BERol rol)
        {
            if (rol == null) throw new ArgumentNullException(nameof(rol));
            var existente = _rolMpr.ObtenerPorId(rol.Id);
            if (existente == null) return null;
            _rolMpr.Guardar(rol);
            _roles = _rolMpr.ObtenerTodos() ?? new List<BERol>();
            return rol;
        }
        public bool EliminarRol(int id)
        {
            var existente = _rolMpr.ObtenerPorId(id);
            if (existente == null) return false;
            _rolMpr.Eliminar(id);
            _roles = _rolMpr.ObtenerTodos() ?? new List<BERol>();
            return true;
        }
        public bool AsociarPermisoARol(int rolId, int permisoId)
        {
            var rol = _rolMpr.ObtenerPorId(rolId);
            var permiso = _permMpr.ObtenerPorId(permisoId);
            if (rol == null || permiso == null) return false;

            rol.Permisos ??= new List<BEPermisoSimple>();
            if (rol.Permisos.Any(p => p.Id == permisoId)) return false;
            rol.Permisos.Add(new BEPermisoSimple { Id = permiso.Id, Nombre = permiso.Nombre, Permiso = permiso.Permiso });
            _rolMpr.Guardar(rol);
            var todos = _rolMpr.ObtenerTodos() ?? new List<BERol>();
            static bool ContieneRecursivo(BERol root, BERol target)
            {
                if (root == null || target == null) return false;
                foreach (var h in root.RolesHijos ?? new List<BERol>())
                {
                    if (h == null) continue;
                    if (h.Id != 0 && target.Id != 0 && h.Id == target.Id) return true;
                    if (ReferenceEquals(h, target)) return true;
                    if (ContieneRecursivo(h, target)) return true;
                }
                return false;
            }

            foreach (var posiblePadre in todos)
            {
                if (posiblePadre == null) continue;
                if (posiblePadre.Id == rol.Id) continue;
                if (!ContieneRecursivo(posiblePadre, rol)) continue;

                if (posiblePadre.Permisos == null) continue;

                var removed = false;
                if (permiso.Id != 0)
                    removed = posiblePadre.Permisos.RemoveAll(p => p.Id == permiso.Id) > 0;
                else
                    removed = posiblePadre.Permisos.RemoveAll(p => p.Permiso == permiso.Permiso) > 0;

                if (removed)
                {
                    _rolMpr.Guardar(posiblePadre);
                }
            }

            _roles = _rolMpr.ObtenerTodos() ?? new List<BERol>();
            return true;
        }
        public bool QuitarPermisoDeRol(int rolId, int permisoId)
        {
            var rol = _rolMpr.ObtenerPorId(rolId);
            if (rol == null || rol.Permisos == null) return false;
            var removed = rol.Permisos.RemoveAll(p => p.Id == permisoId) > 0;
            if (removed) _rolMpr.Guardar(rol);
            _roles = _rolMpr.ObtenerTodos() ?? new List<BERol>();
            return removed;
        }
        public bool AsociarRolARol(int rolPadreId, int rolHijoId)
        {
            var padre = GetRoles().FirstOrDefault(r => r.Id == rolPadreId);
            var hijo  = GetRoles().FirstOrDefault(r => r.Id == rolHijoId);
            if (padre == null || hijo == null) return false;

            if (padre.RolesHijos != null && padre.RolesHijos.Any(r => r.Id == rolHijoId))
                throw new InvalidOperationException($"El rol '{hijo.Nombre}' ya está asignado como hijo de '{padre.Nombre}'.");

            try
            {
                padre.AgregarHijo(hijo);
                _rolMpr.Guardar(padre);
                return true;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No se pudo asociar el rol hijo al rol padre.", ex);
            }
        }
        public bool QuitarRolHijo(int rolPadreId, int rolHijoId)
        {
            var padre = GetRoles().FirstOrDefault(r => r.Id == rolPadreId);
            if (padre == null) return false;
            var hijo = padre.RolesHijos.FirstOrDefault(r => r.Id == rolHijoId);
            if (hijo == null) return false;
            var ok = padre.QuitarHijo(hijo);
            if (ok) _rolMpr.Guardar(padre);
            return ok;
        }
    }
}