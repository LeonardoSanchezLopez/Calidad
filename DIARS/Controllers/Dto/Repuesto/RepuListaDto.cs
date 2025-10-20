namespace DIARS.Controllers.Dto.Repuesto
{
    public class RepuListaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public string Marcarepuesto { get; set; }
        public string Proveedor { get; set; }
        public decimal Precio { get; set; }
        public bool Condicion { get; set; }
    }
}
