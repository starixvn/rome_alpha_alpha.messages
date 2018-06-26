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
            NumberOfUnreadMessage unreadMessage) : base(messageType, roleId, unreadMessage)
        {
            Messages = messages;
            StartDate = startDate;
            EndDate = endDate;
        }

        public List<MessageHistoryWrapper> Messages { get; }

        public DateTime StartDate { get; }

        public DateTime EndDate { get; }
        
        public string OldMainRootPath => UrlHelper.MapOldMainPath("~/");

        public string MainRootPath => UrlHelper.MapMainPath("~/");
    }
}
