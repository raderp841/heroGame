using HeroGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroGame.DAL
{
    public interface IInventoryDAL
    {
        bool CreateInventory(int heroId);
        InventoryModel GetInventory(int id);
        InventoryModel GetInventoryByHeroId(int heroesId);
        bool DeleteInventory(int id);
        bool UpdateInventory(int id);
    }
}
