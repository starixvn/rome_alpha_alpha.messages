using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaAppSettings = Alpha.Web.Configurations.AppSettings;

namespace Alpha.Messages.Models
{
    public class HotAnnouncement
    {
        public HotAnnouncement()
        {
            Content = string.Empty;
            Code = string.Empty;
            Source = string.Empty;
        }

        public HotAnnouncement(long id, string content, string code, string source)
        {
            Id = id;
            Content = content;
            Code = code;
            Source = !string.IsNullOrEmpty(source) ? $"{AlphaAppSettings.MessageAppPath}/{source}" : source;
        }

        public long Id { get; }

        public string Content { get; set; }

        public string Code { get; }

        public string Source { get; }
    }
}
