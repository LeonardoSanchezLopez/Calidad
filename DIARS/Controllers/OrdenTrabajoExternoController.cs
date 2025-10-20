using DIARS.Controllers.Dto.OrdenPedido;
using DIARS.Controllers.Dto;
using DIARS.Models;
using DIARS.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DIARS.Controllers.Dto.OrdenTrabajoExterno;

namespace DIARS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenTrabajoExternoController : ControllerBase
    {
        private readonly OrdenTrabajoExternoService _detalleService;
        private readonly IUsuarioService _usuarioService;
        private readonly IValidator<OTEListaDto> _validatorLista;
        private readonly IValidator<OTEAgregaDto> _validatorAgregar;
        private readonly IJwtService _jwtService;

        public OrdenTrabajoExternoController(OrdenTrabajoExternoService detalleService, IUsuarioService usuarioService, IValidator<OTEListaDto> validatorLista, IValidator<OTEAgregaDto> validatorAgregar, IJwtService jwtService)
        {
            _detalleService = detalleService;
            _usuarioService = usuarioService;
            _validatorLista = validatorLista;
            _validatorAgregar = validatorAgregar;
            _jwtService = jwtService;
        }

        [HttpGet]
        public IActionResult ListarOrdenTrabajoExterno()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = _jwtService.validarToken(identity, _usuarioService);

            if (!rToken.success)
                return Unauthorized(new { message = rToken.message });

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
                var lista = _detalleService.ListarOrdenTrabajoExterno();

                if (lista == null || lista.Count == 0)
                    return NotFound(new { mensaje = "No hay datos disponibles." });

                return Ok(new ResponseDto<List<OTEListaDto>>
                {
                    EjecucionExitosa = true,
                    MensajeError = null,
                    Data = lista
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al obtener los datos.",
                    error = ex.Message
                });
            }
        }

        [HttpPost]
        public IActionResult CrearOrdenTrabajoExterno([FromBody] OTEAgregaDto dto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = _jwtService.validarToken(identity, _usuarioService);

            if (!rToken.success)
                return Unauthorized(new { message = rToken.message });

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

            var validationResult = _validatorAgregar.Validate(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseDto<List<string>>
                {
                    EjecucionExitosa = false,
                    MensajeError = "Errores de validación",
                    Data = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                });
            }

            var resultado = _detalleService.InsertarOrdenTrabajoExterno(dto);

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
        public IActionResult ObtenerOrdenTrabajoExterno(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = _jwtService.validarToken(identity, _usuarioService);

            if (!rToken.success)
                return Unauthorized(new { message = rToken.message });

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

            var detalle = _detalleService.GetOrdenTrabajoExternoId(id);

            if (detalle == null)
            {
                return NotFound(new ResponseDto<object>
                {
                    MensajeError = "Datos no encontrados"
                });
            }

            return Ok(new ResponseDto<OTEListaDto>
            {
                EjecucionExitosa = true,
                MensajeError = null,
                Data = detalle
            });
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarPersona(int id)
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
                bool resultado = _detalleService.InhabilitarOTE(id);

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