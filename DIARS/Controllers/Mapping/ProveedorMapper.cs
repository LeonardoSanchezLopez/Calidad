using DIARS.Controllers.Dto.Proveedor;
using DIARS.Models;
using Riok.Mapperly.Abstractions;

namespace DIARS.Controllers.Mapping
{
    [Mapper]
    public partial class ProveedorMapper
    {
        // ENTIDAD → DTO Listar
        [MapProperty(nameof(Proveedor.CodigoP), nameof(ProListaDto.Id))]
        [MapProperty(nameof(Proveedor.Nombre), nameof(ProListaDto.Nombre))]
        [MapProperty(nameof(Proveedor.Direccion), nameof(ProListaDto.Direccion))]
        [MapProperty(nameof(Proveedor.Telefono), nameof(ProListaDto.Telefono))]
        [MapProperty(nameof(Proveedor.Correo), nameof(ProListaDto.Correo))]
        [MapProperty(nameof(Proveedor.EstadoP), nameof(ProListaDto.Condicion))]
        public partial ProListaDto EntityToDto_ProveedorLista(Proveedor entity);

        // DTO Actualizar → ENTIDAD
        [MapProperty(nameof(ProActuDto.Id), nameof(Proveedor.CodigoP))]
        [MapProperty(nameof(ProActuDto.Nombre), nameof(Proveedor.Nombre))]
        [MapProperty(nameof(ProActuDto.Direccion), nameof(Proveedor.Direccion))]
        [MapProperty(nameof(ProActuDto.Telefono), nameof(Proveedor.Telefono))]
        [MapProperty(nameof(ProActuDto.Correo), nameof(Proveedor.Correo))]
        [MapProperty(nameof(ProActuDto.Condicion), nameof(Proveedor.EstadoP))]
        public partial Proveedor DtoToEntity_ProveedorActualizar(ProActuDto dto);

        // DTO Agregar → ENTIDAD
        [MapProperty(nameof(ProAgregaDto.Nombre), nameof(Proveedor.Nombre))]
        [MapProperty(nameof(ProAgregaDto.Direccion), nameof(Proveedor.Direccion))]
        [MapProperty(nameof(ProAgregaDto.Telefono), nameof(Proveedor.Telefono))]
        [MapProperty(nameof(ProAgregaDto.Correo), nameof(Proveedor.Correo))]
        public partial Proveedor DtoToEntity_ProveedorAgregar(ProAgregaDto dto);
    }
}
