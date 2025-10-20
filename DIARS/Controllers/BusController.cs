using DIARS.Controllers.Dto;
using DIARS.Controllers.Dto.Bus;
using DIARS.Models;
using DIARS.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;
using System;
using System.Collections.Generic;

namespace DIARS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusController : ControllerBase
    {
        // DEPENDENCIAS INYECTADAS
        private readonly IBusService _busService;
        private readonly IUsuarioService _usuarioService;
        private readonly IValidator<BusActuDto> _personaactuvalidator;
        private readonly IValidator<BusAgregaDto> _personacreatevalidator;
        private readonly IJwtService _jwtService;

        // CONSTRUCTOR
        public BusController(
            IBusService busService,
            IUsuarioService usuarioService,
            IValidator<BusActuDto> personaactuvalidator,
            IValidator<BusAgregaDto> personacreatevalidator,
            IJwtService jwtService)
        {
            _busService = busService;
            _usuarioService = usuarioService;
            _personaactuvalidator = personaactuvalidator;
            _personacreatevalidator = personacreatevalidator;
            _jwtService = jwtService;
        }

        // --------------------------------------------------------------------
        // GET - ListarPersonas
        // --------------------------------------------------------------------

        [HttpGet] // Atributo corregido: solo aparece una vez
        public IActionResult ListarPersonas()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            // 1. Validar Token (CORRECCIÓN CLAVE: JwtResponse)
            JwtResponse rToken = _jwtService.validarToken(identity, _usuarioService);

            if (!rToken.success)
            {
                return Unauthorized(new { message = rToken.message });
            }

            Usuario usuario = rToken.result;

            // 2. Verificar Usuario Nulo
            if (usuario == null)
            {
                return Unauthorized(new { message = "Usuario asociado al token no encontrado o inválido." });
            }

            // 3. Validación de Rol
            var listarRoles = new[] { "Jefe de Mantenimiento", "Jefe de Compras", "Jefe de Almacen", "Administrador" };

            if (!listarRoles.Contains(usuario.Usu_Rol))
            {
                // Si el rol NO está autorizado, termina aquí. (Resuelve MoqException)
                return Ok(new
                {
                    success = false,
                    message = "No tienes permisos para utilizar estos comandos",
                    result = ""
                });
            }

            // 4. Lógica de Negocio
            try
            {
                List<BusListaDto> personasDto = _busService.ListarBus();

                if (personasDto == null || personasDto.Count == 0)
                    return NotFound(new { mensaje = "No hay personas activas." });

                return Ok(new ResponseDto<List<BusListaDto>> { EjecucionExitosa = true, MensajeError = null, Data = personasDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener la lista de Buses.", error = ex.Message });
            }
        }

        // --------------------------------------------------------------------
        // POST - CrearPersona
        // --------------------------------------------------------------------

        [HttpPost]
        public IActionResult CrearPersona([FromBody] BusAgregaDto personaDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            // 1. CORRECCIÓN CLAVE: JwtResponse
            JwtResponse rToken = _jwtService.validarToken(identity, _usuarioService);

            // Validar antes de la autorización para fallar rápido en datos inválidos
            var validationResult = _personacreatevalidator.Validate(personaDto);

            if (!rToken.success) return Unauthorized(new { message = rToken.message });

            Usuario usuario = rToken.result;

            // 2. Verificar si el usuario es nulo
            if (usuario == null) return Unauthorized(new { message = "Usuario asociado al token no encontrado." });

            // Validación de roles
            var crearRoles = new[] { "Jefe de Mantenimiento", "Administrador" };

            if (!crearRoles.Contains(usuario.Usu_Rol))
            {
                return Ok(new
                {
                    success = false,
                    message = "No tienes permisos para utilizar estos comandos",
                    result = ""
                });
            }

            // Validar
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseDto<List<string>>
                {
                    EjecucionExitosa = false,
                    MensajeError = "Errores de validación",
                    Data = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                });
            }

            var resultado = _busService.InsertarBus(personaDto);

            if (resultado.EjecucionExitosa)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }

        // --------------------------------------------------------------------
        // GET - GetBusById
        // --------------------------------------------------------------------

        [HttpGet("{id}")]
        public ActionResult<BusListaDto> GetBusById(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            // 1. CORRECCIÓN CLAVE: JwtResponse
            JwtResponse rToken = _jwtService.validarToken(identity, _usuarioService);

            if (!rToken.success) return Unauthorized(new { message = rToken.message });

            Usuario usuario = rToken.result;

            // 2. Verificar si el usuario es nulo
            if (usuario == null) return Unauthorized(new { message = "Usuario asociado al token no encontrado." });

            // Validación de roles
            var getByIdRoles = new[] { "Jefe de Mantenimiento", "Administrador" };

            if (!getByIdRoles.Contains(usuario.Usu_Rol))
            {
                return Ok(new
                {
                    success = false,
                    message = "No tienes permisos para utilizar estos comandos",
                    result = ""
                });
            }

            BusListaDto personaDto = _busService.GetBusId(id);

            if (personaDto == null)
            {
                return NotFound(new ResponseDto<object> { MensajeError = "Datos no encontrados" });
            }

            return Ok(new ResponseDto<BusListaDto> { EjecucionExitosa = true, MensajeError = null, Data = personaDto });
        }

        // --------------------------------------------------------------------
        // PUT - ActualizarBus
        // --------------------------------------------------------------------

        [HttpPut]
        public IActionResult ActualizarBus([FromBody] BusActuDto personaDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            // 1. CORRECCIÓN CLAVE: JwtResponse
            JwtResponse rToken = _jwtService.validarToken(identity, _usuarioService);

            if (!rToken.success) return Unauthorized(new { message = rToken.message });

            Usuario usuario = rToken.result;

            // 2. Verificar si el usuario es nulo
            if (usuario == null) return Unauthorized(new { message = "Usuario asociado al token no encontrado." });

            // Validación de roles
            var actualizarRoles = new[] { "Jefe de Mantenimiento", "Administrador" };

            if (!actualizarRoles.Contains(usuario.Usu_Rol))
            {
                return Ok(new
                {
                    success = false,
                    message = "No tienes permisos para utilizar estos comandos",
                    result = ""
                });
            }

            var validationResult = _personaactuvalidator.Validate(personaDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseDto<List<string>>
                {
                    EjecucionExitosa = false,
                    MensajeError = "Errores de validación",
                    Data = validationResult.Errors.Select(e => e.ErrorMessage).ToList()

                });
            }

            var resultado = _busService.ActualizarBus(personaDto);

            if (resultado.EjecucionExitosa)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }

        // --------------------------------------------------------------------
        // DELETE - EliminarPersona
        // --------------------------------------------------------------------

        [HttpDelete("{id}")]
        public IActionResult EliminarPersona(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            // 1. CORRECCIÓN CLAVE: JwtResponse
            JwtResponse rToken = _jwtService.validarToken(identity, _usuarioService);

            if (!rToken.success) return Unauthorized(new { message = rToken.message });

            Usuario usuario = rToken.result;

            // 2. Verificar si el usuario es nulo
            if (usuario == null) return Unauthorized(new { message = "Usuario asociado al token no encontrado." });

            // Validación de roles
            var eliminarRoles = new[] { "Jefe de Mantenimiento", "Administrador" };

            if (!eliminarRoles.Contains(usuario.Usu_Rol))
            {
                return Ok(new
                {
                    success = false,
                    message = "No tienes permisos para utilizar estos comandos",
                    result = ""
                });
            }

            try
            {
                bool resultado = _busService.InhabilitarBus(id);

                if (resultado)
                {
                    return Ok(new { success = true, message = "El Bus ha sido eliminado exitosamente." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "El Bus no existe." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Hubo un error al intentar eliminar a el Bus." });
            }
        }
    }
}