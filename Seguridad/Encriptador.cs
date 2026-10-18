using System;
using System.Security.Cryptography;
using System.Text;

namespace Seguridad
{
    public static class Encriptador
    {

        public static string Encrypt(string pPassword)
        {
            byte[] encriptado = Encoding.Unicode.GetBytes(pPassword);
            string resultado = Convert.ToBase64String(encriptado);
            return resultado;
        }
        public static string Decrypt(string pPasswordEncriptado)
        {
            byte[] desencriptado = Convert.FromBase64String(pPasswordEncriptado);
            string resultado = Encoding.Unicode.GetString(desencriptado);
            return resultado;
        }
    }
}