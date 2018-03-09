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
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_CheckEmail, conn);
                    cmd.Parameters.AddWithValue("@email", email);
                    int numberOfRows = (int)(cmd.ExecuteScalar());

                    return (numberOfRows == 0);
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
        public UserInfoModel SelectUser(string email)
        {            
            return dalHelper.SelectSingle<UserInfoModel>(SQL_SelectUser, connectionString, "email", email);         
        }

        public List<UserInfoModel> SelectAllUsers()
        {
            return dalHelper.SelectList<UserInfoModel>(SQL_SelectAllUsers, connectionString);
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