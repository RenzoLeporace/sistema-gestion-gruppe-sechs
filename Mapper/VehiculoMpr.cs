using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BE;

namespace Mapper
{
    public class VehiculoMpr
    {
        private readonly string _path;

        public VehiculoMpr()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory ?? Directory.GetCurrentDirectory();
            var dataDir = Path.Combine(baseDir, "Data");
            Directory.CreateDirectory(dataDir);
            _path = Path.Combine(dataDir, "vehiculos.xml");
        }

        public List<VehiculoBE> GetAll()
        {
            return XMLHelper.Leer<VehiculoBE>(_path);
        }

        public VehiculoBE GetById(int id)
        {
            return GetAll().FirstOrDefault(v => v.IdVehiculo == id);
        }
        public VehiculoBE GetByPatente(string patente)
        {
            var p = (patente ?? string.Empty).Trim();
            return GetAll().FirstOrDefault(v =>
                string.Equals((v.Patente ?? string.Empty).Trim(), p, StringComparison.OrdinalIgnoreCase));
        }
        public VehiculoBE Add(VehiculoBE nuevo)
        {
            var lista = GetAll();
            var nextId = (lista.Count == 0) ? 1 : lista.Max(v => v.IdVehiculo) + 1;
            nuevo.IdVehiculo = nextId;
            lista.Add(nuevo);
            GuardarTodos(lista);
            return nuevo;
        }
        public void Update(VehiculoBE vehiculo)
        {
            var lista = GetAll();
            var idx = lista.FindIndex(v => v.IdVehiculo == vehiculo.IdVehiculo);
            if (idx >= 0)
            {
                lista[idx] = vehiculo;
                GuardarTodos(lista);
            }
        }
        public void Delete(int id)
        {
            var lista = GetAll();
            var idx = lista.FindIndex(v => v.IdVehiculo == id);
            if (idx >= 0)
            {
                lista.RemoveAt(idx);
                GuardarTodos(lista);
            }
        }
        public void GuardarTodos(List<VehiculoBE> lista)
        {
            XMLHelper.Guardar(_path, lista ?? new List<VehiculoBE>());
        }
    }
}
