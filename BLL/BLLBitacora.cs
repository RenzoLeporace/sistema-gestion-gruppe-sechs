using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public sealed class BLLBitacora
    {
        private static readonly Lazy<BLLBitacora> _inst = new(() => new BLLBitacora());
        public static BLLBitacora Instance => _inst.Value;

        private readonly BitacoraMapper _mapper = new();

        private BLLBitacora() { }

        public void Registrar(BitacoraTipo tipo, string mensaje, string detalle, bool exito)
        {
            var user = BLLUsuario.Instance.CurrentUser;
            var entrada = new BitacoraEntrada
            {
                Tipo = tipo,
                Mensaje = mensaje ?? string.Empty,
                Detalle = detalle ?? string.Empty,
                Resultado = exito ? BitacoraResultado.Exito : BitacoraResultado.Fallo,
                Usuario = user?.Nombre ?? "SYSTEM",
                UsuarioId = user?.Id ?? 0,
                Fecha = DateTime.Now
            };
            _mapper.Agregar(entrada);
        }
        public IEnumerable<BitacoraEntrada> ObtenerTodos()
        {
            return _mapper.ObtenerTodos()
                          .OrderByDescending(e => e.Fecha)
                          .ThenByDescending(e => e.Id);
        }
        public void Limpiar()
        {
            _mapper.Limpiar();
        }
    }
}
