namespace Alpha.Messages.Site.Handlers
{
    using System;
    using MediatR;
    using Models.Message;
    using Params;
    using Requests.MessageHistory;
    using Services;
    using ViewModels;

    public class MessageHistoryHandler : IRequestHandler<MessageHistoryQuery, MessagesHistoryViewModel>
    {
        private readonly MessageHistoryService messageService;
        private readonly NewMessageService newMessageService;
        private readonly AnnouncementService announcementService;

        public MessageHistoryHandler(
            MessageHistoryService messageService,
            NewMessageService newMessageService,
            AnnouncementService announcementService)
        {
            this.messageService = messageService;
            this.newMessageService = newMessageService;
            this.announcementService = announcementService;
        }

        public MessagesHistoryViewModel Handle(MessageHistoryQuery message)
        {
            var lastSeenDate = GetLastSeenDate(message.CustId, message.SubAccId, message.MessageType);
            var messageParams = (MessageHistoryParams)message.ConvertToParameters();
            var messages = messageService.GetMessagesHistory(messageParams);

            var numberOfUnreadMessage = newMessageService.GetNumberOfUnreadMessage(messageParams);
            UpdateUnreadMessageOnSession(message, numberOfUnreadMessage);

            return new MessagesHistoryViewModel(
                messages,
                lastSeenDate,
                message.Language,
                messageParams.StartDate,
                message.EndDate,
                message.MessageType,
                message.RoleId,
                numberOfUnreadMessage);
        }

        private static void UpdateUnreadMessageOnSession(MessageHistoryQuery message, NumberOfUnreadMessage unreadMessage)
        {
            message.NumberOfUnreadMessage = unreadMessage;
        }

        private DateTime GetLastSeenDate(int custId, int subAccId, int messageType)
        {
            var lastSeenMessage = announcementService.GetLastSeenAnnouncement(custId, subAccId);

            return lastSeenMessage.GetLastSeenByType(messageType);
        }

    }
}