using DIARS.Controllers.Dto.DetalleOTE;
using FluentValidation;

namespace DIARS.FluentValidation.DetalleOTE
{
    public class DetalleOTEListaValidation : AbstractValidator<DOTEListaDto>
    {
        public DetalleOTEListaValidation()
        {
            // Código de Trabajo Específico
            RuleFor(x => x.Cod_TrabajoED)
                .GreaterThan(0).WithMessage("Debe especificar un código de trabajo válido.");

            // Repuesto
            RuleFor(x => x.Repuesto)
                .NotEmpty().WithMessage("Debe seleccionar un repuesto válido.");

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
        }
    }
}