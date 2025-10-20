using DIARS.Controllers.Dto.Especialidad;
using FluentValidation;

namespace DIARS.FluentValidation.Especialidad
{
    public class EspeActuDtoValidator : AbstractValidator<EspeActuDto>
    {
        public EspeActuDtoValidator()
        {
            // Validación del ID
            RuleFor(espe => espe.Id)
                .GreaterThan(0).WithMessage("El ID debe ser mayor a 0.");

            // Validación del Nombre
            RuleFor(espe => espe.Nombre)
                .NotEmpty().WithMessage("El campo 'Nombre' no puede estar vacío.")
                .MaximumLength(50).WithMessage("El campo 'Nombre' no puede exceder los 50 caracteres.")
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$").WithMessage("El campo 'Nombre' solo puede contener letras y espacios.");

            // Validación de la Descripción
            RuleFor(espe => espe.Descripcion)
                .NotEmpty().WithMessage("El campo 'Descripción' no puede estar vacío.")
                .MaximumLength(200).WithMessage("El campo 'Descripción' no puede exceder los 200 caracteres.");

        }
    }
}