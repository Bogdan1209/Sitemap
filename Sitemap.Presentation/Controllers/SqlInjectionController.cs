using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitemap.Presentation.Controllers
{
    public class SqlInjectionController:Controller
    {
        public ActionResult Injection()
        {
            return View();
        }
    }
}