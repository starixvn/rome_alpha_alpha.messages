namespace Alpha.Messages.Site.Handlers
{
    using MediatR;
    using Params.Message;
    using Requests.Message;
    using Services;
    using ViewModels.Message;

    public class MessageHandler : IRequestHandler<LatestMessageQuery, LatestMessageViewModel>,
        IRequestHandler<UpdatePrivateMessageAsReadQuery, bool>
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
    }
}