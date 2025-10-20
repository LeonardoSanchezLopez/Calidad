using DIARS.Controllers.Dto.EvaluacionExterna;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Mapping;
using DIARS.Models;
using FluentValidation;
using MySql.Data.MySqlClient;
using System.Data;
using DIARS.Controllers.Dto.EvaluacionInterna;

namespace DIARS.Service
{
    public class EvaluacionInternaService
    {
        private readonly MySQLDatabase _db;
        private readonly IValidator<EvaInAgregaDto> _validator;

        public EvaluacionInternaService(MySQLDatabase db, IValidator<EvaInAgregaDto> validator)
        {
            _db = db;
            _validator = validator;
        }

        public List<EvaInListaDto> ListarEvaluacionInterna()
        {
            List<EvaluacionInterna> lista = new();

            using var connection = _db.GetConnection();
            connection.Open();

            using var command = new MySqlCommand("CALL SP_EvaluacionInterna_Lista()", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new EvaluacionInterna
                {
                    CodigoEI = reader.GetInt32("CodigoEI"),
                    CodigoBus = new Bus
                    {
                        NPlaca = reader.GetString("NPlaca")
                    },
                    Fecha = reader.GetDateTime("Fecha"),
                    TICodigo = new OrdenTrabajoInterno
                    {
                        CodigoTI = reader.GetInt32("TICodigo")
                    },
                    Estado = reader.GetBoolean("Estado"),
                });
            }

            var mapper = new EvaluacionInternaMapper();
            return lista.Select(e => mapper.EntityToDto_EvaInLista(e)).ToList();
        }

        public ResponseDto<bool> InsertarEvaluacionInterno(EvaInAgregaDto dto)
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

            var mapper = new EvaluacionInternaMapper();
            var entity = mapper.DtoToEntity_EvaInAgregar(dto);

            try
            {
                using var connection = _db.GetConnection();
                connection.Open();

                using var command = new MySqlCommand("SP_EvaluacionInterna_Crea", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("p_Placa", entity.CodigoBus.NPlaca);
                command.Parameters.AddWithValue("p_Fecha", entity.Fecha);
                command.Parameters.AddWithValue("p_TICodigo", entity.TICodigo.CodigoTI);

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

        public EvaInListaDto ObtenerEvaluacionInternoPorId(int id)
        {
            EvaluacionInterna entity = null;

            using var connection = _db.GetConnection();
            connection.Open();

            using var command = new MySqlCommand("SP_EvaluacionInterna_ObtenPorId", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("p_CodigoEI", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                entity = new EvaluacionInterna
                {
                    CodigoEI = reader.GetInt32("CodigoEI"),
                    CodigoBus = new Bus
                    {
                        NPlaca = reader.GetString("NPlaca")
                    },
                    Fecha = reader.GetDateTime("Fecha"),
                    TICodigo = new OrdenTrabajoInterno
                    {
                        CodigoTI = reader.GetInt32("TICodigo")
                    },
                    Estado = reader.GetBoolean("Estado"),
                };
            }
            if (entity == null) return null;
            return new EvaluacionInternaMapper().EntityToDto_EvaInLista(entity);
        }

        public bool InhabilitarEvaluacionInterna(int id)
        {
            try
            {
                using (var connection = _db.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_EvaluacionInterna_Inactiva", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("p_CodigoEI", id);
                        var mensajeParam = new MySqlParameter("p_Mensaje", MySqlDbType.VarChar, 255)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(mensajeParam);
                        int rowsAffected = command.ExecuteNonQuery();
                        string mensaje = mensajeParam.Value?.ToString();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}