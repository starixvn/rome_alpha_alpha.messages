namespace Alpha.Messages.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.Messages.Criteria.Feedback;
    using Alpha.Messages.Models.Feedback;
    using Alpha.Messages.Params.Feedback;
    using Alpha.Web.Models;
    using Fanex.Data.Repository;

    public interface FeedbackService
    {
        Task<bool> CheckExistsAsync(int custId, int subAccId, int functionId);

        Task<Result> AddFeedbackAsync(AddFeedbackParam param);

        Task<Tuple<IEnumerable<FeedbackFunction>, IEnumerable<FeedbackArea>>> GetFeedbackFunctionAsync(int functionId);
    }

    public class FeedbackServiceImpl : FeedbackService
    {
        private readonly IDynamicRepository dynamicRepository;

        public FeedbackServiceImpl(IDynamicRepository dynamicRepository)
            => this.dynamicRepository = dynamicRepository;

        public async Task<bool> CheckExistsAsync(int custId, int subAccId, int functionId)
        {
            var criteria = new CheckExistsCriteria(custId, subAccId, functionId);
            await dynamicRepository.ExecuteAsync(criteria);

            return criteria.IsExists;
        }

        public async Task<Result> AddFeedbackAsync(AddFeedbackParam param)
        {
            var criteria = new AddFeedbackCriteria(param.CustId, param.SubAccountId, param.Comment, param.Rate);
            await dynamicRepository.ExecuteAsync(criteria);

            return new Result();
        }

        public Task<Tuple<IEnumerable<FeedbackFunction>, IEnumerable<FeedbackArea>>> GetFeedbackFunctionAsync(int functionId)
            => dynamicRepository.FetchMultipleAsync<FeedbackFunction, FeedbackArea>(new GetFunctionFeedbackCriteria(functionId));
    }
}