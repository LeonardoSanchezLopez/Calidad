// IJwtService.cs
using System.Security.Claims;
using DIARS.Service;

public interface IJwtService
{
    // Asegúrate de que los tipos y el orden sean exactos
    JwtResponse validarToken(ClaimsIdentity identity, IUsuarioService usuarioService);
}