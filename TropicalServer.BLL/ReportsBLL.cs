using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TropicalServer.DAL;

namespace TropicalServer.BLL
{
    public class ReportsBLL
    {
        public DataSet GetProductByProductCategory_BLL(string newItemDescription)
        {
            return (new ReportsDAL().GetProductByProductCategory_DAL(newItemDescription));
        }

        public DataSet GetCustSalesRepNumber_BLL(int newCustSaleRepNum)
        {
            return (new ReportsDAL().GetCustSalesRepNumber_DAL(newCustSaleRepNum));
        }

        // get all tropical users
        public DataSet GetTropicalUsers_BLL()
        {
            return (new ReportsDAL().GetTropicalUsers_DAL());
        }
        public DataSet GetUsersSetting_BLL()
        {
            return (new ReportsDAL().GetUsersSetting_DAL());
        }

        public DataSet GetCustomersSetting_BLL()
        {
            return (new ReportsDAL().GetCustomersSetting_DAL());
        }

        public DataSet GetPriceGroupSetting_BLL()
        {
            return (new ReportsDAL().GetPriceGroupSetting_DAL());
        }

        public DataSet GetRouteInfo_BLL(int newRouteID)
        {
            return (new ReportsDAL().GetRouteInfo_DAL(newRouteID));
        }
        public DataSet GetItemTypeID_BLL()
        {
            return (new ReportsDAL().GetItemTypeID_DAL());
        }

        public DataSet GetItemsDetail_BLL(string itemType)
        {
            return (new ReportsDAL().GetItemsDetail_DAL(itemType));
        }

        public DataSet Sample_BLL()
        {
            return (new ReportsDAL().Sample_DAL());
        }

        public DataSet GetAllCustomerOrders_BLL()
        {
            return (new ReportsDAL().GetAllCustomerOrders_DAL());
        }

        public DataSet spGetOrdersByTimePeriod_BBL(string orderDate)
        {
            return (new ReportsDAL().spGetOrdersByTimePeriod_DAL(orderDate));
        }

        public DataSet CustomerOrders_BBL(string orderDate, string custID, string custName)
        {
            return (new ReportsDAL().CustomerOrders_DAL(orderDate, custID, custName));
        }

        public DataSet GetAllOrders_BBL()
        {
            return (new ReportsDAL().GetAllOrders_DAL());
        }

        public DataSet UpdateOrdersTable_BBL(int orderID, string trackingNo, int CustNo, int routeNo)
        {
            return (new ReportsDAL().UpdateOrdersTable_DAL(orderID, trackingNo, CustNo, routeNo));
        }

        public DataSet UpdateCustomerTable_BBL(int custID, string custName, string custAddress)
        {
            return (new ReportsDAL().UpdateCustomerTable_DAL(custID, custName, custAddress));
        }

        public DataSet DeleteOrder_BBL(int OrderID)
        {
            return (new ReportsDAL().DeleteOrder_DAL(OrderID));
        }
    }
}
