using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace Mapper
{
    public class PersonalMpr
    {
        private readonly string _path;

        public PersonalMpr()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory ?? Directory.GetCurrentDirectory();
            var dataDir = Path.Combine(baseDir, "Data");
            Directory.CreateDirectory(dataDir);
            _path = Path.Combine(dataDir, "personal.xml");
        }

        public List<PersonalBE> GetAll()
        {
            return XMLHelper.Leer<PersonalBE>(_path);
        }
        public PersonalBE Add(PersonalBE nuevo)
        {
            var lista = GetAll();
            var nextId = (lista.Count == 0) ? 1 : lista.Max(s => s.IdPersonal) + 1;
            nuevo.IdPersonal = nextId;
            lista.Add(nuevo);
            XMLHelper.Guardar(_path, lista);
            return nuevo;
        }
        public void GuardarTodos(List<PersonalBE> lista)
        {
            XMLHelper.Guardar(_path, lista ?? new List<PersonalBE>());
        }
        public void Update(PersonalBE actualizado)
        {
            var lista = GetAll();
            var idx = lista.FindIndex(p => p.IdPersonal == actualizado.IdPersonal);
            if (idx >= 0)
            {
                lista[idx] = actualizado;
                XMLHelper.Guardar(_path, lista);
            }
        }
        public void Delete(int idPersonal)
        {
            var lista = GetAll();
            lista.RemoveAll(p => p.IdPersonal == idPersonal);
            XMLHelper.Guardar(_path, lista);
        }
    }
}
