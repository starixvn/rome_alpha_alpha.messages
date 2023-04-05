namespace Alpha.Messages.Site.Requests.Feedback
{
    using MediatR;
    using Sunplus.Agent.Model.Entities;

    public class CheckExistsFeedbackQuery : IAsyncRequest<bool>
    {
        public CheckExistsFeedbackQuery(IUserSession userSession)
        {
            CustId = userSession.CustId;
            SubAccId = userSession.SubAccId;
        }

        public int FunctionId { get; set; }

        public int CustId { get; }

        public int SubAccId { get; }
    }
}