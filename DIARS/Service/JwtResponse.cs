using DIARS.Models;

namespace DIARS.Service
{
    public class JwtResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public Usuario result { get; set; }
    }
}
