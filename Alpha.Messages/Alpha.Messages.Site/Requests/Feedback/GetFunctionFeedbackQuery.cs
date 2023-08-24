namespace Alpha.Messages.Site.Requests.Feedback
{
    using Alpha.Messages.Site.ViewModels.Feedback;
    using MediatR;
    using AlphaPlus.User.Models;

    public class GetFunctionFeedbackQuery : IAsyncRequest<FeedbackFunctionViewModel>
    {
        public GetFunctionFeedbackQuery(IUserSession userSession)
        {
            CustId = userSession.CustId;
            SubAccId = userSession.SubAccId;
            Language = userSession.Language;
        }

        public int Id { get; set; }

        public int CustId { get; }

        public int SubAccId { get; }

        public string Language { get; }
    }
}