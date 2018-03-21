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
        public UserInfoModel User { get; set; }
        public IList<HeroModel> Heroes { get; set; } = new List<HeroModel>();
    }
}