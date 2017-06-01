using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sitemap.Presentation.Models.SitemapData
{
    public class RequestHistory
    {
        [Key]
        public int ReqestHistoryId { get; set; }
        [Required]
        public string SiteDomain { get; set; }
        public DateTime TimeOfStart { get; set; }
        public double? AverageTime { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<DosAttackModel> DosAttack { get; set; }
        public ICollection<ResponseTime> ResponseTime { get; set; }
        public ICollection<SqlInjectionModel> SqlInjection { get; set; }
        public RequestHistory()
        {
            ResponseTime = new List<ResponseTime>();
            DosAttack = new List<DosAttackModel>();
        }
    }
}