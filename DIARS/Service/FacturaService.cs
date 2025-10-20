using DIARS.Controllers.Dto.EvaluacionInterna;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Mapping;
using DIARS.Models;
using FluentValidation;
using MySql.Data.MySqlClient;
using System.Data;
using DIARS.Controllers.Dto.Factura;

namespace DIARS.Service
{
    public class FacturaService
    {
        private readonly MySQLDatabase _db;
        private readonly IValidator<FacAgregaDto> _validator;

        public FacturaService(MySQLDatabase db, IValidator<FacAgregaDto> validator)
        {
            _db = db;
            _validator = validator;
        }

        public List<FacListaDto> ListarFactura()
        {
            List<Factura> lista = new();

            using var connection = _db.GetConnection();
            connection.Open();

            using var command = new MySqlCommand("CALL SP_Factura_Lista()", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Factura
                {
                    CodigoFactura = reader.GetInt32("CodigoFactura"),
                    CodigoOC = new OrdenCompra
                    {
                        CodigoOC = reader.GetInt32("CodigoOC")
                    },
                    Fecha = reader.GetDateTime("Fecha"),
                    Total = reader.GetDecimal("Total"),
                });
            }

            var mapper = new FacturaMapper();
            return lista.Select(e => mapper.EntityToDto_FacLista(e)).ToList();
        }

        public ResponseDto<bool> InsertarFactura(FacAgregaDto dto)
        {
            var response = new ResponseDto<bool>();

            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                response.EjecucionExitosa = false;
                response.MensajeError = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                response.Data = false;
                return response;
            }

            var mapper = new FacturaMapper();
            var entity = mapper.DtoToEntity_FacAgregar(dto);

            try
            {
                using var connection = _db.GetConnection();
                connection.Open();

                using var command = new MySqlCommand("SP_Factura_Crea", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("p_CodigoOC", entity.CodigoOC.CodigoOC);
                command.Parameters.AddWithValue("p_Fecha", entity.Fecha);
                command.Parameters.AddWithValue("p_TOTAL", entity.Total);

                var mensajeParam = new MySqlParameter("p_Mensaje", MySqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(mensajeParam);

                command.ExecuteNonQuery();

                string mensaje = mensajeParam.Value?.ToString();
                response.MensajeError = mensaje;
                response.EjecucionExitosa = mensaje != null && mensaje.Contains("exitosa");
                response.Data = response.EjecucionExitosa;
            }
            catch (MySqlException ex)
            {
                response.EjecucionExitosa = false;
                response.MensajeError = $"Error en BD: {ex.Message}";
                response.Data = false;
            }

            return response;
        }

        public FacListaDto ObtenerFacturaPorId(int id)
        {
            Factura entity = null;

            using var connection = _db.GetConnection();
            connection.Open();

            using var command = new MySqlCommand("SP_Factura_ObtenPorId", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("p_CodigoFactura", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                entity = new Factura
                {
                    CodigoFactura = reader.GetInt32("CodigoFactura"),
                    CodigoOC = new OrdenCompra
                    {
                        CodigoOC = reader.GetInt32("CodigoOC")
                    },
                    Fecha = reader.GetDateTime("Fecha"),
                    Total = reader.GetDecimal("Total"),
                };
            }
            if (entity == null) return null;
            return new FacturaMapper().EntityToDto_FacLista(entity);
        }
    }
}