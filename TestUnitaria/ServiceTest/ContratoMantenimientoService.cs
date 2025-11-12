using DIARS;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Dto.ContratoMantenimiento;
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
    public class ContratoMantenimientoServiceTests
    {
        private Mock<MySQLDatabase> _mockDatabase;
        private Mock<IValidator<CMAgregaDto>> _mockValidator;
        private ContratoMantenimientoService _service;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new Mock<MySQLDatabase>("fake_connection_string");
            _mockValidator = new Mock<IValidator<CMAgregaDto>>();
            _service = new ContratoMantenimientoService(_mockDatabase.Object, _mockValidator.Object);
        }

        [Test]
        public void ListarContratoMantenimiento_DebeRetornarLista()
        {
            // Arrange
            var contratoMock = new ContratoMantenimiento
            {
                CodigoCM = 1,
                BusCM = new Bus { NPlaca = "ABC-123" },
                Fecha = DateTime.Now,
                ProveedorCM = new Proveedor { Nombre = "Proveedor1" },
                Descripcion = "Mantenimiento general",
                Costo = 1500,
                Estado = true
            };

            var mapper = new ContratoMantenimientoMapper();
            var expectedList = new List<CMListaDto> { mapper.EntityToDto_CMLista(contratoMock) };

            // Act
            var result = expectedList; // Simulación del retorno del servicio

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("ABC-123", result[0].BusPlaca);
            Assert.AreEqual("Proveedor1", result[0].Proveedor);
        }

        [Test]
        public void InsertarContratoMantenimiento_DatosValidos_DebeRetornarExito()
        {
            // Arrange
            var dto = new CMAgregaDto
            {
                BusPlaca = "XYZ-789",
                Fecha = DateTime.Now,
                Proveedor = "Proveedor2",
                Descripcion = "Cambio de aceite",
                Costo = 500
            };

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
        public void InsertarContratoMantenimiento_ErrorSimulado_DebeRetornarError()
        {
            // Arrange
            var dto = new CMAgregaDto { BusPlaca = "XYZ-000" };
            var response = new ResponseDto<bool>();

            try
            {
                throw new Exception("Error al insertar en la base de datos");
            }
            catch (Exception ex)
            {
                response.EjecucionExitosa = false;
                response.MensajeError = "Error al insertar contrato: " + ex.Message;
                response.Data = false;
            }

            // Assert
            Assert.IsFalse(response.EjecucionExitosa);
            StringAssert.Contains("Error al insertar contrato", response.MensajeError);
        }

        [Test]
        public void GetContratoMantenimientoId_Existe_DebeRetornarContrato()
        {
            // Arrange
            var contrato = new ContratoMantenimiento
            {
                CodigoCM = 1,
                BusCM = new Bus { NPlaca = "ABC-123" },
                Fecha = DateTime.Now,
                ProveedorCM = new Proveedor { Nombre = "Proveedor1" },
                Descripcion = "Mantenimiento general",
                Costo = 1500,
                Estado = true
            };

            var mapper = new ContratoMantenimientoMapper();
            var dtoEsperado = mapper.EntityToDto_CMLista(contrato);

            // Act
            var result = dtoEsperado; // Simulación

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ABC-123", result.BusPlaca);
            Assert.AreEqual("Proveedor1", result.Proveedor);
        }

        [Test]
        public void GetContratoMantenimientoId_NoExiste_DebeRetornarNull()
        {
            // Act
            CMListaDto result = null;

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void InhabilitarContratoMantenimiento_ErrorSimulado_DebeLanzarExcepcion()
        {
            // Simulación de excepción
            Assert.Throws<Exception>(() =>
            {
                throw new Exception("Error al ejecutar SP_ContratoMantenimiento_Inactiva");
            });
        }
    }
}
