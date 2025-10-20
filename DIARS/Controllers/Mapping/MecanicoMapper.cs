using DIARS.Controllers.Dto.Mecanico;
using DIARS.Models;
using Riok.Mapperly.Abstractions;

namespace DIARS.Controllers.Mapping
{
    [Mapper]
    public partial class MecanicoMapper
    {
        // ENTIDAD → DTO Listar
        [MapProperty(nameof(Mecanico.CodigoM), nameof(MecaListaDto.Id))]
        [MapProperty(nameof(Mecanico.EspecialidadM.NombreS), nameof(MecaListaDto.Especialidad))]
        [MapProperty(nameof(Mecanico.Nombre), nameof(MecaListaDto.Nombre))]
        [MapProperty(nameof(Mecanico.DNI), nameof(MecaListaDto.Dni))]
        [MapProperty(nameof(Mecanico.Domicilio), nameof(MecaListaDto.Domicilio))]
        [MapProperty(nameof(Mecanico.Experiencia), nameof(MecaListaDto.Experiencia))]
        [MapProperty(nameof(Mecanico.Telefono), nameof(MecaListaDto.Telefono))]
        [MapProperty(nameof(Mecanico.Sueldo), nameof(MecaListaDto.Sueldo))]
        [MapProperty(nameof(Mecanico.Turno), nameof(MecaListaDto.Turno))]
        [MapProperty(nameof(Mecanico.FechaContrato), nameof(MecaListaDto.FechaContrato))]
        [MapProperty(nameof(Mecanico.EstadoM), nameof(MecaListaDto.Condicion))]
        public partial MecaListaDto EntityToDto_MecanicoLista(Mecanico entity);

        // DTO Actualizar → ENTIDAD
        [MapProperty(nameof(MecaActuDto.Id), nameof(Mecanico.CodigoM))]
        [MapProperty(nameof(MecaActuDto.Especialidad), nameof(Mecanico.EspecialidadM.NombreS))]
        [MapProperty(nameof(MecaActuDto.Nombre), nameof(Mecanico.Nombre))]
        [MapProperty(nameof(MecaActuDto.Dni), nameof(Mecanico.DNI))]
        [MapProperty(nameof(MecaActuDto.Domicilio), nameof(Mecanico.Domicilio))]
        [MapProperty(nameof(MecaActuDto.Experiencia), nameof(Mecanico.Experiencia))]
        [MapProperty(nameof(MecaActuDto.Telefono), nameof(Mecanico.Telefono))]
        [MapProperty(nameof(MecaActuDto.Sueldo), nameof(Mecanico.Sueldo))]
        [MapProperty(nameof(MecaActuDto.Turno), nameof(Mecanico.Turno))]
        [MapProperty(nameof(MecaActuDto.FechaContrato), nameof(Mecanico.FechaContrato))]
        [MapProperty(nameof(MecaActuDto.Condicion), nameof(Mecanico.EstadoM))]
        public partial Mecanico DtoToEntity_MecanicoActualizar(MecaActuDto dto);

        // DTO Agregar → ENTIDAD
        [MapProperty(nameof(MecaAgregaDto.Especialidad), nameof(Mecanico.EspecialidadM.NombreS))]
        [MapProperty(nameof(MecaAgregaDto.Nombre), nameof(Mecanico.Nombre))]
        [MapProperty(nameof(MecaAgregaDto.Dni), nameof(Mecanico.DNI))]
        [MapProperty(nameof(MecaAgregaDto.Domicilio), nameof(Mecanico.Domicilio))]
        [MapProperty(nameof(MecaAgregaDto.Experiencia), nameof(Mecanico.Experiencia))]
        [MapProperty(nameof(MecaAgregaDto.Telefono), nameof(Mecanico.Telefono))]
        [MapProperty(nameof(MecaAgregaDto.Sueldo), nameof(Mecanico.Sueldo))]
        [MapProperty(nameof(MecaAgregaDto.Turno), nameof(Mecanico.Turno))]
        [MapProperty(nameof(MecaAgregaDto.FechaContrato), nameof(Mecanico.FechaContrato))]
        public partial Mecanico DtoToEntity_MecanicoAgregar(MecaAgregaDto dto);
    }
}
