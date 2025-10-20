using DIARS.Controllers.Dto.Factura;
using DIARS.Models;
using Riok.Mapperly.Abstractions;

namespace DIARS.Controllers.Mapping
{
    [Mapper]
    public partial class FacturaMapper
    {
        // ENTIDAD → DTO Listar
        [MapProperty(nameof(Factura.CodigoFactura), nameof(FacListaDto.Id))]
        [MapProperty(nameof(Factura.CodigoOC.CodigoOC), nameof(FacListaDto.Cod_OrdenC))]
        [MapProperty(nameof(Factura.Fecha), nameof(FacListaDto.FechaRegistro))]
        [MapProperty(nameof(Factura.Total), nameof(FacListaDto.Total))]
        public partial FacListaDto EntityToDto_FacLista(Factura entity);

        // DTO Agregar → ENTIDAD
        [MapProperty(nameof(FacAgregaDto.Cod_OrdenC), nameof(Factura.CodigoOC.CodigoOC))]
        [MapProperty(nameof(FacAgregaDto.FechaRegistro), nameof(Factura.Fecha))]
        [MapProperty(nameof(FacAgregaDto.Total), nameof(Factura.Total))]
        public partial Factura DtoToEntity_FacAgregar(FacAgregaDto dto);
    }
}
