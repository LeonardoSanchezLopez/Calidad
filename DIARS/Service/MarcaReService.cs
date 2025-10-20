using DIARS.Controllers.Dto.Bus;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Mapping;
using DIARS.Models;
using FluentValidation;
using MySql.Data.MySqlClient;
using System.Data;
using DIARS.Controllers.Dto.MarcaRepuesto;
using DIARS.Controllers.Dto.Categoria;

namespace DIARS.Service
{
    public class MarcaReService
    {
        private readonly MySQLDatabase _connectionString;
        private readonly IValidator<MarcaActuDto> _busactuValidator;
        public MarcaReService(MySQLDatabase connectionString, IValidator<MarcaActuDto> busactuValidator)
        {
            _connectionString = connectionString;
            _busactuValidator = busactuValidator;
        }

        public List<MarcaListaDto> ListarMarca()
        {
            List<MarcaRepuesto> listaBus = new List<MarcaRepuesto>();

            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("CALL SP_MarcaRepuesto_Lista()", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaBus.Add(new MarcaRepuesto
                            {
                                CodigoMR = reader.GetInt32("CodigoMR"),
                                Descripcion = reader.GetString("Descripcion"),
                                ProveedorMR = new Proveedor
                                {
                                    Nombre = reader.GetString("Nombre"),
                                },
                                EstadoM = reader.GetBoolean("EstadoM")
                            });
                        }
                    }
                }
            }
            var busMapper = new MarcaMapper();
            return listaBus.Select(persona => busMapper.EntityToDto_MarcaRepuestoLista(persona)).ToList();
        }

        public ResponseDto<bool> InsertarMarca(MarcaAgregaDto personaDto)
        {
            var response = new ResponseDto<bool>();

            try
            {
                var mapper = new MarcaMapper();
                var bus = mapper.DtoToEntity_MarcaRepuestoAgregar(personaDto);

                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_MarcaRepuesto_Crea", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        command.Parameters.AddWithValue("p_Descripcion", bus.Descripcion);
                        command.Parameters.AddWithValue("p_ProveedorMR", bus.ProveedorMR.Nombre);

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

        public ResponseDto<bool> ActualizarMarca(MarcaActuDto personaDto)
        {
            var response = new ResponseDto<bool>();
            var mapper = new MarcaMapper();
            var persona = mapper.DtoToEntity_MarcaRepuestoActualizar(personaDto);

            try
            {
                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_MarcaRepuesto_Actualiza", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("p_CodigoMR", persona.CodigoMR);
                        command.Parameters.AddWithValue("p_ProveedorMR", persona.ProveedorMR.Nombre);
                        command.Parameters.AddWithValue("p_Descripcion", persona.Descripcion);
                        command.Parameters.AddWithValue("p_EstadoM", persona.EstadoM);

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

        public MarcaListaDto GetMarcaId(int id)
        {
            MarcaRepuesto persona = null;


            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("SP_MarcaRepuesto_ObtenPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("p_CodigoMR", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            persona = new MarcaRepuesto
                            {
                                CodigoMR = reader.GetInt32("CodigoMR"),
                                ProveedorMR = new Proveedor
                                {
                                    Nombre = reader.GetString("Nombre"),
                                },
                                Descripcion = reader.GetString("Descripcion"),
                                EstadoM = reader.GetBoolean("EstadoM"),
                            };
                        }
                    }
                }
            }
            if (persona == null)
                return null;

            return new MarcaMapper().EntityToDto_MarcaRepuestoLista(persona);
        }

        public bool InhabilitarMarca(int id)
        {
            try
            {
                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_MarcaRepuesto_Inactiva", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("p_CodigoMR", id);
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
