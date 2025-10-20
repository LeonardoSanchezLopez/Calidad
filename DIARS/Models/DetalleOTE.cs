namespace DIARS.Models
{
    public class DetalleOTE
    {
        public int DetalleOTEID { get; set; }


        public OrdenTrabajoExterno TECodigo { get; set; } = new();
        public Repuesto CodigoRepu { get; set; } = new();
        public string Parte { get; set; }
        public string Pieza { get; set; }
        public int Cantidad { get; set; }
    }
}
