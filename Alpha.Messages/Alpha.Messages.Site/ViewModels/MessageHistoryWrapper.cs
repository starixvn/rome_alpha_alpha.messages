namespace Alpha.Messages.Site.ViewModels
{
    using Constants;
    using Alpha.Messages.Models.Message;
    using Fanex.Age.Utility;
    using Models.Announcement;
    using System;

    public class MessageHistoryWrapper
    {
        private readonly string currentLang;

        public MessageHistoryWrapper(MessageHistory message, DateTime lastSeenAnnouncement, string currentLang)
        {
            CurrentMessage = message;
            this.currentLang = currentLang;
            LastSeenAnnouncement = lastSeenAnnouncement;
        }

        public DateTime LastSeenAnnouncement { get; }

        public MessageHistory CurrentMessage { get; }

        public bool IsNewMessage => CurrentMessage.PostTime > LastSeenAnnouncement;

        public string Content
        {
            get
            {
                var rawContent = CurrentMessage.GetContentByKey(currentLang);

                return rawContent
                    .Replace(MessageConstants.ContentFontStartMarkup, string.Empty)
                    .Replace(MessageConstants.ContentFontEndMarkup, string.Empty)
                    .Replace("@@", CurrentMessage.BetTransId);
            }

        }

        public bool HasRedColor => CurrentMessage.ContentEN.Contains(MessageConstants.ContentFontStartMarkup);
    }
}