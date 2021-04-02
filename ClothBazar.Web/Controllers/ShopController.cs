using ClothBazar.Entities;
using ClothBazar.Services;
using ClothBazar.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [Authorize]
        public ActionResult Checkout()
        {
            CheckoutViewModel model = new CheckoutViewModel();
            var CartProductCookies = Request.Cookies["AddtoCart"];
            if (CartProductCookies != null)
            {
                model.CartProductIDs = CartProductCookies.Value.Split('-').Select(x => int.Parse(x)).ToList();
                model.CartProducts = ProductServices.Instance.GetProducts(model.CartProductIDs);
                model.User = UserManager.FindById(User.Identity.GetUserId());
                model.LatestOrder = OrdersService.Instance.GetLatestOrder();
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

        public JsonResult PlaceOrder(string productIDs)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (!string.IsNullOrEmpty(productIDs))
            {
                var productQuantities = productIDs.Split('-').Select(x => int.Parse(x)).ToList();

                var boughtProducts = ProductServices.Instance.GetProducts(productQuantities.Distinct().ToList());

                Order newOrder = new Order();
                newOrder.UserID = User.Identity.GetUserId();
                newOrder.OrderedAt = DateTime.Now;
                newOrder.Status = "Pending";
                newOrder.TotalAmount = boughtProducts.Sum(x => x.Price * productQuantities.Where(productID => productID == x.ID).Count());

                newOrder.OrderItems = new List<OrderItem>();
                newOrder.OrderItems.AddRange(boughtProducts.Select(x => new OrderItem() { ProductID = x.ID, Quantity = productQuantities.Where(productID => productID == x.ID).Count() }));

                var rowsEffected = ShopService.Instance.SaveOrder(newOrder);

                result.Data = new { Success = true, Rows = rowsEffected };
            }
            else
            {
                result.Data = new { Success = false };
            }

            return result;
        }
    }
}