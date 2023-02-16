namespace Alpha.Messages.Tests.Services
{
    using System.Threading.Tasks;
    using Alpha.Messages.Params.Feedback;
    using Alpha.Messages.Services;
    using Alpha.Web.Models;
    using Fanex.Data.Repository;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class FeedbackServiceImplTests
    {
        private IDynamicRepository dynamicRepository;
        private FeedbackServiceImpl service;
        private CompareLogic compareLogic;

        [SetUp]
        public void SetUp()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            service = new FeedbackServiceImpl(dynamicRepository);
            compareLogic = new CompareLogic();
        }

        [Test]
        public async Task CheckExistsAsync_Always_ReturnsData()
        {
            // Arrange
            var expectedResult = false;

            // Act
            var actualResult = await service.CheckExistsAsync(0);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task AddFeedbackAsync_Always_ReturnsResult()
        {
            // Arrange
            var param = new AddFeedbackParam(0, 0, string.Empty, 0);
            var expectedResult = new Result();

            // Act
            var actualResult = await service.AddFeedbackAsync(param);

            // Assert
            Assert.True(compareLogic.Compare(expectedResult, actualResult).AreEqual);
        }
    }
}
