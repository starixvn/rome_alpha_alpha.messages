namespace Alpha.Messages.Site.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Helpers;
    using Models.Constants;

    public class MessagesViewModel
    {
        private static readonly Dictionary<int, string> MessageTypeNames = new Dictionary<int, string>
        {
            [Constants.MessageType.Private] = "private",
            [Constants.MessageType.Public] = "public",
            [Constants.MessageType.Special] = "special"
        };

        public MessagesViewModel(
            List<MessageWrapper> messages,
            DateTime startDate,
            DateTime endDate,
            int messageType,
            byte roleId)
        {
            Messages = messages;
            StartDate = startDate;
            EndDate = endDate;
            MessageType = messageType;
            MessageTypeName = MessageTypeNames[messageType];
            UserLevelName = UserLevel.GetDisplayName(roleId).ToLower();
        }

        public List<MessageWrapper> Messages { get; }

        public DateTime StartDate { get; }

        public DateTime EndDate { get; }

        public int MessageType { get; } = Constants.MessageType.Public;

        public string MessageTypeName { get; }

        public string UserLevelName { get; }

        public string OldMainRootPath => UrlHelper.MapOldMainPath("~/");

        public string MainRootPath => UrlHelper.MapMainPath("~/");
    }
}
