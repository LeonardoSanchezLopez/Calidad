using DIARS.Controllers.Dto.DetalleOTE;
using DIARS.Models;
using Riok.Mapperly.Abstractions;

namespace DIARS.Controllers.Mapping
{
    [Mapper]
    public partial class DetalleOTEMapper
    {
        // ENTIDAD → DTO Listar
        [MapProperty(nameof(DetalleOTE.DetalleOTEID), nameof(DOTEListaDto.Id))]
        [MapProperty(nameof(DetalleOTE.TECodigo.CodigoTE), nameof(DOTEListaDto.Cod_TrabajoED))]
        [MapProperty(nameof(DetalleOTE.CodigoRepu.NombreR), nameof(DOTEListaDto.Repuesto))]
        [MapProperty(nameof(DetalleOTE.Parte), nameof(DOTEListaDto.Parte))]
        [MapProperty(nameof(DetalleOTE.Pieza), nameof(DOTEListaDto.Pieza))]
        [MapProperty(nameof(DetalleOTE.Cantidad), nameof(DOTEListaDto.Cantidad))]
        public partial DOTEListaDto EntityToDto_DOTELista(DetalleOTE entity);

        // DTO Agregar → ENTIDAD
        [MapProperty(nameof(DOTEAgregaDto.Cod_TrabajoED), nameof(DetalleOTE.TECodigo.CodigoTE))]
        [MapProperty(nameof(DOTEAgregaDto.Repuesto), nameof(DetalleOTE.CodigoRepu.NombreR))]
        [MapProperty(nameof(DOTEAgregaDto.Parte), nameof(DetalleOTE.Parte))]
        [MapProperty(nameof(DOTEAgregaDto.Pieza), nameof(DetalleOTE.Pieza))]
        [MapProperty(nameof(DOTEAgregaDto.Cantidad), nameof(DetalleOTE.Cantidad))]
        public partial DetalleOTE DtoToEntity_DOTEAgregar(DOTEAgregaDto dto);
    }
}
