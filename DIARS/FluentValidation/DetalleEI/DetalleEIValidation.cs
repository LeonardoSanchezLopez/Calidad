using DIARS.Controllers.Dto.DetalleEI;
using FluentValidation;

namespace DIARS.FluentValidation.DetalleEI
{
    public class DetalleEIValidation : AbstractValidator<DEIAgregaDto>
    {
        public DetalleEIValidation()
        {
            // Evaluación Interna (ID)
            RuleFor(x => x.EvaluacionInterna)
                .GreaterThan(0).WithMessage("Debe seleccionar una evaluación interna válida.");

            // Mecánico (ID del mecánico responsable)
            RuleFor(x => x.Mecanico)
                .NotEmpty().WithMessage("Debe seleccionar un mecánico válido.");
        }
    }
}