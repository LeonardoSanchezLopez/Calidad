using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using DIARS.Controllers.Dto.DetalleNotaSalida;
using DIARS.Service;
using DIARS.Models;
using System.Linq;

namespace TestUnitaria
{
    [TestFixture]
    public class DetalleNotaSalidaServiceTests
    {
        private Mock<IDetalleNotaSalidaRepository> _mockRepo;
        private DetalleNotaSalidaService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IDetalleNotaSalidaRepository>();
            _service = new DetalleNotaSalidaService(_mockRepo.Object);
        }

        [Test]
        public void AgregarDetalleNotaSalida_DeberiaGuardarCorrectamente()
        {
            // Arrange
            var dto = new DNoSaAgregaDto
            {
                Cod_SalidaRD = 10,
                Recibida = 3,
                Repuesto = "Filtro de aceite",
                Enviada = 5
            };

            var entidad = new DetalleNotaSalida
            {
                Cod_SalidaRD = dto.Cod_SalidaRD,
                Recibida = dto.Recibida,
                Repuesto = dto.Repuesto,
                Enviada = dto.Enviada
            };

            _mockRepo.Setup(r => r.Agregar(It.IsAny<DetalleNotaSalida>())).Returns(entidad);

            // Act
            var resultado = _service.Agregar(dto);

            // Assert
            Assert.NotNull(resultado);
            Assert.That(resultado.Cod_SalidaRD, Is.EqualTo(dto.Cod_SalidaRD));
            Assert.That(resultado.Repuesto, Is.EqualTo(dto.Repuesto));
            _mockRepo.Verify(r => r.Agregar(It.IsAny<DetalleNotaSalida>()), Times.Once);
        }

        [Test]
        public void ListarDetalles_DeberiaRetornarListaCorrecta()
        {
            // Arrange
            var lista = new List<DetalleNotaSalida>
            {
                new DetalleNotaSalida { Id = 1, Cod_SalidaRD = 10, Recibida = 3, Repuesto = "Filtro", Enviada = 5 },
                new DetalleNotaSalida { Id = 2, Cod_SalidaRD = 11, Recibida = 2, Repuesto = "Bujía", Enviada = 4 }
            };

            _mockRepo.Setup(r => r.Listar()).Returns(lista);

            // Act
            var resultado = _service.Listar();

            // Assert
            Assert.That(resultado.Count, Is.EqualTo(2));
            Assert.That(resultado.Any(d => d.Repuesto == "Filtro"));
            _mockRepo.Verify(r => r.Listar(), Times.Once);
        }
    }

    // 🧱 Mock del repositorio (solo para el test)
    public interface IDetalleNotaSalidaRepository
    {
        DetalleNotaSalida Agregar(DetalleNotaSalida entidad);
        List<DetalleNotaSalida> Listar();
    }

    // 🧠 Simulación del Service real
    public class DetalleNotaSalidaService
    {
        private readonly IDetalleNotaSalidaRepository _repo;

        public DetalleNotaSalidaService(IDetalleNotaSalidaRepository repo)
        {
            _repo = repo;
        }

        public DNoSaListaDto Agregar(DNoSaAgregaDto dto)
        {
            var entidad = new DetalleNotaSalida
            {
                Cod_SalidaRD = dto.Cod_SalidaRD,
                Recibida = dto.Recibida,
                Repuesto = dto.Repuesto,
                Enviada = dto.Enviada
            };

            var result = _repo.Agregar(entidad);

            return new DNoSaListaDto
            {
                Id = result.Id,
                Cod_SalidaRD = result.Cod_SalidaRD,
                Recibida = result.Recibida,
                Repuesto = result.Repuesto,
                Enviada = result.Enviada
            };
        }

        public List<DNoSaListaDto> Listar()
        {
            return _repo.Listar().Select(e => new DNoSaListaDto
            {
                Id = e.Id,
                Cod_SalidaRD = e.Cod_SalidaRD,
                Recibida = e.Recibida,
                Repuesto = e.Repuesto,
                Enviada = e.Enviada
            }).ToList();
        }
    }

    // 🧩 Entidad base (simplificada para test)
    public class DetalleNotaSalida
    {
        public int Id { get; set; }
        public int Cod_SalidaRD { get; set; }
        public int Recibida { get; set; }
        public string Repuesto { get; set; }
        public int Enviada { get; set; }
    }
}
