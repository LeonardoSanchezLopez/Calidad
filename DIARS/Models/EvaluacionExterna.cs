namespace DIARS.Models
{
    public class EvaluacionExterna
    {
        public int CodigoEE { get; set; }
        public Bus CodigoBus { get; set; } = new();
        public Proveedor ProveedorEE { get; set; } = new();
        public DateTime Fecha { get; set; }
        public OrdenTrabajoExterno TECodigo { get; set; } = new();
        public bool Estado { get; set; }
    }
}
