namespace Alpha.Messages.Site.Tests.Controllers
{
    using System;
    using System.Web.Mvc;
    using Alpha.Messages.Site.Controllers;
    using KellermanSoftware.CompareNetObjects;
    using MediatR;
    using Messages.Configurations;
    using Messages.Constants;
    using Messages.Site.Requests.Message;
    using NSubstitute;
    using NUnit.Framework;
    using Sunplus.Agent.Model.Entities;

    [TestFixture]
    public class MessageControllerTests
    {
        private IMediator mediator;
#pragma warning disable CS0618 // Type or member is obsolete
        private IUserSession userSession;
#pragma warning restore CS0618 // Type or member is obsolete
        private MessageController messageController;

        [SetUp]
        public void SetUp()
        {
            mediator = Substitute.For<IMediator>();
#pragma warning disable CS0618 // Type or member is obsolete
            userSession = Substitute.For<IUserSession>();
#pragma warning restore CS0618 // Type or member is obsolete

            messageController = new MessageController(mediator, userSession);
        }


        [Test]
        public void GetLatestMessage_Always_CallsMediatorSendQuery()
        {
            // Arrange
            userSession.CustId.Returns(263);

            // Act 
            messageController.GetLatestMessage();

            // Assert
            mediator.Received().Send(
                Arg.Is<LatestMessageQuery>(q => q.CustId == userSession.CustId));
        }

        [Test]
        public void GetLatestMessage_Always_ReturnJson()
        {
            //// Arrange

            //// Act
            var act = messageController.GetLatestMessage();

            //// Assert
            Assert.IsInstanceOf(typeof(JsonResult), act);
        }

        [Test]
        public void UpdatePrivateMessageAsRead_Always_CallsMediatorSendQuery()
        {
            // Arrange

            // Act 
            messageController.UpdatePrivateMessageAsRead();

            // Assert
            mediator.Received().Send(Arg.Any<UpdatePrivateMessageAsReadQuery>());
        }

        [Test]
        public void UpdatePrivateMessageAsRead_Always_ReturnJson()
        {
            //// Arrange

            //// Act
            var act = messageController.UpdatePrivateMessageAsRead();

            //// Assert
            Assert.IsInstanceOf(typeof(JsonResult), act);
        }

        [Test]
        public void Index_Always_CallsMediatorSendMessageQuery()
        {
            userSession.RoleId.Returns(RoleId.Agent);

            // Act
            messageController.Index();

            // Assert
            mediator.Received().Send(Arg.Any<MessageQuery>());
        }

        [Test]
        public void Messages_Always_CallsMediatorSendMessageQuery()
        {
            var compareLogic = new CompareLogic();
            var startDate = DateTime.Today.AddDays(-3);
            var endDate = DateTime.Today;
            userSession.RoleId.Returns(RoleId.Agent);

            // Arrange
            var query = new MessageQuery(
                userSession,
                MessageType.Public,
                startDate,
                endDate,
                AppSettings.LocalTest);

            // Act
            messageController.Messages(startDate, endDate);

            // Assert
            mediator.Received().Send(Arg.Is<MessageQuery>(p => compareLogic.Compare(p, query).AreEqual));
        }
    }
}
