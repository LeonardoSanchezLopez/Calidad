using Dapper;
using DIARS.Controllers.Dto.DetalleEI;
using DIARS.Controllers.Dto;
using DIARS.Models;
using Org.BouncyCastle.Crypto;
using FluentValidation;
using DIARS.Controllers.Dto.DetalleNotaIngreso;
using DIARS.Controllers.Mapping;
using MySql.Data.MySqlClient;
using System.Data;

namespace DIARS.Service
{
    public class DetalleNotaIngresoService
    {
        private readonly MySQLDatabase _db;
        private readonly IValidator<DNoInAgregaDto> _validator;

        public DetalleNotaIngresoService(MySQLDatabase db, IValidator<DNoInAgregaDto> validator)
        {
            _db = db;
            _validator = validator;
        }

        public List<DNoInListaDto> ListarDetalleNotaIngreso()
        {
            List<DetalleNotaIngreso> lista = new();

            using var connection = _db.GetConnection();
            connection.Open();

            using var command = new MySqlCommand("CALL SP_DetalleNotaIngreso_Lista()", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new DetalleNotaIngreso
                {
                    DetalleNotaIngresoID = reader.GetInt32("DetalleNotaIngresoID"),
                    IRCodigo = new NotaIngresoRepuestos
                    {
                        CodigoIR = reader.GetInt32("CodigoIR")
                    },
                    CantidadRecibida = reader.GetInt32("CantidadRecibida"),
                    CodigoRepu = new Repuesto
                    {
                        NombreR = reader.GetString("NombreR")
                    },
                    CantidadAceptada = reader.GetInt32("CantidadAceptada"),
                    Precio = reader.GetDecimal("Precio")
                });
            }

            var mapper = new DetalleNotaIngresoMapper();
            return lista.Select(e => mapper.EntityToDto_DNoInLista(e)).ToList();
        }

        public ResponseDto<bool> InsertarDetalleNotaIngreso(DNoInAgregaDto dto)
        {
            var response = new ResponseDto<bool>();

            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                response.EjecucionExitosa = false;
                response.MensajeError = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                response.Data = false;
                return response;
            }

            var mapper = new DetalleNotaIngresoMapper();
            var entity = mapper.DtoToEntity_DNoInAgregar(dto);

            try
            {
                using var connection = _db.GetConnection();
                connection.Open();

                using var command = new MySqlCommand("SP_DetalleNotaIngreso_Crea", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("p_IRCodigo", entity.IRCodigo.CodigoIR);
                command.Parameters.AddWithValue("p_CantidadRecibida", entity.CantidadRecibida);
                command.Parameters.AddWithValue("p_NombreRepu", entity.CodigoRepu.NombreR);
                command.Parameters.AddWithValue("p_CantidadAceptada", entity.CantidadAceptada);
                command.Parameters.AddWithValue("p_Precio", entity.Precio);

                var mensajeParam = new MySqlParameter("p_Mensaje", MySqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(mensajeParam);

                command.ExecuteNonQuery();

                string mensaje = mensajeParam.Value?.ToString();
                response.MensajeError = mensaje;
                response.EjecucionExitosa = mensaje != null && mensaje.Contains("exitosa");
                response.Data = response.EjecucionExitosa;
            }
            catch (MySqlException ex)
            {
                response.EjecucionExitosa = false;
                response.MensajeError = $"Error en BD: {ex.Message}";
                response.Data = false;
            }

            return response;
        }

        public DNoInListaDto ObtenerDetalleNotaIngreso(int id)
        {
            DetalleNotaIngreso entity = null;

            using var connection = _db.GetConnection();
            connection.Open();

            using var command = new MySqlCommand("SP_DetalleNotaIngreso_ObtenPorId", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("p_DetalleNotaIngresoID", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                entity = new DetalleNotaIngreso
                {
                    DetalleNotaIngresoID = reader.GetInt32("DetalleNotaIngresoID"),
                    IRCodigo = new NotaIngresoRepuestos
                    {
                        CodigoIR = reader.GetInt32("CodigoIR")
                    },
                    CantidadRecibida = reader.GetInt32("CantidadRecibida"),
                    CodigoRepu = new Repuesto
                    {
                        NombreR = reader.GetString("NombreR")
                    },
                    CantidadAceptada = reader.GetInt32("CantidadAceptada"),
                    Precio = reader.GetDecimal("Precio")
                };
            }

            return entity == null ? null : new DetalleNotaIngresoMapper().EntityToDto_DNoInLista(entity);
        }
    }
}