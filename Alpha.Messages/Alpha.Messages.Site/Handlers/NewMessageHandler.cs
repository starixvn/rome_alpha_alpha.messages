namespace Alpha.Messages.Site.Handlers
{
    using MediatR;
    using Models.Message;
    using Requests;
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

            var parameters = message.ConvertToBaseAnnouncementParameters();

            var unreadMessage = messageService.GetNumberOfUnreadMessage(parameters);

            return unreadMessage;
        }      
    }
}
