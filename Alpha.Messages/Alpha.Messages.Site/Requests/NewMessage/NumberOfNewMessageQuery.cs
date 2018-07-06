namespace Alpha.Messages.Site.Requests.NewMessage
{
    using MediatR;
    using Models.Message;
    using Sunplus.Agent.Model.Entities;

    public class NumberOfNewMessageQuery : MessageHistoryBaseRequest, IRequest<NumberOfUnreadMessage>
    {
        public NumberOfNewMessageQuery(IUserSession userSession) : base(userSession)
        {
        }
    }
}
