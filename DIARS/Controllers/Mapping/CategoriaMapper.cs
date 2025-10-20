
using DIARS.Controllers.Dto.Categoria;
using DIARS.Models;
using Riok.Mapperly.Abstractions;

namespace DIARS.Controllers.Mapping
{
    [Mapper]
    public partial class CategoriaMapper
    {
        // ENTIDAD → DTO Listar
        [MapProperty(nameof(Categoria.CodigoC), nameof(CatListaDto.Id))]
        [MapProperty(nameof(Categoria.NombreC), nameof(CatListaDto.Nombre))]
        [MapProperty(nameof(Categoria.Descripcion), nameof(CatListaDto.Descripcion))]
        [MapProperty(nameof(Categoria.EstadoC), nameof(CatListaDto.Condicion))]
        public partial CatListaDto EntityToDto_CategoriaLista(Categoria entity);

        // DTO Actualizar → ENTIDAD
        [MapProperty(nameof(CatActuDto.Id), nameof(Categoria.CodigoC))]
        [MapProperty(nameof(CatActuDto.Nombre), nameof(Categoria.NombreC))]
        [MapProperty(nameof(CatActuDto.Descripcion), nameof(Categoria.Descripcion))]
        [MapProperty(nameof(CatActuDto.Condicion), nameof(Categoria.EstadoC))]
        public partial Categoria DtoToEntity_CategoriaActualizar(CatActuDto dto);

        // DTO Agregar → ENTIDAD
        [MapProperty(nameof(CatAgregaDto.Nombre), nameof(Categoria.NombreC))]
        [MapProperty(nameof(CatAgregaDto.Descripcion), nameof(Categoria.Descripcion))]
        public partial Categoria DtoToEntity_CategoriaAgregar(CatAgregaDto dto);
    }
}
