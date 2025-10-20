using DIARS.Controllers.Dto.Bus;
using FluentValidation;

namespace DIARS.FluentValidation.Bus
{
    public class BusActuDtoValidator : AbstractValidator<BusActuDto>
    {
        public BusActuDtoValidator()
        {
            RuleFor(bus => bus.Id)
                .GreaterThan(0).WithMessage("El ID del bus debe ser mayor a 0.");

            RuleFor(bus => bus.Marca)
                .NotEmpty().WithMessage("La marca no puede estar vacía.")
                .MaximumLength(50).WithMessage("La marca no puede exceder los 50 caracteres.");

            RuleFor(bus => bus.Modelo)
                .NotEmpty().WithMessage("El modelo no puede estar vacío.")
                .MaximumLength(50).WithMessage("El modelo no puede exceder los 50 caracteres.");


            RuleFor(bus => bus.Placa)
                .NotEmpty().WithMessage("La placa no puede estar vacía.")
                .MaximumLength(10).WithMessage("La placa no puede exceder los 10 caracteres.");

            RuleFor(bus => bus.Chasis)
                .NotEmpty().WithMessage("El número de chasis no puede estar vacío.")
                .MaximumLength(30).WithMessage("El número de chasis no puede exceder los 30 caracteres.");

            RuleFor(bus => bus.Motor)
                .NotEmpty().WithMessage("El número de motor no puede estar vacío.")
                .MaximumLength(30).WithMessage("El número de motor no puede exceder los 30 caracteres.");

            RuleFor(bus => bus.Capacidad)
                .GreaterThan(0).WithMessage("La capacidad debe ser un número mayor a 0.")
                .LessThanOrEqualTo(100).WithMessage("La capacidad máxima permitida es de 100 pasajeros.");

            RuleFor(bus => bus.FechaAdquisicion)
                .NotEmpty().WithMessage("La fecha de adquisición no puede estar vacía.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de adquisición no puede ser futura.");

            RuleFor(bus => bus.Kilometraje)
                .GreaterThanOrEqualTo(0).WithMessage("El kilometraje no puede ser negativo.");

            RuleFor(bus => bus.Combustible)
            .NotEmpty().WithMessage("El tipo de combustible no puede estar vacío.");

            RuleFor(bus => bus.Condicion)
            .NotNull().WithMessage("El estado del bus debe estar definido (activo/inactivo).");

            RuleFor(bus => bus.Combustible)
            .NotEmpty().WithMessage("El tipo de combustible no puede estar vacío.");
        }
    }
}
