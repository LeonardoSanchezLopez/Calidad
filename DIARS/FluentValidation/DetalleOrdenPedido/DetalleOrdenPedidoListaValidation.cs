using DIARS.Controllers.Dto.DetalleOrdenPedido;
using FluentValidation;

namespace DIARS.FluentValidation.DetalleOrdenPedido
{
    public class DetalleOrdenPedidoListaValidation : AbstractValidator<DOrPeListaDto>
    {
        public DetalleOrdenPedidoListaValidation()
        {
            /*
            // Código de Orden de Pedido (ID)
            RuleFor(x => x.Cod_OrdenPD)
                .GreaterThan(0).WithMessage("Debe especificar un código de orden de pedido válido.");

            // Repuesto (ID del repuesto)
            RuleFor(x => x.Repuesto)
                .GreaterThan(0).WithMessage("Debe seleccionar un repuesto válido.");

            // Cantidad
            RuleFor(x => x.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0.");
            */
        }
    }
}