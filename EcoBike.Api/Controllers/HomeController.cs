using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bicimad.Api.Controllers
{
    public partial class HomeController : Controller
    {
        public virtual ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
