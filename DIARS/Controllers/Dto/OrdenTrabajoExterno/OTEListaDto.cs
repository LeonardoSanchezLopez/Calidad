namespace DIARS.Controllers.Dto.OrdenTrabajoExterno
{
    public class OTEListaDto
    {
        public int Id { get; set; }
        public string PlacaBus { get; set; }
        public int Contrato { get; set; }
        public string Proveedor { get; set; }
        public DateTime Fecha { get; set; }
        public bool Condicion { get; set; }
    }
}
