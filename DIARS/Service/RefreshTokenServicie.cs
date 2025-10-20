using Microsoft.AspNetCore.Identity.Data;
using MySql.Data.MySqlClient;

namespace DIARS.Service
{
    public class RefreshTokenServicie
    {
        private readonly MySQLDatabase _connectionString;

        public RefreshTokenServicie(MySQLDatabase connectionString)
        {
            _connectionString = connectionString;
        }

        public void GuardarRefreshToken(int usuarioId, string token, DateTime expiration)
        {
            try
            {
                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand(@"
                        INSERT INTO refreshtokens (Usu_Id, Token, Expiration) 
                        VALUES (@Usu_Id, @Token, @Expiration)", connection))
                    {
                        command.Parameters.AddWithValue("@Usu_Id", usuarioId);
                        command.Parameters.AddWithValue("@Token", token);
                        command.Parameters.AddWithValue("@Expiration", expiration);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar Refresh Token: {ex.Message}");
            }
        }
        public void EliminarRefreshToken(string token)
        {
            try
            {
                using (var connection = _connectionString.GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand(@"
                        DELETE FROM refreshtokens WHERE Token = @Token", connection))
                    {
                        command.Parameters.AddWithValue("@Token", token);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar Refresh Token: {ex.Message}");
            }
        }
    }
}
