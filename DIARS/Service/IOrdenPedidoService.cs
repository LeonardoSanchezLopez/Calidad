using DIARS.Controllers.Dto.OrdenPedido;
using DIARS.Controllers.Dto;

namespace DIARS.Service
{
    public interface IOrdenPedidoService
    {
        List<OrPeListaDto> ListarOrdenPedido();
        ResponseDto<bool> InsertarOrdenPedido(OrPeAgregaDto personaDto);
        OrPeListaDto GetOrdenPedidoId(int id);
        bool InhabilitarOrdenPedidio(int id);
    }
}
