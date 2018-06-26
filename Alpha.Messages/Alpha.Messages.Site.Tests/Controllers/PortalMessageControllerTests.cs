namespace Alpha.Messages.Site.Tests.Controllers
{
    using System;
    using System.Web.Mvc;
    using Configurations;
    using Constants;
    using KellermanSoftware.CompareNetObjects;
    using MediatR;
    using Messages.Constants;
    using Messsages.Site.Requests.PortalMessage;
    using NSubstitute;
    using NUnit.Framework;
    using Site.Controllers;
    using Sunplus.Agent.Model.Entities;

    [TestFixture]
    public class PortalMessageControllerTests
    {
        private IMediator mediator;
        private IUserSession userSession;
        private PortalMessageController controller;
        private CompareLogic compareLogic;

        [SetUp]
        public void SetUp()
        {
            mediator = Substitute.For<IMediator>();
            userSession = Substitute.For<IUserSession>();
            compareLogic = new CompareLogic();

            controller = new PortalMessageController(mediator, userSession);
        }

        [Test]
        public void Index_Always_ReturnsViewWithCorrectViewModel()
        {
            // Arrange
            var expectedViewModel = 10;

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.True(compareLogic.Compare(expectedViewModel, result.Model).AreEqual);
        }

        [Test]
        public void MarkMessageAsRead_Always_CallsMediatorSendMarkMessageAsReadQuery()
        {
            // Arrange
            userSession.CustId.Returns(CustId.ValidId);
            var messageId = 1;

            // Act 
            controller.MarkMessageAsRead(messageId);

            // Assert
            mediator.Received().Send(
                Arg.Is<MarkMessageAsReadQuery>(
                    q => q.CustId == userSession.CustId
                        && q.MessageId == messageId));
        }

        [Test]
        public void MoreMessages_Always_ReturnsCorrectView()
        {
            // Arrange
            var totalMessage = 10;
            var expectedViewName = "Index";

            // Act
            var result = controller.MoreMessages(totalMessage) as ViewResult;

            // Assert
            Assert.AreEqual(expectedViewName, result.ViewName);
        }

        [Test]
        public void MoreMessages_Always_ReturnsViewWithCorrectViewModel()
        {
            // Arrange
            var totalMessage = 10;
            var expectedViewModel = totalMessage;

            // Act
            var result = controller.MoreMessages(totalMessage) as ViewResult;

            // Assert
            Assert.True(compareLogic.Compare(expectedViewModel, result.Model).AreEqual);
        }

        [Test]
        public void Messages_Always_CallsMediatorSendNoticeQuery()
        {
            // Arrange
            userSession.RoleId.Returns(RoleId.Agent);
            var lastPostDate = DateTime.Now;
            var query = new PortalMessageQuery(userSession, 10, AppSettings.LocalTest);

            // Act
            controller.Messages(10);

            // Assert
            mediator.Received().Send(Arg.Is<PortalMessageQuery>(p => compareLogic.Compare(p, query).AreEqual));
        }
    }
}
