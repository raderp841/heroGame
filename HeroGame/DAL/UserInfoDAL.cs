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
        private const string SQL_SelectUserByEmail = "select * from userInfo where email = @email";
        private const string SQL_SelectUserById = "select * from userInfo where id = @id";
        private const string SQL_SelectAllUsers = "select * from userInfo";
        private const string SQL_UpdateUser = "update userInfo set @column = @value where id = @id";
        private const string SQL_DeleteUser = "delete from userInfo where id = @id";

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

            return dalHelper.CheckIfAvailable(SQL_CheckEmail, connectionString, injectionDictionary);
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(connectionString))
            //    {
            //        conn.Open();

            //        SqlCommand cmd = new SqlCommand(SQL_CheckEmail, conn);
            //        cmd.Parameters.AddWithValue("@email", email);
            //        int numberOfRows = (int)(cmd.ExecuteScalar());

            //        return (numberOfRows == 0);
            //    }
            //}
            //catch (SqlException ex)
            //{
            //    throw;
            //}
        }
        public UserInfoModel SelectUserByEmail(string email)
        {
            Dictionary<string, object> injectionDictionary = new Dictionary<string, object>();
            injectionDictionary.Add("email", email);

            return dalHelper.SelectSingle<UserInfoModel>(SQL_SelectUserByEmail, connectionString, injectionDictionary);         
        }

        public List<UserInfoModel> SelectAllUsers()
        {
            return dalHelper.SelectList<UserInfoModel>(SQL_SelectAllUsers, connectionString);
        }

        public UserInfoModel SelectUserById(int id)
        {
            Dictionary<string, object> injectionDictionary = new Dictionary<string, object>();
            injectionDictionary.Add("id", id);

            return dalHelper.SelectSingle<UserInfoModel>(SQL_SelectUserById, connectionString, injectionDictionary);
        }

        public bool UpdateUser(int id, string column, object value)
        {
            Dictionary<string, object> injectionDictionary = new Dictionary<string, object>();
            injectionDictionary.Add("@id", id);
            injectionDictionary.Add("@column", column);
            injectionDictionary.Add("@value", value);

            return dalHelper.SqlForBool(injectionDictionary, SQL_UpdateUser, connectionString);
        }

        public bool DeleteUser(int id)
        {
            Dictionary<string, object> injectionDictionary = new Dictionary<string, object>();
            injectionDictionary.Add("@id", id);

            return dalHelper.SqlForBool(injectionDictionary, SQL_DeleteUser, connectionString);
        }
    }
}