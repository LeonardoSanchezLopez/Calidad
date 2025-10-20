using DIARS.Controllers.Dto.OrdenTrabajoExterno;
using DIARS.Models;
using Riok.Mapperly.Abstractions;

namespace DIARS.Controllers.Mapping
{
    [Mapper]
    public partial class OrdenTrabajoExternoMapper
    {
        // ENTIDAD → DTO Listar
        [MapProperty(nameof(OrdenTrabajoExterno.CodigoTE), nameof(OTEListaDto.Id))]
        [MapProperty(nameof(OrdenTrabajoExterno.CodigoBus.NPlaca), nameof(OTEListaDto.PlacaBus))]
        [MapProperty(nameof(OrdenTrabajoExterno.ContratoCO.CodigoCM), nameof(OTEListaDto.Contrato))]
        [MapProperty(nameof(OrdenTrabajoExterno.Fecha), nameof(OTEListaDto.Fecha))]
        [MapProperty(nameof(OrdenTrabajoExterno.ProveedorTE.Nombre), nameof(OTEListaDto.Proveedor))]
        [MapProperty(nameof(OrdenTrabajoExterno.Estado), nameof(OTEListaDto.Condicion))]
        public partial OTEListaDto EntityToDto_OTELista(OrdenTrabajoExterno entity);

        // DTO Agregar → ENTIDAD
        [MapProperty(nameof(OTEAgregaDto.PlacaBus), nameof(OrdenTrabajoExterno.CodigoBus.NPlaca))]
        [MapProperty(nameof(OTEAgregaDto.Contrato), nameof(OrdenTrabajoExterno.ContratoCO.CodigoCM))]
        [MapProperty(nameof(OTEAgregaDto.Fecha), nameof(OrdenTrabajoExterno.Fecha))]
        [MapProperty(nameof(OTEAgregaDto.Proveedor), nameof(OrdenTrabajoExterno.ProveedorTE.Nombre))]
        public partial OrdenTrabajoExterno DtoToEntity_OTEAgregar(OTEAgregaDto dto);
    }
}
