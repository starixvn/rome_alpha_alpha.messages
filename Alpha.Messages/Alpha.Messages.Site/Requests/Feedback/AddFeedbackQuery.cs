namespace Alpha.Messages.Site.Requests.Feedback
{
    using Alpha.Web.Models;
    using MediatR;

    public class AddFeedbackQuery : IAsyncRequest<Result>
    {
        private const int MaxMessageLength = 4000;

        public string Message { get; set; }

        public int Rate { get; set; }

        public int CustId { get; set; }

        public int SubAccId { get; set; }

        public bool IsInternal { get; set; }

        public bool IsValid()
            => !IsInternal &&
            (Rate != 0 || (!string.IsNullOrWhiteSpace(Message) && Message.Length <= MaxMessageLength));
    }
}