using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Orders
    {
        public int OrderId { get; set; }
        public string ClientName { get; set; }
        public string Products { get; set; }
        public int Weight { get; set; }
        public int Price { get; set; }
    }
}