using DIARS.Controllers.Dto.DetalleOrdenCompra;
using DIARS.Controllers.Dto;

namespace DIARS.Service
{
    public interface IDetalleOrdenCompraService
    {
        List<DOrCoListaDto> ListarDetalleOrdenCompra();
        ResponseDto<bool> InsertarDetalleOrdenCompra(DOrCoAgregaDto dto);
        DOrCoListaDto ObtenerDetalleOrdenCompra(int id);
    }
}