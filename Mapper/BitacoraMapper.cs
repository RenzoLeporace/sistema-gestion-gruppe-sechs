using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BE;

namespace Mapper
{
    public class BitacoraMapper
    {
        private readonly string _path;

        public BitacoraMapper()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory ?? Directory.GetCurrentDirectory();
            var dataDir = Path.Combine(baseDir, "Data");
            Directory.CreateDirectory(dataDir);
            _path = Path.Combine(dataDir, "bitacora.xml");
        }

        public List<BitacoraEntrada> ObtenerTodos()
        {
            return XMLHelper.Leer<BitacoraEntrada>(_path) ?? new List<BitacoraEntrada>();
        }
        public void GuardarTodos(List<BitacoraEntrada> entradas)
        {
            XMLHelper.Guardar(_path, entradas ?? new List<BitacoraEntrada>());
        }
        public void Agregar(BitacoraEntrada entrada)
        {
            if (entrada == null) throw new ArgumentNullException(nameof(entrada));
            var lista = ObtenerTodos();
            if (entrada.Id == 0)
            {
                entrada.Id = lista.Any() ? lista.Max(e => e.Id) + 1 : 1;
            }
            lista.Add(entrada);
            GuardarTodos(lista);
        }
        public void Limpiar()
        {
            GuardarTodos(new List<BitacoraEntrada>());
        }
    }
}
