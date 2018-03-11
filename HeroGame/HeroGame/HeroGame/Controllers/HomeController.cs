using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeroGame.DAL;
using HeroGame.Models;
using HeroGame.Classes;

namespace HeroGame.Controllers
{
    public class HomeController : Controller
    {
        ControllerMethods controllerMethods = new ControllerMethods();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginRegister()
        {
            if (Session["User"] != null)
            {
                Session["User"] = null;

                return RedirectToAction("Index");
            }
            else
            {
                UserInfoModel model = new UserInfoModel();

                return View("LoginRegister", model);
            }
        }
        [HttpPost]
        public ActionResult LoginRegister(UserInfoModel newUser, int logCode)
        {
            UserInfoDAL userDal = new UserInfoDAL();
            HeroDAL heroDal = new HeroDAL();
            UserInfoModel modelUser = new UserInfoModel();
            UserInfo_HeroModel model = new UserInfo_HeroModel();
            List<HeroModel> modelHeroes = new List<HeroModel>();

            ViewBag.ErrorMessage = null;
            if (logCode == 0)
            {
                if (ModelState.IsValid)
                {
                    if (userDal.CheckAvailability(newUser.Email) == false)
                    {
                        ViewBag.ErrorMessage = "Email has already been taken";

                        return View("LoginRegister");

                    }
                    userDal.SaveNewUser(newUser);
                    modelUser = userDal.SelectUserByEmail(newUser.Email);
                    model.UsersInfo = modelUser;
                    modelHeroes = heroDal.GetAllHeroesForUser(modelUser.Id);
                    model.UsersHeroes = controllerMethods.CreateHeroInventoryDictionary(modelHeroes);
                    Session["User"] = modelUser;

                    return View("Game", model);
                }
                else
                {
                    return View("LoginRegister");
                }
            }
            if (logCode == 1)
            {
                string providedPassword = newUser.Password;
                UserInfoModel user = userDal.SelectUserByEmail(newUser.Email);

                if (user.Password == providedPassword)
                {
                    model.UsersInfo = userDal.SelectUserByEmail(newUser.Email);
                    modelHeroes = heroDal.GetAllHeroesForUser(model.UsersInfo.Id);
                    model.UsersHeroes = controllerMethods.CreateHeroInventoryDictionary(modelHeroes);
                    Session["User"] = model.UsersInfo;

                    return View("Game", model);
                }
                else
                {
                    return View("LoginRegister");
                }
            }
            return View("LoginRegister");
        }
        public ActionResult Game()
        {
            if (Session["User"] != null)
            {
                UserInfoModel user = (UserInfoModel)Session["User"];
                UserInfo_HeroModel model = controllerMethods.GetModelforGame(user.Id);
                return View("Game", model);
            }
            return RedirectToAction("LoginRegister");
        }

        [HttpPost]
        public ActionResult Game(string className, string heroName, int userId)
        {
            HeroDAL heroDal = new HeroDAL();
            UserInfo_HeroModel model = new UserInfo_HeroModel();

            if (heroDal.CheckHeroAvailability(userId, heroName))
            {
                model = controllerMethods.GetModelforGame(userId, className, heroName);
                return View("Game", model);
            }
            model = controllerMethods.GetModelforGame(userId);

            return View("Game", model);
        }

        public ActionResult AllUsers()
        {
            UserInfoDAL dal = new UserInfoDAL();
            List<UserInfoModel> model = dal.SelectAllUsers();

            return View("AllUsers", model);
        }

        public ActionResult DeleteHero(int id = -1)
        {
            if(id == -1)
            {
            }
            else
            {
                HeroDAL heroDAL = new HeroDAL();
                InventoryDAL inventoryDAL = new InventoryDAL();
                HeroModel hero = heroDAL.GetSingleHeroById(id);
                InventoryModel inventory = inventoryDAL.GetInventoryByHeroId(id);
                inventoryDAL.DeleteInventory(inventory.Id);
                heroDAL.DeleteHero(id);
            }
            return RedirectToAction("Game");
        }
    }
}