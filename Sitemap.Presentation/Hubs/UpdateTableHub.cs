using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Sitemap.Presentation.Models.SitemapData;

namespace Sitemap.Presentation.Hubs
{
    public class UpdateTableHub:Hub
    {
        public void AddRow(string url, int requestTime)
        {
            Clients.All.addRow(url, requestTime);
        }

        //public void Message(string message)
        //{
        //    // Получаем контекст хаба
        //    var context =
        //        GlobalHost.ConnectionManager.GetHubContext<UpdateTableHub>();
        //    // отправляем сообщение
        //    context.Clients.All.displayMessage(message);
        //    context.Clients.All.addRowToTable(new TableModels() { Url = "Exemble.com", ResponseTime = 80 });

        //}
    }
}