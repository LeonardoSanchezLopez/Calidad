using DIARS.Controllers.Dto.Mecanico;
using DIARS.Controllers.Dto.OrdenPedido;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Mapping;
using DIARS.Models;
using FluentValidation;
using MySql.Data.MySqlClient;
using System.Data;
using DIARS.Controllers.Dto.OrdenTrabajoExterno;
using System.Windows.Input;

namespace DIARS.Service
{
    public class OrdenTrabajoExternoService
    {
        private readonly MySQLDatabase _connectionString;
        private readonly IValidator<OTEAgregaDto> _busactuValidator;
        public OrdenTrabajoExternoService(MySQLDatabase connectionString, IValidator<OTEAgregaDto> busactuValidator)
        {
            _connectionString = connectionString;
            _busactuValidator = busactuValidator;
        }

        public List<OTEListaDto> ListarOrdenTrabajoExterno()
        {
            List<OrdenTrabajoExterno> listaBus = new List<OrdenTrabajoExterno>();

            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("CALL SP_OrdenTrabajoExterno_Lista()", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaBus.Add(new OrdenTrabajoExterno
                            {
                                CodigoTE = reader.GetInt32("CodigoTE"),
                                CodigoBus = new Bus
                                {
                                    NPlaca = reader.GetString("NPlaca"),
                                },
                                ContratoCO = new ContratoMantenimiento
                                {
                                    CodigoCM = reader.GetInt32("CodigoCM"),
                                },
                                Fecha = reader.GetDateTime("Fecha"),
                                ProveedorTE = new Proveedor
                                {
                                    Nombre = reader.GetString("Nombre"),
                                },
                                Estado = reader.GetBoolean("Estado")
                            });
                        }
                    }
                }
            }
            var busMapper = new OrdenTrabajoExternoMapper();
            return listaBus.Select(persona => busMapper.EntityToDto_OTELista(persona)).ToList();
        }

        public ResponseDto<bool> InsertarOrdenTrabajoExterno(OTEAgregaDto personaDto)
        {
            var response = new ResponseDto<bool>();

            try
            {
                var mapper = new OrdenTrabajoExternoMapper();
                var bus = mapper.DtoToEntity_OTEAgregar(personaDto);

                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_OrdenTrabajoExterno_Crea", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("p_PlacaBus", bus.CodigoBus.NPlaca);
                        command.Parameters.AddWithValue("p_ContratoCO", bus.ContratoCO.CodigoCM);
                        command.Parameters.AddWithValue("p_Fecha", bus.Fecha);
                        command.Parameters.AddWithValue("p_ProveedorTE", bus.ProveedorTE.Nombre);
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

        public OTEListaDto GetOrdenTrabajoExternoId(int id)
        {
            OrdenTrabajoExterno persona = null;


            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("SP_OrdenTrabajoExterno_ObtenPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("p_CodigoTE", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            persona = new OrdenTrabajoExterno
                            {
                                CodigoTE = reader.GetInt32("CodigoTE"),
                                CodigoBus = new Bus
                                {
                                    NPlaca = reader.GetString("NPlaca"),
                                },
                                ContratoCO = new ContratoMantenimiento
                                {
                                    CodigoCM = reader.GetInt32("CodigoCM"),
                                },
                                Fecha = reader.GetDateTime("Fecha"),
                                ProveedorTE = new Proveedor
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

            return new OrdenTrabajoExternoMapper().EntityToDto_OTELista(persona);
        }

        public bool InhabilitarOTE(int id)
        {
            try
            {
                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_OrdenTrabajoExterno_Inactiva", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("p_CodigoTE", id);
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