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


        public HomeController(IUserInfoDAL userDal, IHeroDAL heroDal)
        {
            this.userDal = userDal;
            this.heroDal = heroDal;            
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
                    model.User = modelUser;
                    modelHeroes = heroDal.GetAllHeroesForUser(modelUser.Id);
                    
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
                        model.User = userDal.SelectUserByEmail(newUser.Email);
                        modelHeroes = heroDal.GetAllHeroesForUser(model.User.Id);                        
                        Session["User"] = model.User;

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