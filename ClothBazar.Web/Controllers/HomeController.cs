using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Services;
using ClothBazar.Web.ViewModels;
using ClothBazar.Entities;

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

        public ActionResult Payment()
        {
            ViewBag.Message = "Your payment page.";

            return View();
        }

        [HttpPost]
        public ActionResult CreatePayment(Models.RequestData data)
        {
            Order LatestOrder = OrdersService.Instance.GetLatestOrder();

            String merchantKey = "I028xgJGEWcXWdHB";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("MID", "McqBpG10808340449815");
            parameters.Add("CHANNEL_ID", "WEB");
            parameters.Add("INDUSTRY_TYPE_ID", "Retail");
            parameters.Add("WEBSITE", "WEBSTAGING");
            parameters.Add("EMAIL", data.email);
            parameters.Add("MOBILE_NO", data.mobileNumber);
            parameters.Add("CUST_ID", "1");
            parameters.Add("ORDER_ID", LatestOrder.ID.ToString());
            parameters.Add("TXN_AMOUNT", data.amount);
            parameters.Add("CALLBACK_URL", "https://localhost:44325/Home/paytmResponse.net"); //This parameter is not mandatory. Use this to pass the callback url dynamically.

            string checksum = paytm.CheckSum.generateCheckSum(merchantKey, parameters);
            string paytmURL = "https://securegw-stage.paytm.in/theia/processTransaction?orderid=" + LatestOrder.ID.ToString();

            string outputHTML = "<html>";
            outputHTML += "<head>";
            outputHTML += "<title>Merchant Check Out Page</title>";
            outputHTML += "</head>";
            outputHTML += "<body>";
            outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
            outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
            outputHTML += "<table border='1'>";
            outputHTML += "<tbody>";
            foreach (string key in parameters.Keys)
            {
                outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
            }
            outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            outputHTML += "<script type='text/javascript'>";
            outputHTML += "document.f1.submit();";
            outputHTML += "</script>";
            outputHTML += "</form>";
            outputHTML += "</body>";
            outputHTML += "</html>";

            ViewBag.htmlData = outputHTML;

            return View("PaymentPage");
        }

        [HttpPost]
        public ActionResult paytmResponse(Models.PaytmResponse data)
        {
            return View("paytmResponse", data);
        }

    }
}