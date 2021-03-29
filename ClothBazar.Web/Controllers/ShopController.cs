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
            return View(model);
        }
        // GET: Shop
        public ActionResult Index(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            ShopViewModel model = new ShopViewModel();

            model.SearchTerm = searchTerm;
            //model.FeaturedCategories = CategoriesService.Instance.GetFeaturedCategories();
            //model.MaximumPrice = ProductsService.Instance.GetMaximumPrice();

            //pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            //model.SortBy = sortBy;
            model.CategoryID = categoryID;

            //int totalCount = ProductsService.Instance.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);
            model.Products = ProductServices.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID);

            //model.Pager = new Pager(totalCount, pageNo, pageSize);

            return View(model);
        }
    }
}