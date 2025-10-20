using DIARS.Controllers.Dto.EvaluacionInterna;
using DIARS.Controllers.Dto;
using DIARS.Models;
using DIARS.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DIARS.Controllers.Dto.NotaIngresoRepuestos;

namespace DIARS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotaIngresoRepuestoController : ControllerBase
    {
        private readonly NotaIngresoRepuestoService _detalleService;
        private readonly IUsuarioService _usuarioService;
        private readonly IValidator<NIRListaDto> _validatorLista;
        private readonly IValidator<NIRAgregaDto> _validatorAgregar;
        private readonly IJwtService _jwtService;
        public NotaIngresoRepuestoController(NotaIngresoRepuestoService detalleService, IUsuarioService usuarioService, IValidator<NIRListaDto> validatorLista, IValidator<NIRAgregaDto> validatorAgregar, IJwtService jwtService)
        {
            _detalleService = detalleService;
            _usuarioService = usuarioService;
            _validatorLista = validatorLista;
            _validatorAgregar = validatorAgregar;
            _jwtService = jwtService;
        }

        [HttpGet]
        public IActionResult ListarNotaIngresoRepuesto()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = _jwtService.validarToken(identity, _usuarioService);

            if (!rToken.success)
                return Unauthorized(new { message = rToken.message });

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
                var lista = _detalleService.ListarNotaIngresoRepuestos();

                if (lista == null || lista.Count == 0)
                    return NotFound(new { mensaje = "No hay datos disponibles." });

                return Ok(new ResponseDto<List<NIRListaDto>>
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
        public IActionResult CrearNotaIngresoRepuesto([FromBody] NIRAgregaDto dto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = _jwtService.validarToken(identity, _usuarioService);

            if (!rToken.success)
                return Unauthorized(new { message = rToken.message });

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

            var resultado = _detalleService.InsertarNotaIngresoRepuestos(dto);

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
        public IActionResult ObtenerNotaIngresoRepuesto(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = _jwtService.validarToken(identity, _usuarioService);

            if (!rToken.success)
                return Unauthorized(new { message = rToken.message });

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

            var detalle = _detalleService.GetNotaIngresoRepuestosId(id);

            if (detalle == null)
            {
                return NotFound(new ResponseDto<object>
                {
                    MensajeError = "Datos no encontrados"
                });
            }

            return Ok(new ResponseDto<NIRListaDto>
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
                bool resultado = _detalleService.InhabilitarNotaIngreso(id);

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