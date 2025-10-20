using DIARS.Controllers.Dto.Mecanico;
using FluentValidation;

namespace DIARS.FluentValidation.Mecanico
{
    public class MecaAgregaDtoValidator : AbstractValidator<MecaAgregaDto>
    {
        public MecaAgregaDtoValidator()
        {
            // Validación de Especialidad
            RuleFor(meca => meca.Especialidad)
                .NotEmpty().WithMessage("Debe seleccionar una especialidad válida.");

            // Validación del Nombre
            RuleFor(meca => meca.Nombre)
                .NotEmpty().WithMessage("El nombre no puede estar vacío.")
                .MaximumLength(50).WithMessage("El nombre no puede exceder los 50 caracteres.")
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$").WithMessage("El nombre solo puede contener letras y espacios.");

            // Validación del DNI
            RuleFor(meca => meca.Dni)
                .NotEmpty().WithMessage("El DNI no puede estar vacío.")
                .Matches(@"^\d{8}$").WithMessage("El DNI debe tener exactamente 8 dígitos.");

            // Validación del Domicilio
            RuleFor(meca => meca.Domicilio)
                .NotEmpty().WithMessage("El domicilio no puede estar vacío.")
                .MaximumLength(100).WithMessage("El domicilio no puede exceder los 100 caracteres.");

            // Validación de la Experiencia
            RuleFor(meca => meca.Experiencia)
                .NotEmpty().WithMessage("El campo 'Experiencia' no puede estar vacío.")
                .MaximumLength(100).WithMessage("La experiencia no puede exceder los 100 caracteres.");

            // Validación del Teléfono
            RuleFor(meca => meca.Telefono)
                .NotEmpty().WithMessage("El teléfono no puede estar vacío.")
                .Matches(@"^\d{9}$").WithMessage("El teléfono debe tener exactamente 9 dígitos.");

            // Validación del Sueldo
            RuleFor(meca => meca.Sueldo)
                .GreaterThan(0).WithMessage("El sueldo debe ser mayor a 0.")
                .LessThanOrEqualTo(10000).WithMessage("El sueldo no debe exceder los 10,000.");

            // Validación del Turno
            RuleFor(meca => meca.Turno)
                .NotEmpty().WithMessage("El turno no puede estar vacío.")
                .Must(t => new[] { "Mañana", "Tarde", "Noche" }.Contains(t, StringComparer.OrdinalIgnoreCase))
                .WithMessage("El turno debe ser 'Mañana', 'Tarde' o 'Noche'.");

            // Validación de Fecha de Contrato
            RuleFor(meca => meca.FechaContrato)
                .NotEmpty().WithMessage("La fecha de contrato no puede estar vacía.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de contrato no puede ser futura.");
        }
    }
}
