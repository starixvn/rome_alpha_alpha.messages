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
        private readonly AnnouncementService announcementService;
        private readonly NewMessageService messageService;
        public NewMessageHandler(
            AnnouncementService announcementService,
            NewMessageService messageService)
        {
            this.announcementService = announcementService;
            this.messageService = messageService;
        }

        public NumberOfUnreadMessage Handle(NumberOfNewMessageQuery message)
        {
            messageService.MoveNewMessage(message.CustId, message.UserName, message.RoleId, message.IsAdminUser);

            var parameters = message.ConvertToParameters();
            var numberOfNewAnnouncements = announcementService.GetNumberOfNewAnnouncements(parameters);

            var unreadMessage = messageService.GetNumberOfUnreadMessage(message.CustId);

            return new NumberOfUnreadMessage(
                unreadMessage.Normal
                , unreadMessage.Special
                , numberOfNewAnnouncements
                , unreadMessage.Personal);
        }      
    }
}
