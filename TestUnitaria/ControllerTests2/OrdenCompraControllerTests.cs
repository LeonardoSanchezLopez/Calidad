using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DIARS.Controllers;
using DIARS.Controllers.Dto.OrdenCompra;
using DIARS.Service;
using DIARS.Models;
using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using DIARS.Controllers.Dto;
using System;
using Microsoft.AspNetCore.Http;

[TestFixture]
public class OrdenCompraControllerTests
{
    private Mock<IOrdenCompraService> _mockOrdenCompraService;
    private Mock<IUsuarioService> _mockUsuarioService;
    private Mock<IJwtService> _mockJwtService;
    private Mock<IValidator<OrCoAgregaDto>> _mockValidatorAgregar;

    private OrdenCompraController _controller;

    [SetUp]
    public void SetUp()
    {
        _mockOrdenCompraService = new Mock<IOrdenCompraService>();
        _mockUsuarioService = new Mock<IUsuarioService>();
        _mockJwtService = new Mock<IJwtService>();
        _mockValidatorAgregar = new Mock<IValidator<OrCoAgregaDto>>();

        var mockValidatorLista = new Mock<IValidator<OrCoListaDto>>();

        _controller = new OrdenCompraController(
            _mockOrdenCompraService.Object,
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
    public void OrdenCompra_TokenInvalido_DebeRetornarUnauthorized()
    {
        SetupTokenInvalido();
        var result = _controller.ListarOrdenCompra();
        Assert.IsInstanceOf<UnauthorizedObjectResult>(result);
    }

    [Test]
    public void ListarOrdenCompra_RolNoAutorizado_DebeRetornarOkConMensajeDeError()
    {
        SetupUsuarioConRol("Invitado");
        var result = _controller.ListarOrdenCompra() as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);

        _mockOrdenCompraService.Verify(s => s.ListarOrdenCompra(), Times.Never());
    }

    [Test]
    public void ListarOrdenCompra_RolJefeDeAlmacen_DebeRetornarOkConDatos()
    {
        SetupUsuarioConRol("Jefe de Almacen");
        var listaMock = new List<OrCoListaDto> { new OrCoListaDto(), new OrCoListaDto() };
        _mockOrdenCompraService.Setup(s => s.ListarOrdenCompra()).Returns(listaMock);

        var result = _controller.ListarOrdenCompra() as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        _mockOrdenCompraService.Verify(s => s.ListarOrdenCompra(), Times.Once());
    }

    [Test]
    public void ListarOrdenCompra_SinDatos_DebeRetornarNotFound()
    {
        SetupUsuarioConRol("Administrador");
        _mockOrdenCompraService.Setup(s => s.ListarOrdenCompra()).Returns(new List<OrCoListaDto>());

        var result = _controller.ListarOrdenCompra() as NotFoundObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(404, result.StatusCode);
    }

    [Test]
    public void CrearOrdenCompra_DatosValidos_DebeRetornarOk()
    {
        SetupUsuarioConRol("Jefe de Compras");
        var dto = new OrCoAgregaDto { Total = 100 };

        _mockValidatorAgregar.Setup(v => v.Validate(dto)).Returns(new ValidationResult());
        _mockOrdenCompraService.Setup(s => s.InsertarOrdenCompra(dto))
            .Returns(new ResponseDto<bool> { EjecucionExitosa = true, Data = true });

        var result = _controller.CrearOrdenCompra(dto) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        _mockOrdenCompraService.Verify(s => s.InsertarOrdenCompra(dto), Times.Once());
    }

    [Test]
    public void CrearOrdenCompra_DatosInvalidos_DebeRetornarBadRequest()
    {
        SetupUsuarioConRol("Administrador");
        var dto = new OrCoAgregaDto();

        var errorList = new List<ValidationFailure> { new("Proveedor", "Error de Proveedor") };
        var validationResult = new ValidationResult(errorList);
        _mockValidatorAgregar.Setup(v => v.Validate(dto)).Returns(validationResult);

        var result = _controller.CrearOrdenCompra(dto) as BadRequestObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(400, result.StatusCode);

        _mockOrdenCompraService.Verify(s => s.InsertarOrdenCompra(It.IsAny<OrCoAgregaDto>()), Times.Never());
    }

    [Test]
    public void ObtenerOrdenCompra_IdExistente_DebeRetornarOkConDatos()
    {
        SetupUsuarioConRol("Jefe de Compras");
        var id = 1;
        var dtoMock = new OrCoListaDto { Id = id, Proveedor = "Proveedor Test" };

        _mockOrdenCompraService.Setup(s => s.GetOrdenCompraId(id)).Returns(dtoMock);

        var result = _controller.ObtenerOrdenCompra(id) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);

        Assert.IsInstanceOf<ResponseDto<OrCoListaDto>>(result.Value);

        var responseDto = (ResponseDto<OrCoListaDto>)result.Value;
        Assert.IsTrue(responseDto.EjecucionExitosa);
        Assert.AreEqual(id, responseDto.Data!.Id);
    }

    [Test]
    public void ObtenerOrdenCompra_IdNoExistente_DebeRetornarNotFound()
    {
        SetupUsuarioConRol("Administrador");
        var id = 99;

        _mockOrdenCompraService.Setup(s => s.GetOrdenCompraId(id)).Returns((OrCoListaDto)null!);

        var result = _controller.ObtenerOrdenCompra(id) as NotFoundObjectResult;

        // ASSERT
        Assert.IsNotNull(result);
        Assert.AreEqual(404, result.StatusCode);
    }

    [Test]
    public void Inhabilitar_ErrorInterno_DebeRetornarStatusCode500()
    {
        SetupUsuarioConRol("Administrador");
        var id = 1;

        _mockOrdenCompraService.Setup(s => s.InhabilitarOrdenCompra(id)).Throws(new Exception("Error de DB"));

        var result = _controller.EliminarPersona(id) as ObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(500, result.StatusCode);
    }

    [Test]
    public void Inhabilitar_RolNoAutorizado_DebeRetornarOkConMensajeDeError()
    {
        SetupUsuarioConRol("Jefe de Almacen");

        var result = _controller.EliminarPersona(1) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        _mockOrdenCompraService.Verify(s => s.InhabilitarOrdenCompra(It.IsAny<int>()), Times.Never());
    }
}