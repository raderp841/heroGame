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
        bool CreateHero(HeroModel hero, int userInfoId);
        List<HeroModel> GetAllHeroesForUser(int userInfoId);
        bool UpdateHero(string column, object value, int id);
        bool DeleteHero(int id);
        HeroModel GetSingleHeroById(int id);
        HeroModel GetHeroByIdName(int userInfoId, string heroName);
        bool CheckHeroAvailability(int userInfoId, string heroName);
    }
}
