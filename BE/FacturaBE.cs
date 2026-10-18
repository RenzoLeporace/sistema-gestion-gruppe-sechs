using System;

namespace BE
{
    public class FacturaBE
    {
        public int IdFactura { get; set; }
        public int IdContrato { get; set; }
        public string DNI_CUIL_Cliente { get; set; }
        public string NombreCliente { get; set; }

        public DateTime FechaEmision { get; set; }
        public DateTime FechaVencimiento { get; set; }

        public double Subtotal { get; set; }
        public double Impuestos { get; set; }
        public double Total { get; set; }

        public bool Estado_Pago { get; set; }
        public double Saldo_Pendiente { get; set; }
        public string RutaDocumento { get; set; }

        public bool RecargoAplicado { get; set; }
        public double RecargoVencimiento { get; set; }

        public bool Vencida => !Estado_Pago && DateTime.Now.Date > FechaVencimiento.Date;

        public FacturaBE() { }

        public FacturaBE(int idFactura, int idContrato, string dni, string nombreCliente,
                         DateTime fechaEmision, DateTime fechaVencimiento,
                         double subtotal, double impuestos, double total,
                         bool estadoPago, double saldoPendiente, string rutaDocumento)
        {
            IdFactura = idFactura;
            IdContrato = idContrato;
            DNI_CUIL_Cliente = dni;
            NombreCliente = nombreCliente;
            FechaEmision = fechaEmision;
            FechaVencimiento = fechaVencimiento;
            Subtotal = subtotal;
            Impuestos = impuestos;
            Total = total;
            Estado_Pago = estadoPago;
            Saldo_Pendiente = saldoPendiente;
            RutaDocumento = rutaDocumento;
            RecargoAplicado = false;
            RecargoVencimiento = 0;
        }
    }
}
