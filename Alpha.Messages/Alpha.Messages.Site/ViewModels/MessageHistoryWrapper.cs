namespace Alpha.Messages.Site.ViewModels
{
    using Constants;
    using Alpha.Messages.Models.Message;
    using Fanex.Age.Utility;
    using Models.Announcement;

    public class MessageHistoryWrapper
    {
        private readonly string currentLang;

        public MessageHistoryWrapper(MessageHistory message, LastSeenAnnouncement lastSeenAnnouncement, string currentLang)
        {
            CurrentMessage = message;
            this.currentLang = currentLang;
            LastSeenAnnouncement = lastSeenAnnouncement;
        }

        public LastSeenAnnouncement LastSeenAnnouncement { get; }

        public MessageHistory CurrentMessage { get; }

        public bool IsNewMessage
        {
            get
            {
                if (CurrentMessage.MessageType == 3)
                {
                    return CurrentMessage.IsSticky
                        ? CurrentMessage.LastUpdateTime > LastSeenAnnouncement.LastSeenSpecial
                        : CurrentMessage.LastUpdateTime > LastSeenAnnouncement.LastSeenNormal;
                }

                return CurrentMessage.LastUpdateTime > LastSeenAnnouncement.LastSeenPersonal;
            }
        }

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