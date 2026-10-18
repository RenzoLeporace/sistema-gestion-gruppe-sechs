using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace Mapper
{
    public class InformeOperativoMpr
    {
        private readonly string _path;

        public InformeOperativoMpr()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory ?? Directory.GetCurrentDirectory();
            var dataDir = Path.Combine(baseDir, "Data");
            Directory.CreateDirectory(dataDir);
            _path = Path.Combine(dataDir, "informes_operativos.xml");
        }

        public List<InformeOperativoBE> GetAll()
        {
            return XMLHelper.Leer<InformeOperativoBE>(_path);
        }
        public InformeOperativoBE Add(InformeOperativoBE nuevo)
        {
            var lista = GetAll();
            var nextId = (lista.Count == 0) ? 1 : lista.Max(i => i.IdInforme) + 1;
            nuevo.IdInforme = nextId;
            lista.Add(nuevo);
            XMLHelper.Guardar(_path, lista);
            return nuevo;
        }
    }
}
