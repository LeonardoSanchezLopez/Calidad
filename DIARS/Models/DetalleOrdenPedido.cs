namespace DIARS.Models
{
    public class DetalleOrdenPedido
    {
        public int DetalleOrdenPedidoID { get; set; }
        public OrdenPedido OPCodigo { get; set; } = new();
        public int Cantidad { get; set; }
        public Repuesto CodigoRepu { get; set; } = new();
    }
}