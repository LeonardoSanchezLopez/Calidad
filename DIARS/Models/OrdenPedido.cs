namespace DIARS.Models
{
    public class OrdenPedido
    {
        public int CodigoOP { get; set; }
        public DateTime Fecha { get; set; }
        public OrdenTrabajoInterno TICodigo { get; set; } = new();
        public Bus BusCM { get; set; } = new();
        public string JefeEncargado { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
    }
}
