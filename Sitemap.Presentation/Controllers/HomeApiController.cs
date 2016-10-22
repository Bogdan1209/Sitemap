﻿using Microsoft.AspNet.Identity;
using Sitemap.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Sitemap.Presentation.Controllers
{
    public class HomeApiController : ApiController
    {
        //[HttpPost]
        //public async Task<string> PostURlForStart(string url)
        //{
        //    StartResponseEvaluator result = new StartResponseEvaluator();
        //    var res = await result.StartEvaluation(url, User.Identity.GetUserId(), 500);
        //    return res;
        //}
        [HttpPost]
        public  void PostURlForStart(HttpRequestMessage request,[FromBody] string url)
        {
            StartResponseEvaluator result = new StartResponseEvaluator();
            var res = result.StartEvaluation(url, User.Identity.GetUserId(), 500);
            
        }
    }
}