using DIARS.Models;
using DIARS.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;    
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DIARS.Controllers
{
    [ApiController]
    [Route("usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IConfiguration configuration, IUsuarioService usuarioService)
        {
            _configuration = configuration;
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult InicioSesion([FromBody] Login login)
        {
            Usuario usuario = _usuarioService.obtenerUsuarios(login.Usuario, login.Password);

            if (usuario == null)
            {
                return Unauthorized(new { success = false, message = "Credenciales incorrectas" });
            }

            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, jwtSettings["Subject"]),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        new Claim("id", usuario.Usu_Id.ToString()),
        new Claim("usuario", usuario.Usu_Nombre),
        new Claim("rol", usuario.Usu_Rol)
    };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(1),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                Success = true,
                Message = "Inicio de sesión exitoso",
                Token = tokenString,
                Role = usuario.Usu_Rol
            });
        }
    }
}