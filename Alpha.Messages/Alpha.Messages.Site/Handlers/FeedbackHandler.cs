namespace Alpha.Messages.Site.Handlers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Alpha.Messages.AppResources;
    using Alpha.Messages.Models.Feedback;
    using Alpha.Messages.Params.Feedback;
    using Alpha.Messages.Services;
    using Alpha.Messages.Site.Requests.Feedback;
    using Alpha.Messages.Site.ViewModels.Feedback;
    using Alpha.Web.Models;
    using MediatR;

    public class FeedbackHandler :
        IAsyncRequestHandler<AddFeedbackQuery, Result>,
        IAsyncRequestHandler<GetFunctionFeedbackQuery, FeedbackFunctionViewModel>
    {
        private readonly FeedbackService feedbackService;

        public FeedbackHandler(FeedbackService feedbackService)
        {
            this.feedbackService = feedbackService;
        }

        public async Task<Result> Handle(AddFeedbackQuery message)
        {
            if (!message.IsValid())
            {
                return new Result
                {
                    ErrCode = 1,
                    ErrMsg = message.IsInternal ? Resources.NotAuthorized : Resources.Feedback_InvalidParams
                };
            }

            var param = new AddFeedbackParam(message.CustId, message.SubAccId, message.Message, message.Rate);
            var result = await feedbackService.AddFeedbackAsync(param);

            if (result.ErrCode == 0)
            {
                result.ErrMsg = Resources.Feedback_Success;
            }

            return result;
        }

        public async Task<FeedbackFunctionViewModel> Handle(GetFunctionFeedbackQuery message)
        {
            if (await feedbackService.CheckExistsAsync(message.CustId, message.SubAccId, message.Id))
            {
                return new FeedbackFunctionViewModel();
            }

            var feedback = await feedbackService.GetFeedbackFunctionAsync(message.Id);
            var feedbackFunction = feedback.Item1.FirstOrDefault() ?? new FeedbackFunction();
            var feedbackAreas = feedback.Item2;

            return new FeedbackFunctionViewModel(
                feedbackFunction.FuncId,
                feedbackFunction.GetFunctionName(message.Language),
                feedbackAreas.Select(a => new FeedbackAreaViewModel(a.AreaId, a.GetAreaName(message.Language))));
        }
    }
}