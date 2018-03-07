using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeroGame.Models;
using System.Data.SqlClient;
using System.Configuration;

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
    }
}