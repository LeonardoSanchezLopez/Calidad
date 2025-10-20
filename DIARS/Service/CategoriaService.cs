using DIARS.Controllers.Dto.Bus;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Mapping;
using DIARS.Models;
using FluentValidation;
using MySql.Data.MySqlClient;
using System.Data;
using DIARS.Controllers.Dto.Categoria;

namespace DIARS.Service
{
    public class CategoriaService
    {
        private readonly MySQLDatabase _connectionString;
        private readonly IValidator<CatActuDto> _busactuValidator;
        public CategoriaService(MySQLDatabase connectionString, IValidator<CatActuDto> busactuValidator)
        {
            _connectionString = connectionString;
            _busactuValidator = busactuValidator;
        }

        public List<CatListaDto> ListarCategoria()
        {
            List<Categoria> listaBus = new List<Categoria>();

            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("CALL SP_Categoria_Lista()", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaBus.Add(new Categoria
                            {
                                CodigoC = reader.GetInt32("CodigoC"),
                                NombreC = reader.GetString("NombreC"),
                                Descripcion = reader.GetString("Descripcion"),
                                EstadoC = reader.GetBoolean("EstadoC")
                            });
                        }
                    }
                }
            }
            var busMapper = new CategoriaMapper();
            return listaBus.Select(persona => busMapper.EntityToDto_CategoriaLista(persona)).ToList();
        }

        public ResponseDto<bool> InsertarCategoria(CatAgregaDto personaDto)
        {
            var response = new ResponseDto<bool>();

            try
            {
                var mapper = new CategoriaMapper();
                var bus = mapper.DtoToEntity_CategoriaAgregar(personaDto);

                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_Categoria_Crea", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        command.Parameters.AddWithValue("p_NombreC", bus.NombreC);
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

        public ResponseDto<bool> ActualizarCategoria(CatActuDto personaDto)
        {
            var response = new ResponseDto<bool>();
            var mapper = new CategoriaMapper();
            var persona = mapper.DtoToEntity_CategoriaActualizar(personaDto);

            try
            {
                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_Categoria_Actualiza", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("p_CodigoC", persona.CodigoC);
                        command.Parameters.AddWithValue("p_NombreC", persona.NombreC);
                        command.Parameters.AddWithValue("p_Descripcion", persona.Descripcion);
                        command.Parameters.AddWithValue("p_EstadoC", persona.EstadoC);

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

        public CatListaDto GetCategoriaId(int id)
        {
            Categoria persona = null;


            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("SP_Categoria_ObtenPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("p_CodigoC", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            persona = new Categoria
                            {
                                CodigoC = reader.GetInt32("CodigoC"),
                                NombreC = reader.GetString("NombreC"),
                                Descripcion = reader.GetString("Descripcion"),
                                EstadoC = reader.GetBoolean("EstadoC"),
                            };
                        }
                    }
                }
            }
            if (persona == null)
                return null;

            return new CategoriaMapper().EntityToDto_CategoriaLista(persona);
        }

        public bool InhabilitarCategoria(int id)
        {
            try
            {
                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_Categoria_Inactiva", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("p_CodigoC", id);
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
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar SP_Categoria_Inactiva: " + ex.Message, ex);
            }
        }
    }
}