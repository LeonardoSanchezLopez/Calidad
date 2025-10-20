using DIARS.Controllers.Dto.OrdenTrabajoExterno;
using FluentValidation;

namespace DIARS.FluentValidation.OrdenTrabajoExteno
{
    public class OrdenTrabajoExternoValidation : AbstractValidator<OTEAgregaDto>
    {
        public OrdenTrabajoExternoValidation()
        {
            // Bus (ID del bus)
            RuleFor(x => x.PlacaBus)
                .NotEmpty().WithMessage("Debe especificar un bus válido.");

            // Contrato (ID del contrato)
            RuleFor(x => x.Contrato)
                .GreaterThan(0).WithMessage("Debe especificar un contrato válido.");

            // Fecha
            RuleFor(x => x.Fecha)
                .NotEmpty().WithMessage("La fecha no puede estar vacía.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha no puede ser futura.");

            // Proveedor (ID del proveedor)
            RuleFor(x => x.Proveedor)
                .NotEmpty().WithMessage("Debe especificar un proveedor válido.");
        }
    }
}