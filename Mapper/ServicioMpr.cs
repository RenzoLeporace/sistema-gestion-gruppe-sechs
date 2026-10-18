using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BE;

namespace Mapper
{
    public class ServicioMpr
    {
        private readonly string _path;

        public ServicioMpr()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory ?? Directory.GetCurrentDirectory();
            var dataDir = Path.Combine(baseDir, "Data");
            Directory.CreateDirectory(dataDir);
            _path = Path.Combine(dataDir, "servicios.xml");
        }

        public List<ServicioBE> GetAll()
        {
            return XMLHelper.Leer<ServicioBE>(_path);
        }
        public ServicioBE Add(ServicioBE nuevo)
        {
            var lista = GetAll();
            var nextId = (lista.Count == 0) ? 1 : lista.Max(s => s.IdServicio) + 1;
            nuevo.IdServicio = nextId;
            lista.Add(nuevo);
            XMLHelper.Guardar(_path, lista);
            return nuevo;
        }
        public ServicioBE GetById(int id)
        {
            return GetAll().FirstOrDefault(s => s.IdServicio == id);
        }
        public void Update(ServicioBE servicio)
        {
            var lista = GetAll();
            var idx = lista.FindIndex(s => s.IdServicio == servicio.IdServicio);
            if (idx >= 0)
            {
                lista[idx] = servicio;
                XMLHelper.Guardar(_path, lista);
            }
        }
    }
}
