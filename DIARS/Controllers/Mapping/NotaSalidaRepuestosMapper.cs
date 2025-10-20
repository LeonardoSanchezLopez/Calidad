using DIARS.Controllers.Dto.NotaSalidaRepuestos;
using DIARS.Models;
using Riok.Mapperly.Abstractions;

namespace DIARS.Controllers.Mapping
{
    [Mapper]
    public partial class NotaSalidaRepuestosMapper
    {
        // ENTIDAD → DTO Listar
        [MapProperty(nameof(NotaSalidaRepuestos.CodigoSR), nameof(NSRListaDto.Id))]
        [MapProperty(nameof(NotaSalidaRepuestos.BusSR.NPlaca), nameof(NSRListaDto.Bus))]
        [MapProperty(nameof(NotaSalidaRepuestos.Fecha), nameof(NSRListaDto.Fecha))]
        [MapProperty(nameof(NotaSalidaRepuestos.OPCodigo.CodigoOP), nameof(NSRListaDto.Cod_OrdenPedido))]
        [MapProperty(nameof(NotaSalidaRepuestos.Estado), nameof(NSRListaDto.Condicion))]
        public partial NSRListaDto EntityToDto_NSRLista(NotaSalidaRepuestos entity);

        // DTO Agregar → ENTIDAD
        [MapProperty(nameof(NSRAgregaDto.Bus), nameof(NotaSalidaRepuestos.BusSR.NPlaca))]
        [MapProperty(nameof(NSRAgregaDto.Fecha), nameof(NotaSalidaRepuestos.Fecha))]
        [MapProperty(nameof(NSRAgregaDto.Cod_OrdenPedido), nameof(NotaSalidaRepuestos.OPCodigo.CodigoOP))]
        public partial NotaSalidaRepuestos DtoToEntity_NSRAgregar(NSRAgregaDto dto);
    }
}