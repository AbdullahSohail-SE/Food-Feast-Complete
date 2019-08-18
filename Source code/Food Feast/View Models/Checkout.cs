using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Food_Feast.Models;

namespace Food_Feast.ViewModels
{
    public class Checkout
    {
        public Customer customer=new Customer();
        public SqlDataReader items { get; set; }
    }
}