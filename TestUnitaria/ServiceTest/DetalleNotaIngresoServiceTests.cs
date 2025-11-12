using DIARS.Controllers.Dto.DetalleNotaIngreso;
using DIARS.Service;
using DIARS.Service.Database;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace TestUnitaria
{
    [TestFixture]
    public class DetalleNotaIngresoServiceTests
    {
        private Mock<DetalleNotaSalidaMySqlWrapper> _mockDbWrapper;
        private Mock<IValidator<DNoInAgregaDto>> _mockValidator;
        private DetalleNotaIngresoService _service;

        [SetUp]
        public void Setup()
        {
            _mockDbWrapper = new Mock<DetalleNotaSalidaMySqlWrapper>();
            _mockValidator = new Mock<IValidator<DNoInAgregaDto>>();
            _service = new DetalleNotaIngresoService(_mockDbWrapper.Object, _mockValidator.Object);
        }

        // 🧪 Validación fallida
        [Test]
        public void InsertarDetalleNotaIngreso_ValidaError_DebeRetornarFalse()
        {
            var dto = new DNoInAgregaDto
            {
                Cantidad = 0,
                Cod_IngresoRD = 101,
                Repuesto = "Filtro de aire",
                Aceptada = 0,
                Precio = 10.5m
            };

            var errores = new List<ValidationFailure>
            {
                new ValidationFailure("Cantidad", "La cantidad debe ser mayor que 0.")
            };

            _mockValidator.Setup(v => v.Validate(dto))
                .Returns(new ValidationResult(errores));

            var result = _service.InsertarDetalleNotaIngreso(dto);

            Assert.IsFalse(result.EjecucionExitosa);
            Assert.IsFalse(result.Data);
            StringAssert.Contains("La cantidad debe ser mayor que 0.", result.MensajeError);
            _mockDbWrapper.Verify(db => db.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()), Times.Never);
        }

        // ✅ Inserción correcta
        [Test]
        public void InsertarDetalleNotaIngreso_Correcto_DebeRetornarTrue()
        {
            var dto = new DNoInAgregaDto
            {
                Cod_IngresoRD = 202,
                Cantidad = 10,
                Repuesto = "Filtro de aceite",
                Aceptada = 10,
                Precio = 30.0m
            };

            _mockValidator.Setup(v => v.Validate(dto)).Returns(new ValidationResult());

            _mockDbWrapper.Setup(db => db.ExecuteNonQuery(
                "SP_DetalleNotaIngreso_Crea",
                It.IsAny<Dictionary<string, object>>()
            )).Returns(1);

            var result = _service.InsertarDetalleNotaIngreso(dto);

            Assert.IsTrue(result.EjecucionExitosa);
            Assert.IsTrue(result.Data);
            StringAssert.Contains("exitosa", result.MensajeError);

            _mockDbWrapper.Verify(db => db.ExecuteNonQuery(
                "SP_DetalleNotaIngreso_Crea",
                It.Is<Dictionary<string, object>>(p =>
                    (int)p["p_IRCodigo"] == dto.Cod_IngresoRD &&
                    (int)p["p_CantidadRecibida"] == dto.Cantidad &&
                    (string)p["p_NombreRepu"] == dto.Repuesto &&
                    (int)p["p_CantidadAceptada"] == dto.Aceptada &&
                    (decimal)p["p_Precio"] == dto.Precio
                )), Times.Once);
        }

        // ❌ Error en BD
        [Test]
        public void InsertarDetalleNotaIngreso_ErrorEnBD_DebeRetornarError()
        {
            var dto = new DNoInAgregaDto
            {
                Cod_IngresoRD = 303,
                Cantidad = 5,
                Repuesto = "Bujía",
                Aceptada = 5,
                Precio = 12.75m
            };

            _mockValidator.Setup(v => v.Validate(dto)).Returns(new ValidationResult());

            _mockDbWrapper.Setup(db => db.ExecuteNonQuery(
                "SP_DetalleNotaIngreso_Crea",
                It.IsAny<Dictionary<string, object>>()
            )).Throws(new Exception("Error simulado en la BD"));

            var result = _service.InsertarDetalleNotaIngreso(dto);

            Assert.IsFalse(result.EjecucionExitosa);
            Assert.IsFalse(result.Data);
            StringAssert.Contains("Error simulado en la BD", result.MensajeError);
        }
    }
}
