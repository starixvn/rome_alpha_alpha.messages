namespace Alpha.Messages.Site.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Helpers;
    using Models.Message;

    public class MessagesHistoryViewModel : MessageBase
    {
        public MessagesHistoryViewModel(
            List<MessageHistoryWrapper> messages,
            DateTime startDate,
            DateTime endDate,
            int messageType,
            byte roleId,
            NumberOfUnreadMessage unreadMessage) : base(messageType, roleId, startDate, endDate, unreadMessage)
        {
            Messages = messages;
        }

        public List<MessageHistoryWrapper> Messages { get; }

        public string OldMainRootPath => UrlHelper.MapOldMainPath("~/");

        public string MainRootPath => UrlHelper.MapMainPath("~/");
    }
}
