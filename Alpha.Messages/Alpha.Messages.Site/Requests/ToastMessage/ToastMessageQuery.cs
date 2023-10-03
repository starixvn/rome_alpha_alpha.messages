namespace Alpha.Messages.Site.Requests.ToastMessage
{
    using System.Collections.Generic;
    using Alpha.Messages.Models.ToastMessage;
    using AlphaPlus.User.Models;
    using MediatR;

    public class ToastMessageQuery : IRequest<IEnumerable<ToastMessageModel>>
    {
        private readonly IUserSession userSession;

        public ToastMessageQuery(IUserSession userSession)
        {
            this.userSession = userSession;
        }

        public int CustId => userSession.CustId;

        public int SubAccId => userSession.SubAccId;

        public string Language => userSession.LangKey;

        public int LastToastId { get; set; }
    }
}
