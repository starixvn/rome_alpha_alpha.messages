using Alpha.Messages.Models;
using System.Collections.Generic;

namespace Alpha.Messages.Site.ViewModels
{
    public class HotAnnouncementViewModel
    {
        public HotAnnouncementViewModel(IEnumerable<HotAnnouncement> announcements)
        {
            Announcements = announcements;
        }

        public IEnumerable<HotAnnouncement> Announcements { get; }
    }
}