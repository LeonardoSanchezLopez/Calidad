namespace DIARS.Controllers.Dto
{
    public class ResponseDto<T>
    {
        public bool EjecucionExitosa { get; set; }
        public string MensajeError { get; set; }
        public T Data { get; set; }
    }
}