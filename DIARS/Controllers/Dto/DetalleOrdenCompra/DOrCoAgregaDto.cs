namespace DIARS.Controllers.Dto.DetalleOrdenCompra
{
    public class DOrCoAgregaDto
    {
        public int Cod_CompraOD { get; set; } 
        public int Cantidad { get; set; }
        public int CantidadAceptada { get; set; }
        public string Repuesto { get; set; }
        public decimal Precio { get; set; }
    }
}
