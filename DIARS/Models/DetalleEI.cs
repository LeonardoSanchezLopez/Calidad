namespace DIARS.Models
{
    public class DetalleEI
    {
        public int DetalleEvaluacionInternaID { get; set; }
        public EvaluacionInterna EICodigo { get; set; } = new();
        public Mecanico MecanicoEI { get; set; } = new();
    }
}