using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace DIARS.Service.Database
{
    public interface DetalleNotaSalidaMySqlWrapper : IDisposable
    {
        // Ejecuta procedimientos que no retornan datos (INSERT, UPDATE, DELETE)
        int ExecuteNonQuery(string procedureName, Dictionary<string, object> parameters);

        // Ejecuta procedimientos que retornan múltiples registros (SELECT)
        MySqlDataReader ExecuteReader(string procedureName, Dictionary<string, object> parameters);

        // Ejecuta procedimientos que retornan un solo valor (COUNT, MAX, etc.)
        object ExecuteScalar(string procedureName, Dictionary<string, object> parameters);

        // Obtiene una conexión abierta (para uso directo cuando se necesite)
        MySqlConnection GetConnection();
    }
}
