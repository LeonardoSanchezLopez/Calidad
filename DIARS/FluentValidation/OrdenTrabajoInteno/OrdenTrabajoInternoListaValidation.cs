using DIARS.Controllers.Dto.OrdenTrabajoInterno;
using FluentValidation;

namespace DIARS.FluentValidation.OrdenTrabajoInteno
{
    public class OrdenTrabajoInternoListaValidation : AbstractValidator<OTIListaDto>
    {
        public OrdenTrabajoInternoListaValidation()
        {
            /*
            // Bus (ID del bus)
            RuleFor(x => x.Bus)
                .GreaterThan(0).WithMessage("Debe especificar un bus válido.");

            // Fecha
            RuleFor(x => x.Fecha)
                .NotEmpty().WithMessage("La fecha no puede estar vacía.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha no puede ser futura.");

            // Mecanico (ID del mecánico)
            RuleFor(x => x.Mecanico)
                .GreaterThan(0).WithMessage("Debe especificar un mecánico válido.");
            */
        }
    }
}