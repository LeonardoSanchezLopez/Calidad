namespace DIARS.Controllers.Dto.OrdenTrabajoInterno
{
    public class OTIListaDto
    {
        public int Id { get; set; }
        public string PlacaBus { get; set; }
        public DateTime Fecha { get; set; }
        public string Mecanico { get; set; }
        public bool Condicion { get; set; }
    }
}
