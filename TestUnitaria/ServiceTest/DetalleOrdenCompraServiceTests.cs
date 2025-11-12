using DIARS.Controllers.Dto.DetalleOrdenCompra;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Mapping;
using DIARS.Models;
using FluentValidation;
using MySql.Data.MySqlClient;
using System.Data;

namespace DIARS.Service
{
    public class DetalleOrdenCompraService
    {
        private readonly IDbExecutor _dbExecutor;
        private readonly IValidator<DOrCoAgregaDto> _validator;

        public DetalleOrdenCompraService(IDbExecutor dbExecutor, IValidator<DOrCoAgregaDto> validator)
        {
            _dbExecutor = dbExecutor;
            _validator = validator;
        }

        public List<DOrCoListaDto> ListarDetalleOrdenCompra()
        {
            var dt = _dbExecutor.ExecuteStoredProcedure("SP_DetalleOrdenCompra_Lista");

            var lista = new List<DetalleOrdenCompra>();
            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new DetalleOrdenCompra
                {
                    DetalleOrdenCompraID = Convert.ToInt32(row["DetalleOrdenCompraID"]),
                    OCCompra = new OrdenCompra { CodigoOC = Convert.ToInt32(row["CodigoOC"]) },
                    CodigoRep = new Repuesto { NombreR = row["NombreR"].ToString() },
                    Cantidad = Convert.ToInt32(row["Cantidad"]),
                    CantidadAceptada = Convert.ToInt32(row["CantidadAceptada"]),
                    Precio = Convert.ToDecimal(row["Precio"])
                });
            }

            return lista.Select(e => new DetalleOrdenCompraMapper().EntityToDto_DOrCoLista(e)).ToList();
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

            var entity = new DetalleOrdenCompraMapper().DtoToEntity_DOrCoAgregar(dto);

            var mensajeParam = new MySqlParameter("p_Mensaje", MySqlDbType.VarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            try
            {
                _dbExecutor.ExecuteNonQueryStoredProcedure("SP_DetalleOrdenCompra_Crea",
                    new MySqlParameter("p_OCCompra", entity.OCCompra.CodigoOC),
                    new MySqlParameter("p_CodigoRep", entity.CodigoRep.NombreR),
                    new MySqlParameter("p_Cantidad", entity.Cantidad),
                    new MySqlParameter("p_CantidadAceptada", entity.CantidadAceptada),
                    new MySqlParameter("p_Precio", entity.Precio),
                    mensajeParam
                );

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
            var dt = _dbExecutor.ExecuteStoredProcedure("SP_DetalleOrdenCompra_ObtenPorId",
                new MySqlParameter("p_DetalleOrdenCompraID", id));

            if (dt.Rows.Count == 0) return null;

            var row = dt.Rows[0];
            var entity = new DetalleOrdenCompra
            {
                DetalleOrdenCompraID = Convert.ToInt32(row["DetalleOrdenCompraID"]),
                OCCompra = new OrdenCompra { CodigoOC = Convert.ToInt32(row["CodigoOC"]) },
                CodigoRep = new Repuesto { NombreR = row["NombreR"].ToString() },
                Cantidad = Convert.ToInt32(row["Cantidad"]),
                CantidadAceptada = Convert.ToInt32(row["CantidadAceptada"]),
                Precio = Convert.ToDecimal(row["Precio"])
            };

            return new DetalleOrdenCompraMapper().EntityToDto_DOrCoLista(entity);
        }
    }
}
