namespace DIARS.Controllers.Dto.EvaluacionInterna
{
    public class EvaInListaDto
    {
        public int Id { get; set; }
        public string Bus { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int Cod_TrabajoInterno { get; set; }
        public bool Condicion { get; set; }
    }
}
