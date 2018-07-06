namespace Alpha.Messages.Site.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
    using MediatR;
    using Models.Message;
    using Params;
    using Params.Announcement;
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
            var lastSeenMessage = announcementService.GetLastSeenAnnouncement(message.CustId, message.SubAccId);
            var lastSeenDate = lastSeenMessage.GetLastSeenByType(message.MessageType);
            var messageParams = message.ConvertToMessageParameters();
            var messages = messageService.GetMessagesHistory(messageParams);
            var messageWrappers = new List<MessageHistoryWrapper>(messages.Select(m => new MessageHistoryWrapper(m, lastSeenDate, message.Language)));

            var numberOfUnreadMessage = newMessageService.GetNumberOfUnreadMessage(message.ConvertToBaseAnnouncementParameters());

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