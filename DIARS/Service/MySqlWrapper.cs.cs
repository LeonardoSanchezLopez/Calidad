using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace DIARS.Service.Database
{
    public class MySqlWrapper : DetalleNotaSalidaMySqlWrapper
    {
        private readonly string _connectionString;

        public MySqlWrapper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MySqlConnection GetConnection()
        {
            var connection = new MySqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        public int ExecuteNonQuery(string procedureName, Dictionary<string, object> parameters)
        {
            using var connection = GetConnection();
            using var command = new MySqlCommand(procedureName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            foreach (var param in parameters)
                command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);

            return command.ExecuteNonQuery();
        }

        public MySqlDataReader ExecuteReader(string procedureName, Dictionary<string, object> parameters)
        {
            var connection = GetConnection();
            var command = new MySqlCommand(procedureName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            foreach (var param in parameters)
                command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);

            // ⚠️ No cerrar conexión aquí, el lector lo hará
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public object ExecuteScalar(string procedureName, Dictionary<string, object> parameters)
        {
            using var connection = GetConnection();
            using var command = new MySqlCommand(procedureName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            foreach (var param in parameters)
                command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);

            return command.ExecuteScalar();
        }

        public void Dispose()
        {
            // Nada que limpiar, las conexiones se manejan con using
        }
    }
}
