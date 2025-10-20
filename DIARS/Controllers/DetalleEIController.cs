using DIARS.Controllers.Dto.DetalleEI;
using DIARS.Controllers.Dto;
using DIARS.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DIARS.Models;

namespace DIARS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleEIController : ControllerBase
    {
        private readonly DetalleEIService _detalleService;
        private readonly IUsuarioService _usuarioService;
        private readonly IValidator<DEIListaDto> _detalleValidator;
        private readonly IValidator<DEIAgregaDto> _detalleCreateValidator;
        private readonly IJwtService _jwtService;

        public DetalleEIController(DetalleEIService detalleService, IUsuarioService usuarioService, IValidator<DEIListaDto> detalleValidator, IValidator<DEIAgregaDto> detalleCreateValidator, IJwtService jwtService)
        {
            _detalleService = detalleService;
            _usuarioService = usuarioService;
            _detalleValidator = detalleValidator;
            _detalleCreateValidator = detalleCreateValidator;
            _jwtService = jwtService;
        }

        [HttpGet]
        public IActionResult ListarDetalleEvaluacionInterna()
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
                var detalles = _detalleService.ListarDetalleEI();

                if (detalles == null || detalles.Count == 0)
                    return NotFound(new { mensaje = "No hay registros activos." });

                return Ok(new ResponseDto<List<DEIListaDto>>
                {
                    EjecucionExitosa = true,
                    MensajeError = null,
                    Data = detalles
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener la lista de registros.", error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CrearDetalleEvaluacionInterna([FromBody] DEIAgregaDto detalleDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = _jwtService.validarToken(identity, _usuarioService);

            // Validator
            var validationResult = _detalleCreateValidator.Validate(detalleDto);

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

            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseDto<List<string>>
                {
                    EjecucionExitosa = false,
                    MensajeError = "Errores de validación",
                    Data = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                });
            }

            var resultado = _detalleService.InsertarDetalleEI(detalleDto);

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
        public IActionResult GetDetalleEvaluacionInternaById(int id)
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

            var detalle = _detalleService.ObtenerDetalleEI(id);

            if (detalle == null)
            {
                return NotFound(new ResponseDto<object> { MensajeError = "Datos no encontrados" });
            }

            return Ok(new ResponseDto<DEIListaDto>
            {
                EjecucionExitosa = true,
                MensajeError = null,
                Data = detalle
            });
        }
    }
}