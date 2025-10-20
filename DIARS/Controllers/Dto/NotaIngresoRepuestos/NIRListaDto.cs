namespace DIARS.Controllers.Dto.NotaIngresoRepuestos
{
    public class NIRListaDto
    {
        public int Id { get; set; }
        public int Cod_OrdenC { get; set; }
        public DateTime Fecha { get; set; }
        public string Proveedor { get; set; }
        public bool Condicion { get; set; }
    }
}
