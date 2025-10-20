namespace DIARS.Models
{
    public class OrdenCompra
    {
        public int CodigoOC { get; set; }
        public Proveedor CodigoPro { get; set; } = new();
        public DateTime Fecha { get; set; }
        public OrdenPedido OPCodigo { get; set; } = new();
        public string FormaPago { get; set; }
        public decimal Total { get; set; }
        public bool Estado { get; set; }
    }
}
