namespace Alpha.Messages.Site.Requests.Feedback
{
    using Alpha.Messages.Site.ViewModels.Feedback;
    using MediatR;
    using Sunplus.Agent.Model.Entities;

    public class CheckExistsFeedbackQuery : GetFunctionFeedbackQuery, IAsyncRequest<CheckExistsFeedbackViewModel>
    {
        public CheckExistsFeedbackQuery(IUserSession userSession) : base(userSession)
        {
        }
    }
}