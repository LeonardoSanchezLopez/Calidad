using DIARS.Controllers.Dto.Mecanico;
using DIARS.Controllers.Dto.OrdenCompra;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Mapping;
using DIARS.Models;
using FluentValidation;
using MySql.Data.MySqlClient;
using System.Data;
using DIARS.Controllers.Dto.OrdenPedido;

namespace DIARS.Service
{
    public class OrdenPedidoService : IOrdenPedidoService
    {
        private readonly MySQLDatabase _connectionString;
        private readonly IValidator<OrPeAgregaDto> _busactuValidator;
        public OrdenPedidoService(MySQLDatabase connectionString, IValidator<OrPeAgregaDto> busactuValidator)
        {
            _connectionString = connectionString;
            _busactuValidator = busactuValidator;
        }

        public List<OrPeListaDto> ListarOrdenPedido()
        {
            List<OrdenPedido> listaBus = new List<OrdenPedido>();

            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("CALL SP_OrdenPedido_Lista()", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaBus.Add(new OrdenPedido
                            {
                                CodigoOP = reader.GetInt32("CodigoOP"),
                                Fecha = reader.GetDateTime("Fecha"),
                                TICodigo = new OrdenTrabajoInterno
                                {
                                    CodigoTI = reader.GetInt32("CodigoTI"),
                                },
                                BusCM = new Bus
                                {
                                    NPlaca = reader.GetString("NPlaca"),
                                },
                                JefeEncargado = reader.GetString("JefeEncargado"),
                                Descripcion = reader.GetString("Descripcion"),
                                Estado = reader.GetBoolean("Estado")
                            });
                        }
                    }
                }
            }
            var busMapper = new OrdenPedidoMapper();
            return listaBus.Select(persona => busMapper.EntityToDto_OrPeLista(persona)).ToList();
        }

        public ResponseDto<bool> InsertarOrdenPedido(OrPeAgregaDto personaDto)
        {
            var response = new ResponseDto<bool>();

            try
            {
                var mapper = new OrdenPedidoMapper();
                var bus = mapper.DtoToEntity_OrPeAgregar(personaDto);

                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_OrdenPedido_Crea", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("p_Fecha", bus.Fecha);
                        command.Parameters.AddWithValue("p_TICodigo", bus.TICodigo.CodigoTI);
                        command.Parameters.AddWithValue("p_BusPlaca", bus.BusCM.NPlaca);
                        command.Parameters.AddWithValue("p_JefeEncargado", bus.JefeEncargado);
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


        public OrPeListaDto GetOrdenPedidoId(int id)
        {
            OrdenPedido persona = null;


            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("SP_OrdenPedido_ObtenPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("p_CodigoOP", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            persona = new OrdenPedido
                            {
                                CodigoOP = reader.GetInt32("CodigoOP"),
                                Fecha = reader.GetDateTime("Fecha"),
                                TICodigo = new OrdenTrabajoInterno
                                {
                                    CodigoTI = reader.GetInt32("CodigoTI"),
                                },
                                BusCM = new Bus
                                {
                                    NPlaca = reader.GetString("NPlaca"),
                                },
                                JefeEncargado = reader.GetString("JefeEncargado"),
                                Descripcion = reader.GetString("Descripcion"),
                                Estado = reader.GetBoolean("Estado")
                            };
                        }
                    }
                }
            }
            if (persona == null)
                return null;

            return new OrdenPedidoMapper().EntityToDto_OrPeLista(persona);
        }

        public bool InhabilitarOrdenPedidio(int id)
        {
            try
            {
                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_OrdenPedido_Inactiva", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("p_CodigoOP", id);
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