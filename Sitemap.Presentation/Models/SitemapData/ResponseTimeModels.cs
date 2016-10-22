using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sitemap.Presentation.Models.SitemapData
{
    public class ResponseTime      //adding for every URL, in every history
    {
        [Key]
        public int Id { get; set; }
        public int TimeOfResponse { get; set; }
        public int RequestHistoryId { get; set; }
        [ForeignKey("RequestHistoryId")]
        public RequestHistory RequestHistory { get; set; }
        public int UrlId { get; set; }
        [ForeignKey("UrlId")]
        public SavedUrl SavedUrl { get; set; }
    }
}