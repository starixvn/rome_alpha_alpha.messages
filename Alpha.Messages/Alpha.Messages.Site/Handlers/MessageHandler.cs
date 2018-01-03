namespace Alpha.Messages.Site.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MediatR;
    using Params.Message;
    using Requests.Message;
    using Services;
    using ViewModels;

    public class MessageHandler : IRequestHandler<LatestMessageQuery, LatestMessageViewModel>,
        IRequestHandler<UpdatePrivateMessageAsReadQuery, bool>,
        IRequestHandler<MessageQuery, MessagesViewModel>
    {
        private readonly MessageService messageService;

        public MessageHandler(MessageService messageService)
        {
            this.messageService = messageService;
        }
        
        public LatestMessageViewModel Handle(LatestMessageQuery message)
        {
            var publicMessage = messageService.GetLatestPublicMessage(message.RoleId, message.CurrentLang, message.IsTest);
            var privateMessage = messageService.GetLatestPrivateMessage(message.CustId, message.CurrentLang, message.IsTest);
            var unreadPrivateMessage = messageService.CountUnreadPrivateMessage(message.CustId, message.UserName);
            var moveBet = messageService.GetMoveBetsMessage(message.CurrentLang);

            return new LatestMessageViewModel
            {
                PublicMessage = publicMessage,
                PrivateMessage = privateMessage,
                NumberOfUnreadPrivateMessage = unreadPrivateMessage,
                MoveBetsInformation = moveBet
            };
        }

        public bool Handle(UpdatePrivateMessageAsReadQuery message)
        {
            messageService.UpdatePrivateAsRead(new UpdatePrivateMessageAsReadParam(message.UserId, message.UserName, message.IsInternal));

            return true;
        }

        public MessagesViewModel Handle(MessageQuery message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message), "Query cannot be null");
            }

            var messages = messageService.GetMessagesHistory(
                                message.CustId,
                                message.ParentSites,
                                message.SpecialType,
                                message.TotalMessage,
                                message.StartDate,
                                message.EndDate,
                                message.IsTest,
                                message.MessageType);

            var messageWrappers = new List<MessageWrapper>(messages.Select(m => new MessageWrapper(m, message.CurrentLang)));

            var viewModel = new MessagesViewModel(
                messageWrappers,
                message.StartDate,
                message.EndDate,
                message.MessageType,
                message.RoleId);

            return viewModel;
        }
    }
}