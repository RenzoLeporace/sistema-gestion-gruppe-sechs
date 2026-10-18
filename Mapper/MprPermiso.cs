using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace Mapper
{
    public class MprPermiso
    {
        private readonly string _path;

        public MprPermiso()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory ?? Directory.GetCurrentDirectory();
            var dataDir = Path.Combine(baseDir, "Data");
            Directory.CreateDirectory(dataDir);
            _path = Path.Combine(dataDir, "permisos.xml");
        }

        public List<BEPermisoSimple> ObtenerTodos()
        {
            return XMLHelper.Leer<BEPermisoSimple>(_path);
        }
        public void GuardarTodos(List<BEPermisoSimple> permisos)
        {
            XMLHelper.Guardar(_path, permisos);
        }
        public void Guardar(BEPermisoSimple permiso)
        {
            var lista = ObtenerTodos();
            if (permiso == null) throw new ArgumentNullException(nameof(permiso));

            if (permiso.Id == 0)
            {
                permiso.Id = lista.Any() ? lista.Max(p => p.Id) + 1 : 1;
            }
            lista.RemoveAll(p => p.Id == permiso.Id);
            lista.Add(permiso);
            GuardarTodos(lista);
        }
        public void Eliminar(int id)
        {
            var lista = ObtenerTodos();
            lista.RemoveAll(p => p.Id == id);
            GuardarTodos(lista);
        }
        public BEPermisoSimple ObtenerPorId(int id)
        {
            return ObtenerTodos().FirstOrDefault(p => p.Id == id);
        }
    }
}
