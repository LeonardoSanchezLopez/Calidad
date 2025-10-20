using DIARS.Controllers.Dto.Bus;
using DIARS.Controllers.Dto;
using DIARS.Models;
using DIARS.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DIARS.Controllers.Dto.ContratoMantenimiento;

namespace DIARS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoMantenimientoController : ControllerBase
    {
        private readonly ContratoMantenimientoService _personaService;
        private readonly IUsuarioService _usuarioService;
        private readonly IValidator<CMListaDto> _personaactuvalidator;
        private readonly IValidator<CMAgregaDto> _personacreatevalidator;
        private readonly IJwtService _jwtService;
        public ContratoMantenimientoController(ContratoMantenimientoService personaService, IUsuarioService usuarioService, IValidator<CMListaDto> personaactuvalidator, IValidator<CMAgregaDto> personacreatevalidator, IJwtService jwtService)
        {
            _personaService = personaService;
            _usuarioService = usuarioService;
            _personaactuvalidator = personaactuvalidator;
            _personacreatevalidator = personacreatevalidator;
            _jwtService = jwtService;
        }

        [HttpGet]
        public IActionResult ListarContratoMantenimiento()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = _jwtService.validarToken(identity, _usuarioService);

            if (!rToken.success) return Unauthorized(new { message = rToken.message });

            Usuario usuario = rToken.result;

            if (usuario.Usu_Rol != "Jefe de Compras" && usuario.Usu_Rol != "Jefe de Mantenimiento" && usuario.Usu_Rol != "Administrador")
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
                List<CMListaDto> personasDto = _personaService.ListarContratoMantenimiento();

                if (personasDto == null || personasDto.Count == 0)
                    return NotFound(new { mensaje = "No hay personas activas." });

                return Ok(new ResponseDto<List<CMListaDto>> { EjecucionExitosa = true, MensajeError = null, Data = personasDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener la lista de Contratos de Mantenimiento.", error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CrearContratoMantenimiento([FromBody] CMAgregaDto personaDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = _jwtService.validarToken(identity, _usuarioService);
            //validator
            var validationResult = _personacreatevalidator.Validate(personaDto);

            if (!rToken.success) return Unauthorized(new { message = rToken.message });

            Usuario usuario = rToken.result;

            if (usuario.Usu_Rol != "Jefe de Compras" && usuario.Usu_Rol != "Administrador")
            {
                return Ok(new
                {
                    success = false,
                    message = "No tienes permisos para utilizar estos comandos",
                    result = ""
                });
            }

            //validar
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseDto<List<string>>
                {
                    EjecucionExitosa = false,
                    MensajeError = "Errores de validación",
                    Data = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                });
            }
            var resultado = _personaService.InsertarContratoMantenimiento(personaDto);

            if (resultado.EjecucionExitosa)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<CMListaDto> GetContratoMantenimientoById(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = _jwtService.validarToken(identity, _usuarioService);

            if (!rToken.success) return Unauthorized(new { message = rToken.message });

            Usuario usuario = rToken.result;

            if (usuario.Usu_Rol != "Jefe de Compras" && usuario.Usu_Rol != "Administrador")
            {
                return Ok(new
                {
                    success = false,
                    message = "No tienes permisos para utilizar estos comandos",
                    result = ""
                });
            }

            CMListaDto personaDto = _personaService.GetContratoMantenimientoId(id);

            if (personaDto == null)
            {
                return NotFound(new ResponseDto<object> { MensajeError = "Datos no encontrados" });
            }

            return Ok(new ResponseDto<CMListaDto> { EjecucionExitosa = true, MensajeError = null, Data = personaDto });
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarPersona(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var rToken = _jwtService.validarToken(identity, _usuarioService);
            if (!rToken.success) return Unauthorized(new { message = rToken.message });

            Usuario usuario = rToken.result;

            if (usuario.Usu_Rol != "Jefe de Compras" && usuario.Usu_Rol != "Administrador")
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
                bool resultado = _personaService.InhabilitarContratoMantenimiento(id);

                if (resultado)
                {
                    return Ok(new { success = true, message = "El Contrato Mantenimiento se ha sido eliminado exitosamente." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "El Contrato Mantenimiento no existe." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Hubo un error al intentar eliminar a el Contrato Mantenimiento." });
            }
        }
    }
}