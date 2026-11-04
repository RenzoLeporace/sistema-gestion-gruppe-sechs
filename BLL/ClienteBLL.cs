using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BE;
using Mapper;

namespace BLL
{
    public class ClienteBLL
    {
        private readonly ClienteMpr _mapper = new ClienteMpr();
        public List<ClienteBE> Listar()
        {
            return _mapper.GetAll();
        }
        public ClienteBE? BuscarPorDni(string dniCuil)
        {
            return _mapper.GetByDni(dniCuil);
        }
        public ClienteBE Alta(string nombre, string direccion, string telefono, string correo, string dniCuil)
        {
            ValidarDatos(nombre, direccion, telefono, correo, dniCuil, validarExistencia:false);
            var existente = BuscarPorDni(dniCuil);
            if (existente != null)
                throw new InvalidOperationException("El DNI/CUIT ya existe. No se puede dar de alta.");

            var nuevo = new ClienteBE(0, nombre.Trim(), correo.Trim(), dniCuil.Trim(), telefono.Trim(), direccion.Trim());
            return _mapper.Add(nuevo);
        }
        public void Actualizar(string nombre, string direccion, string telefono, string correo, string dniCuil)
        {
            ValidarDatos(nombre, direccion, telefono, correo, dniCuil, validarExistencia:true);
            var existente = BuscarPorDni(dniCuil);
            if (existente == null)
                throw new InvalidOperationException("No existe un cliente con el DNI/CUIT indicado.");

            var modificado = new ClienteBE(existente.IdCliente, nombre.Trim(), correo.Trim(), dniCuil.Trim(), telefono.Trim(), direccion.Trim());
            _mapper.Update(modificado);
        }
        private void ValidarDatos(string nombre, string direccion, string telefono, string correo, string dniCuil, bool validarExistencia)
        {
            if (string.IsNullOrWhiteSpace(dniCuil))
                throw new ArgumentException("DNI/CUIT es obligatorio.");
            if (dniCuil.Length < 7 || dniCuil.Length > 13)
                throw new ArgumentException("DNI/CUIT con longitud inválida.");
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("Nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(direccion))
                throw new ArgumentException("Dirección es obligatoria.");
            if (string.IsNullOrWhiteSpace(telefono))
                throw new ArgumentException("Teléfono es obligatorio.");
            if (!Regex.IsMatch(telefono.Trim(), @"^[0-9+\-() ]{6,20}$"))
                throw new ArgumentException("Formato de teléfono inválido.");
            if (string.IsNullOrWhiteSpace(correo))
                throw new ArgumentException("Correo es obligatorio.");
            if (!Regex.IsMatch(correo.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Formato de correo inválido.");
            if (validarExistencia && BuscarPorDni(dniCuil) == null)
                throw new InvalidOperationException("No existe el cliente para actualizar.");
        }
        public void Eliminar(int idCliente)
        {
            if (idCliente <= 0)
                throw new Exception("Cliente inválido.");

            _mapper.Delete(idCliente);
        }

    }
}
