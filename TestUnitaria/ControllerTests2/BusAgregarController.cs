using NUnit.Framework;
using static NUnit.Framework.Assert;
using DIARS.FluentValidation.Bus;
using DIARS.Controllers.Dto.Bus;
using System;
using System.Linq;
using FluentValidation.TestHelper;

[TestFixture]
public class BusAgregaDtoValidatorTests
{
    private BusAgregaDtoValidator _validator;

    [SetUp]
    public void Setup()
    {
        _validator = new BusAgregaDtoValidator();
    }

    private BusAgregaDto GetValidBusDto()
    {
        return new BusAgregaDto
        {
            Marca = "Volvo",
            Modelo = "B45",
            Piso = "Doble",
            Placa = "ABC-123",
            Chasis = "X12345",
            Motor = "Y67890",
            Capacidad = 50,
            TipoMotor = "Diesel",
            Combustible = "Diesel",
            Kilometraje = 100,
            FechaAdquisicion = DateTime.Now.AddDays(-1)
        };
    }

    [Test]
    public void DtoCompleto_DebeSerValido()
    {
        var busDto = GetValidBusDto();

        var resultado = _validator.Validate(busDto);

        Assert.IsTrue(resultado.IsValid, "El DTO válido no debe contener errores.");
    }


    [Test]
    public void ValidarMarca_DebeFallar_CuandoEstaVacia()
    {
        var busDto = GetValidBusDto();
        busDto.Marca = "";

        var resultado = _validator.TestValidate(busDto);

        resultado.ShouldHaveValidationErrorFor(bus => bus.Marca)
                 .WithErrorMessage("La marca no puede estar vacía.");
    }

    [Test]
    public void ValidarModelo_DebeFallar_CuandoExcede50Caracteres()
    {
        var busDto = GetValidBusDto();
        busDto.Modelo = new string('A', 51);

        var resultado = _validator.TestValidate(busDto);

        resultado.ShouldHaveValidationErrorFor(bus => bus.Modelo)
                 .WithErrorMessage("El modelo no puede exceder los 50 caracteres.");
    }

    [Test]
    public void ValidarCapacidad_DebeFallar_CuandoCapacidadEsCeroOMenor()
    {
        var busDto = GetValidBusDto();
        busDto.Capacidad = 0;

        var resultado = _validator.Validate(busDto);

        Assert.IsFalse(resultado.IsValid, "La validación debería haber fallado, ya que la capacidad es cero.");

        Assert.IsTrue(resultado.Errors.Any(e => e.PropertyName == "Capacidad"), "No se encontró un error para el campo Capacidad.");

        Assert.IsTrue(resultado.Errors.Any(e => e.ErrorMessage.Contains("mayor a 0")), "El mensaje de error no es el esperado.");
    }

    [Test]
    public void ValidarCapacidad_DebeFallar_CuandoCapacidadExcede100()
    {
        var busDto = GetValidBusDto();
        busDto.Capacidad = 101;

        var resultado = _validator.TestValidate(busDto);

        resultado.ShouldHaveValidationErrorFor(bus => bus.Capacidad)
                 .WithErrorMessage("La capacidad máxima permitida es de 100 pasajeros.");
    }

    [Test]
    public void ValidarPlaca_DebeFallar_CuandoPlacaExcede10Caracteres()
    {
        var busDto = GetValidBusDto();
        busDto.Placa = "ABC-1234567";

        var resultado = _validator.TestValidate(busDto);

        resultado.ShouldHaveValidationErrorFor(bus => bus.Placa)
                 .WithErrorMessage("La placa no puede exceder los 10 caracteres.");
    }

    [Test]
    public void ValidarChasis_DebeFallar_CuandoEstaVacio()
    {

        var busDto = GetValidBusDto();
        busDto.Chasis = null;

        var resultado = _validator.TestValidate(busDto);

        resultado.ShouldHaveValidationErrorFor(bus => bus.Chasis)
                 .WithErrorMessage("El número de chasis no puede estar vacío.");
    }

    [Test]
    public void ValidarMotor_DebeFallar_CuandoExcede30Caracteres()
    {
        var busDto = GetValidBusDto();
        busDto.Motor = new string('M', 31);

        var resultado = _validator.TestValidate(busDto);

        resultado.ShouldHaveValidationErrorFor(bus => bus.Motor)
                 .WithErrorMessage("El número de motor no puede exceder los 30 caracteres.");
    }


    [Test]
    public void ValidarFechaAdquisicion_DebeFallar_CuandoEsFutura()
    {
        var busDto = GetValidBusDto();
        busDto.FechaAdquisicion = DateTime.Now.AddDays(1);

        var resultado = _validator.TestValidate(busDto);

        resultado.ShouldHaveValidationErrorFor(bus => bus.FechaAdquisicion)
                 .WithErrorMessage("La fecha de adquisición no puede ser futura.");
    }

    [Test]
    public void ValidarCombustible_DebeFallar_CuandoEsVacio()
    {

        var busDto = GetValidBusDto();
        busDto.Combustible = "";

        var resultado = _validator.TestValidate(busDto);

        resultado.ShouldHaveValidationErrorFor(bus => bus.Combustible)
                 .WithErrorMessage("El tipo de combustible no puede estar vacío.");
    }
}
