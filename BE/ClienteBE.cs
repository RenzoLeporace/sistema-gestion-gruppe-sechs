namespace BE
{
    public class ClienteBE
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string DNI_CUIL { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }

        public ClienteBE() { }

        public ClienteBE(int idCliente, string nombre, string correo, string dNI_CUIL, string telefono, string direccion)
        {
            IdCliente = idCliente;
            Nombre = nombre;
            Correo = correo;
            DNI_CUIL = dNI_CUIL;
            Telefono = telefono;
            Direccion = direccion;
        }
    }
}
