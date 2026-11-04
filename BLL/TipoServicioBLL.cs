using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public class TipoServicioBLL
    {
        private readonly TipoServicioMpr _mapper = new TipoServicioMpr();
        public List<TipoServicioBE> Listar()
        {
            return _mapper.GetAll();
        }
        public TipoServicioBE? BuscarPorDescripcion(string desc)
        {
            if (string.IsNullOrWhiteSpace(desc)) return null;
            return _mapper.GetByDescripcion(desc);
        }
        public double ObtenerTarifaBase(string desc)
        {
            var ts = BuscarPorDescripcion(desc);
            return ts?.TarifaBase ?? 0;
        }
    }
}
