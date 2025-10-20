namespace DIARS.Models
{
    public class ContratoMantenimiento
    {
        public int CodigoCM { get; set; }
        public Bus BusCM { get; set; } = new();
        public DateTime Fecha { get; set; }
        public Proveedor ProveedorCM { get; set; } = new();
        public string Descripcion { get; set; }
        public decimal Costo { get; set; }
        public bool Estado { get; set; }
    }
}
