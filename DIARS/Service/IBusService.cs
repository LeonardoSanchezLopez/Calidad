using DIARS.Controllers.Dto;
using DIARS.Controllers.Dto.Bus;
using System.Collections.Generic;

namespace DIARS.Service
{
    public interface IBusService
    {
        // Añade todos los métodos públicos del BusService
        List<BusListaDto> ListarBus();
        ResponseDto<bool> InsertarBus(BusAgregaDto personaDto);
        ResponseDto<bool> ActualizarBus(BusActuDto personaDto);
        BusListaDto GetBusId(int id);
        bool InhabilitarBus(int id);
    }
}