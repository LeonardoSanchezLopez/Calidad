using DIARS.Controllers.Dto.OrdenCompra;
using DIARS.Models;
using Riok.Mapperly.Abstractions;

namespace DIARS.Controllers.Mapping
{
    [Mapper]
    public partial class OrdenCompraMapper
    {
        // ENTIDAD → DTO Listar
        [MapProperty(nameof(OrdenCompra.CodigoOC), nameof(OrCoListaDto.Id))]
        [MapProperty(nameof(OrdenCompra.CodigoPro.Nombre), nameof(OrCoListaDto.Proveedor))]
        [MapProperty(nameof(OrdenCompra.Fecha), nameof(OrCoListaDto.Fecha))]
        [MapProperty(nameof(OrdenCompra.OPCodigo.CodigoOP), nameof(OrCoListaDto.Cod_OrdenPedido))]
        [MapProperty(nameof(OrdenCompra.FormaPago), nameof(OrCoListaDto.FormaPago))]
        [MapProperty(nameof(OrdenCompra.Total), nameof(OrCoListaDto.Total))]
        [MapProperty(nameof(OrdenCompra.Estado), nameof(OrCoListaDto.Condicion))]
        public partial OrCoListaDto EntityToDto_OrCoLista(OrdenCompra entity);

        // DTO Agregar → ENTIDAD
        [MapProperty(nameof(OrCoAgregaDto.Proveedor), nameof(OrdenCompra.CodigoPro.Nombre))]
        [MapProperty(nameof(OrCoAgregaDto.Fecha), nameof(OrdenCompra.Fecha))]
        [MapProperty(nameof(OrCoAgregaDto.Cod_OrdenPedido), nameof(OrdenCompra.OPCodigo.CodigoOP))]
        [MapProperty(nameof(OrCoAgregaDto.FormaPago), nameof(OrdenCompra.FormaPago))]
        [MapProperty(nameof(OrCoAgregaDto.Total), nameof(OrdenCompra.Total))]
        public partial OrdenCompra DtoToEntity_OrCoAgregar(OrCoAgregaDto dto);
    }
}
