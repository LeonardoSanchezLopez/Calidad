using DIARS.Controllers.Dto.OrdenCompra;
using FluentValidation;

namespace DIARS.FluentValidation.OrdenCompra
{
    public class OrdenCompraListaValidation : AbstractValidator<OrCoListaDto>
    {
        public OrdenCompraListaValidation()
        {
            /*
            // Repuesto (ID del repuesto)
            RuleFor(x => x.Repuesto)
                .GreaterThan(0).WithMessage("Debe especificar un repuesto válido.");

            // Fecha
            RuleFor(x => x.Fecha)
                .NotEmpty().WithMessage("La fecha no puede estar vacía.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha no puede ser futura.");

            // Código de Orden de Pedido
            RuleFor(x => x.Cod_OrdenPedido)
                .GreaterThan(0).WithMessage("Debe especificar un código de orden de pedido válido.");

            // Pago
            RuleFor(x => x.Pago)
                .NotEmpty().WithMessage("El campo de pago no puede estar vacío.")
                .MaximumLength(50).WithMessage("El campo de pago no puede exceder los 50 caracteres.");

            // Total
            RuleFor(x => x.Total)
                .GreaterThan(0).WithMessage("El total debe ser mayor a 0.");
            */
        }
    }
}