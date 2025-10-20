using DIARS.Controllers.Dto.DetalleNotaSalida;
using DIARS.Controllers.Dto;
using DIARS.Models;
using DIARS.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DIARS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleNotaSalidaController : ControllerBase
    {
        private readonly DetalleNotaSalidaService _detalleService;
        private readonly IUsuarioService _usuarioService;
        private readonly IValidator<DNoSaListaDto> _validatorLista;
        private readonly IValidator<DNoSaAgregaDto> _validatorAgregar;
        private readonly IJwtService _jwtService;

        public DetalleNotaSalidaController(DetalleNotaSalidaService detalleService, IUsuarioService usuarioService, IValidator<DNoSaListaDto> validatorLista, IValidator<DNoSaAgregaDto> validatorAgregar, IJwtService jwtService)
        {
            _detalleService = detalleService;
            _usuarioService = usuarioService;
            _validatorLista = validatorLista;
            _validatorAgregar = validatorAgregar;
            _jwtService = jwtService;
        }

        [HttpGet]
        public IActionResult ListarDetalleNotaSalida()
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
                var lista = _detalleService.ListarDetalleNotaSalida();

                if (lista == null || lista.Count == 0)
                    return NotFound(new { mensaje = "No hay datos disponibles." });

                return Ok(new ResponseDto<List<DNoSaListaDto>>
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
        public IActionResult CrearDetalleNotaSalida([FromBody] DNoSaAgregaDto dto)
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

            var resultado = _detalleService.InsertarDetalleNotaSalida(dto);

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
        public IActionResult ObtenerDetalleNotaSalida(int id)
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

            var detalle = _detalleService.ObtenerDetalleNotaSalida(id);

            if (detalle == null)
            {
                return NotFound(new ResponseDto<object>
                {
                    MensajeError = "Datos no encontrados"
                });
            }

            return Ok(new ResponseDto<DNoSaListaDto>
            {
                EjecucionExitosa = true,
                MensajeError = null,
                Data = detalle
            });
        }
    }
}