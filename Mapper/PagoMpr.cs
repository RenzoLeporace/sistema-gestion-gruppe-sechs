using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace Mapper
{
    public class PagoMpr
    {
        private readonly string _path;

        public PagoMpr()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory ?? Directory.GetCurrentDirectory();
            var dataDir = Path.Combine(baseDir, "Data");
            Directory.CreateDirectory(dataDir);
            _path = Path.Combine(dataDir, "pagos.xml");
        }

        public List<PagoBE> GetAll()
        {
            return XMLHelper.Leer<PagoBE>(_path);
        }
        public PagoBE Add(PagoBE nuevo)
        {
            var lista = GetAll();
            var nextId = (lista.Count == 0) ? 1 : lista.Max(p => p.IdPago) + 1;
            nuevo.IdPago = nextId;
            lista.Add(nuevo);
            XMLHelper.Guardar(_path, lista);
            return nuevo;
        }
    }
}
