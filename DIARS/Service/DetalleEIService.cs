using DIARS.Controllers.Dto.DetalleEI;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Mapping;
using DIARS.Models;
using FluentValidation;
using MySql.Data.MySqlClient;
using System.Data;

namespace DIARS.Service
{
    public class DetalleEIService
    {
        private readonly MySQLDatabase _db;
        private readonly IValidator<DEIAgregaDto> _validator;

        public DetalleEIService(MySQLDatabase db, IValidator<DEIAgregaDto> validator)
        {
            _db = db;
            _validator = validator;
        }

        public List<DEIListaDto> ListarDetalleEI()
        {
            List<DetalleEI> lista = new();

            using var connection = _db.GetConnection();
            connection.Open();

            using var command = new MySqlCommand("CALL SP_DetalleEvaluacionInterna_Lista()", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new DetalleEI
                {
                    DetalleEvaluacionInternaID = reader.GetInt32("DetalleEvaluacionInternaID"),
                    EICodigo = new EvaluacionInterna
                    {
                        CodigoEI = reader.GetInt32("CodigoEI")
                    },
                    MecanicoEI = new Mecanico
                    {
                        Nombre = reader.GetString("Nombre")
                    },
                    //FechaRegistro = reader.GetDateTime("Fecha")
                });
            }

            var mapper = new DetalleEIMapper();
            return lista.Select(e => mapper.EntityToDto_DEILista(e)).ToList();
        }

        public ResponseDto<bool> InsertarDetalleEI(DEIAgregaDto dto)
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

            var mapper = new DetalleEIMapper();
            var entity = mapper.DtoToEntity_DEIAgregar(dto);

            try
            {
                using var connection = _db.GetConnection();
                connection.Open();

                using var command = new MySqlCommand("SP_DetalleEvaluacionInterna_Crea", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("p_EICodigo", entity.EICodigo.CodigoEI);
                command.Parameters.AddWithValue("p_MecanicoNombre", entity.MecanicoEI.Nombre);

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

        public DEIListaDto ObtenerDetalleEI(int id)
        {
            DetalleEI entity = null;

            using var connection = _db.GetConnection();
            connection.Open();

            using var command = new MySqlCommand("SP_DetalleEvaluacionInterna_ObtenPorId", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("p_DetalleID", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                entity = new DetalleEI
                {
                    DetalleEvaluacionInternaID = reader.GetInt32("DetalleEvaluacionInternaID"),
                    EICodigo = new EvaluacionInterna
                    {
                        CodigoEI = reader.GetInt32("CodigoEI")
                    },
                    MecanicoEI = new Mecanico
                    {
                        Nombre = reader.GetString("Nombre")
                    },
                    //FechaRegistro = reader.GetDateTime("Fecha")
                };
            }
            if (entity == null) return null;
            return new DetalleEIMapper().EntityToDto_DEILista(entity);
        }
    }
}