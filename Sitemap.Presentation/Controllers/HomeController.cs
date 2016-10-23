﻿using System;
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

namespace Sitemap.Presentation.Controllers
{
    public class HomeController : Controller
    {
        IUnitOfWork uow;

        public HomeController(IUnitOfWork unitOfWork)
        {
            uow = unitOfWork;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> StartEvaluation(string url) //make it in api
        {
            StartResponseEvaluator result = new StartResponseEvaluator();
            string res = await result.StartEvaluation(url, User.Identity.GetUserId(), 500);
            return res;
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