namespace Alpha.Messages.Site.Requests.MarqueeMessage
{
    using MediatR;
    using Models.Message;
    using Sunplus.Agent.Model.Entities;

    public class MoveBetMessageQuery : IRequest<GeneralMoveBetsMessage>
    {
        public MoveBetMessageQuery(IUserSession userSession)
        {
            Language = userSession.Language;
        }

        public string Language { get; }
    }
}
