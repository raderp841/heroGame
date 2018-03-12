using HeroGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroGame.DAL
{
    public interface IUserInfoDAL
    {
        bool SaveNewUser(UserInfoModel user);
        bool CheckAvailability(string email);
        UserInfoModel SelectUserByEmail(string email);
        List<UserInfoModel> SelectAllUsers();
        UserInfoModel SelectUserById(int id);
        bool UpdateUser(int id, string column, object value);
        bool DeleteUser(int id);
    }
}
