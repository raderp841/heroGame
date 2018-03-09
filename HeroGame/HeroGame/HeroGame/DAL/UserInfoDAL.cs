using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeroGame.Models;
using System.Data.SqlClient;
using System.Configuration;
using HeroGame.Helpers;

namespace HeroGame.DAL
{
    public class UserInfoDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        private const string SQL_SaveNewUser = "insert into userInfo values(@firstName, @lastName, @email, @password, null, 'false')";
        private const string SQL_CheckEmail = "select count(*) from userInfo where email = @email";
        private const string SQL_SelectUser = "select * from userInfo where email = @email";
        private const string SQL_SelectAllUsers = "select * from userInfo";

        DalHelpers dalHelper = new DalHelpers();

        public bool SaveNewUser(UserInfoModel user)
        {
            Dictionary<string, object> injectionDictionary = new Dictionary<string, object>();
            injectionDictionary.Add("@firstName", user.FirstName);
            injectionDictionary.Add("@lastName", user.LastName);
            injectionDictionary.Add("@email", user.Email);
            injectionDictionary.Add("@password", user.Password);

            return dalHelper.SqlForBool(injectionDictionary, SQL_SaveNewUser, connectionString);                 
        }

        public bool CheckAvailability(string email)
        {
            Dictionary<string, object> injectionDictionary = new Dictionary<string, object>();
            injectionDictionary.Add("@email", email);

            return dalHelper.SqlForBool(injectionDictionary, SQL_CheckEmail, connectionString);
        }

        public UserInfoModel SelectUser(string email)
        {            
            return dalHelper.SelectSingle<UserInfoModel>(SQL_SelectUser, connectionString, "email", email);         
        }

        public List<UserInfoModel> SelectAllUsers()
        {
            return dalHelper.SelectList<UserInfoModel>(SQL_SelectAllUsers, connectionString);
        }
    }
}