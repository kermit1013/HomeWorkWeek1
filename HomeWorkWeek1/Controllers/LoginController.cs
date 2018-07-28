using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HomeWorkWeek1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(string account, string password)
        {
            if (account == "abc" && password == "123")
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                    account,
                    DateTime.Now,
                    DateTime.Now.AddSeconds(10d),
                    true,
                    account,
                    FormsAuthentication.FormsCookiePath);

                string encTicket = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);

                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Index","Home");
        }

       
        


    }
}