using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sitemap.Presentation.Models.SitemapData
{
    public class ExtremeValuesModels
    {
        public int Id { get; set; }
        public int UrlId { get; set; }
        [ForeignKey("UrlId")]
        public SavedUrl SavedUrl { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }
    }
}