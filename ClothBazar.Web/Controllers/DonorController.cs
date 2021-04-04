using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Entities;
using ClothBazar.Services;
using ClothBazar.Web.ViewModels;

namespace ClothBazar.Web.Controllers
{
    public class DonorController : Controller
    {
        // GET: Donor
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DonorTable(string search)
        {
            DonorViewModel model = new DonorViewModel();

            model.Donors = DonorServices.Instance.GetDetails(search);

            return View(model);
        }
        public ActionResult Create(Donor model)
        {


            DonorServices.Instance.SaveDonor(model);

            return RedirectToAction("Index","Donor");
        }
    }
}