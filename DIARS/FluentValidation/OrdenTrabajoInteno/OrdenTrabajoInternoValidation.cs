using DIARS.Controllers.Dto.OrdenTrabajoInterno;
using FluentValidation;

namespace DIARS.FluentValidation.OrdenTrabajoInteno
{
    public class OrdenTrabajoInternoValidation : AbstractValidator<OTIAgregaDto>
    {
        public OrdenTrabajoInternoValidation()
        {
            // Bus (ID del bus)
            RuleFor(x => x.PlacaBus)
                .NotEmpty().WithMessage("Debe especificar un bus válido.");

            // Fecha
            RuleFor(x => x.Fecha)
                .NotEmpty().WithMessage("La fecha no puede estar vacía.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha no puede ser futura.");

            // Mecanico (ID del mecánico)
            RuleFor(x => x.Mecanico)
                .NotEmpty().WithMessage("Debe especificar un mecánico válido.");
        }
    }
}