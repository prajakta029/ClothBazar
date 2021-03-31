﻿using ClothBazar.Entities;
using ClothBazar.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModels
{
    public class OrderViewModels
    {
        public List<Order> Orders { get; set; }
        public string Status { get; set; }
        public string UserID { get; set; }
    }

    public class OrderdetailsViewModels
    {
        public List<string> AvailableStatuses { get; set; }
        public Order Order { get; set; }
        public ApplicationUser OrderedBy { get; set; }
    }
}