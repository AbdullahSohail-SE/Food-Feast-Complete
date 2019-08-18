using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Food_Feast.Models
{
    public class Category
    {
        public int totalItems { get; set; }
        public string name { get; set; }
    }
    public class Menu
    {
        public int totalItems { get; set; }
        public SqlDataReader menuItems { get; set; }

        public List<Category> categaryList = new List<Category>();
    }

    
}