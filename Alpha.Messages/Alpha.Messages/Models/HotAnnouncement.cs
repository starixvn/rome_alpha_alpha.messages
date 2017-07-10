using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Messages.Models
{
    public class HotAnnouncement
    {
        public long Id { get; set; }

        public string Content { get; set; }

        public string Code { get; set; }

        public string Source { get; set; }

        public string CookieId { get; set; }

        public string Languague { get; set; }
    }
}
