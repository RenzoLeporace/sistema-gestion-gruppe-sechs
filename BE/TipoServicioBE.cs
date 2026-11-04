using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class TipoServicioBE
    {
        public int IdTipoServicio { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public double TarifaBase { get; set; }

        public TipoServicioBE() { }

        public TipoServicioBE(int idTipoServicio, string descripcion, double tarifaBase)
        {
            IdTipoServicio = idTipoServicio;
            Descripcion = descripcion;
            TarifaBase = tarifaBase;
        }
    }
}
