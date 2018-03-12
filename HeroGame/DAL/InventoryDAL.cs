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
    public class InventoryDAL
    {

        private const string SQL_CreateInventoryForHero = "insert into inventory values (0, @heroId)";
        private const string SQL_GetInventoryForHero = "select * from inventory where id = @id";
        private const string SQL_DeleteInventoryForHero = "delete from inventory where id = @id";
        private const string SQL_UpdateInventoryForHero = "update inventory set @column = @value where = @id";
        private const string SQL_GetInventoryByHeroId = "select * from inventory where heroesId = @heroesId";
        private readonly DalHelper dalHelper;

        public InventoryDAL(string connectionString)
        {
            dalHelper = new DalHelper(connectionString);
        }

        public bool CreateInventory(int heroId)
        {
            Dictionary<string, object> injectionDictionary = new Dictionary<string, object>();
            injectionDictionary.Add("@heroId", heroId);

            return dalHelper.SqlForBool(injectionDictionary, SQL_CreateInventoryForHero);
        }
        public InventoryModel GetInventory(int id)
        {
            Dictionary<string, object> injectionDictionary = new Dictionary<string, object>();
            injectionDictionary.Add("id", id);

            return dalHelper.SelectSingle<InventoryModel>(SQL_GetInventoryForHero, injectionDictionary);
        }
        public InventoryModel GetInventoryByHeroId(int heroesId)
        {
            Dictionary<string, object> injectionDictionary = new Dictionary<string, object>();
            injectionDictionary.Add("heroesId", heroesId);

            return dalHelper.SelectSingle<InventoryModel>(SQL_GetInventoryByHeroId, injectionDictionary);
        }

        public bool DeleteInventory(int id)
        {
            Dictionary<string, object> injectionDictionary = new Dictionary<string, object>();
            injectionDictionary.Add("@id", id);

            return dalHelper.SqlForBool(injectionDictionary, SQL_DeleteInventoryForHero);
        }
        public bool UpdateInventory(int id)
        {
            Dictionary<string, object> injectionDictionary = new Dictionary<string, object>();
            injectionDictionary.Add("@id", id);

            return dalHelper.SqlForBool(injectionDictionary, SQL_UpdateInventoryForHero);
        }


    }
}