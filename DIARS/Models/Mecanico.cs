namespace DIARS.Models
{
    public class Mecanico
    {
        public int CodigoM { get; set; }
        public Especialidad EspecialidadM { get; set; } = new();
        public string Nombre { get; set; }
        public string DNI { get; set; }
        public string Domicilio { get; set; }
        public string Experiencia { get; set; }
        public string Telefono { get; set; }
        public decimal Sueldo { get; set; }
        public string Turno { get; set; }
        public DateTime FechaContrato { get; set; }
        public bool EstadoM { get; set; }
    }
}
