using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;
using System.Data;
using Dapper;

namespace HeroGame.Helpers
{
    public class DalHelpers
    {
        public bool SqlForBool(Dictionary<string, object> injectionDictionary, string sqlQuery, string connectionString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlQuery, conn);

                    foreach (var kvp in injectionDictionary)
                    {
                        cmd.Parameters.AddWithValue(kvp.Key, kvp.Value);
                    }
                    return (cmd.ExecuteNonQuery() > 0);
                }
            }         
            catch(SqlException ex)
            {
                throw;
            }
        }
        
        public dynamic SelectSingle<T>(string sqlQuerey, string connectionString, Dictionary<string,object> injectionDictionary)
        {
            
            try
            {
                using (IDbConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var output = conn.QuerySingle<T>(sqlQuerey, injectionDictionary);
                    return output;
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
        }

        public dynamic SelectList<T>(string sqlQuerey, string connectionString, string conditionColumn = null, object conditionValue = null)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            if (conditionColumn != null || conditionValue != null)
            { 
            dictionary.Add(conditionColumn, conditionValue);
            }
            try
            {
                using (IDbConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (var multi = conn.QueryMultiple(sqlQuerey, dictionary))
                    {
                        var invoiceItems = multi.Read<T>();
                        return invoiceItems;
                    }                  
                }
            
            }
            catch(SqlException ex)
            {
                throw;
            }
        }

        public bool CheckIfAvailable(string sqlQuery, string connectionString, Dictionary<string,object> injectionDictionary)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    int output = conn.ExecuteScalar<int>(sqlQuery, injectionDictionary);

                    return (output == 0);
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }
    }
    
}