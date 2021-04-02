using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.Models
{
    public class RequestData
    {
        public string mobileNumber { get; set; }
        public string email { get; set; }
        public string orderid { get; set; }
        public string amount { get; set; }
    }
}