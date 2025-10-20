using DIARS.Controllers.Dto.MarcaRepuesto;
using FluentValidation;

namespace DIARS.FluentValidation.MarcaRepuesto
{
    public class MarcaActuDtoValidator : AbstractValidator<MarcaActuDto>
    {
        public MarcaActuDtoValidator()
        {
            // Validación del ID
            RuleFor(marca => marca.Id)
                .GreaterThan(0).WithMessage("El ID debe ser mayor a 0.");

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