using System;

namespace BE
{
    public class ServicioBE
    {
        public string Estado { get; set; }
        public int IdServicio { get; set; }
        public string ValorServicio { get; set; }
        public DateTime Fecha_hora_inicio { get; set; }
        public DateTime Fecha_hora_fin { get; set; }

        public int IdContrato { get; set; }
        public string TipoServicio { get; set; }
        public int PersonalNecesario { get; set; }
        public int VehiculosNecesarios { get; set; }
        public int? IdInformeOperativo { get; set; }

        public ServicioBE() { }

        public ServicioBE(string estado, int idServicio, string valorServicio, DateTime fecha_hora_inicio, DateTime fecha_hora_fin,
                          int idContrato, string tipoServicio, int personalNecesario, int vehiculosNecesarios)
        {
            Estado = estado;
            IdServicio = idServicio;
            ValorServicio = valorServicio;
            Fecha_hora_inicio = fecha_hora_inicio;
            Fecha_hora_fin = fecha_hora_fin;
            IdContrato = idContrato;
            TipoServicio = tipoServicio;
            PersonalNecesario = personalNecesario;
            VehiculosNecesarios = vehiculosNecesarios;
        }
    }
}
