namespace DIARS.Models
{
    public class DetalleOTI
    {
        public int DetalleOTIID { get; set; }
        public OrdenTrabajoInterno OrdenTrabajoInternoID { get; set; } = new();
        public Repuesto CodigoRepu { get; set; } = new();
        public Mecanico MecanicoTI { get; set; } = new();
        public string Parte { get; set; }
        public string Pieza { get; set; }
        public int Cantidad { get; set; }
    }
}
