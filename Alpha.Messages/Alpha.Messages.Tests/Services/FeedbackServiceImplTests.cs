namespace Alpha.Messages.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.Messages.Criteria.Feedback;
    using Alpha.Messages.Models.Feedback;
    using Alpha.Messages.Params.Feedback;
    using Alpha.Messages.Services;
    using Alpha.Messages.Tests.Fixtures;
    using Fanex.Data.Repository;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class FeedbackServiceImplTests
    {
        private const string FeedBackOnFunctionCacheKey = "Alpha.Messages.Services.FeedbackService.FeedBackOnFunction_{0}";
        private IDynamicRepository dynamicRepository;
        private FeedbackServiceImpl service;
        private CompareLogic compareLogic;
        private FanexCacheFixture fanexCacheFixture;
        private Tuple<IEnumerable<FeedbackFunction>, IEnumerable<FeedbackArea>> feedback;

        [SetUp]
        public void SetUp()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            service = new FeedbackServiceImpl(dynamicRepository);
            compareLogic = new CompareLogic();
            fanexCacheFixture = new FanexCacheFixture();
            fanexCacheFixture.CacheForTesting.TryGet(string.Format(FeedBackOnFunctionCacheKey, 102), out feedback);
        }

        [Test]
        public async Task CheckExistsAsync_Always_CallsExecuteAsync()
        {
            // Arrange & Act
            var actualResult = await service.CheckExistsAsync(123, 0, 102);

            // Assert
            await dynamicRepository.Received().ExecuteAsync(Arg.Is<CheckExistsCriteria>(
                c => 123 == c.CustId && 0 == c.SubAccId && 102 == c.FuncId));
        }

        [Test]
        public async Task AddFeedbackAsync_Always_CallsExecuteAsync()
        {
            // Arrange
            var param = new AddFeedbackParam(123, 0, "Comment", 4, 101, "<root/>");

            // Act
            await service.AddFeedbackAsync(param);

            // Assert
            await dynamicRepository.Received().ExecuteAsync(Arg.Is<AddFeedbackCriteria>(
                c => 123 == c.CustId
                && 0 == c.SubAccId
                && "Comment" == c.Comment
                && 4 == c.Rate
                && 101 == c.FuncId
                && "<root/>" == c.XMLfb));
        }

        //[Test]
        //public async Task GetFeedbackFunctionAsync_Always_CallsFetchMultiple()
        //{
        //    // Arrange & Act
        //    await service.GetFeedbackFunctionAsync(102);

        //    // Assert
        //    dynamicRepository.Received().FetchMultiple<FeedbackFunction, FeedbackArea>(
        //        Arg.Is<GetFeedbackFunctionCriteria>(c => 102 == c.FuncId));
        //}
    }
}
