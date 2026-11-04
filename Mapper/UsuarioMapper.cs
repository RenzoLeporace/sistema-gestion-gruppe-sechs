using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BE;

namespace Mapper
{
    public class UsuarioMapper
    {
        private readonly string _path;
        private readonly RolMpr _rolMpr = new RolMpr();
        private readonly MprPermiso _permMpr = new MprPermiso();
        public class UsuarioDto
        {
            public int Id { get; set; }
            public string? Nombre { get; set; }
            public string PasswordEncrypted { get; set; } = string.Empty;
            public List<int> RolesIds { get; set; } = new();
            public bool Bloqueado { get; set; }
            public bool Activo { get; set; } = true;
            public bool DescifrarClave { get; set; }

            public List<int> PermisosIds { get; set; } = new();
        }

        public UsuarioMapper()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory ?? Directory.GetCurrentDirectory();
            var dataDir = Path.Combine(baseDir, "Data");
            Directory.CreateDirectory(dataDir);
            _path = Path.Combine(dataDir, "usuarios.xml");
        }

        public List<BEUsuario> ObtenerTodos()
        {
            var dtos = XMLHelper.Leer<UsuarioDto>(_path) ?? new List<UsuarioDto>();
            var roles = _rolMpr.ObtenerTodos() ?? new List<BERol>();
            var permisos = _permMpr.ObtenerTodos() ?? new List<BEPermisoSimple>();
            var usuarios = new List<BEUsuario>();

            foreach (var dto in dtos)
            {
                var u = new BEUsuario
                {
                    Id = dto.Id,
                    Nombre = dto.Nombre,
                    PasswordEncrypted = dto.PasswordEncrypted,
                    Bloqueado = dto.Bloqueado,
                    Activo = dto.Activo,
                    DescifrarClave = dto.DescifrarClave
                };
                var rolesAsignados = roles.Where(r => dto.RolesIds != null && dto.RolesIds.Contains(r.Id)).ToList();
                if (rolesAsignados != null && rolesAsignados.Any())
                    u.Roles.AddRange(rolesAsignados);
                var permisosAsignados = permisos.Where(p => dto.PermisosIds != null && dto.PermisosIds.Contains(p.Id)).ToList();
                if (permisosAsignados != null && permisosAsignados.Any())
                    u.Permisos.AddRange(permisosAsignados.Cast<BEComponente>());
                usuarios.Add(u);
            }
            return usuarios;
        }
        public void Guardar(BEUsuario usuario)
        {
            if (usuario == null) throw new ArgumentNullException(nameof(usuario));

            var lista = XMLHelper.Leer<UsuarioDto>(_path) ?? new List<UsuarioDto>();

            if (usuario.Id == 0)
            {
                usuario.Id = lista.Any() ? lista.Max(u => u.Id) + 1 : 1;
            }
            var dto = new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                PasswordEncrypted = usuario.PasswordEncrypted,
                RolesIds = usuario.Roles?.Select(r => r.Id).ToList() ?? new List<int>(),
                Bloqueado = usuario.Bloqueado,
                Activo = usuario.Activo,
                DescifrarClave = usuario.DescifrarClave,
                PermisosIds = usuario.Permisos?.OfType<BEPermisoSimple>().Select(p => p.Id).ToList() ?? new List<int>()
            };

            lista.RemoveAll(u => u.Id == dto.Id);
            lista.Add(dto);
            XMLHelper.Guardar(_path, lista);
        }
        public void Eliminar(int id)
        {
            var lista = XMLHelper.Leer<UsuarioDto>(_path) ?? new List<UsuarioDto>();
            lista.RemoveAll(u => u.Id == id);
            XMLHelper.Guardar(_path, lista);
        }
        public BEUsuario? ObtenerPorId(int id)
        {
            var dtos = XMLHelper.Leer<UsuarioDto>(_path) ?? new List<UsuarioDto>();
            var dto = dtos.FirstOrDefault(d => d.Id == id);
            if (dto == null) return null;

            var roles = _rolMpr.ObtenerTodos().Where(r => dto.RolesIds != null && dto.RolesIds.Contains(r.Id)).ToList();
            var permisos = _permMpr.ObtenerTodos().Where(p => dto.PermisosIds != null && dto.PermisosIds.Contains(p.Id)).ToList();

            var u = new BEUsuario { Id = dto.Id, Nombre = dto.Nombre, PasswordEncrypted = dto.PasswordEncrypted, Bloqueado = dto.Bloqueado, Activo = dto.Activo, DescifrarClave = dto.DescifrarClave };
            if (roles != null && roles.Any())
                u.Roles.AddRange(roles);
            if (permisos != null && permisos.Any())
                u.Permisos.AddRange(permisos.Cast<BEComponente>());

            return u;
        }
    }
}