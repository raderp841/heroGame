using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeroGame.Models;
using HeroGame.DAL;
using System.Configuration;

namespace HeroGame.Classes
{
    public class ControllerMethods
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

       

        //public UserInfo_HeroModel GetModelforGame(int userId, string className = null, string heroName = null)
        //{
            
        //    ControllerMethods controllerMethods = new ControllerMethods();
        //    HeroDAL heroDal = new HeroDAL(connectionString);
        //    InventoryDAL inventoryDAL = new InventoryDAL(connectionString);
        //    UserInfoDAL userInfoDAL = new UserInfoDAL(connectionString);
        //    HeroModel heroModel = new HeroModel();
        //    InventoryModel inventoryModel = new InventoryModel();
        //    UserInfo_HeroModel model = new UserInfo_HeroModel();
        //    UserInfoModel userInfoModel = new UserInfoModel();
        //    IList<HeroModel> list = new List<HeroModel>();
        //    Dictionary<HeroModel, InventoryModel> dictionary = new Dictionary<HeroModel, InventoryModel>();

        //    if (heroName != null && className != null)
        //    {
        //        heroModel.Class = className;
        //        heroModel.HeroName = heroName;
        //        heroModel.UserInfoId = userId;
        //        heroDal.CreateHero(heroModel, userId);
        //        heroModel = heroDal.GetHeroByIdName(userId, heroName);
        //        inventoryDAL.CreateInventory(heroModel.Id);
        //    }

        //    userInfoModel = userInfoDAL.SelectUserById(userId);       
        //    list = heroDal.GetAllHeroesForUser(userId);

        //    dictionary = controllerMethods.CreateHeroInventoryDictionary(list);

        //    model.UsersHeroes = dictionary;
        //    model.UsersInfo = userInfoModel;

        //    return model;
        //}
    }
}