namespace DIARS.Controllers.Dto.OrdenPedido
{
    public class OrPeListaDto
    {
        public int Id { get; set; }
        public int Cod_TrabajoInterno { get; set; }
        public string BusPlaca { get; set; }
        public string Encargado { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public bool Condicion { get; set; }
    }
}
