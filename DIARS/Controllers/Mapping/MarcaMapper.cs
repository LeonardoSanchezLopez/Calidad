using DIARS.Controllers.Dto.MarcaRepuesto;
using DIARS.Models;
using Riok.Mapperly.Abstractions;

namespace DIARS.Controllers.Mapping
{
    [Mapper]
    public partial class MarcaMapper
    {
        // ENTIDAD → DTO Listar
        [MapProperty(nameof(MarcaRepuesto.CodigoMR), nameof(MarcaListaDto.Id))]
        [MapProperty(nameof(MarcaRepuesto.Descripcion), nameof(MarcaListaDto.Descripcion))]
        [MapProperty(nameof(MarcaRepuesto.ProveedorMR.Nombre), nameof(MarcaListaDto.Proveedor))]
        [MapProperty(nameof(MarcaRepuesto.EstadoM), nameof(MarcaListaDto.Condicion))]
        public partial MarcaListaDto EntityToDto_MarcaRepuestoLista(MarcaRepuesto entity);

        // DTO Actualizar → ENTIDAD
        [MapProperty(nameof(MarcaActuDto.Id), nameof(MarcaRepuesto.CodigoMR))]
        [MapProperty(nameof(MarcaActuDto.Descripcion), nameof(MarcaRepuesto.Descripcion))]
        [MapProperty(nameof(MarcaActuDto.Proveedor), nameof(MarcaRepuesto.ProveedorMR.Nombre))]
        [MapProperty(nameof(MarcaActuDto.Condicion), nameof(MarcaRepuesto.EstadoM))]
        public partial MarcaRepuesto DtoToEntity_MarcaRepuestoActualizar(MarcaActuDto dto);

        // DTO Agregar → ENTIDAD
        [MapProperty(nameof(MarcaAgregaDto.Descripcion), nameof(MarcaRepuesto.Descripcion))]
        [MapProperty(nameof(MarcaAgregaDto.Proveedor), nameof(MarcaRepuesto.ProveedorMR.Nombre))]
        public partial MarcaRepuesto DtoToEntity_MarcaRepuestoAgregar(MarcaAgregaDto dto);
    }
}
