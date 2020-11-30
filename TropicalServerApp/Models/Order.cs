using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TropicalServerApp.Models
{
    public class Order
    {
        public string OrderTrackingNo { get; set; }
        public string OrderDate { get; set; }

        public int CustNo { get; set; }

        public string CustName { get; set; }

        public string Address { get; set; }

        public int CustRouteNo { get; set; }

        public int CustID { get; set; }

        public int OrderID { get; set; }


    }
}