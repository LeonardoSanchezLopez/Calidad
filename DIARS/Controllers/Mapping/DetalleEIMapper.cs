using DIARS.Controllers.Dto.DetalleEI;
using DIARS.Models;
using Riok.Mapperly.Abstractions;

namespace DIARS.Controllers.Mapping
{
    [Mapper]
    public partial class DetalleEIMapper
    {
        // ENTIDAD → DTO Listar
        [MapProperty(nameof(DetalleEI.DetalleEvaluacionInternaID), nameof(DEIListaDto.Id))]
        [MapProperty(nameof(DetalleEI.EICodigo.CodigoEI), nameof(DEIListaDto.EvaluacionInterna))]
        [MapProperty(nameof(DetalleEI.MecanicoEI.Nombre), nameof(DEIListaDto.Mecanico))]
        //[MapProperty(nameof(DetalleEI.FechaRegistro), nameof(DEIListaDto.Fecha))]
        public partial DEIListaDto EntityToDto_DEILista(DetalleEI entity);

        // DTO Agregar → ENTIDAD
        [MapProperty(nameof(DEIAgregaDto.EvaluacionInterna), nameof(DetalleEI.EICodigo.CodigoEI))]
        [MapProperty(nameof(DEIAgregaDto.Mecanico), nameof(DetalleEI.MecanicoEI.Nombre))]
        public partial DetalleEI DtoToEntity_DEIAgregar(DEIAgregaDto dto);
    }
}
