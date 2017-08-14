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

        public HotAnnouncement(string id, string content, string code, string source)
        {
            Id = id;
            Content = content;
            Code = code;
            Source = string.IsNullOrEmpty(source) ? source : $"{AlphaAppSettings.MessageAppPath}/{source}";
        }

        public string Id { get; }

        public string Content { get; set; }

        public string Code { get; }

        public string Source { get; }
    }
}
