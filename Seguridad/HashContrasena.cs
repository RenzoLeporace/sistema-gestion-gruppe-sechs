using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Seguridad
{
    /// <summary>
    /// Calcula y verifica hashes de contraseñas con PBKDF2-HMAC-SHA256 y una sal aleatoria por contraseña.
    /// El hash no se puede revertir: para validar un inicio de sesión se vuelve a calcular y se compara.
    /// Formato guardado: <c>PBKDF2-SHA256$iteraciones$sal$hash</c>, con la sal y el hash en Base64.
    /// </summary>
    public static class HashContrasena
    {
        private const string Algoritmo = "PBKDF2-SHA256";
        private const int TamanoSal = 16;
        private const int TamanoHash = 32;
        private const int IteracionesMaximas = 10_000_000;

        /// <summary>Iteraciones recomendadas por OWASP para PBKDF2-HMAC-SHA256.</summary>
        public const int Iteraciones = 600_000;

        public static string Calcular(string contrasena)
        {
            ArgumentNullException.ThrowIfNull(contrasena);
            var sal = RandomNumberGenerator.GetBytes(TamanoSal);
            var hash = Derivar(contrasena, sal, Iteraciones, TamanoHash);
            return string.Join('$', Algoritmo, Iteraciones.ToString(CultureInfo.InvariantCulture),
                Convert.ToBase64String(sal), Convert.ToBase64String(hash));
        }

        public static bool Verificar(string contrasena, string? almacenado)
        {
            if (contrasena == null || !TryLeer(almacenado, out var iteraciones, out var sal, out var esperado))
                return false;
            var hash = Derivar(contrasena, sal, iteraciones, esperado.Length);
            return CryptographicOperations.FixedTimeEquals(hash, esperado);
        }

        public static bool EsHash(string? almacenado) => TryLeer(almacenado, out _, out _, out _);

        /// <summary>Indica si el hash se calculó con menos iteraciones que las actuales y conviene recalcularlo.</summary>
        public static bool NecesitaActualizarse(string? almacenado) =>
            !TryLeer(almacenado, out var iteraciones, out _, out _) || iteraciones < Iteraciones;

        /// <summary>
        /// Lee una contraseña guardada con el formato anterior del sistema, que solo la pasaba a Base64.
        /// Se usa únicamente para migrar esas contraseñas a hash.
        /// </summary>
        public static bool TryLeerFormatoAnterior(string? almacenado, out string contrasena)
        {
            contrasena = string.Empty;
            if (string.IsNullOrEmpty(almacenado) || EsHash(almacenado)) return false;
            try
            {
                contrasena = Encoding.Unicode.GetString(Convert.FromBase64String(almacenado));
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private static byte[] Derivar(string contrasena, byte[] sal, int iteraciones, int tamano) =>
            Rfc2898DeriveBytes.Pbkdf2(contrasena, sal, iteraciones, HashAlgorithmName.SHA256, tamano);

        private static bool TryLeer(string? almacenado, out int iteraciones, out byte[] sal, out byte[] hash)
        {
            iteraciones = 0;
            sal = Array.Empty<byte>();
            hash = Array.Empty<byte>();
            var partes = almacenado?.Split('$');
            if (partes is not { Length: 4 } || partes[0] != Algoritmo) return false;
            if (!int.TryParse(partes[1], NumberStyles.None, CultureInfo.InvariantCulture, out iteraciones)
                || iteraciones <= 0 || iteraciones > IteracionesMaximas)
                return false;
            try
            {
                sal = Convert.FromBase64String(partes[2]);
                hash = Convert.FromBase64String(partes[3]);
            }
            catch (FormatException)
            {
                return false;
            }
            return sal.Length > 0 && hash.Length > 0;
        }
    }
}
