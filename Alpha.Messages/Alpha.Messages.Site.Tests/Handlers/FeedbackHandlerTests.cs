namespace Alpha.Messages.Site.Tests.Handlers
{
    using System.Threading.Tasks;
    using Alpha.Messages.AppResources;
    using Alpha.Messages.Params.Feedback;
    using Alpha.Messages.Services;
    using Alpha.Messages.Site.Requests.Feedback;
    using NSubstitute;
    using NUnit.Framework;
    using Site.Handlers;
    using Sunplus.Agent.Model.Entities;

    [TestFixture]
    public class FeedbackHandlerTests
    {
        private IUserSession userSession;
        private FeedbackService service;
        private FeedbackHandler handler;

        [SetUp]
        public void SetUp()
        {
            userSession = Substitute.For<IUserSession>();
            service = Substitute.For<FeedbackService>();
            handler = new FeedbackHandler(service);
        }

        [TearDown]
        public void Clean()
        {
            userSession = null;
            handler = null;
            service = null;
        }

        [Test]
        public async Task HandleAddFeedbackQuery_IsInternal_ReturnsErrorResult()
        {
            // Arrange
            userSession.IsInternal.Returns(true);
            var query = new AddFeedbackQuery(userSession);

            // Act
            var viewModel = await handler.Handle(query);

            // Assert
            Assert.AreEqual(Resources.NotAuthorized, viewModel.ErrMsg);
        }

        [Test]
        public async Task HandleAddFeedbackQuery_InvalidQuery_ReturnsErrorResult()
        {
            // Arrange
            var query = new AddFeedbackQuery(userSession);

            // Act
            var viewModel = await handler.Handle(query);

            // Assert
            Assert.NotZero(viewModel.ErrCode);
        }

        [Test]
        public async Task HandleAddFeedbackQuery_ValidQueryAndAddSuccess_ReturnsSuccessResult()
        {
            // Arrange
            var query = new AddFeedbackQuery(userSession) { Rate = 2 };
            service.AddFeedbackAsync(Arg.Any<AddFeedbackParam>()).Returns(Task.CompletedTask);

            // Act
            var viewModel = await handler.Handle(query);

            // Assert
            Assert.Zero(viewModel.ErrCode);
        }

        [Test]
        public async Task HandleAddFeedbackQuery_ValidQueryAndAddFailed_ReturnsErrotResult()
        {
            // Arrange
            var query = new AddFeedbackQuery(userSession) { Rate = 2 };
            service.AddFeedbackAsync(Arg.Any<AddFeedbackParam>()).Returns(Task.CompletedTask);

            // Act
            var viewModel = await handler.Handle(query);

            // Assert
            Assert.NotZero(viewModel.ErrCode);
        }
    }
}
