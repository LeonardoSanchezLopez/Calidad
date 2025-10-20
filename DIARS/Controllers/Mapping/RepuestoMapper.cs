using DIARS.Controllers.Dto.Repuesto;
using DIARS.Models;
using Riok.Mapperly.Abstractions;

namespace DIARS.Controllers.Mapping
{
    [Mapper]
    public partial class RepuestoMapper
    {
        // ENTIDAD → DTO Listar
        [MapProperty(nameof(Repuesto.CodigoR), nameof(RepuListaDto.Id))]
        [MapProperty(nameof(Repuesto.NombreR), nameof(RepuListaDto.Nombre))]
        [MapProperty(nameof(Repuesto.CategoriaR.NombreC), nameof(RepuListaDto.Categoria))]
        [MapProperty(nameof(Repuesto.MarcarepuestoR.Descripcion), nameof(RepuListaDto.Marcarepuesto))]
        [MapProperty(nameof(Repuesto.ProveedorR.Nombre), nameof(RepuListaDto.Proveedor))]
        [MapProperty(nameof(Repuesto.Precio), nameof(RepuListaDto.Precio))]
        [MapProperty(nameof(Repuesto.EstadoR), nameof(RepuListaDto.Condicion))]
        public partial RepuListaDto EntityToDto_RepuestoLista(Repuesto entity);

        // DTO Actualizar → ENTIDAD
        [MapProperty(nameof(RepuActuDto.Id), nameof(Repuesto.CodigoR))]
        [MapProperty(nameof(RepuActuDto.Nombre), nameof(Repuesto.NombreR))]
        [MapProperty(nameof(RepuActuDto.Categoria), nameof(Repuesto.CategoriaR.NombreC))]
        [MapProperty(nameof(RepuActuDto.Marcarepuesto), nameof(Repuesto.MarcarepuestoR.Descripcion))]
        [MapProperty(nameof(RepuActuDto.Proveedor), nameof(Repuesto.ProveedorR.Nombre))]
        [MapProperty(nameof(RepuActuDto.Precio), nameof(Repuesto.Precio))]
        [MapProperty(nameof(RepuActuDto.Condicion), nameof(Repuesto.EstadoR))]
        public partial Repuesto DtoToEntity_RepuestoActualizar(RepuActuDto dto);

        // DTO Agregar → ENTIDAD
        [MapProperty(nameof(RepuAgregaDto.Nombre), nameof(Repuesto.NombreR))]
        [MapProperty(nameof(RepuAgregaDto.Categoria), nameof(Repuesto.CategoriaR.NombreC))]
        [MapProperty(nameof(RepuAgregaDto.Marcarepuesto), nameof(Repuesto.MarcarepuestoR.Descripcion))]
        [MapProperty(nameof(RepuAgregaDto.Proveedor), nameof(Repuesto.ProveedorR.Nombre))]
        [MapProperty(nameof(RepuAgregaDto.Precio), nameof(Repuesto.Precio))]
        public partial Repuesto DtoToEntity_RepuestoAgregar(RepuAgregaDto dto);
    }
}
