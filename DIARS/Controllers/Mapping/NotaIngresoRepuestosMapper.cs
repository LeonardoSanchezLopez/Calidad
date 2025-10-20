using DIARS.Controllers.Dto.NotaIngresoRepuestos;
using DIARS.Models;
using Riok.Mapperly.Abstractions;

namespace DIARS.Controllers.Mapping
{
    [Mapper]
    public partial class NotaIngresoRepuestosMapper
    {
        // ENTIDAD → DTO Listar
        [MapProperty(nameof(NotaIngresoRepuestos.CodigoIR), nameof(NIRListaDto.Id))]
        [MapProperty(nameof(NotaIngresoRepuestos.CodigoOC.CodigoOC), nameof(NIRListaDto.Cod_OrdenC))]
        [MapProperty(nameof(NotaIngresoRepuestos.Fecha), nameof(NIRListaDto.Fecha))]
        [MapProperty(nameof(NotaIngresoRepuestos.ProveedorIR.Nombre), nameof(NIRListaDto.Proveedor))]
        [MapProperty(nameof(NotaIngresoRepuestos.Estado), nameof(NIRListaDto.Condicion))]
        public partial NIRListaDto EntityToDto_NIRLista(NotaIngresoRepuestos entity);

        // DTO Agregar → ENTIDAD
        [MapProperty(nameof(NIRAgregaDto.Cod_OrdenC), nameof(NotaIngresoRepuestos.CodigoOC.CodigoOC))]
        [MapProperty(nameof(NIRAgregaDto.Fecha), nameof(NotaIngresoRepuestos.Fecha))]
        [MapProperty(nameof(NIRAgregaDto.Proveedor), nameof(NotaIngresoRepuestos.ProveedorIR.Nombre))]
        public partial NotaIngresoRepuestos DtoToEntity_NIRAgregar(NIRAgregaDto dto);
    }
}
