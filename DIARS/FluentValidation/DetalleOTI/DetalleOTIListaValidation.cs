using DIARS.Controllers.Dto.DetalleOTI;
using FluentValidation;

namespace DIARS.FluentValidation.DetalleOTI
{
    public class DetalleOTIListaValidation : AbstractValidator<DOTIListaDto>
    {
        public DetalleOTIListaValidation()
        {
            /*
            // Código de Orden de Trabajo
            RuleFor(x => x.Cod_OrdenTI)
                .GreaterThan(0).WithMessage("Debe especificar un código de orden de trabajo válido.");

            // Repuesto
            RuleFor(x => x.Repuesto)
                .GreaterThan(0).WithMessage("Debe seleccionar un repuesto válido.");

            // Mecanico (ID del mecánico responsable)
            RuleFor(x => x.Mecanico)
                .GreaterThan(0).WithMessage("Debe seleccionar un mecánico válido.");

            // Parte
            RuleFor(x => x.Parte)
                .NotEmpty().WithMessage("La parte no puede estar vacía.")
                .MaximumLength(100).WithMessage("La parte no puede exceder los 100 caracteres.");

            // Pieza
            RuleFor(x => x.Pieza)
                .NotEmpty().WithMessage("La pieza no puede estar vacía.")
                .MaximumLength(100).WithMessage("La pieza no puede exceder los 100 caracteres.");

            // Cantidad
            RuleFor(x => x.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0.");
            */
        }
    }
}