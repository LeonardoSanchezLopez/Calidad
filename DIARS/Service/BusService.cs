using FluentValidation;
using DIARS.Controllers.Dto.Bus;
using DIARS.Controllers.Mapping;
using DIARS.Models;
using MySql.Data.MySqlClient;
using DIARS.Controllers.Dto;
using System.Data;
namespace DIARS.Service
{
    public class BusService : IBusService
    {
        private readonly MySQLDatabase _connectionString;
        private readonly IValidator<BusActuDto> _busactuValidator;
        public BusService(MySQLDatabase connectionString, IValidator<BusActuDto> busactuValidator)
        {
            _connectionString = connectionString;
            _busactuValidator = busactuValidator;
        }

        public List<BusListaDto> ListarBus()
        {
            List<Bus> listaBus = new List<Bus>();

            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("CALL SP_Bus_Lista()", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaBus.Add(new Bus
                            {
                                BusB = reader.GetInt32("BusB"),
                                Marca = reader.GetString("Marca"),
                                Modelo = reader.GetString("Modelo"),
                                PisoBus = reader.GetString("PisoBus"),
                                NPlaca = reader.GetString("NPlaca"),
                                NChasis = reader.GetString("NChasis"),
                                NMotor = reader.GetString("NMotor"),
                                Capacidad = reader.GetInt32("Capacidad"),
                                TipoMotor = reader.GetString("TipoMotor"),
                                Combustible = reader.GetString("Combustible"),
                                FechaAdquisicion = reader.GetDateTime("FechaAdquisicion"),
                                Kilometraje = reader.GetInt32("Kilometraje"),
                                EstadoB = reader.GetBoolean("EstadoB")
                            });
                        }
                    }
                }
            }
            var busMapper = new BusMapper();
            return listaBus.Select(persona => busMapper.EntityToDto_BusLista(persona)).ToList();
        }

        public ResponseDto<bool> InsertarBus(BusAgregaDto personaDto)
        {
            var response = new ResponseDto<bool>();

            try
            {
                var mapper = new BusMapper();
                var bus = mapper.DtoToEntity_BusAgregar(personaDto);

                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_Bus_Crea", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("p_Marca", bus.Marca);
                        command.Parameters.AddWithValue("p_Modelo", bus.Modelo);
                        command.Parameters.AddWithValue("p_PisoBus", bus.PisoBus);
                        command.Parameters.AddWithValue("p_NPlaca", bus.NPlaca);
                        command.Parameters.AddWithValue("p_NChasis", bus.NChasis);
                        command.Parameters.AddWithValue("p_NMotor", bus.NMotor);
                        command.Parameters.AddWithValue("p_Capacidad", bus.Capacidad);
                        command.Parameters.AddWithValue("p_TipoMotor", bus.TipoMotor);
                        command.Parameters.AddWithValue("p_Combustible", bus.Combustible);
                        command.Parameters.AddWithValue("p_Kilometraje", bus.Kilometraje);
                        command.Parameters.AddWithValue("p_FechaAdquisicion", bus.FechaAdquisicion.ToString("yyyy-MM-dd"));

                        var mensajeParam = new MySqlParameter("p_Mensaje", MySqlDbType.VarChar, 255);
                        mensajeParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(mensajeParam);

                        command.ExecuteNonQuery();

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

        public ResponseDto<bool> ActualizarBus(BusActuDto personaDto)
        {
            var response = new ResponseDto<bool>();
            var mapper = new BusMapper();
            var persona = mapper.DtoToEntity_BusActualizar(personaDto);

            try
            {
                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_Bus_Actualiza", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("p_BusB", persona.BusB);
                        command.Parameters.AddWithValue("p_Marca", persona.Marca);
                        command.Parameters.AddWithValue("p_Modelo", persona.Modelo);
                        command.Parameters.AddWithValue("p_PisoBus", persona.PisoBus);
                        command.Parameters.AddWithValue("p_NPlaca", persona.NPlaca);
                        command.Parameters.AddWithValue("p_NChasis", persona.NChasis);
                        command.Parameters.AddWithValue("p_NMotor", persona.NMotor);
                        command.Parameters.AddWithValue("p_Capacidad", persona.Capacidad);
                        command.Parameters.AddWithValue("p_TipoMotor", persona.TipoMotor);
                        command.Parameters.AddWithValue("p_Combustible", persona.Combustible);
                        command.Parameters.AddWithValue("p_FechaAdquisicion", persona.FechaAdquisicion);
                        command.Parameters.AddWithValue("p_Kilometraje", persona.Kilometraje);
                        command.Parameters.AddWithValue("p_EstadoB", persona.EstadoB);

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

        public BusListaDto GetBusId(int id)
        {
            Bus persona = null;
            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("SP_Bus_ObtenPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("p_BusB", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            persona = new Bus
                            {
                                BusB = reader.GetInt32("BusB"),
                                Marca = reader.GetString("Marca"),
                                Modelo = reader.GetString("Modelo"),
                                PisoBus = reader.GetString("PisoBus"),
                                NPlaca = reader.GetString("NPlaca"),
                                NChasis = reader.GetString("NChasis"),
                                NMotor = reader.GetString("NMotor"),
                                Capacidad = reader.GetInt32("Capacidad"),
                                TipoMotor = reader.GetString("TipoMotor"),
                                Combustible = reader.GetString("Combustible"),
                                FechaAdquisicion = reader.GetDateTime("FechaAdquisicion"),
                                Kilometraje = reader.GetInt32("Kilometraje"),
                                EstadoB = reader.GetBoolean("EstadoB"),
                            };
                        }
                    }
                }
            }
            if (persona == null)
                return null;

            return new BusMapper().EntityToDto_BusLista(persona);
        }

        public bool InhabilitarBus(int id)
        {
            try
            {
                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_Bus_Inactiva", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("p_BusB", id);
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