namespace Alpha.Messages.Site.Requests.Feedback
{
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.Web.Models;
    using MediatR;
    using AlphaPlus.User.Models;

    public class AddFeedbackQuery : IAsyncRequest<Result>
    {
        private const int MaxMessageLength = 4000;

        public AddFeedbackQuery(IUserSession userSession)
        {
            CustId = userSession.CustId;
            SubAccId = userSession.SubAccId;
            IsInternal = userSession.IsInternal;
        }

        public string Message { get; set; }

        public byte Rate { get; set; }

        public int? FunctionId { get; set; }

        public IEnumerable<FeedbackAreaQuery> Areas { get; set; }

        public int CustId { get; }

        public int SubAccId { get; }

        public bool IsInternal { get; }

        public bool IsValid()
            => !IsInternal &&
            (Rate != 0 || (!string.IsNullOrWhiteSpace(Message) && Message.Length <= MaxMessageLength) || Areas?.Any() == true);
    }
}