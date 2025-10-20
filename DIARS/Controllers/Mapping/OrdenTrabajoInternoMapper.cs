using DIARS.Controllers.Dto.OrdenTrabajoInterno;
using DIARS.Models;
using Riok.Mapperly.Abstractions;

namespace DIARS.Controllers.Mapping
{
    [Mapper]
    public partial class OrdenTrabajoInternoMapper
    {
        
        [MapProperty(nameof(OrdenTrabajoInterno.CodigoTI), nameof(OTIListaDto.Id))]
        [MapProperty(nameof(OrdenTrabajoInterno.BusTI.NPlaca), nameof(OTIListaDto.PlacaBus))]
        [MapProperty(nameof(OrdenTrabajoInterno.Fecha), nameof(OTIListaDto.Fecha))]
        [MapProperty(nameof(OrdenTrabajoInterno.MecanicoTI.Nombre), nameof(OTIListaDto.Mecanico))]
        [MapProperty(nameof(OrdenTrabajoInterno.Estado), nameof(OTIListaDto.Condicion))]
        public partial OTIListaDto EntityToDto_OTILista(OrdenTrabajoInterno entity);

        // DTO Agregar → ENTIDAD
        [MapProperty(nameof(OTIAgregaDto.PlacaBus), nameof(OrdenTrabajoInterno.BusTI.NPlaca))]
        [MapProperty(nameof(OTIAgregaDto.Fecha), nameof(OrdenTrabajoInterno.Fecha))]
        [MapProperty(nameof(OTIAgregaDto.Mecanico), nameof(OrdenTrabajoInterno.MecanicoTI.Nombre))]
        public partial OrdenTrabajoInterno DtoToEntity_OTIAgregar(OTIAgregaDto dto);
    }
}
