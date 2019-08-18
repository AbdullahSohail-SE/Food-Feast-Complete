using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Food_Feast.Models
{
    public class Address
    {
        public string area { get; set; }
        public string street { get; set; }
        public string building { get; set; }
        public string floor { get; set; }
    }

    public class Customer:Address
    {
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string message { get; set; }
    }

    public class CustomerValidate : Customer
    {
        public string final { get; set; }
    }
}