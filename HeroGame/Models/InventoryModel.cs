using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeroGame.Models
{
    public class InventoryModel
    {
        public int Id { get; set; }
        public int Coins { get; set; }
        public int HeroesId { get; set; }
    }
}