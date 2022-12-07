using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Try3.Entities;
using Try3.Models;

namespace Try3.Controllers
{
    public class BasketController : Controller
    {
        //Замороженна, ло создания динамических мест

       /* private IPlaceRepository repository;
        ApplicationDbContext db = new ApplicationDbContext();

        public BasketController(IPlaceRepository repo)
        {
            repository = repo;
        }

        public RedirectToRouteResult AddToBasket()
        {
            
            place game = repository.Places
                .FirstOrDefault(g => g.id == Id);

            if (game != null)
            {
                GetCart().AddItem(game, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToRouteResult RemoveFromCart()
        {

            place game = repository.Places
                .FirstOrDefault(g => g.id == Id);

            if (game != null)
            {
                GetCart().RemoveLine(game);
            }
            return RedirectToAction("Index");
        }

        public Basket GetCart()
        {
            Basket basket = (Basket)Session["Basket"];
            if (basket == null)
            {
                basket = new Basket();
                Session["Basket"] = basket;
            }
            return basket;
        }
        public ActionResult Index()
        {
            return View();
         
        }

        public ActionResult Index(string id)
        {
            return View(new BasketIndexViewModel
            {
                basket = GetCart(),
                ReturnUrl = returnUrl
            });
        }*/
    }
}