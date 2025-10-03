using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ProductCrudMVC.Data
{
    public class DbAccess
    {
        private readonly string _connectionString;
        public DbAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<T> GetResultList<T>(string commandText, params object[] parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    DynamicParameters dparam = new DynamicParameters();
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        dparam.Add($"@param{i}", parameters[i]);
                    }
                    return connection.Query<T>(commandText, dparam, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public T GetResultSingle<T>(string commandText, params object[] parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    DynamicParameters dparam = new DynamicParameters();
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        dparam.Add($"@param{i}", parameters[i]);
                    }
                    return connection.QuerySingleOrDefault<T>(commandText, dparam, commandType: CommandType.StoredProcedure)!;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ExecuteCommand(string commandText, params object[] parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    DynamicParameters dparam = new DynamicParameters();
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        dparam.Add($"@param{i}", parameters[i]);
                    }
                    int result = connection.Execute(commandText, dparam, commandType: CommandType.StoredProcedure);
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
