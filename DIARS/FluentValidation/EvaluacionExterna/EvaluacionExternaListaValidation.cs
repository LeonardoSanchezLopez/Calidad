using DIARS.Controllers.Dto.EvaluacionExterna;
using FluentValidation;

namespace DIARS.FluentValidation.EvaluacionExterna
{
    public class EvaluacionExternaListaValidation : AbstractValidator<EvaExListaDto>
    {
        public EvaluacionExternaListaValidation()
        {
            /*
            // Bus (ID del bus)
            RuleFor(x => x.Bus)
                .GreaterThan(0).WithMessage("Debe especificar un bus válido.");

            // Proveedor (ID del proveedor)
            RuleFor(x => x.Proveedor)
                .GreaterThan(0).WithMessage("Debe especificar un proveedor válido.");

            // Fecha de Registro
            RuleFor(x => x.FechaRegistro)
                .NotEmpty().WithMessage("La fecha de registro no puede estar vacía.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de registro no puede ser futura.");

            // Código de Trabajo Externo
            RuleFor(x => x.Cod_TrabajoExterno)
                .GreaterThan(0).WithMessage("Debe especificar un código de trabajo válido.");
            */
        }
    }
}