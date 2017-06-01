using System;

namespace Sitemap.Presentation.Models.SitemapData
{
    public class UrlViewModel
    {
        public string Url { get; set; }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }
        public int CurrentValue { get; set; }
    }

    public class UrlValueModel
    {
        public int UrlId { get; set; }
        public string UserId { get; set; }
        public int ValueId { get; set; }
        public string Url { get; set; }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }
    }

    public class HistoriesViewModel
    {
        public int ReqestHistoryId { get; set; }
        public string SiteDomain { get; set; }
        public DateTime TimeOfStart { get; set; }
        public double AverageTime { get; set; }
    }

    public class LoginDataForInjection
    {
        public string Login { get; set; }
        public string Url { get; set; }
    }

    public class DosAttackViewModel
    {
        public string ip { get; set; }
        public int countOfUsers { get; set; }
    }
}