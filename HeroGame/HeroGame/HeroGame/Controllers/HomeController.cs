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

            UserInfoModel model = new UserInfoModel();
            return View("LoginRegister", model);
        }
        [HttpPost]
        public ActionResult LoginRegister(UserInfoModel newUser)
        {
            ViewBag.ErrorMessage = null;

            if (ModelState.IsValid)
            {
                UserInfoDAL dal = new UserInfoDAL();
                if (dal.CheckAvailability(newUser.Email) == false)
                {
                    ViewBag.ErrorMessage = "Email has already been taken";
                    return View("LoginRegister");

                }
                dal.SaveNewUser(newUser);
                return View("Index");
            }
            else
            {
                return View("LoginRegister");
            }
        }
            public ActionResult Contact()
            {
                ViewBag.Message = "Your contact page.";

                return View();
            }
        }
    }