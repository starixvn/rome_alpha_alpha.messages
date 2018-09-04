namespace Alpha.Messages.Site.Handlers
{
    using MediatR;
    using Models.Message;
    using Requests.NewMessage;
    using Services;

    public class NewMessageHandler :
         IRequestHandler<NumberOfNewMessageQuery, NumberOfUnreadMessage>
    {
        private readonly NewMessageService messageService;
        public NewMessageHandler(
            NewMessageService messageService)
        {
            this.messageService = messageService;
        }

        public NumberOfUnreadMessage Handle(NumberOfNewMessageQuery message)
        {
            messageService.MoveNewMessage(message.CustId, message.UserName, message.RoleId, message.IsAdminUser);

            var parameters = message.ConvertToNumberOfNewMessageParameters();

            var unreadMessage = messageService.CheckNewUnreadMessage(parameters);

            UpdateUnreadMessageSession(message, unreadMessage);

            return unreadMessage;
        }

        private static void UpdateUnreadMessageSession(NumberOfNewMessageQuery message, NumberOfUnreadMessage unreadMessage)
        {            
            message.CurrentNumberOfUnreadMessage = unreadMessage;
        }
    }
}
