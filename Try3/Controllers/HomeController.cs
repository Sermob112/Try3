using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Try3.Models;

namespace Try3.Controllers
{
    public class HomeController : Controller
    {
        
        ApplicationDbContext db = new ApplicationDbContext();
       
        public ActionResult Index()
        {
            IList<string> roles = new List<string> { "Роль не определена" };
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                                    .GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            if (user != null)
                roles = userManager.GetRoles(user.Id);
            return View(roles);
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
        [HttpGet]
        [Authorize]
        public ActionResult Order()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Order(orders orders)
        {
            var carNun = db.Cars.FirstOrDefault(t => t.carNum == orders.carNum);
            var id = User.Identity.GetUserId();
            orders.created = DateTime.Now;
            orders.userId = id;
            orders.carId = carNun.id;
            db.Orders.Add(orders);
            db.SaveChanges();

            return RedirectToAction("Complite");
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

            return RedirectToAction("Complite");
        }

    }
}