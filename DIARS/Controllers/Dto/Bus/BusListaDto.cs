namespace DIARS.Controllers.Dto.Bus
{
    public class BusListaDto
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Piso { get; set; }
        public string Placa { get; set; }
        public string Chasis { get; set; }
        public string Motor { get; set; }
        public int Capacidad { get; set; }
        public string TipoMotor { get; set; }
        public string Combustible { get; set; }
        public DateTime FechaAdquisicion { get; set; }
        public int Kilometraje { get; set; }
        public bool Condicion { get; set; }
    }
}
