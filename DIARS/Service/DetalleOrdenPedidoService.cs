using DIARS.Controllers.Dto.DetalleOrdenPedido;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Mapping;
using DIARS.Models;
using FluentValidation;
using MySql.Data.MySqlClient;
using System.Data;

namespace DIARS.Service
{
    public class DetalleOrdenPedidoService : IDetalleOrdenPedidoService
    {
        private readonly MySQLDatabase _db;
        private readonly IValidator<DOrPeAgregaDto> _validator;

        public DetalleOrdenPedidoService(MySQLDatabase db, IValidator<DOrPeAgregaDto> validator)
        {
            _db = db;
            _validator = validator;
        }

        public List<DOrPeListaDto> ListarDetalleOrdenPedido()
        {
            List<DetalleOrdenPedido> lista = new();

            using var connection = _db.GetConnection();
            connection.Open();

            using var command = new MySqlCommand("CALL SP_DetalleOrdenPedido_Lista()", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new DetalleOrdenPedido
                {
                    DetalleOrdenPedidoID = reader.GetInt32("DetalleOrdenPedidoID"),
                    OPCodigo = new OrdenPedido
                    {
                        CodigoOP = reader.GetInt32("CodigoOP")
                    },
                    CodigoRepu = new Repuesto
                    {
                        NombreR = reader.GetString("NombreR")
                    },
                    Cantidad = reader.GetInt32("Cantidad")
                });
            }

            var mapper = new DetalleOrdenPedidoMapper();
            return lista.Select(e => mapper.EntityToDto_DOrPeLista(e)).ToList();
        }

        public ResponseDto<bool> InsertarDetalleOrdenPedido(DOrPeAgregaDto dto)
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

            var mapper = new DetalleOrdenPedidoMapper();
            var entity = mapper.DtoToEntity_DOrPeAgregar(dto);

            try
            {
                using var connection = _db.GetConnection();
                connection.Open();

                using var command = new MySqlCommand("SP_DetalleOrdenPedido_Crea", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("p_OPCodigo", entity.OPCodigo.CodigoOP);
                command.Parameters.AddWithValue("p_CodigoRepu", entity.CodigoRepu.NombreR);
                command.Parameters.AddWithValue("p_Cantidad", entity.Cantidad);

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

        public DOrPeListaDto ObtenerDetalleOrdenPedido(int id)
        {
            DetalleOrdenPedido entity = null;

            using var connection = _db.GetConnection();
            connection.Open();

            using var command = new MySqlCommand("SP_DetalleOrdenPedido_ObtenPorId", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("p_DetalleOrdenPedidoID", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                entity = new DetalleOrdenPedido
                {
                    DetalleOrdenPedidoID = reader.GetInt32("DetalleOrdenPedidoID"),
                    OPCodigo = new OrdenPedido
                    {
                        CodigoOP = reader.GetInt32("CodigoOP")
                    },
                    CodigoRepu = new Repuesto
                    {
                        NombreR = reader.GetString("NombreR")
                    },
                    Cantidad = reader.GetInt32("Cantidad")
                };
            }
            if (entity == null) return null;
            return new DetalleOrdenPedidoMapper().EntityToDto_DOrPeLista(entity);
        }
    }
}
