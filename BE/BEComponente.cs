using System;
using System.Collections.Generic;

namespace BE
{
    public abstract class BEComponente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public abstract IList<BEComponente> Hijos { get; }

        public abstract void AgregarHijo(BEComponente c);
        public abstract bool QuitarHijo(BEComponente c);
        public abstract void VaciarHijos();

        public BETipoPermiso Permiso { get; set; }
    }
}