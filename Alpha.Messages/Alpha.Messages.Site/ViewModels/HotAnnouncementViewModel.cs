namespace Alpha.Messages.Site.ViewModels
{
    using Alpha.Messages.Models;
    using System.Collections.Generic;

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