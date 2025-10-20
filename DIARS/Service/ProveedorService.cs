using DIARS.Controllers.Dto.Bus;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Mapping;
using DIARS.Models;
using FluentValidation;
using MySql.Data.MySqlClient;
using System.Data;
using DIARS.Controllers.Dto.Proveedor;

namespace DIARS.Service
{
    public class ProveedorService
    {
        private readonly MySQLDatabase _connectionString;
        private readonly IValidator<ProActuDto> _busactuValidator;
        public ProveedorService(MySQLDatabase connectionString, IValidator<ProActuDto> busactuValidator)
        {
            _connectionString = connectionString;
            _busactuValidator = busactuValidator;
        }

        public List<ProListaDto> ListarProveedor()
        {
            List<Proveedor> listaBus = new List<Proveedor>();

            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("CALL SP_Proveedor_Lista()", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaBus.Add(new Proveedor
                            {
                                CodigoP = reader.GetInt32("CodigoP"),
                                Nombre = reader.GetString("Nombre"),
                                Direccion = reader.GetString("Direccion"),
                                Telefono = reader.GetString("Telefono"),
                                Correo = reader.GetString("Correo"),
                                EstadoP = reader.GetBoolean("EstadoP")
                            });
                        }
                    }
                }
            }
            var busMapper = new ProveedorMapper();
            return listaBus.Select(persona => busMapper.EntityToDto_ProveedorLista(persona)).ToList();
        }

        public ResponseDto<bool> InsertarProveedor(ProAgregaDto personaDto)
        {
            var response = new ResponseDto<bool>();

            try
            {
                var mapper = new ProveedorMapper();
                var bus = mapper.DtoToEntity_ProveedorAgregar(personaDto);

                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_Proveedor_Crea", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        command.Parameters.AddWithValue("p_Nombre", bus.Nombre);
                        command.Parameters.AddWithValue("p_Direccion", bus.Direccion);
                        command.Parameters.AddWithValue("p_Telefono", bus.Telefono);
                        command.Parameters.AddWithValue("p_Correo", bus.Correo);

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

        public ResponseDto<bool> ActualizarProveedor(ProActuDto personaDto)
        {
            var response = new ResponseDto<bool>();
            var mapper = new ProveedorMapper();
            var persona = mapper.DtoToEntity_ProveedorActualizar(personaDto);

            try
            {
                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_Proveedor_Actualiza", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("p_CodigoP", persona.CodigoP);
                        command.Parameters.AddWithValue("p_Nombre", persona.Nombre);
                        command.Parameters.AddWithValue("p_Direccion", persona.Direccion);
                        command.Parameters.AddWithValue("p_Telefono", persona.Telefono);
                        command.Parameters.AddWithValue("p_Correo", persona.Correo);
                        command.Parameters.AddWithValue("p_EstadoP", persona.EstadoP);

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

        public ProListaDto GetProveedorId(int id)
        {
            Proveedor persona = null;


            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("SP_Proveedor_ObtenPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("p_CodigoP", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            persona = new Proveedor
                            {
                                CodigoP = reader.GetInt32("CodigoP"),
                                Nombre = reader.GetString("Nombre"),
                                Direccion = reader.GetString("Direccion"),
                                Telefono = reader.GetString("Telefono"),
                                Correo = reader.GetString("Correo"),
                                EstadoP = reader.GetBoolean("EstadoP")
                            };
                        }
                    }
                }
            }
            if (persona == null)
                return null;

            return new ProveedorMapper().EntityToDto_ProveedorLista(persona);
        }

        public bool InhabilitarProveedor(int id)
        {
            try
            {
                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_Proveedor_Inactiva", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("p_CodigoP", id);
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