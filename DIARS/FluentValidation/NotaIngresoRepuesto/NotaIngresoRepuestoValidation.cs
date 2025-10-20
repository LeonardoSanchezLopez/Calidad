using DIARS.Controllers.Dto.NotaIngresoRepuestos;
using FluentValidation;

namespace DIARS.FluentValidation.NotaIngresoRepuesto
{
    public class NotaIngresoRepuestoValidation : AbstractValidator<NIRAgregaDto>
    {
        public NotaIngresoRepuestoValidation()
        {
            // Código de Orden de Compra (Cod_OrdenC)
            RuleFor(x => x.Cod_OrdenC)
                .GreaterThan(0).WithMessage("Debe especificar un código de orden de compra válido.");

            // Fecha
            RuleFor(x => x.Fecha)
                .NotEmpty().WithMessage("La fecha no puede estar vacía.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha no puede ser futura.");

            // Proveedor (ID del proveedor)
            RuleFor(x => x.Proveedor)
                .NotEmpty().WithMessage("Debe especificar un proveedor válido.");
        }
    }
}
