using DIARS.Controllers.Dto.Categoria;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Mapping;
using DIARS.Models;
using FluentValidation;
using MySql.Data.MySqlClient;
using System.Data;
using DIARS.Controllers.Dto.Especialidad;

namespace DIARS.Service
{
    public class EspecialidadService
    {
        private readonly MySQLDatabase _connectionString;
        private readonly IValidator<EspeActuDto> _busactuValidator;
        public EspecialidadService(MySQLDatabase connectionString, IValidator<EspeActuDto> busactuValidator)
        {
            _connectionString = connectionString;
            _busactuValidator = busactuValidator;
        }

        public List<EspeListaDto> ListarEspecialidad()
        {
            List<Especialidad> listaBus = new List<Especialidad>();

            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("CALL SP_Especialidad_Lista()", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaBus.Add(new Especialidad
                            {
                                CodigoS = reader.GetInt32("CodigoS"),
                                NombreS = reader.GetString("NombreS"),
                                Descripcion = reader.GetString("Descripcion"),
                                EstadoE = reader.GetBoolean("EstadoE")
                            });
                        }
                    }
                }
            }
            var busMapper = new EspecialidadMapper();
            return listaBus.Select(persona => busMapper.EntityToDto_EspecialidadLista(persona)).ToList();
        }

        public ResponseDto<bool> InsertarEspecialidad(EspeAgregaDto personaDto)
        {
            var response = new ResponseDto<bool>();

            try
            {
                var mapper = new EspecialidadMapper();
                var bus = mapper.DtoToEntity_EspecialidadAgregar(personaDto);

                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_Especialidad_Crea", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        command.Parameters.AddWithValue("p_NombreC", bus.NombreS);
                        command.Parameters.AddWithValue("p_Descripcion", bus.Descripcion);

                        // Parámetro de salida para capturar el mensaje
                        var mensajeParam = new MySqlParameter("p_Mensaje", MySqlDbType.VarChar, 255);
                        mensajeParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(mensajeParam);

                        command.ExecuteNonQuery();

                        // Capturar el mensaje de la base de datos
                        string mensaje = mensajeParam.Value.ToString();
                        response.MensajeError = mensaje;
                        response.EjecucionExitosa = mensaje.Contains("exitosa");
                        response.Data = response.EjecucionExitosa;
                    }
                }
            }
            catch (MySqlException ex)
            {
                response.EjecucionExitosa = false;
                response.MensajeError = "Error al insertar persona: " + ex.Message;
                response.Data = false;
            }

            return response;
        }

        public ResponseDto<bool> ActualizarEspecialidad(EspeActuDto personaDto)
        {
            var response = new ResponseDto<bool>();
            var mapper = new EspecialidadMapper();
            var persona = mapper.DtoToEntity_EspecialidadActualizar(personaDto);

            try
            {
                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_Especialidad_Actualiza", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("p_CodigoS", persona.CodigoS);
                        command.Parameters.AddWithValue("p_NombreS", persona.NombreS);
                        command.Parameters.AddWithValue("p_Descripcion", persona.Descripcion);
                        command.Parameters.AddWithValue("p_EstadoE", persona.EstadoE);

                        // Parámetro de salida para recibir el mensaje
                        var mensajeOutput = new MySqlParameter("p_Mensaje", MySqlDbType.VarChar, 255)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(mensajeOutput);

                        int filasAfectadas = command.ExecuteNonQuery();

                        response.EjecucionExitosa = filasAfectadas > 0;
                        response.Data = filasAfectadas > 0;
                        response.MensajeError = mensajeOutput.Value.ToString();
                    }
                }
            }
            catch (MySqlException ex)
            {
                response.EjecucionExitosa = false;
                response.MensajeError = "Error al actualizar persona: " + ex.Message;
                response.Data = false;
            }

            return response;
        }

        public EspeListaDto GetEspecialidadId(int id)
        {
            Especialidad persona = null;


            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("SP_Especialidad_ObtenPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("p_CodigoS", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            persona = new Especialidad
                            {
                                CodigoS = reader.GetInt32("CodigoS"),
                                NombreS = reader.GetString("NombreS"),
                                Descripcion = reader.GetString("Descripcion"),
                                EstadoE = reader.GetBoolean("EstadoE"),
                            };
                        }
                    }
                }
            }
            if (persona == null)
                return null;

            return new EspecialidadMapper().EntityToDto_EspecialidadLista(persona);
        }

        public bool InhabilitarEspecialidad(int id)
        {
            try
            {
                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_Especialidad_Inactiva", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("p_CodigoS", id);
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
