namespace Alpha.Messages.Site.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Models.Message;

    public class MessagesHistoryViewModel : MessageBase
    {
        public MessagesHistoryViewModel(
            IEnumerable<MessageHistory> messages,
            DateTime lastSeenAnnouncement,
            string languageKey,
            DateTime startDate,
            DateTime endDate,
            int messageType,
            byte roleId,
            NumberOfUnreadMessage unreadMessage) : base(messageType, roleId, startDate, endDate, unreadMessage)
        {
            Messages = messages;
            LastSeenAnnouncement = lastSeenAnnouncement;
            LanguageKey = languageKey;
        }

        public IEnumerable<MessageHistory> Messages { get; }

        public DateTime LastSeenAnnouncement { get; }

        public string LanguageKey { get; }

        public bool IsNewMessage(DateTime postTime) => postTime > LastSeenAnnouncement;
    }
}
