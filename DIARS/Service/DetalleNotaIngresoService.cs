using Dapper;
using DIARS.Controllers.Dto;
using DIARS.Controllers.Dto.DetalleNotaIngreso;
using DIARS.Controllers.Mapping;
using DIARS.Models;
using DIARS.Service.Database;
using FluentValidation;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DIARS.Service
{
    public class DetalleNotaIngresoService
    {
        private readonly DetalleNotaSalidaMySqlWrapper _db; // 🔹 Usamos la interfaz, no la clase concreta
        private readonly IValidator<DNoInAgregaDto> _validator;

        public DetalleNotaIngresoService(DetalleNotaSalidaMySqlWrapper db, IValidator<DNoInAgregaDto> validator)
        {
            _db = db;
            _validator = validator;
        }

        // 🧾 Listar Detalles
        public List<DNoInListaDto> ListarDetalleNotaIngreso()
        {
            var lista = new List<DetalleNotaIngreso>();
            var mapper = new DetalleNotaIngresoMapper();

            using var reader = _db.ExecuteReader("SP_DetalleNotaIngreso_Lista", null);

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

            reader.Close();
            return lista.Select(mapper.EntityToDto_DNoInLista).ToList();
        }

        // 🧩 Insertar Detalle
        public ResponseDto<bool> InsertarDetalleNotaIngreso(DNoInAgregaDto dto)
        {
            var response = new ResponseDto<bool>();

            try
            {
                // 🧩 Validación
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

                // ⚙️ Parámetros
                var parameters = new Dictionary<string, object>
        {
            { "p_IRCodigo", entity.IRCodigo.CodigoIR },
            { "p_CantidadRecibida", entity.CantidadRecibida },
            { "p_NombreRepu", entity.CodigoRepu.NombreR },
            { "p_CantidadAceptada", entity.CantidadAceptada },
            { "p_Precio", entity.Precio }
        };

                // 🧠 Ejecución BD
                var result = _db.ExecuteNonQuery("SP_DetalleNotaIngreso_Crea", parameters);

                response.EjecucionExitosa = result > 0;
                response.Data = response.EjecucionExitosa;
                response.MensajeError = response.EjecucionExitosa
                    ? "Inserción exitosa"
                    : "No se pudo insertar el detalle de la nota de ingreso.";
            }
            catch (Exception ex)
            {
                // ✅ Captura *cualquier* excepción
                response.EjecucionExitosa = false;
                response.MensajeError = $"Error en BD: {ex.Message}";
                response.Data = false;
            }

            return response;
        }


        // 🔍 Obtener Detalle por ID
        public DNoInListaDto ObtenerDetalleNotaIngreso(int id)
        {
            var parameters = new Dictionary<string, object>
            {
                { "p_DetalleNotaIngresoID", id }
            };

            using var reader = _db.ExecuteReader("SP_DetalleNotaIngreso_ObtenPorId", parameters);

            DetalleNotaIngreso entity = null;

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

            reader.Close();
            return entity == null ? null : new DetalleNotaIngresoMapper().EntityToDto_DNoInLista(entity);
        }
    }
}
