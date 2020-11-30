using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TropicalServerApp.Models;
using TropicalServer.BLL;
using System.Data;

namespace TropicalServerApp.Controllers
{

    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View("Login");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (Request.HttpMethod == "POST")
            {
                if(Request.Cookies["LoginID"] != null)
                {
                    Session["LoginID"] = Request.Cookies["LoginID"].Value;
                }
                user.LoginID = Request.Form["LoginID"];
            }
            return View("Login", user);
        }


        public ActionResult Validate(User user)
        {
            ReportsBLL objBLL = new ReportsBLL();
            DataSet ds = objBLL.GetTropicalUsers_BLL();

            string loginID = Request.Form["LoginID"];
            string password = Request.Form["Password"];
            Session["LoginID"] = Request.Form["LoginID"];
            Session["Password"] = Request.Form["Password"];
            bool isValid = false;

            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow dr in table.Rows)
                {
                    if (dr.ItemArray.GetValue(1).ToString() == Session["LoginID"].ToString() &&
                        dr.ItemArray.GetValue(2).ToString() == Session["Password"].ToString())
                    {
                        isValid = true;
                        break;
                    }

                }
            }

            if (isValid)
            {

                if (Request.Form["checkboxName"] != null && Request.Form["checkboxName"] == "on")
                {
                    int timeout = user.RememberMe ? 525600 : 30; // Timeout in minutes, 525600 = 365 days.
                    var ticket = new FormsAuthenticationTicket(loginID, user.RememberMe, timeout);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                    cookie.Expires = System.DateTime.Now.AddMinutes(timeout);// Not my line
                    cookie.HttpOnly = true; // cookie not available in javascript.
                    Response.Cookies.Add(cookie);
                }

                FormsAuthentication.SetAuthCookie("Cookie", true);
                return RedirectToAction("Orders", "Orders");
            }
            else
            {
                TempData["ErrMsg"] = "Invalid LoginID or Password. Please try again.";
                return RedirectToAction("Login", "Login");

            }

        }

    }
}