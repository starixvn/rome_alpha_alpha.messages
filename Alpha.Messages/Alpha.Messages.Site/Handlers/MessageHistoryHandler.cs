namespace Alpha.Messages.Site.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
    using MediatR;
    using Params;
    using Requests.MessageHistory;
    using Services;
    using ViewModels;

    public class MessageHistoryHandler : IRequestHandler<MessageHistoryQuery, MessagesHistoryViewModel>
    {
        private readonly MessageHistoryService messageService;
        private readonly NewMessageService newMessageService;
        private readonly AnnouncementService announcementService;

        public MessageHistoryHandler(MessageHistoryService messageService
            , NewMessageService newMessageService
            , AnnouncementService announcementService)
        {
            this.messageService = messageService;
            this.newMessageService = newMessageService;
            this.announcementService = announcementService;
        }

        public MessagesHistoryViewModel Handle(MessageHistoryQuery message)
        {
            var lastSeenMessage = announcementService.GetLastSeenAnnouncement(message.CustId);
            var messageParams = new MessageHistoryParams(
                    message.UserId,                    
                    message.CustId,
                    message.IsAdminUser,
                    message.ParentSites,
                    message.SpecialType,
                    message.TotalMessage,
                    message.StartDate,
                    message.EndDate,
                    message.LimitedDateToShowInfo,
                    message.IsTest,
                    message.MessageType);
            var messages = messageService.GetMessagesHistory(messageParams);

            var messageWrappers = new List<MessageHistoryWrapper>(messages.Select(m => new MessageHistoryWrapper(m, lastSeenMessage, message.CurrentLang)));
            var numberOfUnreadMessage = newMessageService.GetNumberOfUnreadMessage(message.CustId);
            
            return new MessagesHistoryViewModel(
                messageWrappers,
                messageParams.StartDate,
                message.EndDate,
                message.MessageType,
                message.RoleId,
                numberOfUnreadMessage);
        }
    }
}