using DIARS.Controllers.Dto.DetalleNotaIngreso;
using DIARS.Models;
using Riok.Mapperly.Abstractions;

namespace DIARS.Controllers.Mapping
{
    [Mapper]
    public partial class DetalleNotaIngresoMapper
    {
        // ENTIDAD → DTO Listar
        [MapProperty(nameof(DetalleNotaIngreso.DetalleNotaIngresoID), nameof(DNoInListaDto.Id))]
        [MapProperty(nameof(DetalleNotaIngreso.IRCodigo.CodigoIR), nameof(DNoInListaDto.Cod_Ingreso))]
        [MapProperty(nameof(DetalleNotaIngreso.CantidadRecibida), nameof(DNoInListaDto.Cantidad))]
        [MapProperty(nameof(DetalleNotaIngreso.CodigoRepu.NombreR), nameof(DNoInListaDto.Repuesto))]
        [MapProperty(nameof(DetalleNotaIngreso.CantidadAceptada), nameof(DNoInListaDto.Aceptada))]
        [MapProperty(nameof(DetalleNotaIngreso.Precio), nameof(DNoInListaDto.Precio))]
        public partial DNoInListaDto EntityToDto_DNoInLista(DetalleNotaIngreso entity);

        // DTO Agregar → ENTIDAD
        [MapProperty(nameof(DNoInAgregaDto.Cod_IngresoRD), nameof(DetalleNotaIngreso.IRCodigo.CodigoIR))]
        [MapProperty(nameof(DNoInAgregaDto.Cantidad), nameof(DetalleNotaIngreso.CantidadRecibida))]
        [MapProperty(nameof(DNoInAgregaDto.Repuesto), nameof(DetalleNotaIngreso.CodigoRepu.NombreR))]
        [MapProperty(nameof(DNoInAgregaDto.Aceptada), nameof(DetalleNotaIngreso.CantidadAceptada))]
        [MapProperty(nameof(DNoInAgregaDto.Precio), nameof(DetalleNotaIngreso.Precio))]
        public partial DetalleNotaIngreso DtoToEntity_DNoInAgregar(DNoInAgregaDto dto);
    }
}
