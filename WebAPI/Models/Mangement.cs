using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Mangement
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public int IsActive { get; set; }
    }
}