namespace DIARS.Models
{
    public class DetalleOrdenCompra
    {
        public int DetalleOrdenCompraID { get; set; }
        public OrdenCompra OCCompra { get; set; } = new();
        public int Cantidad { get; set; }
        public int CantidadAceptada { get; set; }
        public Repuesto CodigoRep { get; set; } = new();
        public decimal Precio { get; set; }
    }
}
