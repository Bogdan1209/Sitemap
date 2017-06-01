using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sitemap.Presentation.Services;
using System.Threading.Tasks;
using Sitemap.Presentation.Models.SitemapData;

namespace Sitemap.Presentation.Controllers
{
    public class DosAttackApiController : ApiController
    {
        DosAttack dosAttack;
        public DosAttackApiController()
        {
            dosAttack = new DosAttack();
        }

        [HttpPost]
        public string StartDosAttack([FromBody]  DosAttackViewModel item)
        {
            dosAttack.siteIp = item.ip;
            dosAttack.Start(item.countOfUsers);
            return "Dos attack started";
        }

        [HttpPost]
        public string ChangeDosAttackState()
        {
            dosAttack.ChangeDosAttackState();
            if(dosAttack.dos == false) {
                return "Dos atteck stoped";
            }
            return "Dos attack started";
        }
    }
}
