using ClothBazar.Services;
using ClothBazar.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class ShopController : Controller
    {
        CheckoutViewModel model = new CheckoutViewModel();
        public ActionResult Checkout()
        {
            var CartProductCookies = Request.Cookies["AddtoCart"];
            if (CartProductCookies != null)
            {
                model.CartProductIDs = CartProductCookies.Value.Split('-').Select(x => int.Parse(x)).ToList();
                model.CartProducts = ProductServices.Instance.GetProducts(model.CartProductIDs);
            }
            return PartialView(model);
        }
        // GET: Shop
        public ActionResult Index()
        {
            return View();
        }
    }
}