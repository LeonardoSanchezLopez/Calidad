using DIARS.Controllers.Dto.MarcaRepuesto;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Mapping;
using DIARS.Models;
using FluentValidation;
using MySql.Data.MySqlClient;
using System.Data;
using DIARS.Controllers.Dto.Repuesto;

namespace DIARS.Service
{
    public class RepuestoService
    {
        private readonly MySQLDatabase _connectionString;
        private readonly IValidator<RepuActuDto> _busactuValidator;
        public RepuestoService(MySQLDatabase connectionString, IValidator<RepuActuDto> busactuValidator)
        {
            _connectionString = connectionString;
            _busactuValidator = busactuValidator;
        }

        public List<RepuListaDto> ListarRepuesto()
        {
            List<Repuesto> listaBus = new List<Repuesto>();

            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("CALL SP_Repuesto_Lista()", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaBus.Add(new Repuesto
                            {
                                CodigoR = reader.GetInt32("CodigoR"),
                                NombreR = reader.GetString("NombreR"),
                                CategoriaR = new Categoria
                                {
                                    NombreC = reader.GetString("NombreC"),
                                },
                                MarcarepuestoR = new MarcaRepuesto
                                {
                                    Descripcion = reader.GetString("Descripcion"),
                                },
                                ProveedorR = new Proveedor
                                {
                                    Nombre = reader.GetString("Nombre"),
                                },
                                Precio = reader.GetDecimal("Precio"),
                                EstadoR = reader.GetBoolean("EstadoR")
                            });
                        }
                    }
                }
            }
            var busMapper = new RepuestoMapper();
            return listaBus.Select(persona => busMapper.EntityToDto_RepuestoLista(persona)).ToList();
        }

        public ResponseDto<bool> InsertarRepuesto(RepuAgregaDto personaDto)
        {
            var response = new ResponseDto<bool>();

            try
            {
                var mapper = new RepuestoMapper();
                var bus = mapper.DtoToEntity_RepuestoAgregar(personaDto);

                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_Repuesto_Crea", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        command.Parameters.AddWithValue("p_NombreR", bus.NombreR);
                        command.Parameters.AddWithValue("p_CategoriaR", bus.CategoriaR.NombreC);
                        command.Parameters.AddWithValue("p_MarcarepuestoR", bus.MarcarepuestoR.Descripcion);
                        command.Parameters.AddWithValue("p_ProveedorR", bus.ProveedorR.Nombre);
                        command.Parameters.AddWithValue("p_Precio", bus.Precio);

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

        public ResponseDto<bool> ActualizarRepuesto(RepuActuDto personaDto)
        {
            var response = new ResponseDto<bool>();
            var mapper = new RepuestoMapper();
            var persona = mapper.DtoToEntity_RepuestoActualizar(personaDto);

            try
            {
                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_Repuesto_Actualiza", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("p_CodigoR", persona.CodigoR);
                        command.Parameters.AddWithValue("p_NombreR", persona.NombreR);
                        command.Parameters.AddWithValue("p_CategoriaR", persona.CategoriaR.NombreC);
                        command.Parameters.AddWithValue("p_MarcarepuestoR", persona.MarcarepuestoR.Descripcion);
                        command.Parameters.AddWithValue("p_ProveedorR", persona.ProveedorR.Nombre);
                        command.Parameters.AddWithValue("p_Precio", persona.Precio);
                        command.Parameters.AddWithValue("p_Estado", persona.EstadoR);

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

        public RepuListaDto GetRepuestoId(int id)
        {
            Repuesto persona = null;


            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("SP_Repuesto_ObtenPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("p_CodigoR", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            persona = new Repuesto
                            {
                                CodigoR = reader.GetInt32("CodigoR"),
                                NombreR = reader.GetString("NombreR"),
                                CategoriaR = new Categoria
                                {
                                    NombreC = reader.GetString("NombreC"),
                                },
                                MarcarepuestoR = new MarcaRepuesto
                                {
                                    Descripcion = reader.GetString("Descripcion"),
                                },
                                ProveedorR = new Proveedor
                                {
                                    Nombre = reader.GetString("Nombre"),
                                },
                                Precio = reader.GetDecimal("Precio"),
                                EstadoR = reader.GetBoolean("EstadoR")
                            };
                        }
                    }
                }
            }
            if (persona == null)
                return null;

            return new RepuestoMapper().EntityToDto_RepuestoLista(persona);
        }

        public bool InhabilitaRepuesto(int id)
        {
            try
            {
                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SP_Repuesto_Inactiva", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("p_CodigoR", id);
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
