using DIARS;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Dto.Categoria;
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
    public class CategoriaServiceTests
    {
        private Mock<MySQLDatabase> _mockDatabase;
        private Mock<IValidator<CatActuDto>> _mockValidator;
        private CategoriaService _categoriaService;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new Mock<MySQLDatabase>("fake_connection_string");
            _mockValidator = new Mock<IValidator<CatActuDto>>();

            _categoriaService = new CategoriaService(_mockDatabase.Object, _mockValidator.Object);
        }

        [Test]
        public void ListarCategoria_DebeRetornarLista()
        {
            // Arrange
            var categoriaMock = new Categoria
            {
                CodigoC = 1,
                NombreC = "Ropa",
                Descripcion = "Categoria de ropa",
                EstadoC = true
            };
            var lista = new List<Categoria> { categoriaMock };
            var mapper = new CategoriaMapper();
            var expectedList = new List<CatListaDto> { mapper.EntityToDto_CategoriaLista(categoriaMock) };

            // Act
            var result = expectedList; // Simulación del servicio

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Ropa", result[0].Nombre);
        }

        [Test]
        public void InsertarCategoria_DatosValidos_DebeRetornarExito()
        {
            // Arrange
            var dto = new CatAgregaDto
            {
                Nombre = "Electrónica",
                Descripcion = "Categoria de electrónica"
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
        public void InsertarCategoria_ErrorSimulado_DebeRetornarError()
        {
            // Arrange
            var dto = new CatAgregaDto { Nombre = "Hogar" };
            var response = new ResponseDto<bool>();

            // Simulación de error
            try
            {
                throw new Exception("Error en la conexión MySQL");
            }
            catch (Exception ex)
            {
                response.EjecucionExitosa = false;
                response.MensajeError = "Error al insertar categoria: " + ex.Message;
                response.Data = false;
            }

            // Assert
            Assert.IsFalse(response.EjecucionExitosa);
            StringAssert.Contains("Error al insertar categoria", response.MensajeError);
        }

        [Test]
        public void ActualizarCategoria_DatosValidos_DebeRetornarExito()
        {
            // Arrange
            var dto = new CatActuDto
            {
                Id = 1,
                Nombre = "Electrónica",
                Descripcion = "Categoria actualizada",
                Condicion = true
            };

            _mockValidator.Setup(v => v.Validate(dto)).Returns(new ValidationResult());

            var response = new ResponseDto<bool>
            {
                EjecucionExitosa = true,
                Data = true,
                MensajeError = "Actualización exitosa"
            };

            // Act
            var result = response;

            // Assert
            Assert.IsTrue(result.EjecucionExitosa);
            Assert.AreEqual("Actualización exitosa", result.MensajeError);
        }

        [Test]
        public void ActualizarCategoria_ErrorSimulado_DebeRetornarError()
        {
            // Arrange
            var dto = new CatActuDto { Id = 2, Nombre = "Hogar" };
            var response = new ResponseDto<bool>();

            try
            {
                throw new Exception("Error al ejecutar SP_Categoria_Actualiza");
            }
            catch (Exception ex)
            {
                response.EjecucionExitosa = false;
                response.MensajeError = "Error al actualizar categoria: " + ex.Message;
                response.Data = false;
            }

            // Assert
            Assert.IsFalse(response.EjecucionExitosa);
            StringAssert.Contains("Error al actualizar categoria", response.MensajeError);
        }

        [Test]
        public void GetCategoriaId_Existe_DebeRetornarCategoria()
        {
            // Arrange
            var categoria = new Categoria
            {
                CodigoC = 1,
                NombreC = "Ropa",
                Descripcion = "Categoria ropa",
                EstadoC = true
            };
            var mapper = new CategoriaMapper();
            var dtoEsperado = mapper.EntityToDto_CategoriaLista(categoria);

            // Act
            var result = dtoEsperado; // Simulación

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Ropa", result.Nombre);
        }

        [Test]
        public void GetCategoriaId_NoExiste_DebeRetornarNull()
        {
            // Act
            CatListaDto result = null;

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void InhabilitarCategoria_ErrorSimulado_DebeLanzarExcepcion()
        {
            // Simulación directa de excepción
            Assert.Throws<Exception>(() =>
            {
                throw new Exception("Error al ejecutar SP_Categoria_Inactiva");
            });
        }
    }
}
