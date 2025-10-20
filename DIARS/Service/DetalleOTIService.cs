using DIARS.Controllers.Dto.DetalleOTE;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Mapping;
using DIARS.Models;
using FluentValidation;
using MySql.Data.MySqlClient;
using System.Data;
using DIARS.Controllers.Dto.DetalleOTI;

namespace DIARS.Service
{
    public class DetalleOTIService
    {
        private readonly MySQLDatabase _db;
        private readonly IValidator<DOTIAgregaDto> _validator;

        public DetalleOTIService(MySQLDatabase db, IValidator<DOTIAgregaDto> validator)
        {
            _db = db;
            _validator = validator;
        }

        public List<DOTIListaDto> ListarDetalleOTI()
        {
            List<DetalleOTI> lista = new();

            using var connection = _db.GetConnection();
            connection.Open();

            using var command = new MySqlCommand("CALL SP_DetalleOTI_Lista()", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new DetalleOTI
                {
                    DetalleOTIID = reader.GetInt32("DetalleOTIID"),
                    OrdenTrabajoInternoID = new OrdenTrabajoInterno
                    {
                        CodigoTI = reader.GetInt32("CodigoTI")
                    },
                    CodigoRepu = new Repuesto
                    {
                        NombreR = reader.GetString("NombreR")
                    },
                    MecanicoTI = new Mecanico
                    {
                        Nombre = reader.GetString("Nombre")
                    },
                    Parte = reader.GetString("Parte"),
                    Pieza = reader.GetString("Pieza"),
                    Cantidad = reader.GetInt32("Cantidad"),
                });
            }

            var mapper = new DetalleOTIMapper();
            return lista.Select(e => mapper.EntityToDto_DOTILista(e)).ToList();
        }

        public ResponseDto<bool> InsertarDetalleOTI(DOTIAgregaDto dto)
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

            var mapper = new DetalleOTIMapper();
            var entity = mapper.DtoToEntity_DOTIAgregar(dto);

            try
            {
                using var connection = _db.GetConnection();
                connection.Open();

                using var command = new MySqlCommand("SP_DetalleOTI_Crea", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("p_OrdenTrabajoInternoID", entity.OrdenTrabajoInternoID.CodigoTI);
                command.Parameters.AddWithValue("p_CodigoRepu", entity.CodigoRepu.NombreR);
                command.Parameters.AddWithValue("p_MecanicoTI", entity.MecanicoTI.Nombre);
                command.Parameters.AddWithValue("p_Parte", entity.Parte);
                command.Parameters.AddWithValue("p_Pieza", entity.Pieza);
                command.Parameters.AddWithValue("p_Cantidad", entity.Cantidad);

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

        public DOTIListaDto ObtenerDetalleOTIPorId(int id)
        {
            DetalleOTI entity = null;

            using var connection = _db.GetConnection();
            connection.Open();

            using var command = new MySqlCommand("SP_DetalleOTI_ObtenPorId", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("p_DetalleOTIID", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                entity = new DetalleOTI
                {
                    DetalleOTIID = reader.GetInt32("DetalleOTIID"),
                    OrdenTrabajoInternoID = new OrdenTrabajoInterno
                    {
                        CodigoTI = reader.GetInt32("CodigoTI")
                    },
                    CodigoRepu = new Repuesto
                    {
                        NombreR = reader.GetString("NombreR")
                    },
                    MecanicoTI = new Mecanico
                    {
                        Nombre = reader.GetString("Nombre")
                    },
                    Parte = reader.GetString("Parte"),
                    Pieza = reader.GetString("Pieza"),
                    Cantidad = reader.GetInt32("Cantidad"),
                };
            }
            if (entity == null) return null;
            return new DetalleOTIMapper().EntityToDto_DOTILista(entity);
        }
    }
}