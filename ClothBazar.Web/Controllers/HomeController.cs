using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Services;
using ClothBazar.Web.ViewModels;

namespace ClothBazar.Web.Controllers
{
    public class HomeController : Controller
    {
        //CategoriesService categoryService = new CategoriesService();
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.FeaturedCategories= CategoriesService.Instance.GetFeaturedCategories();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Mens()
        {
            var model = new ProductCat();
            model.Products = ProductServices.Instance.GetMens();

            return View(model);
        }
        public ActionResult Womens()
        {
            var model = new ProductCat();
            model.Products = ProductServices.Instance.GetWomens();

            return View(model);
        }
        public ActionResult Kids()
        {
            var model = new ProductCat();
            model.Products = ProductServices.Instance.GetKids();

            return View(model);
        }
    }
}