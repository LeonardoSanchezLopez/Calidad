using MySql.Data.MySqlClient;

namespace DIARS
{
    public class MySQLDatabase
    {
        private readonly string _connectionString;

        public MySQLDatabase(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
