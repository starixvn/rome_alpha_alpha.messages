namespace Alpha.Main.Site.Tests.Controllers
{
    using Alpha.Messages.Site.Controllers;
    using MediatR;
    using Messages.Site.Requests.Message;
    using NSubstitute;
    using NUnit.Framework;
    using Sunplus.Agent.Model.Entities;
    using System.Web.Mvc;

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
    }
}
