namespace DIARS.Controllers.Dto.OrdenCompra
{
    public class OrCoListaDto
    {
        public int Id { get; set; }
        public int Cod_OrdenPedido { get; set; }
        public string Proveedor { get; set; }
        public DateTime Fecha { get; set; }
        public string FormaPago { get; set; }
        public decimal Total { get; set; }
        public bool Condicion { get; set; }
    }
}
