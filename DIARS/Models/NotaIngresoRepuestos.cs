namespace DIARS.Models
{
    public class NotaIngresoRepuestos
    {
        public int CodigoIR { get; set; }
        public OrdenCompra CodigoOC { get; set; } = new();
        public DateTime Fecha { get; set; }
        public Proveedor ProveedorIR { get; set; } = new();
        public bool Estado { get; set; } 
    }
}