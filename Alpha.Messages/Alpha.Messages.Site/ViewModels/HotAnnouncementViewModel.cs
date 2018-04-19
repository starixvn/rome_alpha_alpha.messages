namespace Alpha.Messages.Site.ViewModels
{
    using System.Collections.Generic;
    using Models.Announcement;

    public class HotAnnouncementViewModel
    {
        public HotAnnouncementViewModel(IEnumerable<HotAnnouncement> announcements, string cookiesId)
        {
            Announcements = announcements;
            CookiesId = cookiesId;
        }

        public IEnumerable<HotAnnouncement> Announcements { get; }

        public string CookiesId { get; }
    }
}
