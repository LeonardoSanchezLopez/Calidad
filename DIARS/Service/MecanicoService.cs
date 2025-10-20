using DIARS.Controllers.Dto.MarcaRepuesto;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Mapping;
using DIARS.Models;
using FluentValidation;
using MySql.Data.MySqlClient;
using System.Data;
using DIARS.Controllers.Dto.Mecanico;

namespace DIARS.Service
{
    public class MecanicoService
    {
        private readonly MySQLDatabase _connectionString;
        private readonly IValidator<MecaActuDto> _busactuValidator;
        public MecanicoService(MySQLDatabase connectionString, IValidator<MecaActuDto> busactuValidator)
        {
            _connectionString = connectionString;
            _busactuValidator = busactuValidator;
        }

        public List<MecaListaDto> ListarMecanico()
        {
            List<Mecanico> listaBus = new List<Mecanico>();

            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("CALL SP_Mecanico_Lista()", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaBus.Add(new Mecanico
                            {
                                CodigoM = reader.GetInt32("CodigoM"),
                                EspecialidadM = new Especialidad
                                {
                                    NombreS = reader.GetString("NombreS"),
                                },
                                Nombre = reader.GetString("Nombre"),
                                DNI = reader.GetString("DNI"),
                                Domicilio = reader.GetString("Domicilio"),
                                Experiencia = reader.GetString("Experiencia"),
                                Telefono = reader.GetString("Telefono"),
                                Sueldo = reader.GetDecimal("Sueldo"),
                                Turno = reader.GetString("Turno"),
                                FechaContrato = reader.GetDateTime("FechaContrato"),
                                EstadoM = reader.GetBoolean("EstadoM")
                            });
                        }
                    }
                }
            }
            var busMapper = new MecanicoMapper();
            return listaBus.Select(persona => busMapper.EntityToDto_MecanicoLista(persona)).ToList();
        }

        public ResponseDto<bool> InsertarMecanico(MecaAgregaDto personaDto)
        {
            var response = new ResponseDto<bool>();

            try
            {
                var mapper = new MecanicoMapper();
                var bus = mapper.DtoToEntity_MecanicoAgregar(personaDto);

                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_Mecanico_Crea", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        //command.Parameters.AddWithValue("p_CodigoM", bus.CodigoM);
                        command.Parameters.AddWithValue("p_EspecialidadM", bus.EspecialidadM.NombreS);
                        command.Parameters.AddWithValue("p_Nombre", bus.Nombre);
                        command.Parameters.AddWithValue("p_DNI", bus.DNI);
                        command.Parameters.AddWithValue("p_Domicilio", bus.Domicilio);
                        command.Parameters.AddWithValue("p_Experiencia", bus.Experiencia);
                        command.Parameters.AddWithValue("p_Telefono", bus.Telefono);
                        command.Parameters.AddWithValue("p_Sueldo", bus.Sueldo);
                        command.Parameters.AddWithValue("p_Turno", bus.Turno);
                        command.Parameters.AddWithValue("p_FechaContrato", bus.FechaContrato);
                        //command.Parameters.AddWithValue("p_EstadoM", bus.EstadoM);

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

        public ResponseDto<bool> ActualizarMecanico(MecaActuDto personaDto)
        {
            var response = new ResponseDto<bool>();
            var mapper = new MecanicoMapper();
            var bus = mapper.DtoToEntity_MecanicoActualizar(personaDto);

            try
            {
                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_Mecanico_Actualiza", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("p_CodigoM", bus.CodigoM);
                        command.Parameters.AddWithValue("p_EspecialidadM", bus.EspecialidadM.NombreS);
                        command.Parameters.AddWithValue("p_Nombre", bus.Nombre);
                        command.Parameters.AddWithValue("p_DNI", bus.DNI);
                        command.Parameters.AddWithValue("p_Domicilio", bus.Domicilio);
                        command.Parameters.AddWithValue("p_Experiencia", bus.Experiencia);
                        command.Parameters.AddWithValue("p_Telefono", bus.Telefono);
                        command.Parameters.AddWithValue("p_Sueldo", bus.Sueldo);
                        command.Parameters.AddWithValue("p_Turno", bus.Turno);
                        command.Parameters.AddWithValue("p_FechaContrato", bus.FechaContrato);
                        command.Parameters.AddWithValue("p_EstadoM", bus.EstadoM);

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

        public MecaListaDto GetMecanicoId(int id)
        {
            Mecanico persona = null;


            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("SP_Mecanico_ObtenPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("p_CodigoM", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            persona = new Mecanico
                            {
                                CodigoM = reader.GetInt32("CodigoM"),
                                EspecialidadM = new Especialidad
                                {
                                    NombreS = reader.GetString("NombreS"),
                                },
                                Nombre = reader.GetString("Nombre"),
                                DNI = reader.GetString("DNI"),
                                Domicilio = reader.GetString("Domicilio"),
                                Experiencia = reader.GetString("Experiencia"),
                                Telefono = reader.GetString("Telefono"),
                                Sueldo = reader.GetDecimal("Sueldo"),
                                Turno = reader.GetString("Turno"),
                                FechaContrato = reader.GetDateTime("FechaContrato"),
                                EstadoM = reader.GetBoolean("EstadoM")
                            };
                        }
                    }
                }
            }
            if (persona == null)
                return null;

            return new MecanicoMapper().EntityToDto_MecanicoLista(persona);
        }

        public bool InhabilitarMecanico(int id)
        {
            try
            {
                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_Mecanico_Inactiva", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("p_CodigoM", id);
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
