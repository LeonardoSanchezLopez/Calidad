using DIARS.Controllers.Dto.OrdenCompra;
using DIARS.Controllers.Dto;
using DIARS.Models;
using DIARS.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DIARS.Controllers.Dto.OrdenPedido;

namespace DIARS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenPedidoController : ControllerBase
    {
        private readonly IOrdenPedidoService _detalleService;
        private readonly IUsuarioService _usuarioService;
        private readonly IValidator<OrPeListaDto> _validatorLista;
        private readonly IValidator<OrPeAgregaDto> _validatorAgregar;
        private readonly IJwtService _jwtService;

        public OrdenPedidoController(IOrdenPedidoService detalleService, IUsuarioService usuarioService, IValidator<OrPeListaDto> validatorLista, IValidator<OrPeAgregaDto> validatorAgregar, IJwtService jwtService)
        {
            _detalleService = detalleService;
            _usuarioService = usuarioService;
            _validatorLista = validatorLista;
            _validatorAgregar = validatorAgregar;
            _jwtService = jwtService;
        }

        [HttpGet]
        public IActionResult ListarOrdenPedido()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = _jwtService.validarToken(identity, _usuarioService);

            if (!rToken.success)
                return Unauthorized(new { message = rToken.message });

            Usuario usuario = rToken.result;

            if (usuario.Usu_Rol != "Jefe de Almacen" && usuario.Usu_Rol != "Jefe de Compras" && usuario.Usu_Rol != "Administrador")
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
                var lista = _detalleService.ListarOrdenPedido();

                if (lista == null || lista.Count == 0)
                    return NotFound(new { mensaje = "No hay datos disponibles." });

                return Ok(new ResponseDto<List<OrPeListaDto>>
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
        public IActionResult CrearOrdenPedido([FromBody] OrPeAgregaDto dto)
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

            var resultado = _detalleService.InsertarOrdenPedido(dto);

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
        public IActionResult ObtenerOrdenPedido(int id)
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

            var detalle = _detalleService.GetOrdenPedidoId(id);

            if (detalle == null)
            {
                return NotFound(new ResponseDto<object>
                {
                    MensajeError = "Datos no encontrados"
                });
            }

            return Ok(new ResponseDto<OrPeListaDto>
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
                bool resultado = _detalleService.InhabilitarOrdenPedidio(id);

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