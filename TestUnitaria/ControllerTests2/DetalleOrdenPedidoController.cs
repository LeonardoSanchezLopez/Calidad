using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DIARS.Controllers;
using DIARS.Controllers.Dto.DetalleOrdenPedido;
using DIARS.Service;
using DIARS.Models;
using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using DIARS.Controllers.Dto;

[TestFixture]
public class DetalleOrdenPedidoControllerTests
{
    private Mock<IDetalleOrdenPedidoService> _mockDetalleService;
    private Mock<IUsuarioService> _mockUsuarioService;
    private Mock<IJwtService> _mockJwtService;
    private Mock<IValidator<DOrPeAgregaDto>> _mockValidatorAgregar;

    private DetalleOrdenPedidoController _controller;

    private readonly string _rolAdministrador = "Administrador";
    private readonly string _rolJefeAlmacen = "Jefe de Almacen";
    private readonly string _rolNoAutorizado = "Mecanico";

    [SetUp]
    public void SetUp()
    {
        _mockDetalleService = new Mock<IDetalleOrdenPedidoService>();
        _mockUsuarioService = new Mock<IUsuarioService>();
        _mockJwtService = new Mock<IJwtService>();
        _mockValidatorAgregar = new Mock<IValidator<DOrPeAgregaDto>>();
        var mockValidatorLista = new Mock<IValidator<DOrPeListaDto>>();

        _controller = new DetalleOrdenPedidoController(
            _mockDetalleService.Object,
            (IUsuarioService)_mockUsuarioService.Object,
            mockValidatorLista.Object,
            _mockValidatorAgregar.Object,
            _mockJwtService.Object
        );

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        SetupUsuarioConRol(_rolAdministrador);
    }

    private void SetupUsuarioConRol(string rol)
    {
        var usuario = new Usuario { Usu_Rol = rol };
        _mockJwtService.Setup(s => s.validarToken(
            It.IsAny<ClaimsIdentity>(),
            It.IsAny<IUsuarioService>()
        )).Returns(new JwtResponse { success = true, message = "Token OK", result = usuario });
    }

    [Test]
    public void ListarDetalleOrdenPedido_RolAutorizado_DebeRetornarOkConDatos()
    {
        SetupUsuarioConRol(_rolJefeAlmacen);
        var listaMock = new List<DOrPeListaDto> {
            new DOrPeListaDto { Cod_OrdenPD = 1, Repuesto = "Filtro" },
        };
        _mockDetalleService.Setup(s => s.ListarDetalleOrdenPedido()).Returns(listaMock);

        var result = _controller.ListarDetalleOrdenPedido() as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        _mockDetalleService.Verify(s => s.ListarDetalleOrdenPedido(), Times.Once());
    }

    [Test]
    public void ListarDetalleOrdenPedido_SinDatos_DebeRetornarNotFound()
    {
        SetupUsuarioConRol(_rolAdministrador);
        _mockDetalleService.Setup(s => s.ListarDetalleOrdenPedido()).Returns(new List<DOrPeListaDto>());

        var result = _controller.ListarDetalleOrdenPedido() as NotFoundObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(404, result.StatusCode);
        _mockDetalleService.Verify(s => s.ListarDetalleOrdenPedido(), Times.Once());
    }

    [Test]
    public void InsertarDetalleOrdenPedido_DatosValidos_DebeRetornarOk()
    {
        SetupUsuarioConRol(_rolJefeAlmacen);
        var dto = new DOrPeAgregaDto { Cod_OrdenPD = 1, Cantidad = 5, Repuesto = "Bujía" }; // Usando DTO actualizado

        _mockValidatorAgregar.Setup(v => v.Validate(dto)).Returns(new ValidationResult());
        _mockDetalleService.Setup(s => s.InsertarDetalleOrdenPedido(dto))
            .Returns(new ResponseDto<bool> { EjecucionExitosa = true, Data = true });

        var result = _controller.CrearDetalleOrdenPedido(dto) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        _mockDetalleService.Verify(s => s.InsertarDetalleOrdenPedido(dto), Times.Once());
    }

    [Test]
    public void InsertarDetalleOrdenPedido_DatosInvalidos_DebeRetornarBadRequest()
    {
        SetupUsuarioConRol(_rolAdministrador);
        var dto = new DOrPeAgregaDto { Cantidad = 0 };

        var errorList = new List<ValidationFailure> { new("Cantidad", "La cantidad debe ser mayor a cero") };
        var validationResult = new ValidationResult(errorList);
        _mockValidatorAgregar.Setup(v => v.Validate(dto)).Returns(validationResult);

        var result = _controller.CrearDetalleOrdenPedido(dto) as BadRequestObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(400, result.StatusCode);
        _mockDetalleService.Verify(s => s.InsertarDetalleOrdenPedido(It.IsAny<DOrPeAgregaDto>()), Times.Never());
    }

    [Test]
    public void InsertarDetalleOrdenPedido_RolNoAutorizado_DebeRetornarOkConMensajeDeError()
    {
        SetupUsuarioConRol(_rolNoAutorizado);
        var dto = new DOrPeAgregaDto { Cantidad = 1 };
        _mockValidatorAgregar.Setup(v => v.Validate(dto)).Returns(new ValidationResult());

        var result = _controller.CrearDetalleOrdenPedido(dto) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        _mockDetalleService.Verify(s => s.InsertarDetalleOrdenPedido(It.IsAny<DOrPeAgregaDto>()), Times.Never());
    }
}