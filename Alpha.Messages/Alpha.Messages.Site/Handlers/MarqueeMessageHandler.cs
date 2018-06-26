namespace Alpha.Messages.Site.Handlers
{
    using MediatR;
    using Requests;
    using Requests.MarqueeMessage;
    using Services;
    using ViewModels;

    public class MarqueeMessageHandler : IRequestHandler<MarqueeMessageQuery, MarqueeMessageViewModel>
    {
        private readonly MarqueeMessageService messageService;
        public MarqueeMessageHandler(MarqueeMessageService messageService)
        {
            this.messageService = messageService;
        }

        public MarqueeMessageViewModel Handle(MarqueeMessageQuery message)
        {
            var publicMessage = messageService.GetLatestPublicMessage(message.RoleId, message.CurrentLang, message.IsTest);
            var privateMessage = messageService.GetLatestPrivateMessage(message.CustId, message.CurrentLang, message.IsTest);
            var moveBet = messageService.GetMoveBetsMessage(message.CurrentLang);

            return new MarqueeMessageViewModel(publicMessage, privateMessage, moveBet);
        }
    }
}