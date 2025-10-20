namespace DIARS.Controllers.Dto.EvaluacionExterna
{
    public class EvaExListaDto
    {
        public int Id { get; set; }
        public int Cod_TrabajoExterno { get; set; }
        public string Bus { get; set; }
        public string Proveedor { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Condicion { get; set; }
    }
}
