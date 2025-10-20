using DIARS.Controllers.Dto.DetalleNotaSalida;
using DIARS.Models;
using Riok.Mapperly.Abstractions;

namespace DIARS.Controllers.Mapping
{
    [Mapper]
    public partial class DetalleNotaSalidaMapper
    {
        // ENTIDAD → DTO Listar
        [MapProperty(nameof(DetalleNotaSalida.DetalleNotaSalidaID), nameof(DNoSaListaDto.Id))]
        [MapProperty(nameof(DetalleNotaSalida.SRCodigo.CodigoSR), nameof(DNoSaListaDto.Cod_SalidaRD))]
        [MapProperty(nameof(DetalleNotaSalida.CantidadRecibida), nameof(DNoSaListaDto.Recibida))]
        [MapProperty(nameof(DetalleNotaSalida.CodigoRepu.NombreR), nameof(DNoSaListaDto.Repuesto))]
        [MapProperty(nameof(DetalleNotaSalida.CantidadEnviada), nameof(DNoSaListaDto.Enviada))]
        public partial DNoSaListaDto EntityToDto_DNoSaLista(DetalleNotaSalida entity);

        // DTO Agregar → ENTIDAD
        [MapProperty(nameof(DNoSaAgregaDto.Cod_SalidaRD), nameof(DetalleNotaSalida.SRCodigo.CodigoSR))]
        [MapProperty(nameof(DNoSaAgregaDto.Recibida), nameof(DetalleNotaSalida.CantidadRecibida))]
        [MapProperty(nameof(DNoSaAgregaDto.Repuesto), nameof(DetalleNotaSalida.CodigoRepu.NombreR))]
        [MapProperty(nameof(DNoSaAgregaDto.Enviada), nameof(DetalleNotaSalida.CantidadEnviada))]
        public partial DetalleNotaSalida DtoToEntity_DNoSaAgregar(DNoSaAgregaDto dto);
    }
}
