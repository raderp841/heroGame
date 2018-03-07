using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeroGame.Models;
using System.ComponentModel.DataAnnotations;

namespace HeroGame.Models
{
    public class UserInfo_HeroModel
    {
        public List<HeroModel> UsersHeroes { get; set; }
        public UserInfoModel UsersInfo { get; set; }
    }
}