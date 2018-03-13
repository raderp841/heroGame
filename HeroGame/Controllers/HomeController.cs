using System;
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
        private readonly IInventoryDAL inventoryDal;


        public HomeController(IUserInfoDAL userDal, IHeroDAL heroDal, IInventoryDAL inventoryDal)
        {
            this.userDal = userDal;
            this.heroDal = heroDal;
            this.inventoryDal = inventoryDal;
        }


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
            UserInfoModel modelUser = new UserInfoModel();
            UserInfo_HeroModel model = new UserInfo_HeroModel();
            IList<HeroModel> modelHeroes = new List<HeroModel>();

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

                    ViewBag.ErrorMessage = null;
                    ViewBag.LoginError = null;
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

                if (userDal.CheckAvailability(newUser.Email) == false)
                {
                    UserInfoModel user = userDal.SelectUserByEmail(newUser.Email);

                    if (user.Password == providedPassword)
                    {
                        ViewBag.LoginError = null;
                        ViewBag.ErrorMessage = null;
                        model.UsersInfo = userDal.SelectUserByEmail(newUser.Email);
                        modelHeroes = heroDal.GetAllHeroesForUser(model.UsersInfo.Id);
                        model.UsersHeroes = controllerMethods.CreateHeroInventoryDictionary(modelHeroes);
                        Session["User"] = model.UsersInfo;

                        return View("Game", model);
                    }
                }
                else
                {
                    ViewBag.LoginError = "Login or password was incorrect.";
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
            IList<UserInfoModel> model = userDal.SelectAllUsers();

            return View("AllUsers", model);
        }

        public ActionResult DeleteHero(int id = -1)
        {
            if (id == -1)
            {
            }
            else
            {
                HeroModel hero = heroDal.GetSingleHeroById(id);
                InventoryModel inventory = inventoryDal.GetInventoryByHeroId(id);
                inventoryDal.DeleteInventory(inventory.Id);
                heroDal.DeleteHero(id);
            }
            return RedirectToAction("Game");
        }
    }
}