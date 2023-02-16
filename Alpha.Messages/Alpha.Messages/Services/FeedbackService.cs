namespace Alpha.Messages.Services
{
    using System.Threading.Tasks;
    using Alpha.Messages.Criteria.Feedback;
    using Alpha.Messages.Params.Feedback;
    using Alpha.Web.Models;
    using Fanex.Data.Repository;

    public interface FeedbackService
    {
        Task<bool> CheckExistsAsync(int custId);
        Task<Result> AddFeedbackAsync(AddFeedbackParam param);
    }

    public class FeedbackServiceImpl : FeedbackService
    {
        private readonly IDynamicRepository dynamicRepository;

        public FeedbackServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public async Task<bool> CheckExistsAsync(int custId)
        {
            var criteria = new CheckExistsCriteria(custId);

            await dynamicRepository.ExecuteAsync(criteria);

            return criteria.IsExists;
        }

        public async Task<Result> AddFeedbackAsync(AddFeedbackParam param)
        {
            var criteria = new AddFeedbackCriteria(param.CustId, param.SubAccountId, param.Comment, param.Rate);

            await dynamicRepository.ExecuteAsync(criteria);

            return new Result();
        }
    }
}