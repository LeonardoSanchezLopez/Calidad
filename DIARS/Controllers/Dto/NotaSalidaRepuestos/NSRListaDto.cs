namespace DIARS.Controllers.Dto.NotaSalidaRepuestos
{
    public class NSRListaDto
    {
        public int Id { get; set; }
        public string Bus { get; set; }
        public DateTime Fecha { get; set; }
        public int Cod_OrdenPedido { get; set; }
        public bool Condicion { get; set; }
    }
}
