using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TropicalServerApp.ViewModels;
using TropicalServerApp.Models;
using TropicalServer.BLL;
using System.Data;

namespace TropicalServerApp.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        // GET: Orders
        public ActionResult Orders(string OrderDateSelected)
        {
            List<Order> result = GetData(OrderDateSelected);

            return View(result);
        }

        public List<Order> GetData(string OrderDateSelected)
        {
            ReportsBLL objBLL = new ReportsBLL();

            if (!String.IsNullOrEmpty(OrderDateSelected))
            {
                ViewBag.date = OrderDateSelected;
            }

            DataSet ds = objBLL.spGetOrdersByTimePeriod_BBL(OrderDateSelected);
            var myData = ds.Tables[0].AsEnumerable().Select(r => new Order
            {
                OrderTrackingNo = r.Field<string>("OrderTrackingNumber"),
                OrderDate = r.Field<DateTime>("OrderDate").ToString(),
                CustNo = r.Field<int>("OrderCustomerNumber"),
                CustName = r.Field<string>("CustName"),
                Address = r.Field<string>("CustOfficeAddress1"),
                CustRouteNo = r.Field<int>("OrderRouteNumber"),
                CustID = r.Field<int>("CustID"),
                OrderID = r.Field<int>("OrderID")

            });

            List<Order> result = myData.ToList();

            return result;
        }



        public PartialViewResult SearchByCustName(string OrderDateSelected, string searchCustName)
        {
            List<Order> model = GetData(OrderDateSelected);

            var result = model.Where(a => a.CustName.ToLower().Contains(searchCustName)).ToList();

            return PartialView("~/Views/Orders/_OrderTableView.cshtml", result);
        }

        public PartialViewResult SearchByCustID(string OrderDateSelected, int searchCustID)
        {
            List<Order> model = GetData(OrderDateSelected);

            var result = model.Where(a => a.CustID.ToString().Contains(searchCustID.ToString())).ToList();

            return PartialView("~/Views/Orders/_OrderTableView.cshtml", result);

        }
        [HttpGet]
        public ActionResult Details(int ID)
        {
            ReportsBLL objBLL = new ReportsBLL();
            DataSet ds = objBLL.spGetOrdersByTimePeriod_BBL("Last 6 Months");
            var myData = ds.Tables[0].AsEnumerable().Select(r => new Order
            {
                OrderTrackingNo = r.Field<string>("OrderTrackingNumber"),
                //OrderDate = r.Field<DateTime>("OrderDate").ToString(),
                CustNo = r.Field<int>("OrderCustomerNumber"),
                CustName = r.Field<string>("CustName"),
                Address = r.Field<string>("CustOfficeAddress1"),
                CustRouteNo = r.Field<int>("OrderRouteNumber"),
                CustID = r.Field<int>("CustID"),
                OrderID = r.Field<int>("OrderID")

            }); 

            List<Order> result = myData.Where(a => (a.OrderID).Equals(ID)).Distinct().ToList();
            ViewBag.dcount = result.Count;
            //(a.OrderID + "" + a.CustID)
            return View("Details", result);
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            ReportsBLL objBLL = new ReportsBLL();
            DataSet ds = objBLL.spGetOrdersByTimePeriod_BBL("Last 6 Months");
            var myData = ds.Tables[0].AsEnumerable().Select(r => new Order
            {
                OrderTrackingNo = r.Field<string>("OrderTrackingNumber"),
                CustNo = r.Field<int>("OrderCustomerNumber"),
                CustName = r.Field<string>("CustName"),
                Address = r.Field<string>("CustOfficeAddress1"),
                CustRouteNo = r.Field<int>("OrderRouteNumber"),
                CustID = r.Field<int>("CustID"),
                OrderID = r.Field<int>("OrderID")

            });

            List<Order> result = myData.Where(a => (a.OrderID).Equals(ID)).ToList();
            ViewBag.dcount = result.Count;
            return View("Edit", result);
        }
        [HttpPost]
        public ActionResult Edit(int ID, List<Order> o)
        {
            ReportsBLL objBLL = new ReportsBLL();

            int OrderID = int.Parse(Request.Form["editOrderID"]);
            string OrderTrackingNo = Request.Form["editOrderTrackingNo"];
            int CustID = int.Parse(Request.Form["editCustID"]);
            int CustNo = int.Parse(Request.Form["editCustNo"]);
            string CustName = Request.Form["editCustName"];
            string Address = Request.Form["editAddress"];
            int  CustRouteNo = int.Parse(Request.Form["editCustRouteNo"]);

            objBLL.UpdateOrdersTable_BBL(OrderID, OrderTrackingNo, CustNo, CustRouteNo);

            objBLL.UpdateCustomerTable_BBL(CustID, CustName, Address);
            TempData["msg"] = "Record Updated";

            return RedirectToAction("Orders");
        }


        public ActionResult Delete(string OrderDateSelected, string ID)
        {
            ReportsBLL objBLL = new ReportsBLL();
            int OrderID = int.Parse(ID);
            objBLL.DeleteOrder_BBL(OrderID);
            TempData["msg"] = "Record Deleted";
            return RedirectToAction("Orders");
        }
    }
}