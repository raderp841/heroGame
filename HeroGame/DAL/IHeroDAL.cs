using HeroGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroGame.DAL
{
    public interface IHeroDAL
    {
        IList<HeroModel> GetAllHeroesForUser(int userInfoId);
        HeroModel GetHero(int id);        

        bool CreateHero(HeroModel hero, int userInfoId);
        bool UpdateHero(string column, object value, int id);
        bool DeleteHero(int id);
        
    }
}
