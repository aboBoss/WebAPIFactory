using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class User
    {
        public int id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public User()
        {

        }
        public User(int id, string Email, string FirstName, string Password, string LastName, string PhoneNumber)
        {
            this.id = id;
            this.Email = Email;
            this.FirstName = FirstName;
            this.Password = Password;
            this.LastName = LastName;
            this.PhoneNumber = PhoneNumber;


        }
    }
}