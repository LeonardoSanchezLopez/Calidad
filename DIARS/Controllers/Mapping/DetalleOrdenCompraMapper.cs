using DIARS.Controllers.Dto.DetalleOrdenCompra;
using DIARS.Models;
using Riok.Mapperly.Abstractions;

namespace DIARS.Controllers.Mapping
{
    [Mapper]
    public partial class DetalleOrdenCompraMapper
    {
        // ENTIDAD → DTO Listar
        [MapProperty(nameof(DetalleOrdenCompra.DetalleOrdenCompraID), nameof(DOrCoListaDto.Id))]
        [MapProperty(nameof(DetalleOrdenCompra.OCCompra.CodigoOC), nameof(DOrCoListaDto.Cod_CompraOD))]
        [MapProperty(nameof(DetalleOrdenCompra.Cantidad), nameof(DOrCoListaDto.Cantidad))]
        [MapProperty(nameof(DetalleOrdenCompra.CodigoRep.NombreR), nameof(DOrCoListaDto.Repuesto))]
        [MapProperty(nameof(DetalleOrdenCompra.CantidadAceptada), nameof(DOrCoListaDto.CantidadAceptada))]
        [MapProperty(nameof(DetalleOrdenCompra.Precio), nameof(DOrCoListaDto.Precio))]
        public partial DOrCoListaDto EntityToDto_DOrCoLista(DetalleOrdenCompra entity);

        // DTO Agregar → ENTIDAD
        [MapProperty(nameof(DOrCoAgregaDto.Cod_CompraOD), nameof(DetalleOrdenCompra.OCCompra.CodigoOC))]
        [MapProperty(nameof(DOrCoAgregaDto.Cantidad), nameof(DetalleOrdenCompra.Cantidad))]
        [MapProperty(nameof(DOrCoAgregaDto.CantidadAceptada), nameof(DetalleOrdenCompra.CantidadAceptada))]
        [MapProperty(nameof(DOrCoAgregaDto.Repuesto), nameof(DetalleOrdenCompra.CodigoRep.NombreR))]
        [MapProperty(nameof(DOrCoAgregaDto.Precio), nameof(DetalleOrdenCompra.Precio))]
        public partial DetalleOrdenCompra DtoToEntity_DOrCoAgregar(DOrCoAgregaDto dto);
    }
}
