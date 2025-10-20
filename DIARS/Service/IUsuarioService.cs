using DIARS.Models;
using DIARS.Controllers.Dto; // Si usa ResponseDto

namespace DIARS.Service
{
    public interface IUsuarioService
    {
        Usuario obtenerUsuarioPorId(int id);
        Usuario obtenerUsuarios(string nombre, string password);
    }
}