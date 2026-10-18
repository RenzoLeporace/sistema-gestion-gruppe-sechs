using System;

namespace BE
{
    public class PagoBE
    {
        public int IdPago { get; set; }
        public int IdFactura { get; set; }
        public double Monto_Pagado { get; set; }
        public DateTime Fecha_Pago { get; set; }
        public string Metodo_Pago { get; set; }

        public PagoBE() { }

        public PagoBE(int idPago, int idFactura, double monto_Pagado, DateTime fecha_Pago, string metodo_Pago)
        {
            IdPago = idPago;
            IdFactura = idFactura;
            Monto_Pagado = monto_Pagado;
            Fecha_Pago = fecha_Pago;
            Metodo_Pago = metodo_Pago;
        }
    }
}
