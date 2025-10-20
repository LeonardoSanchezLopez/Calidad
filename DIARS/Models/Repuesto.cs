namespace DIARS.Models
{
    public class Repuesto
    {
        public int CodigoR { get; set; }
        public string NombreR { get; set; }
        public Categoria CategoriaR { get; set; } = new();
        public MarcaRepuesto MarcarepuestoR { get; set; } = new();
        public Proveedor ProveedorR { get; set; } = new();
        public decimal Precio { get; set; }
        public bool EstadoR { get; set; }
    }
}
