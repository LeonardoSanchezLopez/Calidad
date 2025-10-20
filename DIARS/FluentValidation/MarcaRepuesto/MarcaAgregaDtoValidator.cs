using DIARS.Controllers.Dto.MarcaRepuesto;
using FluentValidation;

namespace DIARS.FluentValidation.MarcaRepuesto
{
    public class MarcaAgregaDtoValidator : AbstractValidator<MarcaAgregaDto>
    {
        public MarcaAgregaDtoValidator()
        {
            // Validación de la Descripción
            RuleFor(marca => marca.Descripcion)
                .NotEmpty().WithMessage("El campo 'Descripción' no puede estar vacío.")
                .MaximumLength(100).WithMessage("La descripción no puede exceder los 100 caracteres.");

            // Validación del Proveedor
            RuleFor(marca => marca.Proveedor)
                .NotEmpty().WithMessage("Debe seleccionarse un proveedor válido.");
        }
    }
}
