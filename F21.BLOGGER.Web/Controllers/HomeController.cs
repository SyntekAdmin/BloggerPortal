using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F21.BLOGGER.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User == null || !User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
