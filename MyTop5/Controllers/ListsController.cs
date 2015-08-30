using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyTop5.Controllers
{
    public class ListsController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        public ActionResult Search()
        {
            ViewBag.Title = "Search Page";
            return View();
        }

    }
}