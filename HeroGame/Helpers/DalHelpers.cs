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
    public class DalHelper
    {
        readonly string connectionString;
        public DalHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool SqlForBool(Dictionary<string, object> injectionDictionary, string sql)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    foreach (var kvp in injectionDictionary)
                    {
                        cmd.Parameters.AddWithValue(kvp.Key, kvp.Value);
                    }
                    return (cmd.ExecuteNonQuery() > 0);
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public T SelectSingle<T>(string sql, Dictionary<string, object> injectionDictionary)
        {

            try
            {
                using (IDbConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var output = conn.QuerySingle<T>(sql, injectionDictionary);
                    return output;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public IList<T>  SelectList<T>(string sql)
        {
            return SelectList<T>(sql, null, null);
        }

        public IList<T> SelectList<T>(string sql, string conditionColumn = null, object conditionValue = null)
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

                    using (var multi = conn.QueryMultiple(sql, dictionary))
                    {
                        return multi.Read<T>().ToList<T>();                        
                    }
                }

            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public int GetCount(string sql, Dictionary<string, object> injectionDictionary)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    int output = conn.ExecuteScalar<int>(sql, injectionDictionary);

                    return output;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }
    }
}