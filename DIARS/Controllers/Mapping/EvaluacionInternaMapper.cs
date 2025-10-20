using DIARS.Controllers.Dto.EvaluacionInterna;
using DIARS.Models;
using Riok.Mapperly.Abstractions;

namespace DIARS.Controllers.Mapping
{
    [Mapper]
    public partial class EvaluacionInternaMapper
    {
        // ENTIDAD → DTO Listar
        [MapProperty(nameof(EvaluacionInterna.CodigoEI), nameof(EvaInListaDto.Id))]
        [MapProperty(nameof(EvaluacionInterna.CodigoBus.NPlaca), nameof(EvaInListaDto.Bus))]
        [MapProperty(nameof(EvaluacionInterna.Fecha), nameof(EvaInListaDto.FechaRegistro))]
        [MapProperty(nameof(EvaluacionInterna.TICodigo.CodigoTI), nameof(EvaInListaDto.Cod_TrabajoInterno))]
        [MapProperty(nameof(EvaluacionInterna.Estado), nameof(EvaInListaDto.Condicion))]
        public partial EvaInListaDto EntityToDto_EvaInLista(EvaluacionInterna entity);

        // DTO Agregar → ENTIDAD
        [MapProperty(nameof(EvaInAgregaDto.Bus), nameof(EvaluacionInterna.CodigoBus.NPlaca))]
        [MapProperty(nameof(EvaInAgregaDto.FechaRegistro), nameof(EvaluacionInterna.Fecha))]
        [MapProperty(nameof(EvaInAgregaDto.Cod_TrabajoInterno), nameof(EvaluacionInterna.TICodigo.CodigoTI))]
        public partial EvaluacionInterna DtoToEntity_EvaInAgregar(EvaInAgregaDto dto);
    }
}
