namespace Alpha.Messages.Site.Tests.Controllers
{
    using Alpha.Announcements.Models;
    using Alpha.Messages.Site.Controllers;
    using Alpha.Messages.Site.Tests.Fixtures;
    using KellermanSoftware.CompareNetObjects;
    using MediatR;
    using NSubstitute;
    using NUnit.Framework;
    using Site.Requests.NewMessage;

    public class NewMessageControllerTests
    {
        private UserSessionFixture fixtures;
        private IMediator mediator;
        private IParam specificationParam;
        private CompareLogic compareLogic;

        [SetUp]
        public void SetUp()
        {
            fixtures = new UserSessionFixture();
            mediator = NSubstitute.Substitute.For<IMediator>();
            specificationParam = Substitute.For<IParam>();
            compareLogic = new CompareLogic();
        }

        [Test]
        public void Index_Always_SetSpecificationToQuery()
        {
            //// Arrange
            var query = new NumberOfNewMessageQuery(fixtures.UserSession);
            var controller = new NewMessageController(mediator, specificationParam);

            //// Act
            controller.GetNumberOfNewAnnouncement(query);

            //// Assert
            Assert.True(compareLogic.Compare(specificationParam, query.Specification).AreEqual);
        }

        [Test]
        public void Index_Always_SendQueryToMediator()
        {
            //// Arrange
            var query = new NumberOfNewMessageQuery(fixtures.UserSession);
            var controller = new NewMessageController(mediator, specificationParam);

            //// Act
            controller.GetNumberOfNewAnnouncement(query);

            //// Assert
            mediator.Received().Send(query);
        }        
    }
}
