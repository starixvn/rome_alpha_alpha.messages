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
        }

        public AlphaAnnouncement(
            int id,
            string content, 
            string code, 
            DateTime releasedDate,
            DateTime lastSeenDate,
            IEnumerable<ISpecification<IParam>> specifications)
        {
            Id = id;
            ReleasedDate = releasedDate;
            Content = content;
            Code = code;
            Specifications = specifications;
            IsNewAnnouncement = releasedDate >= lastSeenDate;
        }

        public int Id { get; }

        public string Content { get; set; }

        public string Code { get; }        

        public DateTime ReleasedDate { get; }

        public bool IsNewAnnouncement { get; }

        public IEnumerable<ISpecification<IParam>> Specifications { get; }
    }
}
