namespace DIARS.Controllers.Dto.DetalleOTI
{
    public class DOTIListaDto
    {
        public int Id { get; set; }
        public int Cod_OrdenTI { get; set; }
        public string Repuesto { get; set; }
        public string Mecanico { get; set; }
        public string Parte { get; set; }
        public string Pieza { get; set; }
        public int Cantidad { get; set; }
    }
}
