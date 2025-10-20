using DIARS.Controllers.Dto.Bus;
using DIARS.Models;
using Riok.Mapperly.Abstractions;

namespace DIARS.Controllers.Mapping
{
    [Mapper]
    public partial class BusMapper
    {
        // ENTIDAD → DTO Listar
        [MapProperty(nameof(Bus.BusB), nameof(BusListaDto.Id))]
        [MapProperty(nameof(Bus.Marca), nameof(BusListaDto.Marca))]
        [MapProperty(nameof(Bus.Modelo), nameof(BusListaDto.Modelo))]
        [MapProperty(nameof(Bus.PisoBus), nameof(BusListaDto.Piso))]
        [MapProperty(nameof(Bus.NPlaca), nameof(BusListaDto.Placa))]
        [MapProperty(nameof(Bus.NChasis), nameof(BusListaDto.Chasis))]
        [MapProperty(nameof(Bus.NMotor), nameof(BusListaDto.Motor))]
        [MapProperty(nameof(Bus.Capacidad), nameof(BusListaDto.Capacidad))]
        [MapProperty(nameof(Bus.TipoMotor), nameof(BusListaDto.TipoMotor))]
        [MapProperty(nameof(Bus.Combustible), nameof(BusListaDto.Combustible))]
        [MapProperty(nameof(Bus.FechaAdquisicion), nameof(BusListaDto.FechaAdquisicion))]
        [MapProperty(nameof(Bus.Kilometraje), nameof(BusListaDto.Kilometraje))]
        [MapProperty(nameof(Bus.EstadoB), nameof(BusListaDto.Condicion))]
        public partial BusListaDto EntityToDto_BusLista(Bus entity);

        // DTO Actualizar → ENTIDAD
        [MapProperty(nameof(BusActuDto.Id), nameof(Bus.BusB))]
        [MapProperty(nameof(BusActuDto.Marca), nameof(Bus.Marca))]
        [MapProperty(nameof(BusActuDto.Modelo), nameof(Bus.Modelo))]
        [MapProperty(nameof(BusActuDto.Piso), nameof(Bus.PisoBus))]
        [MapProperty(nameof(BusActuDto.Placa), nameof(Bus.NPlaca))]
        [MapProperty(nameof(BusActuDto.Chasis), nameof(Bus.NChasis))]
        [MapProperty(nameof(BusActuDto.Motor), nameof(Bus.NMotor))]
        [MapProperty(nameof(BusActuDto.Capacidad), nameof(Bus.Capacidad))]
        [MapProperty(nameof(BusActuDto.TipoMotor), nameof(Bus.TipoMotor))]
        [MapProperty(nameof(BusActuDto.Combustible), nameof(Bus.Combustible))]
        [MapProperty(nameof(BusActuDto.FechaAdquisicion), nameof(Bus.FechaAdquisicion))]
        [MapProperty(nameof(BusActuDto.Kilometraje), nameof(Bus.Kilometraje))]
        [MapProperty(nameof(BusActuDto.Condicion), nameof(Bus.EstadoB))]
        public partial Bus DtoToEntity_BusActualizar(BusActuDto dto);

        // DTO Agregar → ENTIDAD
        [MapProperty(nameof(BusAgregaDto.Marca), nameof(Bus.Marca))]
        [MapProperty(nameof(BusAgregaDto.Modelo), nameof(Bus.Modelo))]
        [MapProperty(nameof(BusAgregaDto.Piso), nameof(Bus.PisoBus))]
        [MapProperty(nameof(BusAgregaDto.Placa), nameof(Bus.NPlaca))]
        [MapProperty(nameof(BusAgregaDto.Chasis), nameof(Bus.NChasis))]
        [MapProperty(nameof(BusAgregaDto.Motor), nameof(Bus.NMotor))]
        [MapProperty(nameof(BusAgregaDto.Capacidad), nameof(Bus.Capacidad))]
        [MapProperty(nameof(BusAgregaDto.TipoMotor), nameof(Bus.TipoMotor))]
        [MapProperty(nameof(BusAgregaDto.Combustible), nameof(Bus.Combustible))]
        [MapProperty(nameof(BusAgregaDto.Kilometraje), nameof(Bus.Kilometraje))]
        [MapProperty(nameof(BusAgregaDto.FechaAdquisicion), nameof(Bus.FechaAdquisicion))]
        public partial Bus DtoToEntity_BusAgregar(BusAgregaDto dto);
    }
}
