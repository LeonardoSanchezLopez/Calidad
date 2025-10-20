using DIARS.Controllers.Dto.DetalleOTI;
using DIARS.Models;
using Riok.Mapperly.Abstractions;

namespace DIARS.Controllers.Mapping
{
    [Mapper]
    public partial class DetalleOTIMapper
    {
        // ENTIDAD → DTO Listar
        [MapProperty(nameof(DetalleOTI.DetalleOTIID), nameof(DOTIListaDto.Id))]
        [MapProperty(nameof(DetalleOTI.OrdenTrabajoInternoID.CodigoTI), nameof(DOTIListaDto.Cod_OrdenTI))]
        [MapProperty(nameof(DetalleOTI.CodigoRepu.NombreR), nameof(DOTIListaDto.Repuesto))]
        [MapProperty(nameof(DetalleOTI.MecanicoTI.Nombre), nameof(DOTIListaDto.Mecanico))]
        [MapProperty(nameof(DetalleOTI.Parte), nameof(DOTIListaDto.Parte))]
        [MapProperty(nameof(DetalleOTI.Pieza), nameof(DOTIListaDto.Pieza))]
        [MapProperty(nameof(DetalleOTI.Cantidad), nameof(DOTIListaDto.Cantidad))]
        public partial DOTIListaDto EntityToDto_DOTILista(DetalleOTI entity);

        // DTO Agregar → ENTIDAD
        [MapProperty(nameof(DOTIAgregaDto.Cod_OrdenTI), nameof(DetalleOTI.OrdenTrabajoInternoID.CodigoTI))]
        [MapProperty(nameof(DOTIAgregaDto.Repuesto), nameof(DetalleOTI.CodigoRepu.NombreR))]
        [MapProperty(nameof(DOTIAgregaDto.Mecanico), nameof(DetalleOTI.MecanicoTI.Nombre))]
        [MapProperty(nameof(DOTIAgregaDto.Parte), nameof(DetalleOTI.Parte))]
        [MapProperty(nameof(DOTIAgregaDto.Pieza), nameof(DetalleOTI.Pieza))]
        [MapProperty(nameof(DOTIAgregaDto.Cantidad), nameof(DetalleOTI.Cantidad))]
        public partial DetalleOTI DtoToEntity_DOTIAgregar(DOTIAgregaDto dto);
    }
}
