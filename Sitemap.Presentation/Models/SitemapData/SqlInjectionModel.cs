using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sitemap.Presentation.Models.SitemapData
{
    public class SqlInjectionModel
    {
        public int Id { get; set; }
        public string TypeOfAttack { get; set; }
        public bool ResultOfAttack { get; set; }
        public int HistoryId { get; set; }
        [ForeignKey("RequestHistoryId")]
        public RequestHistory RequestHistory { get; set; }
    }
}