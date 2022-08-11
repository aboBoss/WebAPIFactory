using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.DAL;
using WebAPI.Models;

namespace WebAPI.BAL
{
    public class UserBAL
    {
        private readonly UserData _data = new UserData();
        public List<User> GetAllUserFromDb()
        {
            /*
             DAL פעולה שתפעיל את הפעולה המתאימה בשכבת ה 
             DAL משתמשים בשיטה זו כדי לבצע פעולות על המידע שנשלף מחוץ לשכבת ה
            */
            return _data.GetUser();
        }

        internal User AddUser(User user)
        {
            return _data.AddUser(user);
        }


        internal User Login(User user)
        {
            return _data.Login(user);
        }   
    }
}