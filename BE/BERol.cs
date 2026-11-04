using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;

namespace BE
{
    public class BERol : BEComponente
    {
        public string Descripcion { get; set; } = string.Empty;

        public List<BEPermisoSimple> Permisos { get; set; } = new();

        public List<int> RolesHijosIds { get; set; } = new();

        [XmlIgnore]
        public List<BERol> RolesHijos { get; } = new();

        [XmlIgnore]
        public override IList<BEComponente> Hijos
        {
            get
            {
                var lista = new List<BEComponente>();
                if (Permisos != null) lista.AddRange(Permisos);
                if (RolesHijos != null) lista.AddRange(RolesHijos);
                return new ReadOnlyCollection<BEComponente>(lista);
            }
        }
        public override void AgregarHijo(BEComponente c)
        {
            if (c is null) throw new ArgumentNullException(nameof(c));
            if (c is BEPermisoSimple ps)
            {
                if (Permisos.Any(x => (ps.Id != 0 && x.Id == ps.Id) || x.Permiso == ps.Permiso))
                    return;
                foreach (var hijo in RolesHijos)
                {
                    var permisosHijo = ObtenerPermisosRecursivos(hijo);
                    if (ps.Id != 0)
                    {
                        if (permisosHijo.Any(p => p.Id == ps.Id))
                            throw new InvalidOperationException("No se puede agregar el permiso: ya existe en un rol hijo.");
                    }
                    else
                    {
                        if (permisosHijo.Any(p => p.Permiso == ps.Permiso))
                            throw new InvalidOperationException("No se puede agregar el permiso: ya existe en un rol hijo.");
                    }
                }

                Permisos.Add(ps);
                return;
            }
            if (c is BERol rolHijo)
            {
                if (ReferenceEquals(this, rolHijo) || (rolHijo.Id != 0 && rolHijo.Id == this.Id))
                    throw new InvalidOperationException("Un rol no puede contenerse a sí mismo.");
                if (RolesHijos.Any(r => (rolHijo.Id != 0 && r.Id == rolHijo.Id) || ReferenceEquals(r, rolHijo)))
                    return;
                if (CreaCiclo(rolHijo))
                    throw new InvalidOperationException("La operación produciría una referencia cíclica entre roles.");

                RolesHijos.Add(rolHijo);
                if (rolHijo.Id != 0 && !RolesHijosIds.Contains(rolHijo.Id))
                    RolesHijosIds.Add(rolHijo.Id);
                var permisosDelHijo = ObtenerPermisosRecursivos(rolHijo);
                foreach (var ph in permisosDelHijo)
                {
                    if (ph == null) continue;
                    if (ph.Id != 0)
                    {
                        Permisos.RemoveAll(p => p.Id == ph.Id);
                    }
                    else
                    {
                        Permisos.RemoveAll(p => p.Permiso == ph.Permiso);
                    }
                }

                return;
            }
            throw new NotSupportedException("Solo se admiten BEPermisoSimple y BERol como hijos de un rol.");
        }
        public override bool QuitarHijo(BEComponente c)
        {
            if (c is null) return false;
            if (c is BEPermisoSimple ps)
            {
                if (ps.Id != 0)
                    return Permisos.RemoveAll(x => x.Id == ps.Id) > 0;

                return Permisos.RemoveAll(x => x.Permiso == ps.Permiso) > 0;
            }
            if (c is BERol rolHijo)
            {
                var removed = false;
                if (rolHijo.Id != 0)
                {
                    RolesHijosIds.RemoveAll(x => x == rolHijo.Id);
                    removed = RolesHijos.RemoveAll(x => x.Id == rolHijo.Id) > 0;
                }
                else
                {
                    removed = RolesHijos.RemoveAll(x => ReferenceEquals(x, rolHijo)) > 0;
                }
                return removed;
            }
            return false;
        }
        public override void VaciarHijos()
        {
            Permisos?.Clear();
            RolesHijos?.Clear();
            RolesHijosIds?.Clear();
        }
        private bool CreaCiclo(BERol posibleHijo)
        {
            return ContieneRolRecursivo(posibleHijo, this);
        }
        private static bool ContieneRolRecursivo(BERol raiz, BERol objetivo)
        {
            if (raiz is null) return false;
            if (ReferenceEquals(raiz, objetivo)) return true;
            if (raiz.Id != 0 && objetivo.Id != 0 && raiz.Id == objetivo.Id) return true;
            foreach (var hijo in raiz.RolesHijos)
            {
                if (ContieneRolRecursivo(hijo, objetivo)) return true;
            }
            return false;
        }
        private List<BEPermisoSimple> ObtenerPermisosRecursivos(BERol rol)
        {
            var lista = new List<BEPermisoSimple>();
            if (rol is null) return lista;
            if (rol.Permisos != null) lista.AddRange(rol.Permisos);
            foreach (var hijo in rol.RolesHijos)
            {
                lista.AddRange(ObtenerPermisosRecursivos(hijo));
            }
            return lista;
        }
    }
}
