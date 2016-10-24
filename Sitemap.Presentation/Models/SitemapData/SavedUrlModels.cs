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
        public ICollection<ExtremeValuesModels> ExteremeValues { get; set; }
        public ICollection<ResponseTime> ResponseTime { get; set; }
        public SavedUrl()
        {
            ExteremeValues = new List<ExtremeValuesModels>();
            ResponseTime = new List<ResponseTime>();
        }
    }
}