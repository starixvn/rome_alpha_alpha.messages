namespace Alpha.Messages.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.Messages.Criteria.Feedback;
    using Alpha.Messages.Models.Feedback;
    using Alpha.Messages.Params.Feedback;
    using Fanex.Caching;
    using Fanex.Data.Repository;

    public interface FeedbackService
    {
        Task<bool> CheckExistsAsync(int custId, int subAccId, int functionId);

        Task<Tuple<IEnumerable<FeedbackFunction>, IEnumerable<FeedbackArea>>> GetFeedbackFunctionAsync(int functionId);

        Task AddFeedbackAsync(AddFeedbackParam param);
    }

    public class FeedbackServiceImpl : FeedbackService
    {
        private const string FeedBackOnFunctionCacheKey = "Alpha.Messages.Services.FeedbackService.FeedBackOnFunction_{0}";
        private const int FeedBackOnFunctionCacheInMinutes = 60 * 2;

        private readonly IDynamicRepository dynamicRepository;

        public FeedbackServiceImpl(IDynamicRepository dynamicRepository)
            => this.dynamicRepository = dynamicRepository;

        public async Task<bool> CheckExistsAsync(int custId, int subAccId, int functionId)
        {
            var criteria = new CheckExistsCriteria(custId, subAccId, functionId);
            await dynamicRepository.ExecuteAsync(criteria);

            return criteria.IsExists;
        }

        public Task<Tuple<IEnumerable<FeedbackFunction>, IEnumerable<FeedbackArea>>> GetFeedbackFunctionAsync(int functionId)
            => ConfiguredCacheService.GetOrSetAsync(
            string.Format(FeedBackOnFunctionCacheKey, functionId),
            () => dynamicRepository.FetchMultiple<FeedbackFunction, FeedbackArea>(new GetFeedbackFunctionCriteria(functionId)),
            new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(FeedBackOnFunctionCacheInMinutes)));

        public Task AddFeedbackAsync(AddFeedbackParam param)
            => dynamicRepository.ExecuteAsync(new AddFeedbackCriteria(param));
    }
}