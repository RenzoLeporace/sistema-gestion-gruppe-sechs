using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BE;

namespace Mapper
{
    public class RolMpr
    {
        private readonly string _path;

        public RolMpr()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory ?? Directory.GetCurrentDirectory();
            var dataDir = Path.Combine(baseDir, "Data");
            Directory.CreateDirectory(dataDir);
            _path = Path.Combine(dataDir, "roles.xml");
        }

        public List<BERol> ObtenerTodos()
        {
            var roles = XMLHelper.Leer<BERol>(_path) ?? new List<BERol>();
            HidratarRolesHijos(roles);
            return roles;
        }
        public void Guardar(BERol rol)
        {
            if (rol == null) throw new ArgumentNullException(nameof(rol));

            var lista = XMLHelper.Leer<BERol>(_path) ?? new List<BERol>();

            if (rol.Id == 0)
            {
                rol.Id = lista.Any() ? lista.Max(r => r.Id) + 1 : 1;
            }
            SincronizarIdsDesdeReferencias(rol);
            lista.RemoveAll(r => r.Id == rol.Id);
            lista.Add(rol);
            XMLHelper.Guardar(_path, lista);
        }
        public void Eliminar(int id)
        {
            var lista = XMLHelper.Leer<BERol>(_path) ?? new List<BERol>();
            lista.RemoveAll(r => r.Id == id);
            XMLHelper.Guardar(_path, lista);
        }
        public BERol? ObtenerPorId(int id)
        {
            return ObtenerTodos().FirstOrDefault(r => r.Id == id);
        }
        private static void HidratarRolesHijos(List<BERol> roles)
        {
            var byId = roles.Where(r => r != null && r.Id != 0)
                            .ToDictionary(r => r.Id, r => r);
            foreach (var rol in roles)
            {
                rol.RolesHijos.Clear();

                var hijosIds = rol.RolesHijosIds?.Distinct().ToList() ?? new List<int>();
                foreach (var id in hijosIds)
                {
                    if (id == 0) continue;
                    if (!byId.TryGetValue(id, out var hijo)) continue;
                    if (ReferenceEquals(hijo, rol)) continue;

                    if (!rol.RolesHijos.Any(r => r.Id == hijo.Id))
                        rol.RolesHijos.Add(hijo);
                }
            }
        }
        private static void SincronizarIdsDesdeReferencias(BERol rol)
        {
            var ids = (rol.RolesHijos ?? new List<BERol>())
                      .Where(r => r != null && r.Id != 0)
                      .Select(r => r.Id)
                      .Distinct()
                      .ToList();

            rol.RolesHijosIds = ids;
        }
    }
}
