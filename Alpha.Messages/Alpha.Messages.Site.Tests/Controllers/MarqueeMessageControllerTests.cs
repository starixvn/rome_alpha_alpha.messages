namespace Alpha.Messages.Site.Tests.Controllers
{
    using Alpha.Messages.Site.Controllers;
    using MediatR;
    using Messages.Constants;
    using NSubstitute;
    using NUnit.Framework;
    using Site.Requests;
    using Site.Requests.MarqueeMessage;
    using Sunplus.Agent.Model.Entities;

    [TestFixture]
    public class MarqueeMessageControllerTests
    {
        private IMediator mediator;
        private IUserSession userSession;
        private MarqueeMessageController messageController;

        [SetUp]
        public void SetUp()
        {
            mediator = Substitute.For<IMediator>();
            userSession = Substitute.For<IUserSession>();

            messageController = new MarqueeMessageController(mediator, userSession);
        }

        
        [Test]
        public void Index_Always_CallsMediatorSendMessageQuery()
        {
            userSession.RoleId.Returns(RoleId.Agent);

            // Act
            messageController.Index();

            // Assert
            mediator.Received().Send(Arg.Any<MarqueeMessageQuery>());
        }

        [Test]
        public void MoveBet_Always_CallsMediatorSendMessageQuery()
        {
            userSession.RoleId.Returns(RoleId.Agent);

            // Act
            messageController.MoveBet();

            // Assert
            mediator.Received().Send(Arg.Any<MoveBetMessageQuery>());
        }
    }
}
