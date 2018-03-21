using HeroGame.DAL;
using HeroGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeroGame.Controllers
{
    public class UserController : Controller
    {
        private const int Register = 0;
        private const int Login = 1;
        private readonly IUserInfoDAL userInfoDAL;

        public UserController(IUserInfoDAL userInfoDAL)
        {
            this.userInfoDAL = userInfoDAL;
        }


        [Route("login")]
        public ActionResult LoginRegister()
        {
            if (Session["User"] != null)
            {
                return RedirectToAction("Game", "Home");
            }
            else
            {
                UserInfoModel model = new UserInfoModel();
                return View("LoginRegister", model);
            }
        }

        [HttpPost]
        [Route("login")]
        public ActionResult LoginRegister(UserInfoModel newUser, int logCode)
        {
            if (logCode == Register)
            {
                if (ModelState.IsValid)
                {
                    if (!userInfoDAL.CheckAvailability(newUser.Email))
                    {
                        ViewBag.ErrorMessage = "Email has already been taken";
                        return View("LoginRegister");
                    }

                    userInfoDAL.SaveNewUser(newUser);
                    var user = userInfoDAL.SelectUserByEmail(newUser.Email);
                    Session["User"] = user;

                    return RedirectToAction("Game", "Home");
                }
                else
                {
                    return View("LoginRegister");
                }
            }
            if (logCode == Login)
            {
                string providedPassword = newUser.Password;

                if (userInfoDAL.CheckAvailability(newUser.Email) == false)
                {
                    UserInfoModel user = userInfoDAL.SelectUserByEmail(newUser.Email);

                    if (user.Password == providedPassword)
                    {
                        Session["User"] = user;
                        return RedirectToAction("Game", "Home");
                    }
                }
                else
                {
                    ViewBag.LoginError = "Login or password was incorrect.";
                }
            }
            return View("LoginRegister");
        }

    }
}