using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sitemap.Presentation.Models.SitemapData
{
    public class SavedUrl
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Url { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public ICollection<ResponseTime> ResponseTime { get; set; }
        public SavedUrl()
        {
            ResponseTime = new List<ResponseTime>();
        }
    }
}