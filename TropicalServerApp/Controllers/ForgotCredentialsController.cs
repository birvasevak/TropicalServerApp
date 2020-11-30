using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using TropicalServerApp.Models;

namespace TropicalServerApp.Controllers
{
    public class ForgotCredentialsController : Controller
    {
        // GET: ForgotCredentials
        public ActionResult ForgotUsername()
        {
            return View("ForgotLoginID");
        }

        public ActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }

        [HttpPost]
        public ActionResult SendByEmail(User user, EventArgs e)
        {
            string firstname = string.Empty;
            string lastname = string.Empty;
            string loginID = string.Empty;
            string password = string.Empty;
            string userEmail = Request.Form["Email"];

            string constr = Convert.ToString(ConfigurationManager.AppSettings["TropicalServerConnectionString"]);

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = 
                    new SqlCommand("SELECT LoginID, [Password], FirstName, LastName FROM tblTropicalUser WHERE Email = @Email"))
                {
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.Read())
                        {
                            loginID = sdr["LoginID"].ToString();
                            password = sdr["Password"].ToString();
                            firstname = sdr["FirstName"].ToString();
                            lastname = sdr["LastName"].ToString();
                        }
                    }
                    con.Close();
                }
            }


            if (!string.IsNullOrEmpty(password))
            {
                // sender@gmail.com => senderPassword
                MailMessage mm = new MailMessage("sender@gmail.com", userEmail);
                mm.Subject = "Password Recovery";
                mm.Body = string.Format("Hello {0} {1},<br/><br/>Your login information is:<br/>" +
                    "LoginID: {2} <br/>" +
                    "Passowrd: {3}"+
                    "<br/><br/>Thank You.", 
                    firstname, lastname, loginID, password);
                mm.IsBodyHtml = true;
                mm.To.Add(userEmail);

                // SMTP

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential();
                NetworkCred.UserName = "sender@gmail.com";
                NetworkCred.Password = "senderPassword";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                ViewData["confirmation"] = "Password has been sent to your email address.";
                ViewData["emailError"] = "";
                
            }
            else
            {
                ViewData["emailError"] = "This email address does not match our records.";
                ViewData["confirmation"] = "";
            }
            return View("ForgotLoginID");
        }

        [HttpPost]
        public ActionResult SendByLoginid(User user, EventArgs e)
        {
            string firstname = string.Empty;
            string lastname = string.Empty;
            string loginID = Request.Form["LoginID"];
            string password = string.Empty;
            string userEmail = string.Empty;

            string constr = Convert.ToString(ConfigurationManager.AppSettings["TropicalServerConnectionString"]);

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd =
                    new SqlCommand("SELECT [Password], FirstName, LastName, Email FROM tblTropicalUser WHERE LoginID = @LoginID"))
                {
                    cmd.Parameters.AddWithValue("@LoginID", loginID);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.Read())
                        {
                            password = sdr["Password"].ToString();
                            firstname = sdr["FirstName"].ToString();
                            lastname = sdr["LastName"].ToString();
                            userEmail = sdr["Email"].ToString();
                        }
                    }
                    con.Close();
                }
            }


            if (!string.IsNullOrEmpty(password))
            {
                // sender@gmail.com => senderPassword
                MailMessage mm = new MailMessage("sender@gmail.com", userEmail);
                mm.Subject = "Password Recovery";
                mm.Body = string.Format("Hello {0} {1},<br/><br/>Your login information is:<br/>" +
                    "LoginID: {2} <br/>" +
                    "Passowrd: {3}" +
                    "<br/><br/>Thank You.",
                    firstname, lastname, loginID, password);
                mm.IsBodyHtml = true;
                mm.To.Add(userEmail);

                // SMTP

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential();
                NetworkCred.UserName = "sender@gmail.com";
                NetworkCred.Password = "senderPassword";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                ViewData["confirmation"] = "Password has been sent to your email address.";
                ViewData["emailError"] = "";

            }
            else
            {
                ViewData["emailError"] = "This Login ID does not match our records.";
                ViewData["confirmation"] = "";
            }
            return View("ForgotPassword");
        }

    }
}
