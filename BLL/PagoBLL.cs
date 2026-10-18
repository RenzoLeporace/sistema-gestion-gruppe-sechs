using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public class PagoBLL
    {
        private readonly PagoMpr _pagoMpr = new PagoMpr();
        private readonly FacturaMpr _facturaMpr = new FacturaMpr();
        private void AplicarRecargosVencimiento(List<FacturaBE> facturas)
        {
            bool huboCambios = false;
            foreach (var f in facturas)
            {
                if (!f.Estado_Pago &&
                    !f.RecargoAplicado &&
                    DateTime.Now.Date > f.FechaVencimiento.Date)
                {
                    var recargo = Math.Round(f.Total * 0.10, 2);
                    f.RecargoVencimiento = recargo;
                    f.Total = Math.Round(f.Total + recargo, 2);
                    f.Saldo_Pendiente = Math.Round(f.Saldo_Pendiente + recargo, 2);
                    f.RecargoAplicado = true;
                    huboCambios = true;
                }
            }
            if (huboCambios)
            {
                XMLHelper.Guardar("Data\\facturas.xml", facturas);
                AppEvents.NotifyEconomiaActualizada();
            }
        }

        public List<FacturaBE> ListarFacturasPendientesOrdenadas()
        {
            var todas = _facturaMpr.GetAll();
            AplicarRecargosVencimiento(todas);
            return todas
                .Where(f => !f.Estado_Pago && f.Saldo_Pendiente > 0)
                .OrderBy(f => f.FechaVencimiento)
                .ToList();
        }

        public FacturaBE ObtenerPorId(int id)
        {
            return _facturaMpr.GetAll().FirstOrDefault(f => f.IdFactura == id);
        }

        public FacturaBE RegistrarPago(int idFactura, double monto, DateTime fechaPago, string metodo)
        {
            if (monto <= 0)
                throw new InvalidOperationException("El monto del pago debe ser mayor que cero.");
            if (string.IsNullOrWhiteSpace(metodo))
                throw new InvalidOperationException("Debe seleccionar un método de pago.");

            var facturas = _facturaMpr.GetAll();
            AplicarRecargosVencimiento(facturas);

            var factura = facturas.FirstOrDefault(f => f.IdFactura == idFactura)
                ?? throw new InvalidOperationException("Factura no encontrada.");
            if (factura.Estado_Pago || factura.Saldo_Pendiente <= 0)
                throw new InvalidOperationException("La factura ya se encuentra pagada.");

            _pagoMpr.Add(new PagoBE(0, factura.IdFactura, Math.Round(monto, 2), fechaPago, metodo.Trim()));
            var aplicado = Math.Min(monto, factura.Saldo_Pendiente);
            factura.Saldo_Pendiente = Math.Round(factura.Saldo_Pendiente - aplicado, 2);
            if (factura.Saldo_Pendiente <= 0)
            {
                factura.Saldo_Pendiente = 0;
                factura.Estado_Pago = true;
            }
            XMLHelper.Guardar("Data\\facturas.xml", facturas);
            AppEvents.NotifyEconomiaActualizada();

            return factura;
        }
    }
}

