namespace DIARS.Models
{
    public class DetalleNotaIngreso
    {
        public int DetalleNotaIngresoID { get; set; }
        public NotaIngresoRepuestos IRCodigo { get; set; } = new();
        public int CantidadRecibida { get; set; }
        public Repuesto CodigoRepu { get; set; } = new();
        public int CantidadAceptada { get; set; }
        public decimal Precio { get; set; }
    }
}
