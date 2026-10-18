using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;
using Seguridad;

namespace BLL
{
    public sealed class BLLUsuario
    {
        private static readonly Lazy<BLLUsuario> _instance = new(() => new BLLUsuario());
        public static BLLUsuario Instance => _instance.Value;

        private readonly UsuarioMapper _mapper = new UsuarioMapper();
        private List<BEUsuario> _usuarios = new();

        public BEUsuario? CurrentUser { get; private set; }
        private BLLUsuario()
        {
            _usuarios = _mapper.ObtenerTodos() ?? new List<BEUsuario>();
            if (!_usuarios.Any(u => string.Equals(u.Nombre, "ADMIN", StringComparison.OrdinalIgnoreCase)))
            {
                var admin = new BEUsuario
                {
                    Nombre = "ADMIN",
                    PasswordEncrypted = Encriptador.Encrypt("ADMIN"),
                    Activo = true,
                    Bloqueado = false
                };
                var adminRol = BLLManagerSeguridad.Instance.GetRoles()
                    .FirstOrDefault(r => r.Nombre.Equals("Admin", StringComparison.OrdinalIgnoreCase));

                if (adminRol != null)
                {
                    admin.Roles.Add(adminRol);
                }
                _mapper.Guardar(admin);
                _usuarios = _mapper.ObtenerTodos() ?? new List<BEUsuario>();
            }
        }
        public IEnumerable<BEUsuario> GetUsuarios() => _mapper.ObtenerTodos() ?? new List<BEUsuario>();
        public BEUsuario? ObtenerPorNombre(string nombre)
        {
            if (string.IsNullOrEmpty(nombre)) return null;
            return GetUsuarios().FirstOrDefault(u => string.Equals(u.Nombre, nombre, StringComparison.OrdinalIgnoreCase));
        }
        public BEUsuario CrearUsuario(BEUsuario usuario, string password)
        {
            if (usuario == null) throw new ArgumentNullException(nameof(usuario));
            if (string.IsNullOrWhiteSpace(usuario.Nombre)) throw new ArgumentException("El nombre de usuario es requerido.", nameof(usuario));
            if (string.IsNullOrEmpty(password)) throw new ArgumentException("La contraseña es requerida.", nameof(password));
            if (ObtenerPorNombre(usuario.Nombre) != null)
                throw new InvalidOperationException("Ya existe un usuario con ese nombre.");
            usuario.PasswordEncrypted = Encriptador.Encrypt(password);
            _mapper.Guardar(usuario);
            _usuarios = _mapper.ObtenerTodos() ?? new List<BEUsuario>();
            return usuario;
        }
        public BEUsuario ModificarUsuario(BEUsuario usuario)
        {
            if (usuario == null) throw new ArgumentNullException(nameof(usuario));
            _mapper.Guardar(usuario);
            _usuarios = _mapper.ObtenerTodos() ?? new List<BEUsuario>();
            if (CurrentUser != null && CurrentUser.Id == usuario.Id)
            {
                CurrentUser = _mapper.ObtenerPorId(usuario.Id);
            }
            return usuario;
        }
        public bool EliminarUsuario(int id)
        {
            var existente = _mapper.ObtenerPorId(id);
            if (existente == null) return false;
            _mapper.Eliminar(id);
            _usuarios = _mapper.ObtenerTodos() ?? new List<BEUsuario>();
            return true;
        }
        public bool ValidarCredenciales(string nombre, string password)
        {
            nombre = nombre?.Trim();
            password ??= string.Empty;
            var usuario = ObtenerPorNombre(nombre);
            if (usuario == null) return false;
            if (usuario.Bloqueado || !usuario.Activo) return false;
            var almacenada = usuario.PasswordEncrypted;
            if (string.IsNullOrWhiteSpace(almacenada)) return false;
            string desencriptada;
            try
            {
                desencriptada = Encriptador.Decrypt(almacenada);
            }
            catch
            {
                return false;
            }
            if (!string.Equals(desencriptada, password, StringComparison.Ordinal))
                return false;
            CurrentUser = usuario;
            return true;
        }
        public void Logout()
        {
            CurrentUser = null;
        }
        public void AsignarRoles(BEUsuario usuario, IEnumerable<BERol> roles)
        {
            if (usuario == null) throw new ArgumentNullException(nameof(usuario));
            var usuarios = _mapper.ObtenerTodos() ?? new List<BEUsuario>();
            var u = usuarios.FirstOrDefault(x => x.Id == usuario.Id);
            if (u == null) return;
            var candidatos = (roles ?? Enumerable.Empty<BERol>())
                             .Where(r => r != null)
                             .GroupBy(r => r.Id)
                             .Select(g => g.First())
                             .ToList();
            static bool ContieneRecursivo(BERol parent, BERol child)
            {
                if (parent == null || child == null) return false;
                foreach (var h in parent.RolesHijos ?? new List<BERol>())
                {
                    if (h == null) continue;
                    if (h.Id != 0 && child.Id != 0 && h.Id == child.Id) return true;
                    if (ReferenceEquals(h, child)) return true;
                    if (ContieneRecursivo(h, child)) return true;
                }
                return false;
            }
            var finales = new List<BERol>();
            foreach (var r in candidatos)
            {
                var esDescendienteDeOtro = candidatos.Any(p => p != null && p != r && ContieneRecursivo(p, r));
                if (!esDescendienteDeOtro) finales.Add(r);
            }

            u.Roles.Clear();
            if (finales.Any())
                u.Roles.AddRange(finales);

            _mapper.Guardar(u);
            _usuarios = _mapper.ObtenerTodos() ?? new List<BEUsuario>();
        }

        private static IEnumerable<BEPermisoSimple> FlattenPermisosDeRol(BERol rol)
        {
            if (rol == null) yield break;
            foreach (var p in rol.Permisos ?? new List<BEPermisoSimple>())
                yield return p;
            foreach (var hijo in rol.RolesHijos ?? new List<BERol>())
            {
                foreach (var ph in FlattenPermisosDeRol(hijo))
                    yield return ph;
            }
        }
        public void AsignarPermisos(BEUsuario usuario, IEnumerable<BEPermisoSimple> permisos)
        {
            if (usuario == null) throw new ArgumentNullException(nameof(usuario));
            var usuarios = _mapper.ObtenerTodos() ?? new List<BEUsuario>();
            var u = usuarios.FirstOrDefault(x => x.Id == usuario.Id);
            if (u == null) return;
            var existentes = u.Permisos?.OfType<BEPermisoSimple>().ToList() ?? new List<BEPermisoSimple>();
            var nuevos = (permisos ?? Enumerable.Empty<BEPermisoSimple>()).ToList();
            var existentesIds = new HashSet<int>(existentes.Where(p => p.Id != 0).Select(p => p.Id));
            var duplicadosDirectos = nuevos.Where(p =>
                (p.Id != 0 && existentesIds.Contains(p.Id)) ||
                (p.Id == 0 && existentes.Any(e => e.Permiso.Equals(p.Permiso)))).ToList();
            if (duplicadosDirectos.Any())
            {
                var nombres = string.Join(", ", duplicadosDirectos.Select(p => p.Nombre ?? p.Permiso.ToString()));
                throw new InvalidOperationException($"El usuario ya posee los permisos (directos): {nombres}");
            }
            var permisosRol = (u.Roles ?? new List<BERol>())
                .SelectMany(FlattenPermisosDeRol)
                .GroupBy(p => p.Id != 0 ? p.Id.ToString() : $"T:{p.Permiso}")
                .Select(g => g.First())
                .ToList();

            var permisosRolIds = new HashSet<int>(permisosRol.Where(p => p.Id != 0).Select(p => p.Id));
            var permisosRolTipos = new HashSet<BETipoPermiso>(permisosRol.Select(p => p.Permiso));

            var duplicadosPorRol = nuevos.Where(p =>
                (p.Id != 0 && permisosRolIds.Contains(p.Id)) ||
                (p.Id == 0 && permisosRolTipos.Contains(p.Permiso))).ToList();

            if (duplicadosPorRol.Any())
            {
                var nombres = string.Join(", ", duplicadosPorRol.Select(p => p.Nombre ?? p.Permiso.ToString()));
                throw new InvalidOperationException($"No se puede asignar permisos directos que el usuario ya obtiene por sus roles: {nombres}");
            }
            var repetidosEnLote = nuevos.GroupBy(p => p.Id != 0 ? p.Id.ToString() : $"T:{p.Permiso}")
                                        .Where(g => g.Count() > 1)
                                        .Select(g => g.First()).ToList();
            if (repetidosEnLote.Any())
            {
                var nombres = string.Join(", ", repetidosEnLote.Select(p => p.Nombre ?? p.Permiso.ToString()));
                throw new InvalidOperationException($"Se intentó asignar el mismo permiso más de una vez en el lote: {nombres}");
            }

            var union = existentes.Concat(nuevos)
                                  .GroupBy(p => p.Id != 0 ? p.Id.ToString() : $"T:{p.Permiso}")
                                  .Select(g => g.First())
                                  .ToList();

            u.Permisos.Clear();
            if (union.Any())
                u.Permisos.AddRange(union.Cast<BEComponente>());

            _mapper.Guardar(u);
            _usuarios = _mapper.ObtenerTodos() ?? new List<BEUsuario>();
        }
        public void QuitarPermisos(BEUsuario usuario, IEnumerable<BEPermisoSimple> permisos)
        {
            if (usuario == null) throw new ArgumentNullException(nameof(usuario));
            var usuarios = _mapper.ObtenerTodos() ?? new List<BEUsuario>();
            var u = usuarios.FirstOrDefault(x => x.Id == usuario.Id);
            if (u == null) return;
            var removeIds = (permisos ?? Enumerable.Empty<BEPermisoSimple>()).Select(p => p.Id).ToHashSet();
            if (u.Permisos != null)
            {
                u.Permisos.RemoveAll(c => c is BEPermisoSimple ps && removeIds.Contains(ps.Id));
            }
            _mapper.Guardar(u);
            _usuarios = _mapper.ObtenerTodos() ?? new List<BEUsuario>();
        }
    }
}