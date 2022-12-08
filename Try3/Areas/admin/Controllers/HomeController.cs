using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Try3.Models;
using System.Data.SqlClient;


namespace Try3.Areas.admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();
        // GET: admin/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowOrders()
        {
         
            return View(db.Orders);
        }
    
        [HttpGet]
        public ActionResult Delete(int id)
        {
            orders b = db.Orders.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            return View(b);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            orders b = db.Orders.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            db.Orders.Remove(b);
            db.SaveChanges();
            return RedirectToAction("ShowOrders", "Home", new { Area = "admin" });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            orders b = db.Orders.Find(id);
            if (b != null)
            {
                return View(b);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult Edit(orders b)
        {
            /*b.userId = id;*/
            db.Entry(b).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ShowOrders", "Home", new { Area = "admin" });
        }

        public ActionResult ShowUsers()
        {
            return View(db.Users);
        }
        [HttpGet]
        public ActionResult EditUsers(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            ApplicationUser b = db.Users.Find(id);
            if (b != null)
            {
                return View(b);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult EditUsers(ApplicationUser b)
        {
            db.Entry(b).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ShowUsers", "Home", new { Area = "admin" });
        }
        [HttpGet]
        public ActionResult DeleteUsers(string id)
        {
            ApplicationUser b = db.Users.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            return View(b);
        }
        [HttpPost, ActionName("DeleteUsers")]
        public ActionResult DeleteUsersConfirmed(string id)
        {
            ApplicationUser b = db.Users.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            db.Users.Remove(b);
            db.SaveChanges();
            return RedirectToAction("ShowUsers", "Home", new { Area = "admin" });
        }
        [HttpGet]
        public ActionResult CreateUsers()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUsers(ApplicationUser user)
        {
          
            db.Users.Add(user);
            db.SaveChanges();

            return RedirectToAction("ShowUsers", "Home", new { Area = "admin" });
        }
    }
}