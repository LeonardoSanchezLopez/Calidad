using DIARS.Controllers.Dto.Categoria;
using FluentValidation;

namespace DIARS.FluentValidation.Categoria
{
    public class CatActuDtoValidator : AbstractValidator<CatActuDto>
    {
        public CatActuDtoValidator()
        {
            RuleFor(cat => cat.Id)
                .GreaterThan(0).WithMessage("El ID debe ser mayor a 0.");

            RuleFor(cat => cat.Nombre)
                .NotEmpty().WithMessage("El campo 'Nombre' no puede estar vacío.")
                .MaximumLength(50).WithMessage("El campo 'Nombre' no puede exceder los 50 caracteres.")
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$").WithMessage("El campo 'Nombre' solo puede contener letras y espacios.");

            RuleFor(cat => cat.Descripcion)
                .NotEmpty().WithMessage("El campo 'Descripción' no puede estar vacío.")
                .MaximumLength(200).WithMessage("El campo 'Descripción' no puede exceder los 200 caracteres.");
        }
    }
}
