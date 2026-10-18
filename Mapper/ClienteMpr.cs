using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BE;

namespace Mapper
{
    public class ClienteMpr
    {
        private readonly string _path;
        public ClienteMpr()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory ?? Directory.GetCurrentDirectory();
            var dataDir = Path.Combine(baseDir, "Data");
            Directory.CreateDirectory(dataDir);
            _path = Path.Combine(dataDir, "Clientes.xml");
        }
        public List<ClienteBE> GetAll()
        {
            return XMLHelper.Leer<ClienteBE>(_path);
        }
        public ClienteBE GetByDni(string dniCuil)
        {
            if (string.IsNullOrWhiteSpace(dniCuil)) return null;
            return GetAll().FirstOrDefault(c => c.DNI_CUIL.Equals(dniCuil.Trim(), StringComparison.OrdinalIgnoreCase));
        }
        public void GuardarTodos(List<ClienteBE> lista)
        {
            XMLHelper.Guardar(_path, lista);
        }
        public ClienteBE Add(ClienteBE nuevo)
        {
            var lista = GetAll();
            if (lista.Any(c => c.DNI_CUIL.Equals(nuevo.DNI_CUIL, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("El DNI/CUIT ya está registrado.");

            nuevo.IdCliente = lista.Count == 0 ? 1 : lista.Max(c => c.IdCliente) + 1;
            lista.Add(nuevo);
            GuardarTodos(lista);
            return nuevo;
        }
        public void Update(ClienteBE modificado)
        {
            var lista = GetAll();
            var existente = lista.FirstOrDefault(c => c.DNI_CUIL.Equals(modificado.DNI_CUIL, StringComparison.OrdinalIgnoreCase));
            if (existente == null)
                throw new InvalidOperationException("No se encontró el cliente a actualizar.");
            existente.Nombre = modificado.Nombre;
            existente.Correo = modificado.Correo;
            existente.Telefono = modificado.Telefono;
            existente.Direccion = modificado.Direccion;
            GuardarTodos(lista);
        }
        public void Delete(int idCliente)
        {
            var lista = GetAll();
            var cliente = lista.FirstOrDefault(c => c.IdCliente == idCliente);
            if (cliente == null)
                throw new InvalidOperationException("No se encontró el cliente a eliminar.");
            lista.Remove(cliente);
            GuardarTodos(lista);
        }
    }
}
