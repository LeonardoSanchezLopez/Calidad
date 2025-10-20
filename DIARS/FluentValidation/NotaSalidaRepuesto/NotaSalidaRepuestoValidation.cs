using DIARS.Controllers.Dto.NotaSalidaRepuestos;
using FluentValidation;

namespace DIARS.FluentValidation.NotaSalidaRepuesto
{
    public class NotaSalidaRepuestoValidation : AbstractValidator<NSRAgregaDto>
    {
        public NotaSalidaRepuestoValidation()
        {
            // Bus (ID del bus)
            RuleFor(x => x.Bus)
                .NotEmpty().WithMessage("Debe especificar un bus válido.");

            // Fecha
            RuleFor(x => x.Fecha)
                .NotEmpty().WithMessage("La fecha no puede estar vacía.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha no puede ser futura.");

            // Código de Orden de Pedido
            RuleFor(x => x.Cod_OrdenPedido)
                .GreaterThan(0).WithMessage("Debe especificar un código de orden de pedido válido.");
        }
    }
}