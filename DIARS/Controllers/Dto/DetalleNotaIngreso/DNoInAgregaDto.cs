namespace DIARS.Controllers.Dto.DetalleNotaIngreso
{
    public class DNoInAgregaDto
    {
        public int Cod_IngresoRD { get; set; } 
        public int Cantidad { get; set; }
        public string Repuesto { get; set; }
        public int Aceptada { get; set; }
        public decimal Precio { get; set; }
    }
}