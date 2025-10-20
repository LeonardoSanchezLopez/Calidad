using DIARS.Controllers.Dto.ContratoMantenimiento;
using FluentValidation;

namespace DIARS.FluentValidation.ContratoMantenimiento
{
    public class ContratoMantenimientoValidation : AbstractValidator<CMAgregaDto>
    {
        public ContratoMantenimientoValidation()
        {
            // Bus (ID del bus relacionado)
            RuleFor(x => x.BusPlaca)
                .NotEmpty().WithMessage("Debe seleccionar un bus válido.");

            // Fecha (de mantenimiento o compra)
            RuleFor(x => x.Fecha)
                .NotEmpty().WithMessage("La fecha no puede estar vacía.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha no puede ser futura.");

            // Proveedor (ID del proveedor relacionado)
            RuleFor(x => x.Proveedor)
                .NotEmpty().WithMessage("Debe seleccionar un proveedor válido.");

            // Descripción
            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("La descripción no puede estar vacía.")
                .MaximumLength(250).WithMessage("La descripción no puede exceder los 250 caracteres.");

            // Costo
            RuleFor(x => x.Costo)
                .GreaterThan(0).WithMessage("El costo debe ser mayor a 0.");
        }
    }
}
