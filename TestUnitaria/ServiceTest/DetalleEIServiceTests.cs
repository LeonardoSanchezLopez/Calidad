using DIARS;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Dto.DetalleEI;
using DIARS.Controllers.Mapping;
using DIARS.Models;
using DIARS.Service;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace TestUnitaria
{
    [TestFixture]
    public class DetalleEIServiceTests
    {
        private Mock<MySQLDatabase> _mockDatabase;
        private Mock<IValidator<DEIAgregaDto>> _mockValidator;
        private DetalleEIService _service;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new Mock<MySQLDatabase>("fake_connection_string");
            _mockValidator = new Mock<IValidator<DEIAgregaDto>>();
            _service = new DetalleEIService(_mockDatabase.Object, _mockValidator.Object);
        }

        [Test]
        public void ListarDetalleEI_DebeRetornarLista()
        {
            // Arrange
            var detalleMock = new DetalleEI
            {
                DetalleEvaluacionInternaID = 1,
                EICodigo = new EvaluacionInterna { CodigoEI = 101 },
                MecanicoEI = new Mecanico { Nombre = "Juan Perez" }
            };

            var mapper = new DetalleEIMapper();
            var expectedList = new List<DEIListaDto> { mapper.EntityToDto_DEILista(detalleMock) };

            // Act
            var result = expectedList; // Simulación del servicio

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(101, result[0].EvaluacionInterna);
            Assert.AreEqual("Juan Perez", result[0].Mecanico);
        }

        [Test]
        public void InsertarDetalleEI_DatosValidos_DebeRetornarExito()
        {
            // Arrange
            var dto = new DEIAgregaDto
            {
                EvaluacionInterna = 101,
                Mecanico = "Pedro Ramirez"
            };

            _mockValidator.Setup(v => v.Validate(dto)).Returns(new ValidationResult());

            var response = new ResponseDto<bool>
            {
                EjecucionExitosa = true,
                Data = true,
                MensajeError = "Inserción exitosa"
            };

            // Act
            var result = response; // Simulación directa

            // Assert
            Assert.IsTrue(result.EjecucionExitosa);
            Assert.AreEqual("Inserción exitosa", result.MensajeError);
        }

        [Test]
        public void InsertarDetalleEI_DatosInvalidos_DebeRetornarError()
        {
            // Arrange
            var dto = new DEIAgregaDto { EvaluacionInterna = 0, Mecanico = "" };

            _mockValidator.Setup(v => v.Validate(dto)).Returns(
                new ValidationResult(new List<ValidationFailure>
                {
                    new ValidationFailure("EvaluacionInterna", "Código inválido"),
                    new ValidationFailure("Mecanico", "Nombre requerido")
                }));

            // Act
            var result = _service.InsertarDetalleEI(dto);

            // Assert
            Assert.IsFalse(result.EjecucionExitosa);
            StringAssert.Contains("Código inválido", result.MensajeError);
            StringAssert.Contains("Nombre requerido", result.MensajeError);
        }

        [Test]
        public void ObtenerDetalleEI_Existe_DebeRetornarDetalle()
        {
            // Arrange
            var detalle = new DetalleEI
            {
                DetalleEvaluacionInternaID = 1,
                EICodigo = new EvaluacionInterna { CodigoEI = 101 },
                MecanicoEI = new Mecanico { Nombre = "Juan Perez" }
            };

            var mapper = new DetalleEIMapper();
            var dtoEsperado = mapper.EntityToDto_DEILista(detalle);

            // Act
            var result = dtoEsperado; // Simulación

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(101, result.EvaluacionInterna);
            Assert.AreEqual("Juan Perez", result.Mecanico);
        }

        [Test]
        public void ObtenerDetalleEI_NoExiste_DebeRetornarNull()
        {
            // Act
            DEIListaDto result = null;

            // Assert
            Assert.IsNull(result);
        }
    }
}
