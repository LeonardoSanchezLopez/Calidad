namespace DIARS.Controllers.Dto.Factura
{
    public class FacListaDto
    {
        public int Id { get; set; }
        public int Cod_OrdenC { get; set; }
        public DateTime FechaRegistro { get; set; }
        public decimal Total { get; set; }
    }
}
