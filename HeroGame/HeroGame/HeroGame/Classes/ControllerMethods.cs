﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeroGame.Models;
using HeroGame.DAL;

namespace HeroGame.Classes
{
    public class ControllerMethods
    {
        public Dictionary<HeroModel, InventoryModel> CreateHeroInventoryDictionary(List<HeroModel> heroes)
        {
            InventoryDAL inventoryDAL = new InventoryDAL();
            Dictionary<HeroModel, InventoryModel> output = new Dictionary<HeroModel, InventoryModel>();
            HeroModel hero = new HeroModel();
            InventoryModel inventory = new InventoryModel();

            for(int i = 0; i < heroes.Count; i++)
            {
                hero = heroes[i];
                inventory = inventoryDAL.GetInventoryByHeroId(hero.Id);
                output.Add(hero, inventory);
            }

            return output;
        }

        public UserInfo_HeroModel GetModelforGame(int userId, string className = null, string heroName = null)
        {
            
            ControllerMethods controllerMethods = new ControllerMethods();
            HeroDAL heroDal = new HeroDAL();
            InventoryDAL inventoryDAL = new InventoryDAL();
            UserInfoDAL userInfoDAL = new UserInfoDAL();
            HeroModel heroModel = new HeroModel();
            InventoryModel inventoryModel = new InventoryModel();
            UserInfo_HeroModel model = new UserInfo_HeroModel();
            UserInfoModel userInfoModel = new UserInfoModel();
            List<HeroModel> list = new List<HeroModel>();
            Dictionary<HeroModel, InventoryModel> dictionary = new Dictionary<HeroModel, InventoryModel>();

            if (heroName != null && className != null)
            {
                heroModel.Class = className;
                heroModel.HeroName = heroName;
                heroModel.UserInfoId = userId;
                heroDal.CreateHero(heroModel, userId);
                heroModel = heroDal.GetHeroByIdName(userId, heroName);
                inventoryDAL.CreateInventory(heroModel.Id);
            }

            userInfoModel = userInfoDAL.SelectUserById(userId);       
            list = heroDal.GetAllHeroesForUser(userId);

            dictionary = controllerMethods.CreateHeroInventoryDictionary(list);

            model.UsersHeroes = dictionary;
            model.UsersInfo = userInfoModel;

            return model;
        }
    }
}