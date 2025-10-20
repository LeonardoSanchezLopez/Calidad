using DIARS.Controllers.Dto.Proveedor;
using FluentValidation;

namespace DIARS.FluentValidation.Proveedor
{
    public class ProActuDtoValidator : AbstractValidator<ProActuDto>
    {
        public ProActuDtoValidator()
        {
            // Validación del ID
            RuleFor(pro => pro.Id)
                .GreaterThan(0).WithMessage("El ID debe ser mayor a 0.");

            // Validación del Nombre
            RuleFor(pro => pro.Nombre)
                .NotEmpty().WithMessage("El nombre no puede estar vacío.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.")
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$").WithMessage("El nombre solo puede contener letras y espacios.");

            // Validación de la Dirección
            RuleFor(pro => pro.Direccion)
                .NotEmpty().WithMessage("La dirección no puede estar vacía.")
                .MaximumLength(150).WithMessage("La dirección no puede exceder los 150 caracteres.");

            // Validación del Teléfono
            RuleFor(pro => pro.Telefono)
                .NotEmpty().WithMessage("El teléfono no puede estar vacío.")
                .Matches(@"^\d{9}$").WithMessage("El teléfono debe tener exactamente 9 dígitos.");

            // Validación del Correo
            RuleFor(pro => pro.Correo)
                .NotEmpty().WithMessage("El correo no puede estar vacío.")
                .EmailAddress().WithMessage("El correo no tiene un formato válido.")
                .MaximumLength(100).WithMessage("El correo no puede exceder los 100 caracteres.");

            // Validación de Condición
            RuleFor(pro => pro.Condicion)
                .NotNull().WithMessage("El campo 'Condición' debe ser 'true' o 'false'.");
        }
    }
}
