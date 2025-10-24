using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DIARS.Controllers;
using DIARS.Controllers.Dto.OrdenPedido;
using DIARS.Service;
using DIARS.Models;
using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using DIARS.Controllers.Dto;

[TestFixture]
public class OrdenPedidoControllerTests
{
    private Mock<IOrdenPedidoService> _mockOrdenPedidoService;
    private Mock<IUsuarioService> _mockUsuarioService;
    private Mock<IJwtService> _mockJwtService;
    private Mock<IValidator<OrPeAgregaDto>> _mockValidatorAgregar;

    private OrdenPedidoController _controller;

    private readonly string _rolAdministrador = "Administrador";
    private readonly string _rolJefeAlmacen = "Jefe de Almacen";
    private readonly string _rolJefeCompras = "Jefe de Compras";
    private readonly string _rolNoAutorizado = "Vendedor";

    [SetUp]
    public void SetUp()
    {
        _mockOrdenPedidoService = new Mock<IOrdenPedidoService>();
        _mockUsuarioService = new Mock<IUsuarioService>();
        _mockJwtService = new Mock<IJwtService>();
        _mockValidatorAgregar = new Mock<IValidator<OrPeAgregaDto>>();
        var mockValidatorLista = new Mock<IValidator<OrPeListaDto>>();

        _controller = new OrdenPedidoController(
            _mockOrdenPedidoService.Object,
            _mockUsuarioService.Object,
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

    private void SetupTokenInvalido()
    {
        _mockJwtService.Setup(s => s.validarToken(
            It.IsAny<ClaimsIdentity>(),
            It.IsAny<IUsuarioService>()
        )).Returns(new JwtResponse { success = false, message = "Token Invalido", result = null! });
    }


    [Test]
    public void OrdenPedido_TokenInvalido_DebeRetornarUnauthorized()
    {
        SetupTokenInvalido();
        var result = _controller.ListarOrdenPedido();
        Assert.IsInstanceOf<UnauthorizedObjectResult>(result);
    }

    [Test]
    public void ListarOrdenPedido_RolJefeDeCompras_DebeRetornarOkConDatos()
    {
        SetupUsuarioConRol(_rolJefeCompras);
        var listaMock = new List<OrPeListaDto> { new OrPeListaDto(), new OrPeListaDto() };
        _mockOrdenPedidoService.Setup(s => s.ListarOrdenPedido()).Returns(listaMock);

        var result = _controller.ListarOrdenPedido() as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        _mockOrdenPedidoService.Verify(s => s.ListarOrdenPedido(), Times.Once());
    }

    [Test]
    public void ListarOrdenPedido_SinDatos_DebeRetornarNotFound()
    {
        SetupUsuarioConRol(_rolAdministrador);
        _mockOrdenPedidoService.Setup(s => s.ListarOrdenPedido()).Returns(new List<OrPeListaDto>());

        var result = _controller.ListarOrdenPedido() as NotFoundObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(404, result.StatusCode);
    }


    [Test]
    public void CrearOrdenPedido_DatosValidos_DebeRetornarOk()
    {
        SetupUsuarioConRol(_rolJefeAlmacen);
        var dto = new OrPeAgregaDto { BusPlaca = "A1B-234", Descripcion = "Falla de motor" }; // Usando propiedades del DTO

        _mockValidatorAgregar.Setup(v => v.Validate(dto)).Returns(new ValidationResult());
        _mockOrdenPedidoService.Setup(s => s.InsertarOrdenPedido(dto))
            .Returns(new ResponseDto<bool> { EjecucionExitosa = true, Data = true });

        var result = _controller.CrearOrdenPedido(dto) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        _mockOrdenPedidoService.Verify(s => s.InsertarOrdenPedido(dto), Times.Once());
    }

    [Test]
    public void CrearOrdenPedido_DatosInvalidos_DebeRetornarBadRequest()
    {
        SetupUsuarioConRol(_rolAdministrador);
        var dto = new OrPeAgregaDto();

        var errorList = new List<ValidationFailure> { new("BusPlaca", "La placa es obligatoria") };
        var validationResult = new ValidationResult(errorList);
        _mockValidatorAgregar.Setup(v => v.Validate(dto)).Returns(validationResult);

        var result = _controller.CrearOrdenPedido(dto) as BadRequestObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(400, result.StatusCode);
        _mockOrdenPedidoService.Verify(s => s.InsertarOrdenPedido(It.IsAny<OrPeAgregaDto>()), Times.Never());
    }

    [Test]
    public void CrearOrdenPedido_RolJefeDeComprasNoAutorizado_DebeRetornarOkConMensajeDeError()
    {
        SetupUsuarioConRol(_rolJefeCompras);
        var dto = new OrPeAgregaDto();
        _mockValidatorAgregar.Setup(v => v.Validate(dto)).Returns(new ValidationResult());

        var result = _controller.CrearOrdenPedido(dto) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        _mockOrdenPedidoService.Verify(s => s.InsertarOrdenPedido(It.IsAny<OrPeAgregaDto>()), Times.Never());
    }

    [Test]
    public void Inhabilitar_ErrorInterno_DebeRetornarStatusCode500()
    {
        SetupUsuarioConRol("Administrador");
        var id = 1;

        _mockOrdenPedidoService.Setup(s => s.InhabilitarOrdenPedidio(id)).Throws(new Exception("Error de DB"));

        var result = _controller.EliminarPersona(id) as ObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(500, result.StatusCode);
    }
}