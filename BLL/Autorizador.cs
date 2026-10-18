using BLL;
using BE;

namespace Seguridad
{
    public class Autorizador
    {
        public static bool UsuarioActualTienePermiso(string permisoCodigo)
        {
            var current = BLLUsuario.Instance.CurrentUser;
            return BLLManagerSeguridad.Instance.UsuarioTienePermiso(current, permisoCodigo);
        }
        public static bool AutorizarYMostrar(string permisoCodigo)
        {
            if (!UsuarioActualTienePermiso(permisoCodigo))
            {
                MessageBox.Show("No tiene permisos para realizar esta acción.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}
