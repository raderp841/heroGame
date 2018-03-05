using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HeroGame.Models
{
    public class HeroModel
    {
        public int Id { get; set; }
        public bool IsALive { get; set; }
        public string Class { get; set; }
        public int Lvl { get; set; }
        public int InventoryId { get; set; }
        public int Health { get; set; }
        public string HeroName { get; set; }
        public int UserId { get; set; }
    }
}