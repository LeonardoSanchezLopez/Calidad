using DIARS.Controllers.Dto.EvaluacionInterna;
using FluentValidation;

namespace DIARS.FluentValidation.EvaluacionInterna
{
    public class EvaluacionInternaValidation : AbstractValidator<EvaInAgregaDto>
    {
        public EvaluacionInternaValidation()
        {
            // Bus (ID del bus)
            RuleFor(x => x.Bus)
                .NotEmpty().WithMessage("Debe especificar un bus válido.");

            // Fecha de Registro
            RuleFor(x => x.FechaRegistro)
                .NotEmpty().WithMessage("La fecha de registro no puede estar vacía.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de registro no puede ser futura.");

            // Código de Trabajo Interno
            RuleFor(x => x.Cod_TrabajoInterno)
                .GreaterThan(0).WithMessage("Debe especificar un código de trabajo válido.");
        }
    }
}