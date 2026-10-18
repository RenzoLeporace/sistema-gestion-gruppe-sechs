using System;
using System.Collections.Generic;

namespace BE
{
    public sealed class BEPermisoSimple : BEComponente
    {
        public BEPermisoSimple() { }

        public BEPermisoSimple(int id, string nombre, BETipoPermiso permiso)
        {
            Id = id;
            Nombre = nombre;
            Permiso = permiso;
        }

        public override IList<BEComponente> Hijos => Array.Empty<BEComponente>();

        public override void AgregarHijo(BEComponente c)
        {
            throw new InvalidOperationException("Un PermisoSimple no puede tener hijos.");
        }

        public override bool QuitarHijo(BEComponente c)
        {
            throw new InvalidOperationException("Un PermisoSimple no puede tener hijos.");
        }

        public override void VaciarHijos()
        {
        }

        public override string ToString() => Nombre ?? base.ToString();
    }
}
