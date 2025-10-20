using DIARS.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DIARS.Controllers
{
    [Route("api/RefreshToken")]
    [ApiController]
    public class RefreshTokenController : ControllerBase
    {
        private readonly MySQLDatabase _connectionString;
        private readonly IConfiguration _configuration;
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<RefreshTokenController> _logger;  // Aquí definimos el logger

        // Constructor con inyección de dependencias
        public RefreshTokenController(MySQLDatabase connectionString, IConfiguration configuration, IUsuarioService usuarioService, ILogger<RefreshTokenController> logger)
        {
            _connectionString = connectionString;
            _configuration = configuration;
            _usuarioService = usuarioService;
            _logger = logger;  // Asignamos el logger inyectado
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest request)
        {
            if (string.IsNullOrEmpty(request?.Token))
                return BadRequest(new { success = false, message = "Token requerido" });

            try
            {
                await using var conn = _connectionString.GetConnection();
                await conn.OpenAsync();

                const string query = "SELECT Usu_Id FROM usuariostokens WHERE Token = @Token AND Expiracion > NOW()";
                await using var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Token", request.Token);
                var result = await cmd.ExecuteScalarAsync();

                if (result == null)
                    return Unauthorized(new { success = false, message = "Token inválido o expirado" });

                int userId = Convert.ToInt32(result);

                // Generar nuevos tokens
                string newAccessToken = GenerateJwtToken(userId);
                string newRefreshToken = GenerateRefreshToken();
                DateTime newExpiration = DateTime.UtcNow.AddDays(7);

                // Loguear el evento de refresco
                _logger.LogInformation("Refrescando token..."); // Este es el mensaje que verás en los logs

                // Actualizar el refresh token en la BD
                const string updateQuery = "UPDATE usuariostokens SET Token = @NewToken, Expiracion = @ExpDate WHERE Usu_Id = @UserId";
                await using var updateCmd = new MySqlCommand(updateQuery, conn);
                updateCmd.Parameters.AddWithValue("@NewToken", newRefreshToken);
                updateCmd.Parameters.AddWithValue("@ExpDate", newExpiration);
                updateCmd.Parameters.AddWithValue("@UserId", userId);
                await updateCmd.ExecuteNonQueryAsync();

                return Ok(new { success = true, accessToken = newAccessToken, refreshToken = newRefreshToken });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Error interno del servidor", error = ex.Message });
            }
        }

        private string GenerateJwtToken(int userId)
        {
            // Corregido: Obtener la clave correcta
            var secretKey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new Exception("La clave JWT es nula o vacía.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: new[] { new Claim("id", userId.ToString()) },
                expires: DateTime.UtcNow.AddMinutes(30), //TIEMPO DEL TOKENNNNNNNNNNNNNNNNNNNNNNNNNNNNNN
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString(); // Genera un nuevo Refresh Token aleatorio
        }
    }

    public class TokenRequest
    {
        public string Token { get; set; }
    }
}
