using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HeroGame.Models
{
    public class HeroModel
    {
        /// <summary>
        /// Hero Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Class of the Hero
        /// </summary>
        public string Class { get; set; }                

        /// <summary>
        /// Available Health
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Hero's name
        /// </summary>
        public string HeroName { get; set; }

        /// <summary>
        /// Remaining wallet value.
        /// </summary>
        public int Coins { get; set; }

        /// <summary>
        /// Referenced User Id
        /// </summary>
        public int UserInfoId { get; set; }
    }
}