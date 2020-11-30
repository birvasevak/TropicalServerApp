using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TropicalServerApp.ViewModels
{
    public class CustomerOrdersVM
    {
        public int CustID { get; set; }
        public int CustNo { get; set; }

        public string CustName { get; set; }

        public string Address { get; set; }

        public int CustRouteNo { get; set; }
    }
}