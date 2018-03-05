using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeroGame.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace HeroGame.DAL
{
    public class HeroDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        private const string SQL_CreateHero = "Insert into hero values('true', @class, 1, @inventoryId, 100, @heroName)";
        private const string SQL_GetAllHeroes = "Select * from heroes where u"

        public bool CreateHero( HeroModel hero)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_CreateHero, conn);
                    cmd.Parameters.AddWithValue("@class", hero.Class);
                    cmd.Parameters.AddWithValue("@heroName", hero.HeroName);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return (rowsAffected > 0);
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
        }
    }
}