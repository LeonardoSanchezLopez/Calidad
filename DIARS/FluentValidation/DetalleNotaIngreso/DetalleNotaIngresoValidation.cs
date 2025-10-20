using DIARS.Controllers.Dto.DetalleNotaIngreso;
using FluentValidation;

namespace DIARS.FluentValidation.DetalleNotaIngreso
{
    public class DetalleNotaIngresoValidation : AbstractValidator<DNoInAgregaDto>
    {
        public DetalleNotaIngresoValidation()
        {
            // Código de Ingreso (ID de detalle nota de ingreso)
            RuleFor(x => x.Cod_IngresoRD)
                .GreaterThan(0).WithMessage("Debe especificar un código de ingreso válido.");

            // Cantidad (número de repuestos)
            RuleFor(x => x.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0.");

            // Repuesto (ID del repuesto)
            RuleFor(x => x.Repuesto)
                .NotEmpty().WithMessage("Debe seleccionar un repuesto válido.");

            // Aceptada (cantidad aceptada del repuesto)
            RuleFor(x => x.Aceptada)
                .GreaterThanOrEqualTo(0).WithMessage("La cantidad aceptada no puede ser negativa.")
                .LessThanOrEqualTo(x => x.Cantidad).WithMessage("La cantidad aceptada no puede ser mayor a la cantidad total.");

            // Precio
            RuleFor(x => x.Precio)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a 0.");
        }
    }
}