using DIARS.Controllers.Dto.Categoria;
using DIARS.Controllers.Dto;
using DIARS.Models;
using DIARS.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DIARS.Controllers.Dto.MarcaRepuesto;

namespace DIARS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly MarcaReService _personaService;
        private readonly IUsuarioService _usuarioService;
        private readonly IValidator<MarcaActuDto> _personaactuvalidator;
        private readonly IValidator<MarcaAgregaDto> _personacreatevalidator;
        private readonly IJwtService _jwtService;
        public MarcaController(MarcaReService personaService, IUsuarioService usuarioService, IValidator<MarcaActuDto> personaactuvalidator, IValidator<MarcaAgregaDto> personacreatevalidator, IJwtService jwtService)
        {
            _personaService = personaService;
            _usuarioService = usuarioService;
            _personaactuvalidator = personaactuvalidator;
            _personacreatevalidator = personacreatevalidator;
            _jwtService = jwtService;
        }

        [HttpGet]
        public IActionResult ListarMarca()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = _jwtService.validarToken(identity, _usuarioService);

            if (!rToken.success) return Unauthorized(new { message = rToken.message });

            Usuario usuario = rToken.result;

            if (usuario.Usu_Rol != "Jefe de Almacen" && usuario.Usu_Rol != "Administrador")
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
                List<MarcaListaDto> personasDto = _personaService.ListarMarca();

                if (personasDto == null || personasDto.Count == 0)
                    return NotFound(new { mensaje = "No hay personas activas." });

                return Ok(new ResponseDto<List<MarcaListaDto>> { EjecucionExitosa = true, MensajeError = null, Data = personasDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener la lista de Marcas.", error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CrearMarca([FromBody] MarcaAgregaDto personaDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = _jwtService.validarToken(identity, _usuarioService);
            //validator
            var validationResult = _personacreatevalidator.Validate(personaDto);

            if (!rToken.success) return Unauthorized(new { message = rToken.message });

            Usuario usuario = rToken.result;

            if (usuario.Usu_Rol != "Jefe de Almacen" && usuario.Usu_Rol != "Administrador")
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
            var resultado = _personaService.InsertarMarca(personaDto);

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
        public ActionResult<MarcaListaDto> GetBusById(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = _jwtService.validarToken(identity, _usuarioService);

            if (!rToken.success) return Unauthorized(new { message = rToken.message });

            Usuario usuario = rToken.result;

            if (usuario.Usu_Rol != "Jefe de Almacen" && usuario.Usu_Rol != "Administrador")
            {
                return Ok(new
                {
                    success = false,
                    message = "No tienes permisos para utilizar estos comandos",
                    result = ""
                });
            }

            MarcaListaDto personaDto = _personaService.GetMarcaId(id);

            if (personaDto == null)
            {
                return NotFound(new ResponseDto<object> { MensajeError = "Datos no encontrados" });
            }

            return Ok(new ResponseDto<MarcaListaDto> { EjecucionExitosa = true, MensajeError = null, Data = personaDto });
        }

        [HttpPut]
        public IActionResult ActualizarMarca([FromBody] MarcaActuDto personaDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = _jwtService.validarToken(identity, _usuarioService);

            if (!rToken.success) return Unauthorized(new { message = rToken.message });

            Usuario usuario = rToken.result;

            if (usuario.Usu_Rol != "Jefe de Almacen" && usuario.Usu_Rol != "Administrador")
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

            var resultado = _personaService.ActualizarMarca(personaDto);

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
        public IActionResult EliminarMarca(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var rToken = _jwtService.validarToken(identity, _usuarioService);
            if (!rToken.success) return Unauthorized(new { message = rToken.message });

            Usuario usuario = rToken.result;

            if (usuario.Usu_Rol != "Jefe de Almacen" && usuario.Usu_Rol != "Administrador")
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
                bool resultado = _personaService.InhabilitarMarca(id);

                if (resultado)
                {
                    return Ok(new { success = true, message = "La Marca ha sido eliminada exitosamente." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "La Marca no existe." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Hubo un error al intentar eliminar la Marca." });
            }
        }
    }
}
