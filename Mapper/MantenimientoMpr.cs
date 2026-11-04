using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BE;
using System.IO;

namespace Mapper
{
    public class MantenimientoMpr
    {
        private readonly string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "mantenimientos.xml");

        public List<MantenimientoBE> GetAll() => XMLHelper.Leer<MantenimientoBE>(_path);

        public MantenimientoBE? GetById(int id) => GetAll().FirstOrDefault(m => m.IdMantenimiento == id);

        public List<MantenimientoBE> GetByVehiculo(int idVehiculo)
        {
            return GetAll()
                .Where(m =>
                    m.IdVehiculo == idVehiculo ||
                    (m.IdVehiculo == 0 && ExtractIdFromDescripcion(m.Descripcion) == idVehiculo)
                )
                .ToList();
        }

        public MantenimientoBE Add(MantenimientoBE nuevo)
        {
            var lista = GetAll();
            var nextId = (lista.Count == 0) ? 1 : lista.Max(m => m.IdMantenimiento) + 1;
            nuevo.IdMantenimiento = nextId;
            lista.Add(nuevo);
            XMLHelper.Guardar(_path, lista);
            return nuevo;
        }

        public void Update(MantenimientoBE mant)
        {
            var lista = GetAll();
            var idx = lista.FindIndex(m => m.IdMantenimiento == mant.IdMantenimiento);
            if (idx >= 0)
            {
                lista[idx] = mant;
                XMLHelper.Guardar(_path, lista);
            }
        }

        private static int ExtractIdFromDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion)) return 0;
            var match = Regex.Match(descripcion, @"ID\s+(\d+)", RegexOptions.IgnoreCase);
            if (match.Success && int.TryParse(match.Groups[1].Value, out var id))
                return id;
            return 0;
        }
    }
}
