using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public class UserData
    {
        private static string conStr;
        private static object cmd;

        //private static bool local = false;
        private readonly DBServerices _db = DBServerices.GetDbServices();

        internal User Login(User user)
        {
            string sql = $@"select * from dbo.LoginUser('{user.Email}','{user.Password}')";
            SqlCommand cmd = _db.CreateCommand(sql);
            DataTable dt = _db.Select(cmd);
            return _db.ConvertDataTable<User>(dt)[0];
        }

     
        public List<User> GetUser()
        {
            string sql = "select * from User_Details";
            SqlCommand cmd = _db.CreateCommand(sql);
            DataTable dt = _db.Select(cmd);
            return _db.ConvertDataTable<User>(dt);
        }
        public List<User> GetUserByEmail(string email)
        {
            string sql = $"select * from User_Details where Email='{email}'";
            SqlCommand cmd = _db.CreateCommand(sql);
            DataTable dt = _db.Select(cmd);
            return _db.ConvertDataTable<User>(dt);
        }
        internal User AddUser(User user)
        {
            string sql = $"Exec AddUser '{user.Email}',N'{user.FirstName}','{user.Password}',N'{user.LastName}','{user.PhoneNumber}'";
            SqlCommand cmd = _db.CreateCommand(sql);
            int res = _db.ExecuteAndClose(cmd);
            if (res > 0)
            {
                return GetUserByEmail(user.Email)[0];
            }
            return null;
        }

    }
}