using DIARS.Controllers.Dto.DetalleOrdenPedido;
using DIARS.Models;
using Riok.Mapperly.Abstractions;

namespace DIARS.Controllers.Mapping
{
    [Mapper]
    public partial class DetalleOrdenPedidoMapper
    {
        // ENTIDAD → DTO Listar
        [MapProperty(nameof(DetalleOrdenPedido.DetalleOrdenPedidoID), nameof(DOrPeListaDto.Id))]
        [MapProperty(nameof(DetalleOrdenPedido.OPCodigo.CodigoOP), nameof(DOrPeListaDto.Cod_OrdenPD))]
        [MapProperty(nameof(DetalleOrdenPedido.Cantidad), nameof(DOrPeListaDto.Cantidad))]
        [MapProperty(nameof(DetalleOrdenPedido.CodigoRepu.NombreR), nameof(DOrPeListaDto.Repuesto))]
        public partial DOrPeListaDto EntityToDto_DOrPeLista(DetalleOrdenPedido entity);

        // DTO Agregar → ENTIDAD
        [MapProperty(nameof(DOrPeAgregaDto.Cod_OrdenPD), nameof(DetalleOrdenPedido.OPCodigo.CodigoOP))]
        [MapProperty(nameof(DOrPeAgregaDto.Cantidad), nameof(DetalleOrdenPedido.Cantidad))]
        [MapProperty(nameof(DOrPeAgregaDto.Repuesto), nameof(DetalleOrdenPedido.CodigoRepu.NombreR))]
        public partial DetalleOrdenPedido DtoToEntity_DOrPeAgregar(DOrPeAgregaDto dto);
    }
}