using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeroGame.Models;
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
        public object SelectingSingleObject(Dictionary<string, object> injectionDictionary, string sqlQuery, string connectionString, object returnType)
        {
            object output = returnType;

            PropertyInfo[] propertyInformation = returnType.GetType().GetProperties();

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

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        for (int i = 0; i < propertyInformation.Length; i++)
                        {
                            // output.propertyInformation[i] =  ------------------------CANT FIGURE OUT THIS PART
                        }
                    }
                    return output;
                }

            }
            catch (SqlException ex)
            {
                throw;
            }
            return output;
        }
        
        public object SelectSingle(string sqlQuerey, string connectionString, string conditionColumn, object conditionValue )
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add(conditionColumn, conditionValue);
            try
            {
                using (IDbConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var output = conn.QuerySingle<UserInfoModel>(sqlQuerey, dictionary);
                    return output;
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
        }
    }
    
}