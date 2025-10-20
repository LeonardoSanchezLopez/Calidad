using DIARS.Controllers.Dto.DetalleOrdenPedido;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Mapping;
using DIARS.Models;
using FluentValidation;
using MySql.Data.MySqlClient;
using System.Data;
using DIARS.Controllers.Dto.DetalleOTE;

namespace DIARS.Service
{
    public class DetalleOTEService
    {
        private readonly MySQLDatabase _db;
        private readonly IValidator<DOTEAgregaDto> _validator;

        public DetalleOTEService(MySQLDatabase db, IValidator<DOTEAgregaDto> validator)
        {
            _db = db;
            _validator = validator;
        }

        public List<DOTEListaDto> ListarDetalleOTE()
        {
            List<DetalleOTE> lista = new();

            using var connection = _db.GetConnection();
            connection.Open();

            using var command = new MySqlCommand("CALL SP_DetalleOTE_Lista()", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new DetalleOTE
                {
                    DetalleOTEID = reader.GetInt32("DetalleOTEID"),
                    TECodigo = new OrdenTrabajoExterno
                    {
                        CodigoTE = reader.GetInt32("CodigoTE")
                    },
                    CodigoRepu = new Repuesto
                    {
                        NombreR = reader.GetString("NombreR")
                    },
                    Parte = reader.GetString("Parte"),
                    Pieza = reader.GetString("Pieza"),
                    Cantidad = reader.GetInt32("Cantidad"),
                });
            }

            var mapper = new DetalleOTEMapper();
            return lista.Select(e => mapper.EntityToDto_DOTELista(e)).ToList();
        }

        public ResponseDto<bool> InsertarDetalleOTE(DOTEAgregaDto dto)
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

            var mapper = new DetalleOTEMapper();
            var entity = mapper.DtoToEntity_DOTEAgregar(dto);

            try
            {
                using var connection = _db.GetConnection();
                connection.Open();

                using var command = new MySqlCommand("SP_DetalleOTE_Crea", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("p_TECodigo", entity.TECodigo.CodigoTE);
                command.Parameters.AddWithValue("p_CodigoRepu", entity.CodigoRepu.NombreR);
                command.Parameters.AddWithValue("p_Parte", entity.Parte);
                command.Parameters.AddWithValue("p_Pieza", entity.Pieza);
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

        public DOTEListaDto ObtenerDetalleOTEPorId(int id)
        {
            DetalleOTE entity = null;

            using var connection = _db.GetConnection();
            connection.Open();

            using var command = new MySqlCommand("SP_DetalleOTE_ObtenPorId", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("p_DetalleOTEID", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                entity = new DetalleOTE
                {
                    DetalleOTEID = reader.GetInt32("DetalleOTEID"),
                    TECodigo = new OrdenTrabajoExterno
                    {
                        CodigoTE = reader.GetInt32("CodigoTE")
                    },
                    CodigoRepu = new Repuesto
                    {
                        NombreR = reader.GetString("NombreR")
                    },
                    Parte = reader.GetString("Parte"),
                    Pieza = reader.GetString("Pieza"),
                    Cantidad = reader.GetInt32("Cantidad"),
                };
            }
            if (entity == null) return null;
            return new DetalleOTEMapper().EntityToDto_DOTELista(entity);
        }
    }
}
