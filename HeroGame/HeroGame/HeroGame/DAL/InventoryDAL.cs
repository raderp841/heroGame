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
        private string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        private const string SQL_CreateInventoryForHero = "insert into inventory values (0, @heroId)";
        DalHelpers dalHelper = new DalHelpers();

        public bool CreateInventory(int heroId)
        {
            Dictionary<string, object> injectionDictionary = new Dictionary<string, object>();
            injectionDictionary.Add("@heroId", heroId);

            return dalHelper.SqlForBool(injectionDictionary, SQL_CreateInventoryForHero, connectionString);
        }
    }
}