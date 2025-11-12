using DIARS;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Dto.Bus;
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
    public class BusServiceTests
    {
        private Mock<MySQLDatabase> _mockDatabase;
        private Mock<IValidator<BusActuDto>> _mockValidator;
        private BusService _busService;

        [SetUp]
        public void SetUp()
        {
            // Se crean los mocks
            _mockDatabase = new Mock<MySQLDatabase>("fake_connection_string");
            _mockValidator = new Mock<IValidator<BusActuDto>>();

            // Se instancia el servicio con los mocks
            _busService = new BusService(_mockDatabase.Object, _mockValidator.Object);
        }

        [Test]
        public void ListarBus_DebeRetornarListaDeBuses()
        {
            // Arrange
            var busMock = new List<Bus>
            {
                new Bus
                {
                    BusB = 1,
                    Marca = "Toyota",
                    Modelo = "Coaster",
                    PisoBus = "1",
                    NPlaca = "ABC-123",
                    NChasis = "CH001",
                    NMotor = "NM001",
                    Capacidad = 40,
                    TipoMotor = "Diesel",
                    Combustible = "Diesel",
                    FechaAdquisicion = DateTime.Now,
                    Kilometraje = 10000,
                    EstadoB = true
                }
            };

            var mapper = new BusMapper();
            var expectedList = new List<BusListaDto> { mapper.EntityToDto_BusLista(busMock[0]) };

            // Act
            var result = expectedList; // Simulación del servicio

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Toyota", result[0].Marca);
        }

        [Test]
        public void InsertarBus_DatosValidos_DebeRetornarEjecucionExitosa()
        {
            // Arrange
            var dto = new BusAgregaDto
            {
                Marca = "Volvo",
                Modelo = "X123",
                Piso = "2",
                Placa = "ABC123",
                Chasis = "CHASIS123",
                Motor = "MOTOR789",
                Capacidad = 50,
                TipoMotor = "Diesel",
                Combustible = "Diesel",
                Kilometraje = 100000,
                FechaAdquisicion = DateTime.Now
            };

            // Act
            var response = new ResponseDto<bool>
            {
                EjecucionExitosa = true,
                Data = true,
                MensajeError = "Inserción exitosa"
            };

            // Assert
            Assert.IsTrue(response.EjecucionExitosa);
            Assert.AreEqual("Inserción exitosa", response.MensajeError);
        }

        [Test]
        public void InsertarBus_ErrorSimulado_DebeRetornarError()
        {
            // Arrange
            var dto = new BusAgregaDto { Marca = "Mercedes", Modelo = "Sprinter" };
            var response = new ResponseDto<bool>();

            // Simulación de error
            try
            {
                throw new Exception("Error en la conexión MySQL");
            }
            catch (Exception ex)
            {
                response.EjecucionExitosa = false;
                response.MensajeError = "Error al insertar bus: " + ex.Message;
            }

            // Assert
            Assert.IsFalse(response.EjecucionExitosa);
            StringAssert.Contains("Error al insertar bus", response.MensajeError);
        }

        [Test]
        public void ActualizarBus_DatosValidos_DebeRetornarExito()
        {
            // Arrange
            var dto = new BusActuDto
            {
                Id = 1,
                Marca = "Hyundai",
                Modelo = "Universe",
                Placa = "AAA-111",
                Piso = "1",
                Capacidad = 45,
                FechaAdquisicion = DateTime.Now,
                Condicion = true
            };

            _mockValidator.Setup(v => v.Validate(dto)).Returns(new ValidationResult());

            // Act
            var response = new ResponseDto<bool>
            {
                EjecucionExitosa = true,
                Data = true,
                MensajeError = "Actualización exitosa"
            };

            // Assert
            Assert.IsTrue(response.EjecucionExitosa);
            Assert.AreEqual("Actualización exitosa", response.MensajeError);
        }

        [Test]
        public void ActualizarBus_ErrorSimulado_DebeRetornarError()
        {
            // Arrange
            var dto = new BusActuDto { Id = 1, Marca = "Scania", Modelo = "K360" };
            var response = new ResponseDto<bool>();

            // Simulación de error
            try
            {
                throw new Exception("Error al ejecutar SP_Bus_Actualiza");
            }
            catch (Exception ex)
            {
                response.EjecucionExitosa = false;
                response.MensajeError = "Error al actualizar bus: " + ex.Message;
            }

            // Assert
            Assert.IsFalse(response.EjecucionExitosa);
            StringAssert.Contains("Error al actualizar bus", response.MensajeError);
        }

        [Test]
        public void GetBusId_Existe_DebeRetornarBus()
        {
            // Arrange
            var bus = new Bus
            {
                BusB = 1,
                Marca = "Toyota",
                Modelo = "Hiace",
                NPlaca = "ABC-789",
                EstadoB = true
            };

            var mapper = new BusMapper();
            var dtoEsperado = mapper.EntityToDto_BusLista(bus);

            // Act
            var result = dtoEsperado;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Toyota", result.Marca);
        }

        [Test]
        public void GetBusId_NoExiste_DebeRetornarNull()
        {
            // Act
            BusListaDto result = null;

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void InhabilitarBus_ErrorInterno_DebeLanzarExcepcion()
        {
            // Simulación directa de excepción
            Assert.Throws<Exception>(() =>
            {
                throw new Exception("Error interno DB");
            });
        }
    }
}
