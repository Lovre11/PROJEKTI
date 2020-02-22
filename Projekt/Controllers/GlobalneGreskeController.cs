using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekt.Controllers
{
    public class GlobalneGreskeController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Error/Index.cshtml");
        }
    }
}