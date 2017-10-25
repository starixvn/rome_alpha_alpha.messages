namespace Alpha.Messages.Models.Announcement
{
    using System.Collections.Generic;
    using Announcements.Models;
    using Announcements.Specs;
    using AlphaAppSettings = Web.Configurations.AppSettings;

    public class HotAnnouncement
    {
        public HotAnnouncement()
        {
            Content = string.Empty;
            Code = string.Empty;
            Source = string.Empty;
        }

        public HotAnnouncement(
            string id,
            string content, 
            string code, 
            string source,
            IEnumerable<ISpecification<IParam>> specifications = null)
        {
            Id = id;
            Content = content;
            Code = code;
            Source = string.IsNullOrEmpty(source) ? source : $"{AlphaAppSettings.MessageAppPath}/{source}";
            Specifications = specifications;
        }

        public string Id { get; }

        public string Content { get; set; }

        public string Code { get; }

        public string Source { get; }

        public IEnumerable<ISpecification<IParam>> Specifications { get; }
    }
}
