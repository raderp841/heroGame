using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeroGame.Models;
using System.Data.SqlClient;
using System.Configuration;
using HeroGame.Helpers;

namespace HeroGame.DAL
{
    public class HeroDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        private const string SQL_CreateHero = "Insert into hero values(@class, 1, 100, @heroName, @userInfoId)";
        private const string SQL_GetAllHeroes = "Select * from heroes where userInfoId = @userInfoId";
        DalHelpers dalHelper = new DalHelpers();
        public bool CreateHero( HeroModel hero, int userInfoId)
        {
            Dictionary<string, object> injectionDictionary = new Dictionary<string, object>();
            injectionDictionary.Add("@class", hero.Class);
            injectionDictionary.Add("@heroName", hero.HeroName);
            injectionDictionary.Add("@userInfoId", userInfoId);


           return dalHelper.SqlForBool(injectionDictionary, SQL_CreateHero, connectionString);
            
        }
        public List<HeroModel> GetAllHeroesForUser(int userInfoId)
        {
            List<HeroModel> heroes = new List<HeroModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllHeroes, conn);
                    cmd.Parameters.AddWithValue("@userInfoId", userInfoId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        HeroModel hero = new HeroModel();
                        hero.Class = Convert.ToString(reader["class"]);
                        hero.Health = Convert.ToInt32(reader["health"]);
                        hero.HeroName = Convert.ToString(reader["heroName"]);
                        hero.Id = Convert.ToInt32(reader["id"]);
                        hero.InventoryId = Convert.ToInt32(reader["inventoryId"]);
                        hero.Lvl = Convert.ToInt32(reader["lvl"]);
                        hero.UserInfoId = Convert.ToInt32(reader["userInfoId"]);

                        heroes.Add(hero);
                    }
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            return heroes;
        }
    }
}