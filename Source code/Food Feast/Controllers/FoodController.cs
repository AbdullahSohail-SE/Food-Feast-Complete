using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using  System.Data.SqlClient;
using System.Net;
using System.Web.UI.WebControls;
using Food_Feast.Models;
using Food_Feast.ViewModels;
using System.Net.Mail;

namespace Food_Feast.Controllers
{
    public class FoodController : Controller
    {
        // GET: Food
        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Home(string payment,string building,string street,string area,string floor,string phone)
        {
            if (payment != string.Empty)
            {
                var db =new DB();

                SqlParameter Customer = new SqlParameter("@customerId", SqlDbType.Int);
                Customer.Value = System.Web.HttpContext.Current.Session["loginId"];

                SqlParameter Payment = new SqlParameter("@payment_method",SqlDbType.NVarChar);
                Payment.Value = payment;

                var sqlParameter = new SqlParameter[2] { Customer, Payment };
                int affected = db.ExeccuteCommandAffected("Create_order", sqlParameter);

                if (affected >= 2)
                {
                    SqlParameter CustomerId = new SqlParameter("@customerId", SqlDbType.Int);
                    CustomerId.Value = System.Web.HttpContext.Current.Session["loginId"];
                    SqlParameter Building = new SqlParameter("@building", SqlDbType.NVarChar);
                    Building.Value = building;
                    SqlParameter Street = new SqlParameter("@street", SqlDbType.NVarChar);
                    Street.Value = street;
                    SqlParameter Area = new SqlParameter("@area", SqlDbType.NVarChar);
                    Area.Value = area;
                    SqlParameter Floor = new SqlParameter("@floor", SqlDbType.Int);
                    Floor.Value = floor;
                    SqlParameter Phone = new SqlParameter("@phone", SqlDbType.NVarChar);
                    Phone.Value = phone;

                    var sqlParameterAddress = new SqlParameter[6] { CustomerId, Building, Street, Area, Floor, Phone };
                    db.ExeccuteCommandAffected("update_customer_address", sqlParameterAddress);

                    int deleted = db.ExeccuteCommandAffectedWithoutParameter("delete_all_from_cart");
                    if(deleted>=1)
                    System.Web.HttpContext.Current.Session["itemsInCart"] = 0;

                    /*SqlParameter CustomerMailTo = new SqlParameter("@customerId", SqlDbType.Int);
                    CustomerMailTo.Value = System.Web.HttpContext.Current.Session["loginId"];
                    var sqlParameterMail = new SqlParameter[1]{ CustomerMailTo };

                    var email=db.ExeccuteCommandReader("select_customer_email_from_id", sqlParameterMail);
                    if (email.HasRows)
                    {
                        email.Read();
                        var to = new MailAddress(email[0].ToString(),
                            System.Web.HttpContext.Current.Session["loginName"].ToString());

                        var from = new MailAddress("abaig4211@gmail.com", "Food Feast");

                        MailMessage mailMessage = new MailMessage(from, to)
                        {
                            Subject = "Food Feast",
                            Body = "Your order has been recieved"
                        };

                        var smtp = new SmtpClient("smtp.gmail.com",587)
                        {
                            //DeliveryMethod = SmtpDeliveryMethod.Network,
                            //UseDefaultCredentials = true,
                            Credentials = new  NetworkCredential("abaig4211@gmail.com", ""),
                            EnableSsl = true
                        };
                        smtp.Send(mailMessage);   
                    }*/

                    return View();
                }
                else
                {
                    return RedirectToAction("Checkout");
                }
            }
            else
            {
                return RedirectToAction("Checkout");
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Signout()
        {
            System.Web.HttpContext.Current.Session["loggedIn"] = false;
            System.Web.HttpContext.Current.Session["loginName"] = "";

            return View("Home");
            
        }

        [HttpPost]
        public ActionResult Login(string phone, string password)
        {
            var db = new DB();
            var validationError = new CustomerValidate();

            if (phone!=String.Empty && password!=String.Empty)
            {
                var Phone = new SqlParameter("@phone", SqlDbType.NVarChar);
                Phone.Value = phone;
                var sqlParameter = new SqlParameter[1] {Phone};

                var data = db.ExeccuteCommandReader("select_login_customer", sqlParameter);

                if (data.HasRows)
                {
                    data.Read();

                    if (data["phone"].ToString() == phone && data["password"].ToString() == password)
                    {
                        var customer = new Customer{ name = data["name"].ToString() };
                        System.Web.HttpContext.Current.Session["loggedIn"] = true;
                        System.Web.HttpContext.Current.Session["loginId"] = int.Parse(data["customer_id"].ToString());
                        System.Web.HttpContext.Current.Session["loginName"] = data["name"].ToString();
                        return View("Home", customer);
                    }

                    else if (data["phone"].ToString() == phone && data["password"].ToString() != password)
                    {
                        validationError.password = "Incorrect password";
                        return View("Login", validationError);
                    }
                    else
                    {
                        validationError.phone = "Incorrect phone number";
                        validationError.password = "Incorrect password";
                        return View("Login", validationError);
                    }
                }
                else
                {
                    validationError.final = "First register yourself";
                    return View("Login", validationError);
                }
            }
            else
            {
                validationError.final = "Empty phone number or password";
                return View("Login", validationError);
            }
        }

        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(string name, string phone, string email, string password, string retypePassword, string building, string street, string area, string floor)
        {
            var db = new DB();
            var validationError = new CustomerValidate();

            if (phone != string.Empty && password!=string.Empty && retypePassword!=string.Empty)
            {
                int invalids=0;

                if (phone.Length != 11)
                {
                    validationError.phone = "Phone length must be of 11 digits";
                    ++invalids;
                }

                if (email != string.Empty)
                {
                    if (email.Contains("&") && email.Contains(".com"))
                    { 
                        validationError.email = "Incorrect email";
                    ++invalids;
                    }
                }

                if (retypePassword != password)
                {
                    validationError.password = "Password doesn't match";
                    ++invalids;
                }

                if (invalids > 0)
                    return View(validationError);

                else
                {
                    var Name = new SqlParameter("@name", SqlDbType.NVarChar);
                    Name.Value = name;
                    var Phone = new SqlParameter("@phone", SqlDbType.NVarChar);
                    Phone.Value = phone;
                    var Password = new SqlParameter("@password", SqlDbType.NVarChar);
                    Password.Value = password;
                    var Email = new SqlParameter("@email", SqlDbType.NVarChar);
                    Email.Value = email;
                    var Building = new SqlParameter("@building", SqlDbType.NVarChar);
                    Building.Value = building;
                    var Street = new SqlParameter("@street", SqlDbType.NVarChar);
                    Street.Value = street;
                    var Area = new SqlParameter("@area", SqlDbType.NVarChar);
                    Area.Value = area;
                    var Floor = new SqlParameter("@floor", SqlDbType.NVarChar);
                    Floor.Value = floor;

                    var sqlParameter = new SqlParameter[8] { Name, Phone, Password, Email, Building, Street, Area, Floor };

                    int rowInsert = db.ExeccuteCommandAffected("insert_customer", sqlParameter);

                    if(rowInsert==2)
                        return View("Login");
                    else
                        return View("Signup", validationError);
                }
            }
            else
            {
                validationError.final = "Enter above fields properly";
                return View(validationError);
            }
        }

        public ActionResult Menu(int? id)
        {
            var db = new DB();

            if (id != null)
            {
                var Item = new SqlParameter("@item", SqlDbType.Int);
                Item.Value = id;
                var sqlParameter = new SqlParameter[1] { Item };

                int items = db.ExeccuteCommandAffected("insert_cart", sqlParameter);

                if (items == 1)
                {
                    int itemsInCart = int.Parse(System.Web.HttpContext.Current.Session["itemsInCart"].ToString());
                    ++itemsInCart;
                    System.Web.HttpContext.Current.Session["itemsInCart"] = itemsInCart;
                }
                
            }

            var menu = new Models.Menu();
            var data = db.ExeccuteCommandReaderWithoutParameter("get_category_name_with_items_count");

            if (data.HasRows)
            {
                while (data.Read())
                {
                    var category = new Category
                    {
                        name = data[0].ToString(),
                        totalItems = int.Parse(data[1].ToString())
                    };

                    menu.categaryList.Add(category);
                }
            }
            data = db.ExeccuteCommandReaderWithoutParameter("select_fooditem_with_category");
            menu.menuItems = data;

            data = db.ExeccuteCommandReaderWithoutParameter("get_count_items");
            data.Read();
            menu.totalItems =int.Parse(data[0].ToString());

            return View(menu);
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(string name, string email, string message, string type, string city)
        {
            var db = new DB();
            var validationError = new CustomerValidate();

            if (message != string.Empty && name != string.Empty && email != string.Empty)
            {
                int invalids = 0;

                if (message.Length < 10)
                {
                    validationError.message = "Message is too short";
                    ++invalids;
                }

                if (email != string.Empty)
                {
                    if (email.Contains("&") && email.Contains(".com"))
                    {
                        validationError.email = "Incorrect email";
                        ++invalids;
                    }
                }

                if (name == string.Empty)
                {
                    validationError.name = "Empty name";
                    ++invalids;
                }

                if (invalids > 0)
                    return View(validationError);
                else
                {
                    var Email = new SqlParameter("@email", SqlDbType.NVarChar);
                    Email.Value = email;
                    var City = new SqlParameter("@city", SqlDbType.NVarChar);
                    City.Value = city;
                    var Type = new SqlParameter("@type", SqlDbType.NVarChar);
                    Type.Value = type;
                    var Message = new SqlParameter("@message", SqlDbType.NVarChar);
                    Message.Value = message;

                    var sqlParameter = new SqlParameter[4] { Email,City,Type, Message};

                    int rowInsert = db.ExeccuteCommandAffected("insert_customer_review", sqlParameter);

                    if (rowInsert == 1)
                        return View("Home");
                    else
                    {
                        validationError.final = "Your review couldn't submitted";
                        return View("Contact", validationError);
                    }
                }
            }
            
            else
            {
                validationError.final = "Enter above fields properly";
                return View(validationError);
            }
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Order(int? id)
        {
            var db = new DB();
            if (id == null)
            {
                var items = db.ExeccuteCommandReaderWithoutParameter("items_from_cart");
                return View(items);
            }
            else
            {
                var Item = new SqlParameter("@item", SqlDbType.Int);
                Item.Value = id;

                var sqlParameter = new SqlParameter[1] { Item };

                int itemDelete = db.ExeccuteCommandAffected("delete_from_cart", sqlParameter);

                if (itemDelete == 1)
                {
                    int itemsInCart = int.Parse(System.Web.HttpContext.Current.Session["itemsInCart"].ToString());
                    --itemsInCart;
                    System.Web.HttpContext.Current.Session["itemsInCart"] = itemsInCart;
                
                    var items = db.ExeccuteCommandReaderWithoutParameter("items_from_cart");
                    return View(items);
                }
                else
                {
                    return View();
                }
            }
        }

        [HttpGet]
        public ActionResult Checkout()
        {
                var db = new DB();
                var checkout = new Checkout();

                var custiomerId = new SqlParameter("@id", SqlDbType.Int);
                custiomerId.Value = System.Web.HttpContext.Current.Session["loginid"];
                var sqlParameter = new SqlParameter[1] {custiomerId};

                var customer = db.ExeccuteCommandReader("select_login_customer_from_id", sqlParameter);

                customer.Read();
                checkout.customer.phone = customer[0].ToString();
                checkout.customer.area = customer[1].ToString();
                checkout.customer.street = customer[2].ToString();
                checkout.customer.building = customer[3].ToString();
                checkout.customer.floor = customer[4].ToString();

                var data = db.ExeccuteCommandReaderWithoutParameter("items_from_cart");
                checkout.items = data;

                return View(checkout);
        }

        [HttpPost]
        public ActionResult Checkout(FormCollection form)
        {
            if (form.HasKeys())
            {
                var quantities = new List<int>();
                    
                for (int field = 0; field < form.Count; ++field)
                {
                    if(form[field]==string.Empty)
                        quantities.Add(1);
                    else
                        quantities.Add(int.Parse(form[field].ToString()));

                }

                var db = new DB();


                for (int quantity = 0; quantity < quantities.Count; ++quantity)
                {
                    var Quantity = new SqlParameter("@quantity", SqlDbType.Int);
                    Quantity.Value = quantities[quantity];
                    var sqlParameter = new SqlParameter[1] { Quantity };

                    int affected = db.ExeccuteCommandAffected("Create_checkout", sqlParameter);
                }

                return RedirectToAction("Checkout");
            }
            else
            {
                return RedirectToAction("Order");
            }

            
        }
        
    }
}