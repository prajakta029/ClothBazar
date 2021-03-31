﻿using ClothBazar.Services;
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
    public class OrdersController : Controller
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

        // GET: Orders
        public ActionResult Index(string userID, string status)
        {
            OrderViewModels model = new OrderViewModels();
            model.UserID = userID;
            model.Status = status;

            model.Orders = OrdersService.Instance.SearchOrders(userID, status);
            var totalRecords = OrdersService.Instance.SearchOrdersCount(userID, status);

            return View(model);
        }

        public ActionResult Details(int ID)
        {
            OrderdetailsViewModels model = new OrderdetailsViewModels();

            model.Order = OrdersService.Instance.GetOrderByID(ID);

            if (model.Order != null)
            {
                model.OrderedBy = UserManager.FindById(model.Order.UserID);
            }

            model.AvailableStatuses = new List<string>() { "Pending", "In Progress", "Delivered" };

            return PartialView(model);
        }

        public JsonResult ChangeStatus(string status, int ID)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            result.Data = new { Success = OrdersService.Instance.UpdateOrderStatus(ID, status) };

            return result;
        }

    }
}