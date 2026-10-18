using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BE;

namespace Mapper
{
    public class ContratoMpr
    {
        private readonly string _path;
        public ContratoMpr()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory ?? Directory.GetCurrentDirectory();
            var dataDir = Path.Combine(baseDir, "Data");
            Directory.CreateDirectory(dataDir);
            _path = Path.Combine(dataDir, "Contratos.xml");
        }
        public List<ContratoBE> GetAll()
        {
            return XMLHelper.Leer<ContratoBE>(_path) ?? new List<ContratoBE>();
        }
        public void GuardarTodos(List<ContratoBE> lista)
        {
            XMLHelper.Guardar(_path, lista ?? new List<ContratoBE>());
        }
        public ContratoBE Add(ContratoBE nuevo)
        {
            var lista = GetAll();
            nuevo.IdContrato = lista.Count == 0 ? 1 : lista.Max(c => c.IdContrato) + 1;
            lista.Add(nuevo);
            GuardarTodos(lista);
            return nuevo;
        }
    }
}
