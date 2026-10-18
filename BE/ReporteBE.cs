using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public abstract class ReporteBE
    {
        public DateTime FechaGeneracion { get; set; }
        public string Titulo { get; set; }

        public ReporteBE(DateTime fechaGeneracion, string titulo)
        {
            FechaGeneracion = fechaGeneracion;
            Titulo = titulo;
        }
    }
}
