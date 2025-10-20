using DIARS.Controllers.Dto.DetalleNotaSalida;
using FluentValidation;

namespace DIARS.FluentValidation.DetalleNotaSalida
{
    public class DetalleNotaSalidaValidation : AbstractValidator<DNoSaAgregaDto>
    {
        public DetalleNotaSalidaValidation()
        {
            // Código de Salida (ID de detalle nota de salida)
            RuleFor(x => x.Cod_SalidaRD)
                .GreaterThan(0).WithMessage("Debe especificar un código de salida válido.");

            // Repuesto (ID del repuesto)
            RuleFor(x => x.Repuesto)
                .NotEmpty().WithMessage("Debe seleccionar un repuesto válido.");

            // Enviada (cantidad enviada)
            RuleFor(x => x.Enviada)
                .GreaterThan(0).WithMessage("La cantidad enviada debe ser mayor a 0.");

            // Recibida (cantidad recibida)
            RuleFor(x => x.Recibida)
                .GreaterThanOrEqualTo(0).WithMessage("La cantidad recibida no puede ser negativa.");
                //.LessThanOrEqualTo(x => x.Enviada).WithMessage("La cantidad recibida no puede ser mayor que la cantidad enviada.");
        }
    }
}