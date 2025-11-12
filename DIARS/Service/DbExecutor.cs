using MySql.Data.MySqlClient;
using System.Data;

namespace DIARS.Service
{
    public class DbExecutor : IDbExecutor
    {
        private readonly MySQLDatabase _db;

        public DbExecutor(MySQLDatabase db)
        {
            _db = db;
        }

        public DataTable ExecuteStoredProcedure(string spName, params MySqlParameter[] parameters)
        {
            using var connection = _db.GetConnection();
            connection.Open();
            using var command = new MySqlCommand(spName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            if (parameters != null)
                command.Parameters.AddRange(parameters);

            using var adapter = new MySqlDataAdapter(command);
            var dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public int ExecuteNonQueryStoredProcedure(string spName, params MySqlParameter[] parameters)
        {
            using var connection = _db.GetConnection();
            connection.Open();
            using var command = new MySqlCommand(spName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            if (parameters != null)
                command.Parameters.AddRange(parameters);

            return command.ExecuteNonQuery();
        }

        public object ExecuteScalarStoredProcedure(string spName, params MySqlParameter[] parameters)
        {
            using var connection = _db.GetConnection();
            connection.Open();
            using var command = new MySqlCommand(spName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            if (parameters != null)
                command.Parameters.AddRange(parameters);

            return command.ExecuteScalar();
        }
    }
}
