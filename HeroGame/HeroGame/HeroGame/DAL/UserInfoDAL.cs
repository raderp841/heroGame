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

        public bool CheckAvailability(string email)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd2 = new SqlCommand(SQL_CheckEmail, conn);
                    cmd2.Parameters.AddWithValue("@email", email);
                    int numberOfRows = (int)(cmd2.ExecuteScalar());

                    if (numberOfRows > 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }

                }
            }
            catch(SqlException ex)
            {
                throw;
            }
        }

        public UserInfoModel SelectUser(string email)
        {
            UserInfoModel output = new UserInfoModel();

            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_SelectUser, conn);
                    cmd.Parameters.AddWithValue("@email", email);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        output.Id = Convert.ToInt32(reader["id"]);
                        output.FirstName = Convert.ToString(reader["firstName"]);
                        output.LastName = Convert.ToString(reader["lastName"]);
                        output.Email = Convert.ToString(reader["email"]);
                        output.Password = Convert.ToString(reader["password"]);
                        output.IsAdmin = Convert.ToBoolean(reader["isAdmin"]);
                    }

                    
                }
            }
            catch(SqlException ex)
            {
                throw;
            }

            return output;
        }
    }
}