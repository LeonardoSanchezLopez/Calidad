using DIARS.Controllers.Dto.MarcaRepuesto;
using DIARS.Controllers.Dto;
using DIARS.Models;
using DIARS.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DIARS.Controllers.Dto.Mecanico;

namespace DIARS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MecanicoController : ControllerBase
    {
        private readonly MecanicoService _personaService;
        private readonly IUsuarioService _usuarioService;
        private readonly IValidator<MecaActuDto> _personaactuvalidator;
        private readonly IValidator<MecaAgregaDto> _personacreatevalidator;
        private readonly IJwtService _jwtService;
        public MecanicoController(MecanicoService personaService, IUsuarioService usuarioService, IValidator<MecaActuDto> personaactuvalidator, IValidator<MecaAgregaDto> personacreatevalidator, IJwtService jwtService)
        {
            _personaService = personaService;
            _usuarioService = usuarioService;
            _personaactuvalidator = personaactuvalidator;
            _personacreatevalidator = personacreatevalidator;
            _jwtService = jwtService;
        }

        [HttpGet]
        public IActionResult ListarMecanico()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = _jwtService.validarToken(identity, _usuarioService);

            if (!rToken.success) return Unauthorized(new { message = rToken.message });

            Usuario usuario = rToken.result;

            if (usuario.Usu_Rol != "Jefe de Mantenimiento" && usuario.Usu_Rol != "Administrador")
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
                List<MecaListaDto> personasDto = _personaService.ListarMecanico();

                if (personasDto == null || personasDto.Count == 0)
                    return NotFound(new { mensaje = "No hay personas activas." });

                return Ok(new ResponseDto<List<MecaListaDto>> { EjecucionExitosa = true, MensajeError = null, Data = personasDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener la lista de personas.", error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CrearMecanico([FromBody] MecaAgregaDto personaDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = _jwtService.validarToken(identity, _usuarioService);
            //validator
            var validationResult = _personacreatevalidator.Validate(personaDto);

            if (!rToken.success) return Unauthorized(new { message = rToken.message });

            Usuario usuario = rToken.result;

            if (usuario.Usu_Rol != "Jefe de Mantenimiento" && usuario.Usu_Rol != "Administrador")
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
            var resultado = _personaService.InsertarMecanico(personaDto);

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
        public ActionResult<MecaListaDto> GetMecanicoById(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = _jwtService.validarToken(identity, _usuarioService);

            if (!rToken.success) return Unauthorized(new { message = rToken.message });

            Usuario usuario = rToken.result;

            if (usuario.Usu_Rol != "Jefe de Mantenimiento" && usuario.Usu_Rol != "Administrador")
            {
                return Ok(new
                {
                    success = false,
                    message = "No tienes permisos para utilizar estos comandos",
                    result = ""
                });
            }

            MecaListaDto personaDto = _personaService.GetMecanicoId(id);

            if (personaDto == null)
            {
                return NotFound(new ResponseDto<object> { MensajeError = "Datos no encontrados" });
            }
            return Ok(new ResponseDto<MecaListaDto> { EjecucionExitosa = true, MensajeError = null, Data = personaDto });
        }

        [HttpPut]
        public IActionResult ActualizarMecanico([FromBody] MecaActuDto personaDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = _jwtService.validarToken(identity, _usuarioService);

            if (!rToken.success) return Unauthorized(new { message = rToken.message });

            Usuario usuario = rToken.result;

            if (usuario.Usu_Rol != "Jefe de Mantenimiento" && usuario.Usu_Rol != "Administrador")
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

            var resultado = _personaService.ActualizarMecanico(personaDto);

            if (resultado.EjecucionExitosa)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarMecanico(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var rToken = _jwtService.validarToken(identity, _usuarioService);
            if (!rToken.success) return Unauthorized(new { message = rToken.message });

            Usuario usuario = rToken.result;

            if (usuario.Usu_Rol != "Jefe de Mantenimiento" && usuario.Usu_Rol != "Administrador")
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
                bool resultado = _personaService.InhabilitarMecanico(id);

                if (resultado)
                {
                    return Ok(new { success = true, message = "El Mecanico ha sido eliminada exitosamente." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "El Mecanico no existe." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Hubo un error al intentar eliminar al Mecanico." });
            }
        }
    }
}