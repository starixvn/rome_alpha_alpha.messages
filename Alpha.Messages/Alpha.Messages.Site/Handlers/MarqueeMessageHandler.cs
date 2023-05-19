namespace Alpha.Messages.Site.Handlers
{
    using MediatR;
    using Models.Message;
    using Requests.MarqueeMessage;
    using Services;
    using ViewModels;

    public class MarqueeMessageHandler :
        IRequestHandler<MarqueeMessageQuery, MarqueeMessageViewModel>,
        IRequestHandler<MoveBetMessageQuery, GeneralMoveBetsMessage>
    {
        private readonly MarqueeMessageService messageService;
        private readonly NewMessageService newMessageService;

        public MarqueeMessageHandler(MarqueeMessageService messageService, NewMessageService newMessageService)
        {
            this.messageService = messageService;
            this.newMessageService = newMessageService;
        }

        public MarqueeMessageViewModel Handle(MarqueeMessageQuery message)
        {
            newMessageService.MoveNewMessage(message.CustId, message.UserName, message.RoleId, message.IsAdminUser);

            GeneralMarqueeMessage publicMessage;
            GeneralMarqueeMessage privateMessage;

            if (Configurations.AppSettings.DisableMessages)
            {
                publicMessage = new GeneralMarqueeMessage();
                privateMessage = new GeneralMarqueeMessage();
            }
            else
            {
                publicMessage = messageService.GetLatestPublicMessage(message.RoleId, message.CurrentLang, message.IsTest, message.CustomerSite);
                privateMessage = messageService.GetLatestPrivateMessage(message.CustId, message.CurrentLang, message.IsTest);
            }

            var moveBet = messageService.GetMoveBetsMessage(message.CurrentLang);

            return new MarqueeMessageViewModel(publicMessage, privateMessage, moveBet);
        }

        public GeneralMoveBetsMessage Handle(MoveBetMessageQuery message)
            => messageService.GetMoveBetsMessage(message.Language);
    }
}
