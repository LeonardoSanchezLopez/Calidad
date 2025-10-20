using DIARS.Controllers.Dto.Mecanico;
using DIARS.Controllers.Dto.NotaIngresoRepuestos;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Mapping;
using DIARS.Models;
using FluentValidation;
using MySql.Data.MySqlClient;
using System.Data;
using DIARS.Controllers.Dto.NotaSalidaRepuestos;

namespace DIARS.Service
{
    public class NotaSalidaRepuestoService
    {
        private readonly MySQLDatabase _connectionString;
        private readonly IValidator<NSRAgregaDto> _busactuValidator;
        public NotaSalidaRepuestoService(MySQLDatabase connectionString, IValidator<NSRAgregaDto> busactuValidator)
        {
            _connectionString = connectionString;
            _busactuValidator = busactuValidator;
        }

        public List<NSRListaDto> ListarNotaSalidaRepuesto()
        {
            List<NotaSalidaRepuestos> listaBus = new List<NotaSalidaRepuestos>();

            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("CALL SP_NotaSalidaRepuesto_Lista()", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaBus.Add(new NotaSalidaRepuestos
                            {
                                CodigoSR = reader.GetInt32("CodigoSR"),
                                BusSR = new Bus
                                {
                                    NPlaca = reader.GetString("NPlaca"),
                                },
                                Fecha = reader.GetDateTime("Fecha"),
                                OPCodigo = new OrdenPedido
                                {
                                    CodigoOP = reader.GetInt32("CodigoOP"),
                                },
                                Estado = reader.GetBoolean("Estado")
                            });
                        }
                    }
                }
            }
            var busMapper = new NotaSalidaRepuestosMapper();
            return listaBus.Select(persona => busMapper.EntityToDto_NSRLista(persona)).ToList();
        }

        public ResponseDto<bool> InsertarNotaSalidaRepuestos(NSRAgregaDto personaDto)
        {
            var response = new ResponseDto<bool>();

            try
            {
                var mapper = new NotaSalidaRepuestosMapper();
                var bus = mapper.DtoToEntity_NSRAgregar(personaDto);

                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_NotaSalidaRepuesto_Crea", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        //command.Parameters.AddWithValue("p_CodigoM", bus.CodigoM);
                        command.Parameters.AddWithValue("p_BusSR", bus.BusSR.NPlaca);
                        command.Parameters.AddWithValue("p_Fecha", bus.Fecha);
                        command.Parameters.AddWithValue("p_OPCodigo", bus.OPCodigo.CodigoOP);

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


        public NSRListaDto GetNotaSalidaRepuestosId(int id)
        {
            NotaSalidaRepuestos persona = null;


            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("SP_NotaSalidaRepuesto_ObtenPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("p_CodigoSR", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            persona = new NotaSalidaRepuestos
                            {
                                CodigoSR = reader.GetInt32("CodigoSR"),
                                BusSR = new Bus
                                {
                                    NPlaca = reader.GetString("NPlaca"),
                                },
                                Fecha = reader.GetDateTime("Fecha"),
                                OPCodigo = new OrdenPedido
                                {
                                    CodigoOP = reader.GetInt32("CodigoOP"),
                                },
                                Estado = reader.GetBoolean("Estado")
                            };
                        }
                    }
                }
            }
            if (persona == null)
                return null;

            return new NotaSalidaRepuestosMapper().EntityToDto_NSRLista(persona);
        }

        public bool InhabilitarNotaSalida(int id)
        {
            try
            {
                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_NotaSalidaRepuesto_Inactiva", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("p_CodigoSR", id);
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