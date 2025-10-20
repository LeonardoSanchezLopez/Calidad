using DIARS.Controllers.Dto.Mecanico;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Mapping;
using DIARS.Models;
using FluentValidation;
using MySql.Data.MySqlClient;
using System.Data;
using DIARS.Controllers.Dto.NotaIngresoRepuestos;

namespace DIARS.Service
{
    public class NotaIngresoRepuestoService
    {
        private readonly MySQLDatabase _connectionString;
        private readonly IValidator<NIRAgregaDto> _busactuValidator;
        public NotaIngresoRepuestoService(MySQLDatabase connectionString, IValidator<NIRAgregaDto> busactuValidator)
        {
            _connectionString = connectionString;
            _busactuValidator = busactuValidator;
        }

        public List<NIRListaDto> ListarNotaIngresoRepuestos()
        {
            List<NotaIngresoRepuestos> listaBus = new List<NotaIngresoRepuestos>();

            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("CALL SP_NotaIngresoRepuestos_Lista()", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaBus.Add(new NotaIngresoRepuestos
                            {
                                CodigoIR = reader.GetInt32("CodigoIR"),
                                CodigoOC = new OrdenCompra
                                {
                                    CodigoOC = reader.GetInt32("CodigoOC"),
                                },
                                Fecha = reader.GetDateTime("Fecha"),
                                ProveedorIR = new Proveedor
                                {
                                    Nombre = reader.GetString("Nombre"),
                                },
                                Estado = reader.GetBoolean("Estado")
                            });
                        }
                    }
                }
            }
            var busMapper = new NotaIngresoRepuestosMapper();
            return listaBus.Select(persona => busMapper.EntityToDto_NIRLista(persona)).ToList();
        }

        public ResponseDto<bool> InsertarNotaIngresoRepuestos(NIRAgregaDto personaDto)
        {
            var response = new ResponseDto<bool>();

            try
            {
                var mapper = new NotaIngresoRepuestosMapper();
                var bus = mapper.DtoToEntity_NIRAgregar(personaDto);

                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_NotaIngresoRepuestos_Crea", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        //command.Parameters.AddWithValue("p_CodigoM", bus.CodigoM);
                        command.Parameters.AddWithValue("p_CodigoOC", bus.CodigoOC.CodigoOC);
                        command.Parameters.AddWithValue("p_Fecha", bus.Fecha);
                        command.Parameters.AddWithValue("p_ProveedorIR", bus.ProveedorIR.Nombre);

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


        public NIRListaDto GetNotaIngresoRepuestosId(int id)
        {
            NotaIngresoRepuestos persona = null;


            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("SP_NotaIngresoRepuestos_ObtenPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("p_CodigoIR", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            persona = new NotaIngresoRepuestos
                            {
                                CodigoIR = reader.GetInt32("CodigoIR"),
                                CodigoOC = new OrdenCompra
                                {
                                    CodigoOC = reader.GetInt32("CodigoOC"),
                                },
                                Fecha = reader.GetDateTime("Fecha"),
                                ProveedorIR = new Proveedor
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

            return new NotaIngresoRepuestosMapper().EntityToDto_NIRLista(persona);
        }

        public bool InhabilitarNotaIngreso(int id)
        {
            try
            {
                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_NotaIngresoRepuestos_Inactiva", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("p_CodigoIR", id);
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
