using DIARS.Controllers.Dto.DetalleOrdenCompra;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Mapping;
using DIARS.Models;
using FluentValidation;
using MySql.Data.MySqlClient;
using System.Data;

namespace DIARS.Service
{
    public class DetalleOrdenCompraService : IDetalleOrdenCompraService
    {
        private readonly MySQLDatabase _db;
        private readonly IValidator<DOrCoAgregaDto> _validator;

        public DetalleOrdenCompraService(MySQLDatabase db, IValidator<DOrCoAgregaDto> validator)
        {
            _db = db;
            _validator = validator;
        }

        public List<DOrCoListaDto> ListarDetalleOrdenCompra()
        {
            List<DetalleOrdenCompra> lista = new();

            using var connection = _db.GetConnection();
            connection.Open();

            using var command = new MySqlCommand("CALL SP_DetalleOrdenCompra_Lista()", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new DetalleOrdenCompra
                {
                    DetalleOrdenCompraID = reader.GetInt32("DetalleOrdenCompraID"),
                    OCCompra = new OrdenCompra
                    {
                        CodigoOC = reader.GetInt32("CodigoOC")
                    },
                    CodigoRep = new Repuesto
                    {
                        NombreR = reader.GetString("NombreR")
                    },
                    Cantidad = reader.GetInt32("Cantidad"),
                    CantidadAceptada = reader.GetInt32("CantidadAceptada"),
                    Precio = reader.GetDecimal("Precio")
                });
            }

            var mapper = new DetalleOrdenCompraMapper();
            return lista.Select(e => mapper.EntityToDto_DOrCoLista(e)).ToList();
        }

        public ResponseDto<bool> InsertarDetalleOrdenCompra(DOrCoAgregaDto dto)
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

            var mapper = new DetalleOrdenCompraMapper();
            var entity = mapper.DtoToEntity_DOrCoAgregar(dto);

            try
            {
                using var connection = _db.GetConnection();
                connection.Open();

                using var command = new MySqlCommand("SP_DetalleOrdenCompra_Crea", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("p_OCCompra", entity.OCCompra.CodigoOC);
                command.Parameters.AddWithValue("p_CodigoRep", entity.CodigoRep.NombreR);
                command.Parameters.AddWithValue("p_Cantidad", entity.Cantidad);
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

        public DOrCoListaDto ObtenerDetalleOrdenCompra(int id)
        {
            DetalleOrdenCompra entity = null;

            using var connection = _db.GetConnection();
            connection.Open();

            using var command = new MySqlCommand("SP_DetalleOrdenCompra_ObtenPorId", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("p_DetalleOrdenCompraID", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                entity = new DetalleOrdenCompra
                {
                    DetalleOrdenCompraID = reader.GetInt32("DetalleOrdenCompraID"),
                    OCCompra = new OrdenCompra
                    {
                        CodigoOC = reader.GetInt32("CodigoOC")
                    },
                    CodigoRep = new Repuesto
                    {
                        NombreR = reader.GetString("NombreR")
                    },
                    Cantidad = reader.GetInt32("Cantidad"),
                    CantidadAceptada = reader.GetInt32("CantidadAceptada"),
                    Precio = reader.GetDecimal("Precio")
                };
            }
            if (entity == null) return null;
            return new DetalleOrdenCompraMapper().EntityToDto_DOrCoLista(entity);
        }
    }
}