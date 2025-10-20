namespace DIARS.Models
{
    public class Factura
    {
        public int CodigoFactura { get; set; }
        public OrdenCompra CodigoOC { get; set; } = new();
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
    }
}
