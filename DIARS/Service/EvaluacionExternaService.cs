using DIARS.Controllers.Dto.DetalleOTI;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Mapping;
using DIARS.Models;
using FluentValidation;
using MySql.Data.MySqlClient;
using System.Data;
using DIARS.Controllers.Dto.EvaluacionExterna;

namespace DIARS.Service
{
    public class EvaluacionExternaService
    {
        private readonly MySQLDatabase _db;
        private readonly IValidator<EvaExAgregaDto> _validator;

        public EvaluacionExternaService(MySQLDatabase db, IValidator<EvaExAgregaDto> validator)
        {
            _db = db;
            _validator = validator;
        }

        public List<EvaExListaDto> ListarEvaluacionExterna()
        {
            List<EvaluacionExterna> lista = new();

            using var connection = _db.GetConnection();
            connection.Open();

            using var command = new MySqlCommand("CALL SP_EvaluacionExterna_Lista()", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new EvaluacionExterna
                {
                    CodigoEE = reader.GetInt32("CodigoEE"),
                    CodigoBus = new Bus
                    {
                        NPlaca = reader.GetString("NPlaca")
                    },
                    ProveedorEE = new Proveedor
                    {
                        Nombre = reader.GetString("Nombre")
                    },
                    Fecha = reader.GetDateTime("Fecha"),
                    TECodigo = new OrdenTrabajoExterno
                    {
                        CodigoTE = reader.GetInt32("CodigoTE")
                    },
                    Estado = reader.GetBoolean("Estado"),
                });
            }

            var mapper = new EvaluacionExternoMapper();
            return lista.Select(e => mapper.EntityToDto_EvaExLista(e)).ToList();
        }

        public ResponseDto<bool> InsertarEvaluacionExterno(EvaExAgregaDto dto)
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

            var mapper = new EvaluacionExternoMapper();
            var entity = mapper.DtoToEntity_EvaExAgregar(dto);

            try
            {
                using var connection = _db.GetConnection();
                connection.Open();

                using var command = new MySqlCommand("SP_EvaluacionExterna_Crea", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("p_CodigoBus", entity.CodigoBus.NPlaca);
                command.Parameters.AddWithValue("p_ProveedorEE", entity.ProveedorEE.Nombre);
                command.Parameters.AddWithValue("p_Fecha", entity.Fecha);
                command.Parameters.AddWithValue("p_TECodigo", entity.TECodigo.CodigoTE);

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

        public EvaExListaDto ObtenerEvaluacionExternoPorId(int id)
        {
            EvaluacionExterna entity = null;

            using var connection = _db.GetConnection();
            connection.Open();

            using var command = new MySqlCommand("SP_EvaluacionExterna_ObtenPorId", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("p_CodigoEE", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                entity = new EvaluacionExterna
                {
                    CodigoEE = reader.GetInt32("CodigoEE"),
                    CodigoBus = new Bus
                    {
                        NPlaca = reader.GetString("NPlaca")
                    },
                    ProveedorEE = new Proveedor
                    {
                        Nombre = reader.GetString("Nombre")
                    },
                    Fecha = reader.GetDateTime("Fecha"),
                    TECodigo = new OrdenTrabajoExterno
                    {
                        CodigoTE = reader.GetInt32("CodigoTE")
                    },
                    Estado = reader.GetBoolean("Estado"),
                };
            }
            if (entity == null) return null;
            return new EvaluacionExternoMapper().EntityToDto_EvaExLista(entity);
        }

        public bool InhabilitarEvaluacionExterna(int id)
        {
            try
            {
                using (var connection = _db.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_EvaluacionExterna_Inactiva", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("p_CodigoEE", id);
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
