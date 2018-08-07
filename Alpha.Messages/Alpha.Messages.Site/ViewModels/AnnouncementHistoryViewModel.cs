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
            NumberOfUnreadMessage unreadMessage) : base(messageType, roleId, fromDate, toDate, unreadMessage)
        {
            Announcements = announcements;
            MessageType = Constants.MessageType.System;
        }

        public IEnumerable<AlphaAnnouncement> Announcements { get; }

        public DateTime MinDate => DateTime.MinValue;

        public DateTime MaxDate => DateTime.Now;

        public DateTime CurrentDate => DateTime.Now;
    }
}