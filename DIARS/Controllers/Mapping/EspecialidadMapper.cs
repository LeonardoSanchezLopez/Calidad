using DIARS.Controllers.Dto.Especialidad;
using DIARS.Models;
using Riok.Mapperly.Abstractions;

namespace DIARS.Controllers.Mapping
{
    [Mapper]
    public partial class EspecialidadMapper
    {
        // ENTIDAD → DTO Listar
        [MapProperty(nameof(Especialidad.CodigoS), nameof(EspeListaDto.Id))]
        [MapProperty(nameof(Especialidad.NombreS), nameof(EspeListaDto.Nombre))]
        [MapProperty(nameof(Especialidad.Descripcion), nameof(EspeListaDto.Descripcion))]
        [MapProperty(nameof(Especialidad.EstadoE), nameof(EspeListaDto.Condicion))]
        public partial EspeListaDto EntityToDto_EspecialidadLista(Especialidad entity);

        // DTO Actualizar → ENTIDAD
        [MapProperty(nameof(EspeActuDto.Id), nameof(Especialidad.CodigoS))]
        [MapProperty(nameof(EspeActuDto.Nombre), nameof(Especialidad.NombreS))]
        [MapProperty(nameof(EspeActuDto.Descripcion), nameof(Especialidad.Descripcion))]
        [MapProperty(nameof(EspeActuDto.Condicion), nameof(Especialidad.EstadoE))]
        public partial Especialidad DtoToEntity_EspecialidadActualizar(EspeActuDto dto);

        // DTO Agregar → ENTIDAD
        [MapProperty(nameof(EspeAgregaDto.Nombre), nameof(Especialidad.NombreS))]
        [MapProperty(nameof(EspeAgregaDto.Descripcion), nameof(Especialidad.Descripcion))]
        public partial Especialidad DtoToEntity_EspecialidadAgregar(EspeAgregaDto dto);
    }
}
