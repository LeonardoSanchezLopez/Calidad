using DIARS.Controllers.Dto.EvaluacionExterna;
using DIARS.Models;
using Riok.Mapperly.Abstractions;

namespace DIARS.Controllers.Mapping
{
    [Mapper]
    public partial class EvaluacionExternoMapper
    {
        // ENTIDAD → DTO Listar
        [MapProperty(nameof(EvaluacionExterna.CodigoEE), nameof(EvaExListaDto.Id))]
        [MapProperty(nameof(EvaluacionExterna.CodigoBus.NPlaca), nameof(EvaExListaDto.Bus))]
        [MapProperty(nameof(EvaluacionExterna.ProveedorEE.Nombre), nameof(EvaExListaDto.Proveedor))]
        [MapProperty(nameof(EvaluacionExterna.Fecha), nameof(EvaExListaDto.FechaRegistro))]
        [MapProperty(nameof(EvaluacionExterna.TECodigo.CodigoTE), nameof(EvaExListaDto.Cod_TrabajoExterno))]
        [MapProperty(nameof(EvaluacionExterna.Estado), nameof(EvaExListaDto.Condicion))]
        public partial EvaExListaDto EntityToDto_EvaExLista(EvaluacionExterna entity);

        // DTO Agregar → ENTIDAD
        [MapProperty(nameof(EvaExAgregaDto.Bus), nameof(EvaluacionExterna.CodigoBus.NPlaca))]
        [MapProperty(nameof(EvaExAgregaDto.Proveedor), nameof(EvaluacionExterna.ProveedorEE.Nombre))]
        [MapProperty(nameof(EvaExAgregaDto.FechaRegistro), nameof(EvaluacionExterna.Fecha))]
        [MapProperty(nameof(EvaExAgregaDto.Cod_TrabajoExterno), nameof(EvaluacionExterna.TECodigo.CodigoTE))]
        public partial EvaluacionExterna DtoToEntity_EvaExAgregar(EvaExAgregaDto dto);
    }
}
