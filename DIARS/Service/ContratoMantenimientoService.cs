using DIARS.Controllers.Dto.MarcaRepuesto;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Mapping;
using DIARS.Models;
using FluentValidation;
using MySql.Data.MySqlClient;
using System.Data;
using DIARS.Controllers.Dto.ContratoMantenimiento;

namespace DIARS.Service
{
    public class ContratoMantenimientoService
    {
        private readonly MySQLDatabase _connectionString;
        private readonly IValidator<CMAgregaDto> _busactuValidator;
        public ContratoMantenimientoService (MySQLDatabase connectionString, IValidator<CMAgregaDto> busactuValidator)
        {
            _connectionString = connectionString;
            _busactuValidator = busactuValidator;
        }

        public List<CMListaDto> ListarContratoMantenimiento()
        {
            List<ContratoMantenimiento> listaBus = new List<ContratoMantenimiento>();

            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("CALL SP_ContratoMantenimiento_Lista()", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaBus.Add(new ContratoMantenimiento
                            {
                                CodigoCM = reader.GetInt32("CodigoCM"),
                                BusCM = new Bus
                                {
                                    NPlaca = reader.GetString("Placa"),
                                },
                                Fecha = reader.GetDateTime("Fecha"),
                                ProveedorCM = new Proveedor
                                {
                                    Nombre = reader.GetString("Nombre"),
                                },
                                Descripcion = reader.GetString("Descripcion"),
                                Costo = reader.GetDecimal("Costo"),
                                Estado = reader.GetBoolean("Estado")
                            });
                        }
                    }
                }
            }
            var busMapper = new ContratoMantenimientoMapper();
            return listaBus.Select(persona => busMapper.EntityToDto_CMLista(persona)).ToList();
        }
                                

        public ResponseDto<bool> InsertarContratoMantenimiento(CMAgregaDto personaDto)
        {
            var response = new ResponseDto<bool>();

            try
            {
                var mapper = new ContratoMantenimientoMapper();
                var bus = mapper.DtoToEntity_CMAgregar(personaDto);

                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_ContratoMantenimiento_Crea", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                       // command.Parameters.AddWithValue("p_CodigoCM", bus.CodigoCM);
                        command.Parameters.AddWithValue("p_BusPlaca", bus.BusCM.NPlaca);
                        command.Parameters.AddWithValue("p_Fecha", bus.Fecha);
                        command.Parameters.AddWithValue("p_ProveedorNombre", bus.ProveedorCM.Nombre);
                        command.Parameters.AddWithValue("p_Descripcion", bus.Descripcion);
                        command.Parameters.AddWithValue("p_Costo", bus.Costo);
                        

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

        public CMListaDto GetContratoMantenimientoId(int id)
        {
            ContratoMantenimiento persona = null;


            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("SP_ContratoMantenimiento_ObtenPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("p_CodigoCM", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            persona = new ContratoMantenimiento
                            {
                                CodigoCM = reader.GetInt32("CodigoCM"),
                                BusCM = new Bus
                                {
                                    NPlaca = reader.GetString("NPlaca"),
                                },
                                Fecha = reader.GetDateTime("Fecha"),
                                ProveedorCM = new Proveedor
                                {
                                    Nombre = reader.GetString("Nombre"),
                                },
                                Descripcion = reader.GetString("Descripcion"),
                                Costo = reader.GetDecimal("Costo"),
                                Estado = reader.GetBoolean("Estado")
                            };
                        }
                    }
                }
            }
            if (persona == null)
                return null;

            return new ContratoMantenimientoMapper().EntityToDto_CMLista(persona);
        }

        public bool InhabilitarContratoMantenimiento(int id)
        {
            try
            {
                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_ContratoMantenimiento_Inactiva", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("p_CodigoCM", id);
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