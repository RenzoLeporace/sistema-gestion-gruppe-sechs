using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BE;

namespace Mapper
{
    public class TipoServicioMpr
    {
        private readonly string _path;
        public TipoServicioMpr()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory ?? Directory.GetCurrentDirectory();
            var dataDir = Path.Combine(baseDir, "Data");
            Directory.CreateDirectory(dataDir);
            _path = Path.Combine(dataDir, "TiposServicio.xml");
        }
        public List<TipoServicioBE> GetAll()
        {
            var lista = XMLHelper.Leer<TipoServicioBE>(_path) ?? new List<TipoServicioBE>();
            var existentes = new HashSet<string>(
                lista.Select(l => (l.Descripcion ?? "").Trim().ToUpperInvariant())
            );
            var requeridos = new[] { "ESCOLTA", "EVENTO", "TRANSPORTE" };
            int nextId = (lista.Count == 0) ? 1 : lista.Max(t => t.IdTipoServicio) + 1;
            bool agrego = false;
            foreach (var desc in requeridos)
            {
                if (!existentes.Contains(desc))
                {
                    lista.Add(new TipoServicioBE(nextId++, desc, 0));
                    agrego = true;
                }
            }
            if (agrego)
                GuardarTodos(lista);
            return lista;
        }
        public void GuardarTodos(List<TipoServicioBE> lista)
        {
            XMLHelper.Guardar(_path, lista);
        }
        public TipoServicioBE? GetByDescripcion(string descripcion)
        {
            return GetAll().FirstOrDefault(t => t.Descripcion.Equals(descripcion, StringComparison.OrdinalIgnoreCase));
        }
    }
}
