using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEUsuario
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public bool Bloqueado { get; set; }
        public bool Activo { get; set; } = true;
        public bool DebeCambiarClave { get; set; }

        List<BEComponente> _permisos;
        List<BERol> _roles;
        public BEUsuario()
        {
            _permisos = new List<BEComponente>();
            _roles = new List<BERol>();
        }
        public List<BEComponente> Permisos
        {
            get
            {
                return _permisos;
            }
        }
        public List<BERol> Roles
        {
            get
            {
                return _roles;
            }
        }
        public override string ToString()
        {
            return Nombre ?? string.Empty;
        }
    }
}