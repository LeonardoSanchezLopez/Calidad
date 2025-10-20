namespace DIARS.Controllers.Dto.DetalleNotaIngreso
{
    public class DNoInListaDto
    {
        public int Id { get; set; }
        public int Cod_Ingreso { get; set; }
        public int Cantidad { get; set; }
        public string Repuesto { get; set; }
        public int Aceptada { get; set; }
        public decimal Precio { get; set; }
    }
}
