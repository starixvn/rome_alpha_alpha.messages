namespace Alpha.Messages.Site.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Alpha.Messages.Models.Announcement;
    using Models.Message;

    public class AnnouncementHistoryViewModel : MessageBase
    {
        public AnnouncementHistoryViewModel(
            IEnumerable<AlphaAnnouncement> announcements,
            DateTime fromDate,
            DateTime toDate,
            int messageType,
            byte roleId,
            NumberOfUnreadMessage unreadMessage) : base(messageType, roleId, unreadMessage)
        {
            Announcements = announcements;
            FromDate = fromDate;
            ToDate = toDate;
            MessageType = Constants.MessageType.System;
        }

        public IEnumerable<AlphaAnnouncement> Announcements { get; }

        public DateTime FromDate { get; }

        public DateTime ToDate { get; }

    }
}