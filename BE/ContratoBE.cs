using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ContratoBE
    {
        public int IdContrato { get; set; }
        public int IdCliente { get; set; }
        public string DNI_CUIL_Cliente { get; set; } = string.Empty;
        public double Costo_Estimado { get; set; }
        public DateTime Fecha_Inicio { get; set; }
        public DateTime Fecha_Fin { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Tipo_Servicio { get; set; } = string.Empty;
        public string Recursos_Estimados { get; set; } = string.Empty;

        public ContratoBE() { }

        public ContratoBE(int idContrato, int idCliente, string dniCuilCliente, double costoEstimado,
                          DateTime fechaInicio, DateTime fechaFin, string estado, string tipoServicio, string recursosEstimados)
        {
            IdContrato = idContrato;
            IdCliente = idCliente;
            DNI_CUIL_Cliente = dniCuilCliente;
            Costo_Estimado = costoEstimado;
            Fecha_Inicio = fechaInicio;
            Fecha_Fin = fechaFin;
            Estado = estado;
            Tipo_Servicio = tipoServicio;
            Recursos_Estimados = recursosEstimados;
        }
    }
}
