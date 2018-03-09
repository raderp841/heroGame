using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeroGame.DAL;
using HeroGame.Models;

namespace HeroGame.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginRegister()
        {
            if(Session["User"] != null)
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
                    modelUser = userDal.SelectUser(newUser.Email);
                    model.UsersInfo = modelUser;
                    modelHeroes = heroDal.GetAllHeroesForUser(modelUser.Id);
                    Session["User"] = modelUser;
                    return View("Game", model);
                }
                else
                {
                    return View("LoginRegister");
                }
            }
            if(logCode == 1)
            {
                string providedPassword = newUser.Password;
                UserInfoModel user = userDal.SelectUser(newUser.Email);

                if(user.Password == providedPassword)
                {
                    model.UsersInfo = userDal.SelectUser(newUser.Email);
                    model.UsersHeroes = heroDal.GetAllHeroesForUser(model.UsersInfo.Id);
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
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Game(string className, string heroName, int userId)
        {
            return View();
        }

        public ActionResult AllUsers()
        {
            UserInfoDAL dal = new UserInfoDAL();
            List<UserInfoModel> model = dal.SelectAllUsers();

            return View("AllUsers", model);
        }
    }
}