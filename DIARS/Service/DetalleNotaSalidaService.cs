using DIARS.Controllers.Dto.DetalleNotaSalida;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Mapping;
using DIARS.Models;
using FluentValidation;
using MySql.Data.MySqlClient;
using System.Data;

namespace DIARS.Service
{
    public class DetalleNotaSalidaService
    {
        private readonly MySQLDatabase _db;
        private readonly IValidator<DNoSaAgregaDto> _validator;

        public DetalleNotaSalidaService(MySQLDatabase db, IValidator<DNoSaAgregaDto> validator)
        {
            _db = db;
            _validator = validator;
        }

        public List<DNoSaListaDto> ListarDetalleNotaSalida()
        {
            List<DetalleNotaSalida> lista = new();

            using var connection = _db.GetConnection();
            connection.Open();

            using var command = new MySqlCommand("CALL SP_DetalleNotaSalida_Lista()", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new DetalleNotaSalida
                {
                    DetalleNotaSalidaID = reader.GetInt32("DetalleNotaSalidaID"),
                    SRCodigo = new NotaSalidaRepuestos
                    {
                        CodigoSR = reader.GetInt32("CodigoSR")
                    },
                    CantidadRecibida = reader.GetInt32("CantidadRecibida"),
                    CodigoRepu = new Repuesto
                    {
                        NombreR = reader.GetString("NombreR")
                    },
                    CantidadEnviada = reader.GetInt32("CantidadEnviada")
                });
            }

            var mapper = new DetalleNotaSalidaMapper();
            return lista.Select(e => mapper.EntityToDto_DNoSaLista(e)).ToList();
        }

        public ResponseDto<bool> InsertarDetalleNotaSalida(DNoSaAgregaDto dto)
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

            var mapper = new DetalleNotaSalidaMapper();
            var entity = mapper.DtoToEntity_DNoSaAgregar(dto);

            try
            {
                using var connection = _db.GetConnection();
                connection.Open();

                using var command = new MySqlCommand("SP_DetalleNotaSalida_Crea", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("p_DSRCodigo", entity.SRCodigo.CodigoSR);
                command.Parameters.AddWithValue("p_NombreRepu", entity.CodigoRepu.NombreR);
                command.Parameters.AddWithValue("p_CantidadRecibida", entity.CantidadRecibida);
                command.Parameters.AddWithValue("p_CantidadEnviada", entity.CantidadEnviada);

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

        public DNoSaListaDto ObtenerDetalleNotaSalida(int id)
        {
            DetalleNotaSalida entity = null;

            using var connection = _db.GetConnection();
            connection.Open();

            using var command = new MySqlCommand("SP_DetalleNotaSalida_ObtenPorId", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("p_DetalleNotaSalidaID", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                entity = new DetalleNotaSalida
                {
                    DetalleNotaSalidaID = reader.GetInt32("DetalleNotaSalidaID"),
                    SRCodigo = new NotaSalidaRepuestos
                    {
                        CodigoSR = reader.GetInt32("CodigoSR")
                    },
                    CantidadRecibida = reader.GetInt32("CantidadRecibida"),
                    CodigoRepu = new Repuesto
                    {
                        NombreR = reader.GetString("NombreR")
                    },
                    CantidadEnviada = reader.GetInt32("CantidadEnviada")
                };
            }
            if (entity == null) return null;
            return new DetalleNotaSalidaMapper().EntityToDto_DNoSaLista(entity);
        }
    }
}
