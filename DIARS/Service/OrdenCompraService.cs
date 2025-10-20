using DIARS.Controllers.Dto.Mecanico;
using DIARS.Controllers.Dto.NotaSalidaRepuestos;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Mapping;
using DIARS.Models;
using FluentValidation;
using MySql.Data.MySqlClient;
using System.Data;
using DIARS.Controllers.Dto.OrdenCompra;

namespace DIARS.Service
{
    public class OrdenCompraService : IOrdenCompraService
    {
        private readonly MySQLDatabase _connectionString;
        private readonly IValidator<OrCoAgregaDto> _busactuValidator;
        public OrdenCompraService(MySQLDatabase connectionString, IValidator<OrCoAgregaDto> busactuValidator)
        {
            _connectionString = connectionString;
            _busactuValidator = busactuValidator;
        }

        public List<OrCoListaDto> ListarOrdenCompra()
        {
            List<OrdenCompra> listaBus = new List<OrdenCompra>();

            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("CALL SP_OrdenCompra_Lista()", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaBus.Add(new OrdenCompra
                            {
                                CodigoOC = reader.GetInt32("CodigoOC"),
                                CodigoPro = new Proveedor
                                {
                                    Nombre = reader.GetString("Nombre"),
                                },
                                Fecha = reader.GetDateTime("Fecha"),
                                OPCodigo = new OrdenPedido
                                {
                                    CodigoOP = reader.GetInt32("CodigoOP"),
                                },
                                FormaPago = reader.GetString("FormaPago"),
                                Total = reader.GetDecimal("TOTAL"),
                                Estado = reader.GetBoolean("EstadoC")
                            });
                        }
                    }
                }
            }
            var busMapper = new OrdenCompraMapper();
            return listaBus.Select(persona => busMapper.EntityToDto_OrCoLista(persona)).ToList();
        }

        public ResponseDto<bool> InsertarOrdenCompra(OrCoAgregaDto personaDto)
        {
            var response = new ResponseDto<bool>();

            try
            {
                var mapper = new OrdenCompraMapper();
                var bus = mapper.DtoToEntity_OrCoAgregar(personaDto);

                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_OrdenCompra_Crea", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        //command.Parameters.AddWithValue("p_CodigoM", bus.CodigoM);
                        command.Parameters.AddWithValue("p_NombrePro", bus.CodigoPro.Nombre);
                        command.Parameters.AddWithValue("p_Fecha", bus.Fecha);
                        command.Parameters.AddWithValue("p_OPCodigo", bus.OPCodigo.CodigoOP);
                        command.Parameters.AddWithValue("p_FormaPago", bus.FormaPago);
                        command.Parameters.AddWithValue("p_TOTAL", bus.Total);

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


        public OrCoListaDto GetOrdenCompraId(int id)
        {
            OrdenCompra persona = null;


            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("SP_OrdenCompra_ObtenPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("p_CodigoOC", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            persona = new OrdenCompra
                            {
                                CodigoOC = reader.GetInt32("CodigoOC"),
                                CodigoPro = new Proveedor
                                {
                                    Nombre = reader.GetString("Nombre"),
                                },
                                Fecha = reader.GetDateTime("Fecha"),
                                OPCodigo = new OrdenPedido
                                {
                                    CodigoOP = reader.GetInt32("CodigoOP"),
                                },
                                FormaPago = reader.GetString("FormaPago"),
                                Total = reader.GetDecimal("TOTAL"),
                                Estado = reader.GetBoolean("EstadoC")
                            };
                        }
                    }
                }
            }
            if (persona == null)
                return null;

            return new OrdenCompraMapper().EntityToDto_OrCoLista(persona);
        }

        public bool InhabilitarOrdenCompra(int id)
        {
            try
            {
                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_OrdenCompra_Inactiva", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("p_CodigoOC", id);
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