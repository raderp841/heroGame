﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeroGame.DAL;
using HeroGame.Models;
using HeroGame.Classes;
using System.Configuration;

namespace HeroGame.Controllers
{
    public class HomeController : Controller
    {
        ControllerMethods controllerMethods = new ControllerMethods();
        private readonly IUserInfoDAL userDal;
        private readonly IHeroDAL heroDal;        


        public HomeController(IUserInfoDAL userDal, IHeroDAL heroDal)
        {
            this.userDal = userDal;
            this.heroDal = heroDal;            
        }


        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Game()
        {
            if (Session["User"] != null)
            {
                UserInfoModel user = Session["User"] as UserInfoModel;
                var heroes = heroDal.GetAllHeroesForUser(user.Id);
                var model = new UserInfo_HeroModel()
                {
                    User = user,
                    Heroes = heroes
                };
                                
                return View("Game", model);
            }

            return RedirectToAction("LoginRegister");
        }

        [HttpPost]
        public ActionResult Game(string className, string heroName, int userId)
        {            
            var hero = new HeroModel()
            {
                Class = className,
                HeroName = heroName,                
            };
            heroDal.CreateHero(hero, userId);
            

            return RedirectToAction("Game");            
        }

        public ActionResult AllUsers()
        {
            IList<UserInfoModel> model = userDal.SelectAllUsers();

            return View("AllUsers", model);
        }

        public ActionResult DeleteHero(int id = -1)
        {
            if (id != -1)
            {                
                heroDal.DeleteHero(id);
            }
            return RedirectToAction("Game");
        }

        public ActionResult InGame()
        {
            return View("InGame");
        }
    }
}