namespace Alpha.Messages.Site.Tests.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Alpha.Messages.Site.Controllers;
    using Alpha.Messages.Site.Requests.Feedback;
    using Alpha.Web.Models;
    using KellermanSoftware.CompareNetObjects;
    using MediatR;
    using NSubstitute;
    using NUnit.Framework;
    using Sunplus.Agent.Model.Entities;

    [TestFixture]
    [Category(" Feedback")]
    public class FeedbackControllerTests
    {
        private FeedbackController controller;
        private IMediator mediator;
        private IUserSession userSession;
        private CompareLogic compareLogic;

        [SetUp]
        public void SetUp()
        {
            mediator = Substitute.For<IMediator>();
            userSession = Substitute.For<IUserSession>();
            controller = new FeedbackController(mediator);
            compareLogic = new CompareLogic();
        }

        [Test]
        public void Index_Always_ReturnsView()
        {
            // Arrange
            var expectedViewName = string.Empty;

            // Act
            var result = controller.Index();

            // Assert
            Assert.AreEqual(expectedViewName, result.ViewName);
        }

        [Test]
        public async Task Add_Always_ReturnsAddFeedbackResult()
        {
            // Arrange
            userSession.RoleId.Returns((byte)4);
            userSession.SubAccId.Returns(1);

            var query = new AddFeedbackQuery(userSession);
            var result = new Result();

            mediator.SendAsync(Arg.Any<AddFeedbackQuery>()).Returns(Task.FromResult(result));

            // Act
            var actualResult = await controller.Add(query) as JsonResult;

            // Assert
            Assert.True(compareLogic.Compare(result, actualResult.Data).AreEqual);
        }
    }
}
