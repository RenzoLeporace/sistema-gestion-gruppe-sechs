using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BE;

namespace Mapper
{
    public class FacturaMpr
    {
        private readonly string _path;

        public FacturaMpr()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory ?? Directory.GetCurrentDirectory();
            var dataDir = Path.Combine(baseDir, "Data");
            Directory.CreateDirectory(dataDir);
            _path = Path.Combine(dataDir, "facturas.xml");
        }

        public List<FacturaBE> GetAll()
        {
            return XMLHelper.Leer<FacturaBE>(_path);
        }
        public FacturaBE Add(FacturaBE nueva)
        {
            var lista = GetAll();
            var nextId = (lista.Count == 0) ? 1 : lista.Max(f => f.IdFactura) + 1;
            nueva.IdFactura = nextId;
            lista.Add(nueva);
            XMLHelper.Guardar(_path, lista);
            return nueva;
        }
        public List<FacturaBE> GetByContrato(int idContrato)
        {
            return GetAll().Where(f => f.IdContrato == idContrato).ToList();
        }
        public void GuardarTodos(List<FacturaBE> facturas)
        {
            XMLHelper.Guardar(_path, facturas);
        }
    }
}
