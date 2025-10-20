using DIARS.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using FluentValidation;
using System.Text;
using DIARS;
using DIARS.Controllers.Dto.Bus;
using DIARS.FluentValidation.Bus;
using DIARS.Controllers.Dto.Categoria;
using DIARS.FluentValidation.Categoria;
using DIARS.Controllers.Dto.Especialidad;
using DIARS.FluentValidation.Especialidad;
using DIARS.Controllers.Dto.MarcaRepuesto;
using DIARS.FluentValidation.MarcaRepuesto;
using DIARS.Controllers.Dto.Mecanico;
using DIARS.FluentValidation.Mecanico;
using DIARS.Controllers.Dto.Proveedor;
using DIARS.FluentValidation.Proveedor;
using DIARS.Controllers.Dto.Repuesto;
using DIARS.FluentValidation.Repuesto;
using DIARS.Controllers.Dto.ContratoMantenimiento;
using DIARS.Controllers.Dto.DetalleEI;
using DIARS.Controllers.Dto.DetalleNotaIngreso;
using DIARS.Controllers.Dto.DetalleNotaSalida;
using DIARS.Controllers.Dto.DetalleOrdenCompra;
using DIARS.Controllers.Dto.DetalleOrdenPedido;
using DIARS.Controllers.Dto.DetalleOTE;
using DIARS.Controllers.Dto.DetalleOTI;
using DIARS.Controllers.Dto.EvaluacionExterna;
using DIARS.Controllers.Dto.EvaluacionInterna;
using DIARS.Controllers.Dto.Factura;
using DIARS.Controllers.Dto.NotaIngresoRepuestos;
using DIARS.Controllers.Dto.NotaSalidaRepuestos;
using DIARS.Controllers.Dto.OrdenCompra;
using DIARS.Controllers.Dto.OrdenPedido;
using DIARS.Controllers.Dto.OrdenTrabajoExterno;
using DIARS.Controllers.Dto.OrdenTrabajoInterno;
using DIARS.FluentValidation.ContratoMantenimiento;
using DIARS.FluentValidation.DetalleEI;
using DIARS.FluentValidation.DetalleNotaIngreso;
using DIARS.FluentValidation.DetalleNotaSalida;
using DIARS.FluentValidation.DetalleOrdenCompra;
using DIARS.FluentValidation.DetalleOrdenPedido;
using DIARS.FluentValidation.DetalleOTE;
using DIARS.FluentValidation.DetalleOTI;
using DIARS.FluentValidation.EvaluacionExterna;
using DIARS.FluentValidation.EvaluacionInterna;
using DIARS.FluentValidation.Factura;
using DIARS.FluentValidation.NotaIngresoRepuesto;
using DIARS.FluentValidation.NotaSalidaRepuesto;
using DIARS.FluentValidation.OrdenCompra;
using DIARS.FluentValidation.OrdenPedido;
using DIARS.FluentValidation.OrdenTrabajoExteno;
using DIARS.FluentValidation.OrdenTrabajoInteno;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EMTRAFESA", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Jwt Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var MySqlConfiguration = new MySQLDatabase(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddSingleton(MySqlConfiguration);

builder.Services.AddScoped<IBusService, BusService>();

builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<EspecialidadService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<MecanicoService>();
builder.Services.AddScoped<MarcaReService>();
builder.Services.AddScoped<ProveedorService>();
builder.Services.AddScoped<RepuestoService>();
builder.Services.AddScoped<IJwtService, JwtService>();


builder.Services.AddScoped<ContratoMantenimientoService>();
builder.Services.AddScoped<DetalleEIService>();
builder.Services.AddScoped<DetalleNotaIngresoService>();
builder.Services.AddScoped<DetalleNotaSalidaService>();
builder.Services.AddScoped<IDetalleOrdenCompraService, DetalleOrdenCompraService>();
builder.Services.AddScoped<IDetalleOrdenPedidoService, DetalleOrdenPedidoService>();
builder.Services.AddScoped<DetalleOTEService>();
builder.Services.AddScoped<DetalleOTIService>();
builder.Services.AddScoped<EvaluacionExternaService>();
builder.Services.AddScoped<EvaluacionInternaService>();
builder.Services.AddScoped<FacturaService>();
builder.Services.AddScoped<NotaIngresoRepuestoService>();

builder.Services.AddScoped<NotaSalidaRepuestoService>();
builder.Services.AddScoped<IOrdenCompraService, OrdenCompraService>();
builder.Services.AddScoped<IOrdenPedidoService, OrdenPedidoService>();
builder.Services.AddScoped<OrdenTrabajoExternoService>();
builder.Services.AddScoped<OrdenTrabajoInternoService>();

builder.Services.AddScoped<IValidator<BusActuDto>, BusActuDtoValidator>();
builder.Services.AddScoped<IValidator<BusAgregaDto>, BusAgregaDtoValidator>();

builder.Services.AddScoped<IValidator<CatActuDto>, CatActuDtoValidator>();
builder.Services.AddScoped<IValidator<CatAgregaDto>, CatAgregaDtoValidator>();

builder.Services.AddScoped<IValidator<EspeActuDto>, EspeActuDtoValidator>();
builder.Services.AddScoped<IValidator<EspeAgregaDto>, EspeAgregaDtoValidator>();

builder.Services.AddScoped<IValidator<MarcaActuDto>, MarcaActuDtoValidator>();
builder.Services.AddScoped<IValidator<MarcaAgregaDto>, MarcaAgregaDtoValidator>();

builder.Services.AddScoped<IValidator<MecaActuDto>, MecaActuDtoValidator>();
builder.Services.AddScoped<IValidator<MecaAgregaDto>, MecaAgregaDtoValidator>();

builder.Services.AddScoped<IValidator<ProActuDto>, ProActuDtoValidator>();
builder.Services.AddScoped<IValidator<ProAgregaDto>, ProAgregaDtoValidator>();

builder.Services.AddScoped<IValidator<RepuActuDto>, RepuActuDtoValidator>();
builder.Services.AddScoped<IValidator<RepuAgregaDto>, RepuAgregaDtoValidator>();

builder.Services.AddScoped<IValidator<CMAgregaDto>, ContratoMantenimientoValidation>();
builder.Services.AddScoped<IValidator<CMListaDto>, ContratoMantenimientoListaValidation>();

builder.Services.AddScoped<IValidator<DEIAgregaDto>, DetalleEIValidation>();
builder.Services.AddScoped<IValidator<DEIListaDto>, DetalleEIListaValidation>();

builder.Services.AddScoped<IValidator<DNoInAgregaDto>, DetalleNotaIngresoValidation>();
builder.Services.AddScoped<IValidator<DNoInListaDto>, DetalleNotaIngresoListaValidation>();

builder.Services.AddScoped<IValidator<DNoSaAgregaDto>, DetalleNotaSalidaValidation>();
builder.Services.AddScoped<IValidator<DNoSaListaDto>, DetalleNotaSalidaListaValidation>();

builder.Services.AddScoped<IValidator<DOrCoAgregaDto>, DetalleOrdenCompraValidation>();
builder.Services.AddScoped<IValidator<DOrCoListaDto>, DetalleOrdenCompraListaValidation>();

builder.Services.AddScoped<IValidator<DOrPeAgregaDto>, DetalleOrdenPedidoValidation>();
builder.Services.AddScoped<IValidator<DOrPeListaDto>, DetalleOrdenPedidoListaValidation>();

builder.Services.AddScoped<IValidator<DOTEAgregaDto>, DetalleOTEValidation>();
builder.Services.AddScoped<IValidator<DOTEListaDto>, DetalleOTEListaValidation>();

builder.Services.AddScoped<IValidator<DOTIAgregaDto>, DetalleOTIValidation>();
builder.Services.AddScoped<IValidator<DOTIListaDto>, DetalleOTIListaValidation>();

builder.Services.AddScoped<IValidator<EvaExAgregaDto>, EvaluacionExternaValidation>();
builder.Services.AddScoped<IValidator<EvaExListaDto>, EvaluacionExternaListaValidation>();

builder.Services.AddScoped<IValidator<EvaInAgregaDto>, EvaluacionInternaValidation>();
builder.Services.AddScoped<IValidator<EvaInListaDto>, EvaluacionInternaListaValidation>();

builder.Services.AddScoped<IValidator<FacAgregaDto>, FacturaValidation>();
builder.Services.AddScoped<IValidator<FacListaDto>, FacturaListaValidation>();

builder.Services.AddScoped<IValidator<NIRAgregaDto>, NotaIngresoRepuestoValidation>();
builder.Services.AddScoped<IValidator<NIRListaDto>, NotaIngresoRepuestoListaValidation>();

builder.Services.AddScoped<IValidator<NSRAgregaDto>, NotaSalidaRepuestoValidation>();
builder.Services.AddScoped<IValidator<NSRListaDto>, NotaSalidaRepuestoListaValidation>();

builder.Services.AddScoped<IValidator<OrCoAgregaDto>, OrdenCompraValidation>();
builder.Services.AddScoped<IValidator<OrCoListaDto>, OrdenCompraListaValidation>();

builder.Services.AddScoped<IValidator<OrPeAgregaDto>, OrdenPedidoValidation>();
builder.Services.AddScoped<IValidator<OrPeListaDto>, OrdenPedidoListaValidation>();

builder.Services.AddScoped<IValidator<OTEAgregaDto>, OrdenTrabajoExternoValidation>();
builder.Services.AddScoped<IValidator<OTEListaDto>, OrdenTrabajoExternoListaValidation>();

builder.Services.AddScoped<IValidator<OTIAgregaDto>, OrdenTrabajoInternoValidation>();
builder.Services.AddScoped<IValidator<OTIListaDto>, OrdenTrabajoInternoListaValidation>();

//builder.Services.AddScoped<IValidator<RepuAgregaDto>, RepuAgregaDtoValidator>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
var app = builder.Build();
// Configuración de Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
