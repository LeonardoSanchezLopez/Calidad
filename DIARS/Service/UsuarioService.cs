using DIARS.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace DIARS.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly MySQLDatabase _connectionString;

        public UsuarioService(MySQLDatabase connectionString)
        {
            _connectionString = connectionString;
        }

        public Usuario obtenerUsuarios(string nombre, string password)
        {
            Usuario usuario = null;
            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("SP_Usuario_Validar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("u_nombre", nombre);
                    command.Parameters.AddWithValue("u_password", password);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new Usuario
                            {
                                Usu_Id = reader.GetInt32("Usu_Id"),
                                Usu_Nombre = reader.GetString("Usu_Nombre"),
                                Usu_Rol = reader.GetString("Usu_Rol")
                            };
                        }
                    }
                }
            }
            return usuario;
        }

        public Usuario obtenerUsuarioPorId(int id)
        {
            Usuario usuario = null;
            using (var connection = _connectionString.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("SP_Usuario_ObtenPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("u_Usu_Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuario = new Usuario
                            {
                                Usu_Id = reader.GetInt32("Usu_Id"),
                                Usu_Nombre = reader.GetString("Usu_Nombre"),
                                Usu_Rol = reader.GetString("Usu_Rol")
                            };
                        }
                    }
                }

            }
            return usuario;
        }
    }
}