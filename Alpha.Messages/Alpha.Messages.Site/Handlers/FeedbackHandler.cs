namespace Alpha.Messages.Site.Handlers
{
    using System.Threading.Tasks;
    using Alpha.Messages.Params.Feedback;
    using Alpha.Messages.Services;
    using Alpha.Messages.Site.Requests.Feedback;
    using Alpha.Web.Models;
    using MediatR;

    public class FeedbackHandler :
        IAsyncRequestHandler<AddFeedbackQuery, Result>
    {
        private readonly FeedbackService service;

        public FeedbackHandler(FeedbackService service)
        {
            this.service = service;
        }

        public async Task<Result> Handle(AddFeedbackQuery message)
        {
            if (!message.IsValid())
            {
                return new Result
                {
                    ErrCode = 1,
                    ErrMsg = AppResources.Resources.Feedback_InvalidParams
                };
            }

            var param = new AddFeedbackParam(message.CustId, message.SubAccId, message.Message, message.Rate);
            var result = await service.AddFeedbackAsync(param);

            if (result.ErrCode == 0)
            {
                result.ErrMsg = AppResources.Resources.Feedback_Success;
            }

            return result;
        }
    }
}