using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DIARS.Controllers;
using DIARS.Controllers.Dto.DetalleOrdenCompra;
using DIARS.Service;
using DIARS.Models;
using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using DIARS.Controllers.Dto;
using System;
using Microsoft.AspNetCore.Http;
using DIARS.Service;

[TestFixture]
public class DetalleOrdenCompraControllerTests
{
    private Mock<IDetalleOrdenCompraService> _mockDetalleService;
    private Mock<IUsuarioService> _mockUsuarioService;
    private Mock<IJwtService> _mockJwtService;
    private Mock<IValidator<DOrCoAgregaDto>> _mockValidatorAgregar;

    private DetalleOrdenCompraController _controller;

    [SetUp]
    public void SetUp()
    {
        _mockDetalleService = new Mock<IDetalleOrdenCompraService>();
        _mockUsuarioService = new Mock<IUsuarioService>();
        _mockJwtService = new Mock<IJwtService>();
        _mockValidatorAgregar = new Mock<IValidator<DOrCoAgregaDto>>();

        var mockValidatorLista = new Mock<IValidator<DOrCoListaDto>>();

        _controller = new DetalleOrdenCompraController(
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

        SetupUsuarioConRol("Administrador");
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
    public void DetalleOrdenCompra_TokenInvalido_DebeRetornarUnauthorized()
    {
        SetupTokenInvalido();
        var result = _controller.ListarDetalleOrdenCompra();
        Assert.IsInstanceOf<UnauthorizedObjectResult>(result);
    }

    [Test]
    public void ListarDetalleOrdenCompra_RolAutorizado_DebeRetornarOkConDatos()
    {
        SetupUsuarioConRol("Jefe de Compras");
        var listaMock = new List<DOrCoListaDto> {
            new DOrCoListaDto { Cod_CompraOD = 1, Repuesto = "Filtro", Precio = 50.0m },
            new DOrCoListaDto { Cod_CompraOD = 2, Repuesto = "Batería", Precio = 120.0m }
        };
        _mockDetalleService.Setup(s => s.ListarDetalleOrdenCompra()).Returns(listaMock);
        var result = _controller.ListarDetalleOrdenCompra() as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        _mockDetalleService.Verify(s => s.ListarDetalleOrdenCompra(), Times.Once());

        Assert.IsInstanceOf<ResponseDto<List<DOrCoListaDto>>>(result.Value);
        var responseDto = (ResponseDto<List<DOrCoListaDto>>)result.Value;
        Assert.IsTrue(responseDto.EjecucionExitosa);
        Assert.AreEqual(2, responseDto.Data.Count);
    }

    [Test]
    public void ListarDetalleOrdenCompra_SinDatos_DebeRetornarNotFound()
    {
        SetupUsuarioConRol("Administrador");
        _mockDetalleService.Setup(s => s.ListarDetalleOrdenCompra()).Returns(new List<DOrCoListaDto>());

        var result = _controller.ListarDetalleOrdenCompra() as NotFoundObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(404, result.StatusCode);
        _mockDetalleService.Verify(s => s.ListarDetalleOrdenCompra(), Times.Once());
    }

    [Test]
    public void ListarDetalleOrdenCompra_RolNoAutorizado_DebeRetornarOkConMensajeDeError()
    {
        SetupUsuarioConRol("Vendedor");

        var result = _controller.ListarDetalleOrdenCompra() as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        _mockDetalleService.Verify(s => s.ListarDetalleOrdenCompra(), Times.Never());
    }


    [Test]
    public void InsertarDetalleOrdenCompra_DatosValidos_DebeRetornarOk()
    {
        SetupUsuarioConRol("Administrador");
        var dto = new DOrCoAgregaDto { Cod_CompraOD = 1, Cantidad = 5, Precio = 10.0m, Repuesto = "Filtro" };

        _mockValidatorAgregar.Setup(v => v.Validate(dto)).Returns(new ValidationResult());

        _mockDetalleService.Setup(s => s.InsertarDetalleOrdenCompra(dto))
            .Returns(new ResponseDto<bool> { EjecucionExitosa = true, Data = true, MensajeError = "Detalle agregado" });

        var result = _controller.CrearDetalleOrdenCompra(dto) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        _mockDetalleService.Verify(s => s.InsertarDetalleOrdenCompra(dto), Times.Once());

        Assert.IsInstanceOf<ResponseDto<bool>>(result.Value);
        var responseDto = (ResponseDto<bool>)result.Value;
        Assert.IsTrue(responseDto.EjecucionExitosa);
    }

    [Test]
    public void InsertarDetalleOrdenCompra_DatosInvalidos_DebeRetornarBadRequest()
    {
        SetupUsuarioConRol("Jefe de Compras");
        var dto = new DOrCoAgregaDto { Cantidad = 0 };

        var errorList = new List<ValidationFailure> { new("Cantidad", "La cantidad debe ser mayor a cero") };
        var validationResult = new ValidationResult(errorList);
        _mockValidatorAgregar.Setup(v => v.Validate(dto)).Returns(validationResult);

        var result = _controller.CrearDetalleOrdenCompra(dto) as BadRequestObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(400, result.StatusCode);

        _mockDetalleService.Verify(s => s.InsertarDetalleOrdenCompra(It.IsAny<DOrCoAgregaDto>()), Times.Never());
    }

    [Test]
    public void InsertarDetalleOrdenCompra_RolNoAutorizado_DebeRetornarOkConMensajeDeError()
    {
        SetupUsuarioConRol("Jefe de Almacen");
        var dto = new DOrCoAgregaDto { Cantidad = 1 };
        _mockValidatorAgregar.Setup(v => v.Validate(dto)).Returns(new ValidationResult());

        var result = _controller.CrearDetalleOrdenCompra(dto) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        _mockDetalleService.Verify(s => s.InsertarDetalleOrdenCompra(It.IsAny<DOrCoAgregaDto>()), Times.Never());
    }

    [Test]
    public void ObtenerDetalleOrdenCompra_IdExistente_DebeRetornarOkConDatos()
    {
        SetupUsuarioConRol("Jefe de Compras");
        var id = 1;
        var dtoMock = new DOrCoListaDto { Id = id, Repuesto = "Neumático", Precio = 200.0m };

        _mockDetalleService.Setup(s => s.ObtenerDetalleOrdenCompra(id)).Returns(dtoMock);
        var result = _controller.ObtenerDetalleOrdenCompra(id) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);

        Assert.IsInstanceOf<ResponseDto<DOrCoListaDto>>(result.Value);
        var responseDto = (ResponseDto<DOrCoListaDto>)result.Value;

        Assert.IsTrue(responseDto.EjecucionExitosa);
        Assert.AreEqual(id, responseDto.Data!.Id);
    }

    [Test]
    public void ObtenerDetalleOrdenCompra_IdNoExistente_DebeRetornarNotFound()
    {
        SetupUsuarioConRol("Administrador");
        var id = 99;

        _mockDetalleService.Setup(s => s.ObtenerDetalleOrdenCompra(id)).Returns((DOrCoListaDto)null!);
        var result = _controller.ObtenerDetalleOrdenCompra(id) as NotFoundObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(404, result.StatusCode);
    }

    [Test]
    public void ObtenerDetalleOrdenCompra_RolNoAutorizado_DebeRetornarOkConMensajeDeError()
    {
        SetupUsuarioConRol("Jefe de Almacen");
        var result = _controller.ObtenerDetalleOrdenCompra(1) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        _mockDetalleService.Verify(s => s.ObtenerDetalleOrdenCompra(It.IsAny<int>()), Times.Never());
    }
}