using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Try3.Models;
using System.Dynamic;
using System.Configuration;
using System.Data.SqlClient;

namespace Try3.Controllers
{
    public class HomeController : Controller
    {
        
        ApplicationDbContext db = new ApplicationDbContext();
       
        public ActionResult Index()
        {
           /* IList<string> roles = new List<string> { "Роль не определена" };
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                                    .GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            if (user != null)
                roles = userManager.GetRoles(user.Id);*/
            return View(/*roles*/);
        }
      
        public ActionResult About()
        {
            /*    ViewBag.Message = "Your application description page.";

                return View();*/
            return RedirectToAction("Index", "Home", new { Area = "admin" });
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Complite()
        {
            var Id = User.Identity.GetUserId();
            var b = db.Orders.FirstOrDefault(t => t.userId == Id);
          /*  var b = new orders { userId = Id };*/
            /*var  b = db.Orders.Find(id);*/
            ViewBag.Message = b.id;
            return View();
        }
        public ActionResult CompliteCar()
        {
            var Id = User.Identity.GetUserId();
            var b = db.Cars.FirstOrDefault(t => t.userId == Id);
            ViewBag.Message = b.id.ToString();
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult Order()
        {
            dynamic model = new ExpandoObject();
            model.Places = GetPlaces();
            model.Orders = GetOrders();
            var Id = User.Identity.GetUserId();
          
            return View(model);
        }

        [HttpGet]
        public ActionResult GetOrder(int id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            place b = db.Places.Find(id);
            if (b != null)
            {
                return View(b);
            }
            return HttpNotFound();
        }
        /*   [HttpPost]
           public ActionResult GetOrder(int placeid)
           {
             *//*  int myInt = System.Convert.ToInt32(placeid);*/
        /*   place b = db.Places.Find(placeid);
           if (b == null)
           {
               return HttpNotFound();
           }*//*

           if (placeid == null)
           {
               return HttpNotFound();
           }
           orders b = db.Orders.Find(placeid);
           if (b != null)
           {
               return View(b);
           }
           return HttpNotFound();
           *//* var Id = User.Identity.GetUserId();
            var orders = new orders();
            dynamic model = new ExpandoObject();
            model.Places = GetPlaces();
            model.Orders = GetOrders();
            var user = db.Users.Find(Id);
            var carNun = db.Cars.FirstOrDefault(t => t.userId == user.Id);
            var id = User.Identity.GetUserId();
            orders.created = DateTime.Now;
            orders.userId = id;
            orders.carNum = carNun.carNum;
            orders.placeId = b.id;
            orders.carId = carNun.id;*//*


           return View(b);


       }*/
        [HttpPost, ActionName("GetOrder")]
        public ActionResult GetOrderFor(int id)
        {
            place b = db.Places.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            var Id = User.Identity.GetUserId();
            var orders = new orders();
            dynamic model = new ExpandoObject();
            model.Places = GetPlaces();
            model.Orders = GetOrders();
            var user = db.Users.Find(Id);
            var carNun = db.Cars.FirstOrDefault(t => t.userId == user.Id);
            var iduser = User.Identity.GetUserId();
            orders.created = DateTime.Now;
            orders.userId = iduser;
            orders.carNum = carNun.carNum;
            orders.placeId = b.id;
            orders.carId = carNun.id;

            db.Orders.Add(orders);
            db.SaveChanges();

            return RedirectToAction("Complite");
        }
        private List<orders> GetOrders()
        {
            var USId = User.Identity.GetUserId();
            SqlParameter nameParam = new SqlParameter("@USId", USId);

            List<orders> customers = new List<orders>();
            string query = "select * from orders";
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.Add(nameParam);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(new orders
                            {
                                id = int.Parse(sdr["id"].ToString()),
                                userId = sdr["userId"].ToString(),
                                placeId = int.Parse(sdr["placeId"].ToString()),
                                created = DateTime.Parse(sdr["created"].ToString()),
                                quantity = int.Parse(sdr["quantity"].ToString()),
                                carNum = sdr["carNum"].ToString()
              

                            });
                        }
                    }
                    con.Close();
                    return customers;
                }
            }
        }
        private List<place> GetPlaces()
        {
            var USId = User.Identity.GetUserId();
            SqlParameter nameParam = new SqlParameter("@USId", USId);

            List<place> customers = new List<place>();
            string query = "select * from places  where id not in (select placeId from orders)";
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.Add(nameParam);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(new place
                            {
                                id = int.Parse(sdr["id"].ToString()),
                                price = int.Parse(sdr["price"].ToString()),
                         
                            });
                        }
                    }
                    con.Close();
                    return customers;
                }
            }
        }


        [HttpGet]
        [Authorize]
        public ActionResult AddCar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCar(cars cars)
        {
            var id = User.Identity.GetUserId();

            cars.userId = id;
            db.Cars.Add(cars);
            db.SaveChanges();

            return RedirectToAction("CompliteCar");
        }
        [Authorize]
        public ActionResult Authrized()
        {
            return View(db.Users);
        }
    }
}