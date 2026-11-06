using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace Mapper
{
    public class AsignacionVehiculoMpr
    {
        private readonly string _path;

        public AsignacionVehiculoMpr()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory ?? Directory.GetCurrentDirectory();
            var dataDir = Path.Combine(baseDir, "Data");
            Directory.CreateDirectory(dataDir);
            _path = Path.Combine(dataDir, "asignaciones_vehiculos.xml");
        }

        public List<AsignacionVehiculoBE> GetAll()
        {
            return XMLHelper.Leer<AsignacionVehiculoBE>(_path);
        }
        public AsignacionVehiculoBE Add(AsignacionVehiculoBE nueva)
        {
            var lista = GetAll();
            var nextId = (lista.Count == 0) ? 1 : lista.Max(a => a.IdAsignacionVehiculo) + 1;
            nueva.IdAsignacionVehiculo = nextId;
            lista.Add(nueva);
            XMLHelper.Guardar(_path, lista);
            return nueva;
        }
        public List<AsignacionVehiculoBE> GetByServicio(int idServicio)
        {
            return GetAll().Where(a => a.IdServicio == idServicio && a.IdServicio > 0).ToList();
        }
        public List<AsignacionVehiculoBE> GetByVehiculo(int idVehiculo)
        {
            return GetAll().Where(a => a.IdVehiculo == idVehiculo && a.IdVehiculo > 0).ToList();
        }
    }
}
