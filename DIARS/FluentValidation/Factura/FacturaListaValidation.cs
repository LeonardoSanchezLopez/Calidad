using DIARS.Controllers.Dto.Factura;
using FluentValidation;

namespace DIARS.FluentValidation.Factura
{
    public class FacturaListaValidation : AbstractValidator<FacListaDto>
    {
        public FacturaListaValidation()
        {
            /*
            // Código de Orden de Compra (Cod_OrdenC)
            RuleFor(x => x.Cod_OrdenC)
                .GreaterThan(0).WithMessage("Debe especificar un código de orden de compra válido.");

            // Fecha de Registro
            RuleFor(x => x.FechaRegistro)
                .NotEmpty().WithMessage("La fecha de registro no puede estar vacía.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de registro no puede ser futura.");

            // Total
            RuleFor(x => x.Total)
                .GreaterThan(0).WithMessage("El total debe ser mayor a 0.");
            */
        }
    }
}