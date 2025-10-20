using DIARS.Controllers.Dto.ContratoMantenimiento;
using DIARS.Models;
using Riok.Mapperly.Abstractions;

namespace DIARS.Controllers.Mapping
{
    [Mapper]
    public partial class ContratoMantenimientoMapper
    {
        // ENTIDAD → DTO Listar
        [MapProperty(nameof(ContratoMantenimiento.CodigoCM), nameof(CMListaDto.Id))]
        [MapProperty(nameof(ContratoMantenimiento.BusCM.NPlaca), nameof(CMListaDto.BusPlaca))]
        [MapProperty(nameof(ContratoMantenimiento.Fecha), nameof(CMListaDto.Fecha))]
        [MapProperty(nameof(ContratoMantenimiento.ProveedorCM.Nombre), nameof(CMListaDto.Proveedor))]
        [MapProperty(nameof(ContratoMantenimiento.Descripcion), nameof(CMListaDto.Descripcion))]
        [MapProperty(nameof(ContratoMantenimiento.Costo), nameof(CMListaDto.Costo))]
        [MapProperty(nameof(ContratoMantenimiento.Estado), nameof(CMListaDto.Condicion))]
        public partial CMListaDto EntityToDto_CMLista(ContratoMantenimiento entity);

        // DTO Agregar → ENTIDAD
        [MapProperty(nameof(CMAgregaDto.BusPlaca), nameof(ContratoMantenimiento.BusCM.NPlaca))]
        [MapProperty(nameof(CMAgregaDto.Fecha), nameof(ContratoMantenimiento.Fecha))]
        [MapProperty(nameof(CMAgregaDto.Proveedor), nameof(ContratoMantenimiento.ProveedorCM.Nombre))]
        [MapProperty(nameof(CMAgregaDto.Descripcion), nameof(ContratoMantenimiento.Descripcion))]
        [MapProperty(nameof(CMAgregaDto.Costo), nameof(ContratoMantenimiento.Costo))]
        public partial ContratoMantenimiento DtoToEntity_CMAgregar(CMAgregaDto dto);
    }
}
