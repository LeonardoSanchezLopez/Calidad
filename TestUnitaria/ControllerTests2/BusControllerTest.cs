using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using DIARS.Controllers;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Dto.Bus;
using DIARS.Models;
using DIARS.Service;
using FluentValidation;
using System.Linq;
using System;

[TestFixture]
public class BusControllerTests
{
    private Mock<IBusService> _mockBusService;
    private Mock<IUsuarioService> _mockUsuarioService;
    private Mock<IJwtService> _mockJwtService;
    private Mock<IValidator<BusActuDto>> _mockBusActuValidator;
    private Mock<IValidator<BusAgregaDto>> _mockBusAgregaValidator;
    private BusController _controller;

    [SetUp]
    public void SetUp()
    {
        _mockBusService = new Mock<IBusService>();
        _mockUsuarioService = new Mock<IUsuarioService>();
        _mockJwtService = new Mock<IJwtService>();
        _mockBusActuValidator = new Mock<IValidator<BusActuDto>>();
        _mockBusAgregaValidator = new Mock<IValidator<BusAgregaDto>>();

        _controller = new BusController(
            _mockBusService.Object,
            _mockUsuarioService.Object,
            _mockBusActuValidator.Object,
            _mockBusAgregaValidator.Object,
            _mockJwtService.Object
        );

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };
    }


    [Test]
    public void ListarBuses_RolAutorizado_DebeRetornarOkConDatos()
    {
        var busList = new List<BusListaDto> { new BusListaDto { Id = 1, Marca = "BMW" } };
        var usuarioAutorizado = new Usuario { Usu_Rol = "Administrador" };

        _mockJwtService.Setup(s => s.validarToken(
            It.IsAny<ClaimsIdentity>(),
            It.IsAny<IUsuarioService>()
        )).Returns(new JwtResponse { success = true, message = "Token OK", result = usuarioAutorizado }); // Usamos JwtResponse

        _mockBusService.Setup(s => s.ListarBus()).Returns(busList);

        var result = _controller.ListarPersonas() as OkObjectResult;

        _mockBusService.Verify(s => s.ListarBus(), Times.Once, "Se esperaba la llamada a IBusService.ListarBus() al ser un rol autorizado.");

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);

        var responseDto = result.Value as ResponseDto<List<BusListaDto>>;
        Assert.IsTrue(responseDto.EjecucionExitosa);
        Assert.AreEqual(1, responseDto.Data.Count);
    }



    [Test]
    public void ListarBuses_TokenInvalido_DebeRetornarUnauthorized()
    {
        _mockJwtService.Setup(s => s.validarToken(
            It.IsAny<ClaimsIdentity>(),
            It.IsAny<IUsuarioService>()
        )).Returns(new JwtResponse { success = false, message = "Token Inválido", result = (Usuario)null });

        var result = _controller.ListarPersonas() as UnauthorizedObjectResult;
        _mockBusService.Verify(s => s.ListarBus(), Times.Never, "El servicio de negocio NO debe llamarse si el token es inválido.");

        Assert.IsNotNull(result);
        Assert.AreEqual(401, result.StatusCode);
    }

    [Test]
    public void CrearBus_DatosValidos_DebeRetornarOk()
    {
        var personaDto = new BusAgregaDto { Marca = "Volvo", Capacidad = 40 };
        var usuarioAutorizado = new Usuario { Usu_Rol = "Administrador" };
        var expectedResponse = new ResponseDto<bool> { EjecucionExitosa = true, MensajeError = null, Data = true };

        _mockJwtService.Setup(s => s.validarToken(It.IsAny<ClaimsIdentity>(), It.IsAny<IUsuarioService>()))
            .Returns(new JwtResponse { success = true, message = "Token OK", result = usuarioAutorizado });

        _mockBusAgregaValidator.Setup(v => v.Validate(personaDto)).Returns(new FluentValidation.Results.ValidationResult());
        _mockBusService.Setup(s => s.InsertarBus(personaDto)).Returns(expectedResponse);

        var result = _controller.CrearPersona(personaDto) as OkObjectResult;

        _mockBusService.Verify(s => s.InsertarBus(personaDto), Times.Once);
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);

        var response = result.Value as ResponseDto<bool>;
        Assert.IsTrue(response.EjecucionExitosa);
    }

    [Test]
    public void CrearBus_DatosInvalidos_DebeRetornarBadRequest()
    {
        var personaDto = new BusAgregaDto { Marca = " ", Capacidad = 0 };
        var usuarioAutorizado = new Usuario { Usu_Rol = "Administrador" };

        _mockJwtService.Setup(s => s.validarToken(It.IsAny<ClaimsIdentity>(), It.IsAny<IUsuarioService>()))
            .Returns(new JwtResponse { success = true, message = "Token OK", result = usuarioAutorizado }); // Usamos JwtResponse

        var validationErrors = new List<FluentValidation.Results.ValidationFailure>
        {
            new FluentValidation.Results.ValidationFailure("Marca", "La marca es requerida")
        };
        _mockBusAgregaValidator.Setup(v => v.Validate(personaDto)).Returns(new FluentValidation.Results.ValidationResult(validationErrors));

        var result = _controller.CrearPersona(personaDto) as BadRequestObjectResult;

        _mockBusService.Verify(s => s.InsertarBus(It.IsAny<BusAgregaDto>()), Times.Never, "El servicio no debe llamarse si hay errores de validación.");
        Assert.IsNotNull(result);
        Assert.AreEqual(400, result.StatusCode);

        var responseDto = result.Value as ResponseDto<List<string>>;
        Assert.IsFalse(responseDto.EjecucionExitosa);
        Assert.AreEqual(1, responseDto.Data.Count);
    }


    [Test]
    public void GetBusById_BusExiste_DebeRetornarBus()
    {
        int busId = 1;
        var busDto = new BusListaDto { Id = busId, Marca = "Mercedes" };
        var usuarioAutorizado = new Usuario { Usu_Rol = "Administrador" };

        _mockJwtService.Setup(s => s.validarToken(It.IsAny<ClaimsIdentity>(), It.IsAny<IUsuarioService>()))
            .Returns(new JwtResponse { success = true, message = "Token OK", result = usuarioAutorizado });

        _mockBusService.Setup(s => s.GetBusId(busId)).Returns(busDto);

        var actionResult = _controller.GetBusById(busId);
        var result = actionResult.Result as OkObjectResult;

        _mockBusService.Verify(s => s.GetBusId(busId), Times.Once);
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);

        var responseDto = result.Value as ResponseDto<BusListaDto>;
        Assert.IsTrue(responseDto.EjecucionExitosa);
        Assert.AreEqual(busId, responseDto.Data.Id);
    }
}