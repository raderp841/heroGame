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
        private const string SQL_CreateHero = "Insert into heroes values(@class, 1, 100, @heroName, @userInfoId)";
        private const string SQL_GetAllHeroes = "Select * from heroes where userInfoId = @userInfoId";
        private const string SQL_UpdateHero = "Update heroes set @column = @value where id = @id";
        private const string SQL_DeleteHero = "Delete from heroes where id = @id";
        private const string SQL_GetSingleHero = "Select * from heroes where id = @id";
        private const string SQL_CheckHeroName = "select count(*) from heroes where userInfoId = @userInfoId and heroName = @heroName";
        private const string SQL_GetHeroByName = "select * from heroes where userInfoId = @userInfoId and heroName = @heroName";
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
            return dalHelper.SelectList<HeroModel>(SQL_GetAllHeroes, connectionString, "userInfoId", userInfoId);
        }

        public bool UpdateHero(string column, object value, int id)
        {
            Dictionary<string, object> injectionDictionary = new Dictionary<string, object>();
            injectionDictionary.Add("@column", column);
            injectionDictionary.Add("@value", value);
            injectionDictionary.Add("@id", id);

            return dalHelper.SqlForBool(injectionDictionary, SQL_UpdateHero, connectionString);
        }

        public bool DeleteHero(int id)
        {
            Dictionary<string, object> injectionDictionary = new Dictionary<string, object>();
            injectionDictionary.Add("@id", id);

            return dalHelper.SqlForBool(injectionDictionary, SQL_DeleteHero, connectionString);
        }

        public HeroModel GetSingleHeroById(int id)
        {
            Dictionary<string, object> injectionDictionary = new Dictionary<string, object>();
            injectionDictionary.Add("id", id);

            return dalHelper.SelectSingle<HeroModel>(SQL_GetSingleHero, connectionString, injectionDictionary);
        }

        public HeroModel GetHeroByIdName(int userInfoId, string heroName)
        {
            Dictionary<string, object> injectionDictionary = new Dictionary<string, object>();
            injectionDictionary.Add("userInfoId", userInfoId);
            injectionDictionary.Add("heroName", heroName);

            return dalHelper.SelectSingle<HeroModel>(SQL_GetHeroByName, connectionString, injectionDictionary);
        }

        public bool CheckHeroAvailability(int userInfoId, string heroName)
        {
            Dictionary<string, object> injectionDictionary = new Dictionary<string, object>();
            injectionDictionary.Add("@userInfoId", userInfoId);
            injectionDictionary.Add("@heroName", heroName);

            return dalHelper.CheckIfAvailable(SQL_CheckHeroName, connectionString, injectionDictionary);   
        }
    }
}