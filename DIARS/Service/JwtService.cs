using System.Security.Claims;
using DIARS.Models; // Asegúrate de tener este using si 'Usuario' está aquí
using System.Linq;
using System;

namespace DIARS.Service
{
    public class JwtService : IJwtService
    {
        public JwtResponse validarToken(ClaimsIdentity identity, IUsuarioService usuarioService)
        {
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    // ❌ Antes: return new { ... }
                    // ✅ AHORA: Retorna una instancia explícita de JwtResponse
                    return new JwtResponse
                    {
                        success = false,
                        message = "Verificar si estas enviando un token valido",
                        result = null // El tipo 'result' en JwtResponse debe ser Usuario o null si falla.
                    };
                }

                var id = identity.Claims.FirstOrDefault(x => x.Type == "id")?.Value;

                // Manejar el caso donde no hay ID (token inválido)
                if (string.IsNullOrEmpty(id))
                {
                    return new JwtResponse
                    {
                        success = false,
                        message = "Token no contiene un ID de usuario válido",
                        result = null
                    };
                }

                var usuario = usuarioService.obtenerUsuarioPorId(int.Parse(id));

                // Manejar el caso donde el usuario no existe
                if (usuario == null)
                {
                    return new JwtResponse
                    {
                        success = false,
                        message = "Usuario asociado al token no encontrado",
                        result = null
                    };
                }


                // ❌ Antes: return new { ... }
                // ✅ AHORA: Retorna una instancia explícita de JwtResponse
                return new JwtResponse
                {
                    success = true,
                    message = "exito",
                    result = usuario
                };

            }
            catch (Exception ex)
            {
                // ❌ Antes: return new { ... }
                // ✅ AHORA: Retorna una instancia explícita de JwtResponse
                return new JwtResponse
                {
                    success = false,
                    message = "Catch: " + ex.Message,
                    result = null,
                };
            }
        }
    }
}