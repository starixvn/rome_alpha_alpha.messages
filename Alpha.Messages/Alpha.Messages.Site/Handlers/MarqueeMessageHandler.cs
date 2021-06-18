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

        public MarqueeMessageHandler(MarqueeMessageService messageService)
        {
            this.messageService = messageService;
        }

        public MarqueeMessageViewModel Handle(MarqueeMessageQuery message)
        {
            GeneralMarqueeMessage publicMessage;
            GeneralMarqueeMessage privateMessage;

            if (Configurations.AppSettings.DisableMessages)
            {
                publicMessage = new GeneralMarqueeMessage();
                privateMessage = new GeneralMarqueeMessage();
            }
            else
            {
                publicMessage = messageService.GetLatestPublicMessage(message.RoleId, message.CurrentLang, message.IsTest);
                privateMessage = messageService.GetLatestPrivateMessage(message.CustId, message.CurrentLang, message.IsTest);
            }

            var moveBet = messageService.GetMoveBetsMessage(message.CurrentLang);

            return new MarqueeMessageViewModel(publicMessage, privateMessage, moveBet);
        }

        public GeneralMoveBetsMessage Handle(MoveBetMessageQuery message)
            => messageService.GetMoveBetsMessage(message.Language);
    }
}
