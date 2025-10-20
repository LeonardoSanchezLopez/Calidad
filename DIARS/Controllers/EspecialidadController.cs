using DIARS.Controllers.Dto.Categoria;
using DIARS.Controllers.Dto;
using DIARS.Models;
using DIARS.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DIARS.Controllers.Dto.Especialidad;
    

namespace DIARS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadController : ControllerBase
    {
        private readonly EspecialidadService _personaService;
        private readonly IUsuarioService _usuarioService;
        private readonly IValidator<EspeActuDto> _personaactuvalidator;
        private readonly IValidator<EspeAgregaDto> _personacreatevalidator;
        private readonly IJwtService _jwtService;
        public EspecialidadController(EspecialidadService personaService, IUsuarioService usuarioService, IValidator<EspeActuDto> personaactuvalidator, IValidator<EspeAgregaDto> personacreatevalidator, IJwtService jwtService)
        {
            _personaService = personaService;
            _usuarioService = usuarioService;
            _personaactuvalidator = personaactuvalidator;
            _personacreatevalidator = personacreatevalidator;
            _jwtService = jwtService;
        }

        [HttpGet]
        public IActionResult ListarEspecialidad()
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
                List<EspeListaDto> personasDto = _personaService.ListarEspecialidad();

                if (personasDto == null || personasDto.Count == 0)
                    return NotFound(new { mensaje = "No hay personas activas." });

                return Ok(new ResponseDto<List<EspeListaDto>> { EjecucionExitosa = true, MensajeError = null, Data = personasDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener la lista de Especialidades.", error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CrearEspecialidad([FromBody] EspeAgregaDto personaDto)
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
            var resultado = _personaService.InsertarEspecialidad(personaDto);

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
        public ActionResult<EspeListaDto> GetEspecialidadById(int id)
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

            EspeListaDto personaDto = _personaService.GetEspecialidadId(id);

            if (personaDto == null)
            {
                return NotFound(new ResponseDto<object> { MensajeError = "Datos no encontrados" });
            }

            return Ok(new ResponseDto<EspeListaDto> { EjecucionExitosa = true, MensajeError = null, Data = personaDto });
        }

        [HttpPut]
        public IActionResult ActualizarEspecialidad([FromBody] EspeActuDto personaDto)
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

            var resultado = _personaService.ActualizarEspecialidad(personaDto);

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
        public IActionResult EliminarEspecialidad(int id)
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
                bool resultado = _personaService.InhabilitarEspecialidad(id);

                if (resultado)
                {
                    return Ok(new { success = true, message = "La Especialidad ha sido eliminada exitosamente." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "La Especialidad no existe." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Hubo un error al intentar eliminar la Especialidad." });
            }
        }
    }
}