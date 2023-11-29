namespace Alpha.Messages.Site.Requests.ToastMessage
{
    using Alpha.Web.Models;
    using AlphaPlus.User.Models;
    using MediatR;

    public class MarkReadMessageQuery : IRequest<Result>
    {
        private readonly IUserSession userSession;

        public MarkReadMessageQuery(IUserSession userSession)
        {
            this.userSession = userSession;
        }

        public int CustId => userSession.CustId;

        public int SubAccId => userSession.SubAccId;

        public int Id { get; set; }
    }
}
