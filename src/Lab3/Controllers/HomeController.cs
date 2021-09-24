using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            ViewBag.FirstGetUrl = "api/student";

            return View();
        }
        public ActionResult Error(string message)
        {
            ViewBag.Title = "Error";
            ViewBag.ErrorMessage = message;

            return PartialView("Error");
        }
    }
}
