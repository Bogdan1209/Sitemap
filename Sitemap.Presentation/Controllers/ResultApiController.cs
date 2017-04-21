using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sitemap.Presentation.Repositories;
using Sitemap.Presentation.Models.SitemapData;
using Sitemap.Presentation.Services;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Data.Entity;
using Sitemap.Presentation.Interfaces;
using AutoMapper;
using Ninject;

namespace Sitemap.Presentation.Controllers
{
    public class ResultApiController : ApiController
    {
        IUnitOfWork uow;
        public ResultApiController()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            uow = ninjectKernel.Get<IUnitOfWork>();
        }

         [Authorize]
         [HttpGet]
         [Route("api/resultapi/GetHistoryAsync")]
         public async Task<List<HistoriesViewModel>> GetHistoryAsync()
         {
             Mapper.Initialize(hvm => hvm.CreateMap<RequestHistory, HistoriesViewModel>());
            var availableHistories = Mapper.Map<List<RequestHistory>, List<HistoriesViewModel>>(await uow.Histories.FindByUserIdAsync(User.Identity.GetUserId()));
            return availableHistories;
        }
        /*[HttpGet]
        [Authorize]
        [Route("api/resultapi/GetHistoryAsync/{pageNumber:int?}/{orderBy:alpha?}")]
        public async Task<HistoriesPageViewModel> GetHistoryAsync(int pageNumber = 1, string orderBy = "")
        {
            Mapper.Initialize(hvm => hvm.CreateMap<RequestHistory, HistoriesViewModel>());
            var availableHistories = Mapper.Map<List<RequestHistory>, List<HistoriesViewModel>>(await uow.Histories.FindByUserIdAsync(User.Identity.GetUserId()));

            int pageSize = 3; // количество объектов на страницу
            IEnumerable<HistoriesViewModel> historiesPage = availableHistories.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = pageNumber, PageSize = pageSize, TotalItems = availableHistories.Count() };
            HistoriesPageViewModel hpvm = new HistoriesPageViewModel { PageInfo = pageInfo, Histories = historiesPage };
            return hpvm;
        }*/

        [HttpGet]
        [Authorize]
        public async Task<List<UrlViewModel>> UrlsFromHistoryAsync(int id)
        {
            List<UrlViewModel> evaluatedUrl = await uow.SavedUrls.GetRespondTimeAsync(id, User.Identity.GetUserId());
            return evaluatedUrl;
        }

        [HttpGet]
        [Authorize]
        public async Task<RequestHistory> DeleteHistoryAsync(int id) //handle the exception
        {
            List<RequestHistory> historiesCollection = await uow.Histories.FindByUserIdAsync(User.Identity.GetUserId());
            RequestHistory history = historiesCollection.FirstOrDefault(h => h.ReqestHistoryId == id);
            if (history == null)
            {
                throw new ApplicationException("History not found!");
            }
            return history;
        }

        [HttpPost]
        public async Task DeleteHistoryConfirmedAsync([FromBody] int id)
        {
            await uow.Histories.DeleteAsync(id);
            await uow.SaveAsync();
        }
    }
}
