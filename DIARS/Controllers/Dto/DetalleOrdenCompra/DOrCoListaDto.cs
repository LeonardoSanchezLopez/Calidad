namespace DIARS.Controllers.Dto.DetalleOrdenCompra
{
    public class DOrCoListaDto
    {
        public int Id { get; set; }
        public int Cod_CompraOD { get; set; } //Codigo de detalle orden compra
        public int Cantidad { get; set; }
        public int CantidadAceptada { get; set; }
        public string Repuesto { get; set; }
        public decimal Precio { get; set; }
    }
}