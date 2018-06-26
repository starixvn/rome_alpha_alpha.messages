namespace Alpha.Messages.Models.Announcement
{
    using System;
    using System.Collections.Generic;
    using Announcements.Models;
    using Announcements.Specs;
    using AlphaAppSettings = Web.Configurations.AppSettings;

    public class AlphaAnnouncement
    {
        public AlphaAnnouncement()
        {
            Content = string.Empty;
            Code = string.Empty;
            Source = string.Empty;
        }

        public AlphaAnnouncement(
            int id,
            string content, 
            string code, 
            string source,
            DateTime releasedDate,
            DateTime lastSeenDate,
            IEnumerable<ISpecification<IParam>> specifications = null)
        {
            Id = id;
            ReleasedDate = releasedDate;
            Content = content;
            Code = code;
            Source = string.IsNullOrEmpty(source) ? source : $"{AlphaAppSettings.MessageAppPath}/{source}";
            Specifications = specifications;
            IsNewAnnouncement = releasedDate > lastSeenDate;
        }

        public int Id { get; }

        public string Content { get; set; }

        public string Code { get; }

        public string Source { get; }

        public DateTime ReleasedDate { get; }

        public bool IsNewAnnouncement { get; }

        public IEnumerable<ISpecification<IParam>> Specifications { get; }
    }
}
