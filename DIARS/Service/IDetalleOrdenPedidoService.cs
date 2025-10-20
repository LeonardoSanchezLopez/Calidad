using DIARS.Controllers.Dto.DetalleOrdenPedido;
using DIARS.Controllers.Dto;

namespace DIARS.Service
{
    public interface IDetalleOrdenPedidoService
    {
        List<DOrPeListaDto> ListarDetalleOrdenPedido();
        ResponseDto<bool> InsertarDetalleOrdenPedido(DOrPeAgregaDto dto);
        DOrPeListaDto ObtenerDetalleOrdenPedido(int id);
    }
}
