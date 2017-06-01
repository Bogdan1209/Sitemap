using Sitemap.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Sitemap.Presentation.Models.SitemapData;
using Sitemap.Presentation.Interfaces;
using Ninject;
using Sitemap.Presentation.Repositories;
using Microsoft.AspNet.Identity;

namespace Sitemap.Presentation.Controllers
{
    [AllowAnonymous]
    public class InjectionApiController : ApiController
    {
        IUnitOfWork uow;
        SqlInjection sqlInjection;
        public InjectionApiController()
        {
            sqlInjection = new SqlInjection();
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            uow = ninjectKernel.Get<IUnitOfWork>();
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/InjectionApi/LoginInjection")]
        public async Task<string> LoginInjection([FromBody] LoginDataForInjection item)
        {
            RequestHistory history = await CreateHistoryAsync(item.Url, User.Identity.GetUserId());
            var message = "SQL injection not found";
            var result = true;
            string pass = "' OR 'a'='a";
            var postData = $"login={item.Login}&password={pass}";
            var pageCookies = sqlInjection.SendPost(item.Url, "");
            var loginCookies = sqlInjection.SendPost(item.Url, postData);
            if (!new HashSet<string>(pageCookies).SetEquals(loginCookies))
            {
                result = false;
                message = "SQL injection for login found";
            }
            SqlInjectionModel injectionResult = new SqlInjectionModel { TypeOfAttack = "Login Injection", ResultOfAttack = result, HistoryId = history.ReqestHistoryId };
            uow.SqlInjection.Create(injectionResult);
            return message;
        }

        [HttpPost]
        public async Task<string> GetInjection([FromBody]  string url)
        {
            RequestHistory history = await CreateHistoryAsync(url, User.Identity.GetUserId());
            var message = "SQL injection not found";
            var result = true;
            var withoutSqlInjection = sqlInjection.SendGet(url);
            var getParametrs = sqlInjection.ChangeGetParametrs(url);
            var withSqlInjection = sqlInjection.SendGet(getParametrs, url);
            if (withoutSqlInjection != withSqlInjection)
            {
                result = false;
                message = "SQL injection found";
            }
            SqlInjectionModel injectionResult = new SqlInjectionModel { TypeOfAttack = "Get Injection", ResultOfAttack = result, HistoryId = history.ReqestHistoryId };
            uow.SqlInjection.Create(injectionResult);
            return message;
        }
        private async Task<RequestHistory> CreateHistoryAsync(string path, string userId)
        {
            var domain = new Uri(path);
            RequestHistory newHistory = new RequestHistory { UserId = userId, TimeOfStart = DateTime.Now, SiteDomain = domain.Host };
            uow.Histories.Create(newHistory);
            await uow.SaveAsync();
            return newHistory;
        }
    }
}
