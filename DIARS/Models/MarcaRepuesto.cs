namespace DIARS.Models
{
    public class MarcaRepuesto
    {
        public int CodigoMR { get; set; }
        public string Descripcion { get; set; }
        public Proveedor ProveedorMR { get; set; } = new();
        public bool EstadoM { get; set; }
    }
}
