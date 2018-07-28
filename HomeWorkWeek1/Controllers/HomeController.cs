using HomeWorkWeek1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeWorkWeek1.Controllers
{
   
    public class HomeController : Controller
    {
        private Entities db = new Entities();
        [Authorize]
        public ActionResult Index()
        {
  

            return View(db.ViewCount.ToList().Take(10));
         
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}