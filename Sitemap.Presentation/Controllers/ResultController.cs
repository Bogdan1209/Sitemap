﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Sitemap.Presentation.Repositories;
using Sitemap.Presentation.Models.SitemapData;
using Sitemap.Presentation.Interfaces;
using System.Threading;
using Ninject;

namespace Sitemap.Presentation.Controllers
{
    public class ResultController : Controller
    {
        [Inject]
        public IUnitOfWork uow { private get; set; }

        public ActionResult Histories()
        {
            return View();
        }
        [Authorize]
        public ActionResult ShowListOfHistories()
        {
            return View();
        }

        public ActionResult UrlFromHistory(int historyId)
        {
            return View("Result");
        }

    }
}