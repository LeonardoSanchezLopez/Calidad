namespace DIARS.Models
{
    public class OrdenTrabajoExterno
    {
        public int CodigoTE { get; set; }
        public Bus CodigoBus { get; set; } = new();
        public ContratoMantenimiento ContratoCO { get; set; } = new();
        public DateTime Fecha { get; set; }
        public Proveedor ProveedorTE { get; set; } = new();
        public bool Estado { get; set; }
    }
}
