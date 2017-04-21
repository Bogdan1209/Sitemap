using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Sitemap.Presentation.Models.SitemapData;
using Sitemap.Presentation.Repositories;
using Microsoft.AspNet.Identity;
using AutoMapper;
using Sitemap.Presentation.Interfaces;
using Ninject;
                                                                    //REFACRORING ALL. OMG SHIT CODE
namespace Sitemap.Presentation.Services
{
    class PageReaderService : ParseService
    {
        IUnitOfWork uow;
        public PageReaderService()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            uow = ninjectKernel.Get<IUnitOfWork>();
        }


        List<string> readed = new List<string>();
        //AutoMapper
        MapperConfiguration UrlMapper = new MapperConfiguration(r => r.CreateMap<UrlValueModel, SavedUrl>()
        .ForMember(urval => urval.Id, url => url.MapFrom(src => src.UrlId)));
        MapperConfiguration ExtremeValueMapper = new MapperConfiguration(e => e.CreateMap<UrlValueModel, ExtremeValues>()
        .ForMember(exv => exv.Id, uvm => uvm.MapFrom(src => src.ValueId)));

        public async Task LinksSearchOnSitemap(string path, int historyId, string userId)//unique max n min value 4 every user
        {
            Uri domain = new Uri(path);
            string xmlFile = await DownloadDocumentAsync(path);
            List<string> sitemapLinks = new List<string>();
            sitemapLinks = XmlLinksParser(xmlFile);
            if (sitemapLinks == null)
            {
                return;
            }

            List<UrlValueModel> urlFromHistory = new List<UrlValueModel>();
            //found history, which created for this evaluation
            RequestHistory previousHistoriesOfThisDomain = await uow.Histories.FindByDomainAsync(domain.Host);
            if (previousHistoriesOfThisDomain != null)
            {
                urlFromHistory = await uow.SavedUrls.GetUrlValueAsync(domain.Host, userId);
            }

            for (int i = 0; i < sitemapLinks.Count; i++)
            {
                int responseTime = GetResponse(sitemapLinks[i]);
                if (responseTime == -1)
                {
                    continue;
                }

                SavedUrl newUrl;
                UrlValueModel currentUrl = urlFromHistory.FirstOrDefault(c => c.Url == sitemapLinks[i]);
                if (currentUrl == null)
                {
                    currentUrl = new UrlValueModel
                    {
                        Url = sitemapLinks[i],
                    };
                    var mapper = UrlMapper.CreateMapper();
                    newUrl = mapper.Map<SavedUrl>(currentUrl);
                    uow.SavedUrls.Create(newUrl);
                    await uow.SaveAsync();
                    currentUrl.UrlId = newUrl.Id;
                    ExtremeValues newValues = new ExtremeValues
                    {
                        UrlId = newUrl.Id,
                        UserId = userId,
                        MaxValue = responseTime,
                        MinValue = responseTime

                    };
                    uow.ExtremeValues.Create(newValues);
                }
                else
                {
                    var mapper = ExtremeValueMapper.CreateMapper();
                    if (currentUrl.MaxValue < responseTime)
                    {
                        currentUrl.MaxValue = responseTime;
                        ExtremeValues extremeMaxValue = mapper.Map<ExtremeValues>(currentUrl);
                        uow.ExtremeValues.Update(extremeMaxValue);
                    }
                    else if (currentUrl.MinValue > responseTime)
                    {
                        currentUrl.MinValue = responseTime;
                        ExtremeValues extremeMinValue = mapper.Map<ExtremeValues>(currentUrl);
                        uow.ExtremeValues.Update(extremeMinValue);
                    }
                }
                ResponseTime newResponseTime = new ResponseTime
                {
                    RequestHistoryId = historyId,
                    UrlId = currentUrl.UrlId,
                    TimeOfResponse = responseTime
                };
                uow.ResponseTime.Create(newResponseTime);
                await uow.SaveAsync();

            }
        }

        public async Task LinksSearchOnPage(string url, int notMore, int historyId, string userId)
        {
            Uri domain = new Uri(url);
            List<string> urls = new List<string>();
            urls.Add(url);

            urls = await FindUrlsAsync(urls, notMore);
            //find urls in database
            List<UrlValueModel> urlFromHistory = new List<UrlValueModel>();
            RequestHistory previousHistoriesOfThisDomain = await uow.Histories.FindByDomainAsync(domain.Host);
            if (previousHistoriesOfThisDomain != null)
            {
                urlFromHistory = await uow.SavedUrls.GetUrlValueAsync(domain.Host, userId);
            }

            SavedUrl newUrl;
            for (int i = 0; i < urls.Count; i++)
            {
                int responseTime = GetResponse(urls[i]);
                if (responseTime == -1)
                {
                    continue;
                }

                UrlValueModel currentUrl = urlFromHistory.FirstOrDefault(c => c.Url == urls[i]);
                if (currentUrl == null)
                {
                    currentUrl = new UrlValueModel
                    {
                        Url = urls[i],
                    };
                    var mapper = UrlMapper.CreateMapper();
                    newUrl = mapper.Map<SavedUrl>(currentUrl);
                    uow.SavedUrls.Create(newUrl);
                    await uow.SaveAsync();
                    currentUrl.UrlId = newUrl.Id;
                    ExtremeValues newValues = new ExtremeValues
                    {
                        UrlId = newUrl.Id,
                        UserId = userId,
                        MaxValue = responseTime,
                        MinValue = responseTime

                    };
                    uow.ExtremeValues.Create(newValues);

                }
                else
                {
                    var mapper = ExtremeValueMapper.CreateMapper();
                    if (currentUrl.MaxValue < responseTime)
                    {
                        currentUrl.MaxValue = responseTime;
                        ExtremeValues extremeMaxValue = mapper.Map<ExtremeValues>(currentUrl);
                        uow.ExtremeValues.Update(extremeMaxValue);
                    }
                    else if (currentUrl.MinValue > responseTime)
                    {
                        currentUrl.MinValue = responseTime;
                        ExtremeValues extremeMinValue = mapper.Map<ExtremeValues>(currentUrl);
                        uow.ExtremeValues.Update(extremeMinValue);
                    }
                }
                ResponseTime newResponseTime = new ResponseTime
                {
                    RequestHistoryId = historyId,
                    UrlId = currentUrl.UrlId,
                    TimeOfResponse = responseTime
                };
                uow.ResponseTime.Create(newResponseTime);
                await uow.SaveAsync();
            }
        }
    }
}