namespace DIARS.Models
{
    public class DetalleNotaSalida
    {
        public int DetalleNotaSalidaID { get; set; }
        public NotaSalidaRepuestos SRCodigo { get; set; } = new();
        public int CantidadRecibida { get; set; }
        public Repuesto CodigoRepu { get; set; } = new();
        public int CantidadEnviada { get; set; }
    }
}
