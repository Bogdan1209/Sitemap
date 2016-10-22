using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Sitemap.Presentation.Hubs;
using Sitemap.Presentation.Repositories;
using Sitemap.Presentation.Models.SitemapData;
using Sitemap.Presentation.Interfaces;
using System.Threading;

namespace Sitemap.Presentation.Controllers
{
    public class ResultController : Controller
    {
        IUnitOfWork uow;

        public ResultController(IUnitOfWork unitOfWork)
        {
            uow = unitOfWork;
        }

        [Authorize]
        public ActionResult ShowListOfHistories()
        {
            return View();
        }

    }
}