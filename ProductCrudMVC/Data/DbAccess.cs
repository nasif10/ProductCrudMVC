using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace ProductCrudMVC.Data
{
    public class DbAccess
    {
        private readonly string _connectionString;
        public DbAccess()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
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
                    return connection.QuerySingle<T>(commandText, dparam, commandType: CommandType.StoredProcedure);
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("No row found." + ex.Message);
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