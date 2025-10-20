using DIARS.Controllers.Dto.ContratoMantenimiento;
using DIARS.Controllers.Dto.OrdenPedido;
using DIARS.Models;
using Riok.Mapperly.Abstractions;

namespace DIARS.Controllers.Mapping
{
    [Mapper]
    public partial class OrdenPedidoMapper
    {
        // ENTIDAD → DTO Listar
        [MapProperty(nameof(OrdenPedido.CodigoOP), nameof(OrPeListaDto.Id))]
        [MapProperty(nameof(OrdenPedido.Fecha), nameof(OrPeListaDto.Fecha))]
        [MapProperty(nameof(OrdenPedido.TICodigo.CodigoTI), nameof(OrPeListaDto.Cod_TrabajoInterno))]
        [MapProperty(nameof(OrdenPedido.BusCM.NPlaca), nameof(OrPeListaDto.BusPlaca))]
        [MapProperty(nameof(OrdenPedido.JefeEncargado), nameof(OrPeListaDto.Encargado))]
        [MapProperty(nameof(OrdenPedido.Descripcion), nameof(OrPeListaDto.Descripcion))]
        [MapProperty(nameof(OrdenPedido.Estado), nameof(OrPeListaDto.Condicion))]
        public partial OrPeListaDto EntityToDto_OrPeLista(OrdenPedido entity);

        // DTO Agregar → ENTIDAD
        [MapProperty(nameof(OrPeAgregaDto.Fecha), nameof(OrdenPedido.Fecha))]
        [MapProperty(nameof(OrPeAgregaDto.Cod_TrabajoInterno), nameof(OrdenPedido.TICodigo.CodigoTI))]
        [MapProperty(nameof(OrPeAgregaDto.BusPlaca), nameof(OrdenPedido.BusCM.NPlaca))]
        [MapProperty(nameof(OrPeAgregaDto.Encargado), nameof(OrdenPedido.JefeEncargado))]
        [MapProperty(nameof(OrPeAgregaDto.Descripcion), nameof(OrdenPedido.Descripcion))]
        public partial OrdenPedido DtoToEntity_OrPeAgregar(OrPeAgregaDto dto);
    }
}
