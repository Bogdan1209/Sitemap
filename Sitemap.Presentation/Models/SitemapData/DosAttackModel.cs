using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sitemap.Presentation.Models.SitemapData
{
    public class DosAttackModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Domain { get; set; }
        public int CountOfUsers { get; set; }
        [ForeignKey("RequestHistoryId")]
        public RequestHistory RequestHistory { get; set; }
    }
}