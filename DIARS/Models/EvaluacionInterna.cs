namespace DIARS.Models
{
    public class EvaluacionInterna
    {
        public int CodigoEI { get; set; }
        public Bus CodigoBus { get; set; } = new();
        public DateTime Fecha { get; set; }
        public OrdenTrabajoInterno TICodigo { get; set; } = new();
        public bool Estado { get; set; }
    }
}
