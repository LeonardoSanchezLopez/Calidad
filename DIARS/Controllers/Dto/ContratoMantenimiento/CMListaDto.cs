namespace DIARS.Controllers.Dto.ContratoMantenimiento
{
    public class CMListaDto
    {
        public int Id { get; set; }
        public string BusPlaca { get; set; }
        public DateTime Fecha { get; set; }
        public string Proveedor { get; set; }
        public string Descripcion { get; set; }
        public decimal Costo { get; set; }
        public bool Condicion { get; set; }
    }
}
