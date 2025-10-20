using DIARS.Controllers.Dto.OrdenCompra;
using DIARS.Controllers.Dto;

namespace DIARS.Service
{
    public interface IOrdenCompraService
    {
        List<OrCoListaDto> ListarOrdenCompra();
        ResponseDto<bool> InsertarOrdenCompra(OrCoAgregaDto personaDto);
        OrCoListaDto GetOrdenCompraId(int id);
        bool InhabilitarOrdenCompra(int id);
    }
}
