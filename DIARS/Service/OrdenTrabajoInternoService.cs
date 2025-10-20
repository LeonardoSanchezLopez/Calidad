using DIARS.Controllers.Dto.Mecanico;
using DIARS.Controllers.Dto.OrdenTrabajoExterno;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Mapping;
using DIARS.Models;
using FluentValidation;
using MySql.Data.MySqlClient;
using System.Data;
using DIARS.Controllers.Dto.OrdenTrabajoInterno;

namespace DIARS.Service
{
    public class OrdenTrabajoInternoService
    {
        private readonly MySQLDatabase _connectionString;
        private readonly IValidator<OTIAgregaDto> _busactuValidator;
        public OrdenTrabajoInternoService(MySQLDatabase connectionString, IValidator<OTIAgregaDto> busactuValidator)
        {
            _connectionString = connectionString;
            _busactuValidator = busactuValidator;
        }

        public List<OTIListaDto> ListarOrdenTrabajoInterno()
        {
            List<OrdenTrabajoInterno> listaBus = new List<OrdenTrabajoInterno>();

            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("CALL SP_OrdenTI_Lista()", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaBus.Add(new OrdenTrabajoInterno
                            {
                                CodigoTI = reader.GetInt32("CodigoTI"),
                                BusTI = new Bus
                                {
                                    NPlaca = reader.GetString("NPlaca"),
                                },
                                Fecha = reader.GetDateTime("Fecha"),
                                MecanicoTI = new Mecanico
                                {
                                    Nombre = reader.GetString("Nombre"),
                                },
                                Estado = reader.GetBoolean("Estado")
                            });
                        }
                    }
                }
            }
            var busMapper = new OrdenTrabajoInternoMapper();
            return listaBus.Select(persona => busMapper.EntityToDto_OTILista(persona)).ToList();
        }

        public ResponseDto<bool> InsertarOrdenTrabajoInterno(OTIAgregaDto personaDto)
        {
            var response = new ResponseDto<bool>();

            try
            {
                var mapper = new OrdenTrabajoInternoMapper();
                var bus = mapper.DtoToEntity_OTIAgregar(personaDto);

                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_OrdenTI_Crea", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("p_BusTI", bus.BusTI.NPlaca);
                        command.Parameters.AddWithValue("p_Fecha", bus.Fecha);
                        command.Parameters.AddWithValue("p_MecanicoTI", bus.MecanicoTI.Nombre);

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


        public OTIListaDto GetOrdenTrabajoInternoId(int id)
        {
            OrdenTrabajoInterno persona = null;


            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("SP_OrdenTI_ObtenPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("p_CodigoTI", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            persona = new OrdenTrabajoInterno
                            {
                                CodigoTI = reader.GetInt32("CodigoTI"),
                                BusTI = new Bus
                                {
                                    NPlaca = reader.GetString("NPlaca"),
                                },
                                Fecha = reader.GetDateTime("Fecha"),
                                MecanicoTI = new Mecanico
                                {
                                    Nombre = reader.GetString("Nombre"),
                                },
                                Estado = reader.GetBoolean("Estado")
                            };
                        }
                    }
                }
            }
            if (persona == null)
                return null;

            return new OrdenTrabajoInternoMapper().EntityToDto_OTILista(persona);
        }

        public bool InhabilitarOTI(int id)
        {
            try
            {
                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_OrdenTrabajoInterno_Inactiva", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("p_CodigoTI", id);
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