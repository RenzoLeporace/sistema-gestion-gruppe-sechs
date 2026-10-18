using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PersonalBE
    {
        public int IdPersonal { get; set; }
        public string Especialidad { get; set; }
        public string Nombre { get; set; }
        public string DNI { get; set; }
        public bool Estado_Disponibilidad { get; set; }
        public string Habilitaciones { get; set; }
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string TurnosDisponibles { get; set; } = string.Empty;
        public DateTime? VencimientoLicenciaArmas { get; set; }
        public string Capacitaciones { get; set; } = string.Empty;

        public PersonalBE() { }

        public PersonalBE(int idPersonal, string especialidad, string nombre, string dNI, bool estado_Disponibilidad, string habilitaciones)
        {
            IdPersonal = idPersonal;
            Especialidad = especialidad;
            Nombre = nombre;
            DNI = dNI;
            Estado_Disponibilidad = estado_Disponibilidad;
            Habilitaciones = habilitaciones;
        }
    }
}
