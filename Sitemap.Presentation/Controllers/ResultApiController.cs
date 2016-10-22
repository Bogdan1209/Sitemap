﻿using System;
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

namespace Sitemap.Presentation.Controllers
{
    public class ResultApiController : ApiController
    {
        IUnitOfWork uow;

        public ResultApiController(IUnitOfWork unitOfWork)
        {
            uow = unitOfWork;
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

        [HttpGet]
        public async Task<List<UrlViewModel>> UrlsFromHistoryAsync(int historyId)
        {
            List<UrlViewModel> evaluatedUrl = await uow.SavedUrls.GetRespondTimeAsync(historyId);
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
        public async void DeleteHistoryConfirmedAsync([FromBody] int id)
        {
            uow.Histories.DeleteAsync(id);
            await uow.SaveAsync();
        }
    }
}