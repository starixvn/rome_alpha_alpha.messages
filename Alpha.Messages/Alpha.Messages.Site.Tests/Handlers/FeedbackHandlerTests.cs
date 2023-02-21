namespace Alpha.Messages.Site.Tests.Handlers
{
    using System.Threading.Tasks;
    using Alpha.Messages.AppResources;
    using Alpha.Messages.Params.Feedback;
    using Alpha.Messages.Services;
    using Alpha.Messages.Site.Requests.Feedback;
    using Alpha.Web.Models;
    using NSubstitute;
    using NUnit.Framework;
    using Site.Handlers;

    [TestFixture]
    public class FeedbackHandlerTests
    {
        private FeedbackService service;
        private FeedbackHandler handler;

        [SetUp]
        public void SetUp()
        {
            service = Substitute.For<FeedbackService>();
            handler = new FeedbackHandler(service);
        }

        [TearDown]
        public void Clean()
        {
            handler = null;
            service = null;
        }

        [Test]
        public async Task HandleCheckExistsFeedbackQuery_Always_ReturnsCheckResult()
        {
            // Arrange
            var query = new CheckExistsFeedbackQuery();
            var result = true;
            service.CheckExistsAsync(Arg.Any<int>(), Arg.Any<int>()).Returns(Task.FromResult(result));

            // Act
            var viewModel = await handler.Handle(query);

            // Assert
            Assert.AreEqual(viewModel, result);
        }

        [Test]
        public async Task HandleAddFeedbackQuery_IsInternal_ReturnsErrorResult()
        {
            // Arrange
            var query = new AddFeedbackQuery { IsInternal = true };

            // Act
            var viewModel = await handler.Handle(query);

            // Assert
            Assert.AreEqual(Resources.NotAuthorized, viewModel.ErrMsg);
        }

        [Test]
        public async Task HandleAddFeedbackQuery_InvalidQuery_ReturnsErrorResult()
        {
            // Arrange
            var query = new AddFeedbackQuery();

            // Act
            var viewModel = await handler.Handle(query);

            // Assert
            Assert.NotZero(viewModel.ErrCode);
        }

        [Test]
        public async Task HandleAddFeedbackQuery_ValidQueryAndAddSuccess_ReturnsSuccessResult()
        {
            // Arrange
            var query = new AddFeedbackQuery { Rate = 2 };
            service.AddFeedbackAsync(Arg.Any<AddFeedbackParam>()).Returns(new Result());

            // Act
            var viewModel = await handler.Handle(query);

            // Assert
            Assert.Zero(viewModel.ErrCode);
        }

        [Test]
        public async Task HandleAddFeedbackQuery_ValidQueryAndAddFailed_ReturnsErrotResult()
        {
            // Arrange
            var query = new AddFeedbackQuery { Rate = 2 };
            service.AddFeedbackAsync(Arg.Any<AddFeedbackParam>()).Returns(new Result { ErrCode = 1 });

            // Act
            var viewModel = await handler.Handle(query);

            // Assert
            Assert.NotZero(viewModel.ErrCode);
        }
    }
}
