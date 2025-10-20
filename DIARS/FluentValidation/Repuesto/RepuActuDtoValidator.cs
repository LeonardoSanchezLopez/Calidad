using DIARS.Controllers.Dto.Repuesto;
using FluentValidation;

namespace DIARS.FluentValidation.Repuesto
{
    public class RepuActuDtoValidator : AbstractValidator<RepuActuDto>
    {
        public RepuActuDtoValidator()
        {
            // Validación del ID
            RuleFor(r => r.Id)
                .GreaterThan(0).WithMessage("El ID debe ser mayor a 0.");

            // Validación del Nombre
            RuleFor(r => r.Nombre)
                .NotEmpty().WithMessage("El nombre del repuesto no puede estar vacío.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.")
                .Matches(@"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s\-\(\)]+$").WithMessage("El nombre solo puede contener letras, números, espacios, guiones y paréntesis.");

            // Validación de la Categoría
            RuleFor(r => r.Categoria)
                .NotEmpty().WithMessage("Debe seleccionar una categoría válida.");

            // Validación de la Marca del Repuesto
            RuleFor(r => r.Marcarepuesto)
                .NotEmpty().WithMessage("Debe seleccionar una marca válida.");

            // Validación del Proveedor
            RuleFor(r => r.Proveedor)
                .NotEmpty().WithMessage("Debe seleccionar un proveedor válido.");

            // Validación del Precio
            RuleFor(r => r.Precio)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a 0.")
                .LessThanOrEqualTo(100000).WithMessage("El precio no puede exceder los 100,000.");

            // Validación de la Condición
            RuleFor(r => r.Condicion)
                .NotNull().WithMessage("El campo 'Condición' debe ser 'true' o 'false'.");
        }
    }
}
