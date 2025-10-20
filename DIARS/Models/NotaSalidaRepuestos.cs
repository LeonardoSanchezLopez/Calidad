namespace DIARS.Models
{
    public class NotaSalidaRepuestos
    {
        public int CodigoSR { get; set; }
        public Bus BusSR { get; set; } = new();
        public DateTime Fecha { get; set; }
        public OrdenPedido OPCodigo { get; set; } = new();
        public bool Estado { get; set; }
    }
}