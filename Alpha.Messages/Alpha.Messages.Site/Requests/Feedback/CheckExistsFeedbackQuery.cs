namespace Alpha.Messages.Site.Requests.Feedback
{
    using MediatR;

    public class CheckExistsFeedbackQuery : IAsyncRequest<bool>
    {
        public int CustId { get; set; }
        public int SubAccId { get; set; }
    }
}