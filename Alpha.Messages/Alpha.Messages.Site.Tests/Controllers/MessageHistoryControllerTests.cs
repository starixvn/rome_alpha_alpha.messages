namespace Alpha.Messages.Site.Tests.Controllers
{
    using System;
    using Alpha.Messages.Site.Controllers;
    using KellermanSoftware.CompareNetObjects;
    using MediatR;
    using Messages.Configurations;
    using Messages.Constants;
    using NSubstitute;
    using NUnit.Framework;
    using Site.Requests.MessageHistory;
    using Sunplus.Agent.Model.Entities;

    [TestFixture]
    public class MessageHistoryControllerTests
    {
        private IMediator mediator;
        private IUserSession userSession;
        private MessageHistoryController messageController;

        [SetUp]
        public void SetUp()
        {
            mediator = Substitute.For<IMediator>();
            userSession = Substitute.For<IUserSession>();

            messageController = new MessageHistoryController(mediator, userSession);
        }        

        [Test]
        public void Index_Always_CallsMediatorSendMessageQuery()
        {
            userSession.RoleId.Returns(RoleId.Agent);

            // Act
            messageController.Index(MessageType.Normal);

            // Assert
            mediator.Received().Send(Arg.Any<MessageHistoryQuery>());
        }

        [Test]
        public void Messages_Always_CallsMediatorSendMessageQuery()
        {
            var compareLogic = new CompareLogic();
            var startDate = DateTime.Today.AddDays(-3);
            var endDate = DateTime.Today;
            userSession.RoleId.Returns(RoleId.Agent);

            // Arrange
            var query = new MessageHistoryQuery(
                userSession,
                MessageType.Normal,
                startDate,
                endDate,
                AppSettings.LocalTest);

            // Act
            messageController.Messages(startDate, endDate);

            // Assert
            mediator.Received().Send(Arg.Is<MessageHistoryQuery>(p => compareLogic.Compare(p, query).AreEqual));
        }
    }
}
