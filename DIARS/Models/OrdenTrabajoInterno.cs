namespace DIARS.Models
{
    public class OrdenTrabajoInterno
    {
        public int CodigoTI { get; set; }
        public Bus BusTI { get; set; } = new();
        public DateTime Fecha { get; set; }
        public Mecanico MecanicoTI { get; set; } = new();
        public bool Estado { get; set; }
    }
}