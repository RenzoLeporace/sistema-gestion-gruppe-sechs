using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BE;

namespace Mapper
{
    public class AsignacionPersonalMpr
    {
        private readonly string _path;

        public AsignacionPersonalMpr()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory ?? Directory.GetCurrentDirectory();
            var dataDir = Path.Combine(baseDir, "Data");
            Directory.CreateDirectory(dataDir);_path = Path.Combine(dataDir, "asignaciones_personal.xml");
        }

        public List<AsignacionPersonalBE> GetAll()
        {
            var lista = XMLHelper.Leer<AsignacionPersonalBE>(_path) ?? new List<AsignacionPersonalBE>();
            lista = lista
                .Where(a => !(a.IdPersonal == 0 &&
                              a.IdServicio == 0 &&
                              a.Inicio == default &&
                              a.Fin == default))
                .ToList();

            return lista;
        }

        public AsignacionPersonalBE Add(AsignacionPersonalBE nueva)
        {
            var lista = GetAll();
            var nextId = (lista.Count == 0) ? 1 : lista.Max(a => a.IdAsignacionPersonal) + 1;
            nueva.IdAsignacionPersonal = nextId;
            lista.Add(nueva);
            XMLHelper.Guardar(_path, lista);
            return nueva;
        }

        public List<AsignacionPersonalBE> GetByServicio(int idServicio)
        {
            return GetAll().Where(a => a.IdServicio == idServicio).ToList();
        }
        public void LimpiarBasura()
        {
            var lista = XMLHelper.Leer<AsignacionPersonalBE>(_path) ?? new List<AsignacionPersonalBE>();
            var validas = lista.Where(a =>
                a.IdPersonal > 0 &&
                a.IdServicio > 0 &&
                a.Inicio != default &&
                a.Fin != default &&
                a.Inicio < a.Fin).ToList();
            XMLHelper.Guardar(_path, validas);
        }
    }
}
