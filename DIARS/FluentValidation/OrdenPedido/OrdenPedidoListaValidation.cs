using DIARS.Controllers.Dto.OrdenPedido;
using FluentValidation;

namespace DIARS.FluentValidation.OrdenPedido
{
    public class OrdenPedidoListaValidation : AbstractValidator<OrPeListaDto>
    {
        public OrdenPedidoListaValidation()
        {
            /*
            // Fecha
            RuleFor(x => x.Fecha)
                .NotEmpty().WithMessage("La fecha no puede estar vacía.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha no puede ser futura.");

            // Código de Trabajo Interno
            RuleFor(x => x.Cod_TrabajoInterno)
                .GreaterThan(0).WithMessage("Debe especificar un código de trabajo válido.");

            // Encargado
            RuleFor(x => x.Encargado)
                .NotEmpty().WithMessage("El campo encargado no puede estar vacío.")
                .MaximumLength(100).WithMessage("El campo encargado no puede exceder los 100 caracteres.");

            // Descripción
            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("La descripción no puede estar vacía.")
                .MaximumLength(500).WithMessage("La descripción no puede exceder los 500 caracteres.");
            */
        }
    }
}