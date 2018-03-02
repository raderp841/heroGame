using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeroGame.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace HeroGame.DAL
{
    public class UserInfoDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        private const string SQL_SaveNewUser = "insert into userInfo values(@firstName, @lastName, @email, @password, null, 'false')";
        private const string SQL_CheckEmail = "select count(*) from userInfo where email = @email";
        private const string SQL_SelectUser = "select * from userInfo where email = @email";

        public bool SaveNewUser(UserInfoModel user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd2 = new SqlCommand(SQL_CheckEmail, conn);
                    cmd2.Parameters.AddWithValue("@email", user.Email);
                    int numberOfRows = (int)(cmd2.ExecuteScalar());

                    if(numberOfRows > 0)
                    {
                        return false;
                    }

                    SqlCommand cmd = new SqlCommand(SQL_SaveNewUser, conn);
                    cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", user.LastName);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return (rowsAffected > 0);

                }
            }
            catch(SqlException ex)
            {
                throw;
            }
        }
    }
}