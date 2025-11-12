using MySql.Data.MySqlClient;
using System.Data;

namespace DIARS.Service
{
    public interface IDbExecutor
    {
        DataTable ExecuteStoredProcedure(string spName, params MySqlParameter[] parameters);
        int ExecuteNonQueryStoredProcedure(string spName, params MySqlParameter[] parameters);
        object ExecuteScalarStoredProcedure(string spName, params MySqlParameter[] parameters);
    }
}
