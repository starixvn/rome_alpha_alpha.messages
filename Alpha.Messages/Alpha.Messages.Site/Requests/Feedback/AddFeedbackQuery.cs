namespace Alpha.Messages.Site.Requests.Feedback
{
    using Alpha.Web.Models;
    using MediatR;
    using Sunplus.Agent.Model.Entities;

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

        public int CustId { get; }

        public int SubAccId { get; }

        public bool IsInternal { get; }

        public bool IsValid()
            => !IsInternal &&
            (Rate != 0 || (!string.IsNullOrWhiteSpace(Message) && Message.Length <= MaxMessageLength));
    }
}