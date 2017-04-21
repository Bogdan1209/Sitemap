using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Sitemap.Presentation.Hubs;
using Sitemap.Presentation.Models.SitemapData;
using Sitemap.Presentation.Services;
using Sitemap.Presentation.Repositories;
using Sitemap.Presentation.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Ninject;

namespace Sitemap.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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