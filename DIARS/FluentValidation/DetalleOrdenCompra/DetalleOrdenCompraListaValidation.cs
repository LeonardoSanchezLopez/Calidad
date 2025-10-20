using DIARS.Controllers.Dto.DetalleOrdenCompra;
using FluentValidation;

namespace DIARS.FluentValidation.DetalleOrdenCompra
{
    public class DetalleOrdenCompraListaValidation : AbstractValidator<DOrCoListaDto>
    {
        public DetalleOrdenCompraListaValidation()
        {
            /*
            // Código de Compra (ID de detalle de orden de compra)
            RuleFor(x => x.Cod_CompraOD)
                .GreaterThan(0).WithMessage("Debe especificar un código de compra válido.");

            // Repuesto (ID del repuesto)
            RuleFor(x => x.Repuesto)
                .GreaterThan(0).WithMessage("Debe seleccionar un repuesto válido.");

            // Cantidad
            RuleFor(x => x.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0.");

            // Precio
            RuleFor(x => x.Precio)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a 0.");
            */
        }
    }
}