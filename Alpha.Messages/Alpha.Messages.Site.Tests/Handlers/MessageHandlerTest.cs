namespace Alpha.Messages.Site.Tests.Handlers
{
    using Constants;
    using Models.Message;
    using NSubstitute;
    using NUnit.Framework;
    using Params.Message;
    using Ploeh.AutoFixture;
    using Services;
    using Site.Handlers;
    using Site.Requests.Message;
    using Site.ViewModels.Message;
    using Sunplus.Agent.Model.Entities;

    [TestFixture]
    public class MessageHanlderTests
    {
        private MessageService messageService;
        private MessageHandler messageHandler;
#pragma warning disable CS0618 // Type or member is obsolete
        private IUserSession userSession;
#pragma warning restore CS0618 // Type or member is obsolete
        private Fixture fixture;
        [SetUp]
        public void SetUp()
        {
            messageService = Substitute.For<MessageService>();
            messageHandler = new MessageHandler(messageService);

#pragma warning disable CS0618 // Type or member is obsolete
            userSession = Substitute.For<IUserSession>();
#pragma warning restore CS0618 // Type or member is obsolete
            userSession.Language.Returns(Language.English);
            userSession.CustId.Returns(263);
            userSession.RoleId.Returns(RoleId.Agent);
            fixture = new Fixture();
        }
        
        [Test]
        public void HandleLatestMessageQuery_Always_Call4Methods()
        {
            //// Arrange
            messageService.GetLatestPublicMessage(Arg.Any<byte>(), Arg.Any<string>(), Arg.Any<bool>())
                .Returns(fixture.Create<GeneralLatestMessage>());
            messageService.GetLatestPrivateMessage(Arg.Any<int>(), Arg.Any<string>(), Arg.Any<bool>())
                .Returns(fixture.Create<GeneralLatestMessage>());
            messageService.CountUnreadPrivateMessage(Arg.Any<int>(), Arg.Any<string>())
                .Returns(fixture.Create<int>());
            messageService.GetMoveBetsMessage(Arg.Any<string>())
                .Returns(fixture.Create<GeneralMoveBetsMessage>());

            //// Act
            messageHandler.Handle(new LatestMessageQuery(userSession, false));

            //// Assert
            messageService.Received().GetLatestPublicMessage(Arg.Any<byte>(), Arg.Any<string>(), Arg.Any<bool>());
            messageService.Received().GetLatestPrivateMessage(Arg.Any<int>(), Arg.Any<string>(), Arg.Any<bool>());
            messageService.Received().CountUnreadPrivateMessage(Arg.Any<int>(), Arg.Any<string>());
            messageService.Received().GetMoveBetsMessage(Arg.Any<string>());
        }

        [Test]
        public void HandleLatestMessageQuery_Always_ReturnViewModel()
        {
            //// Arrange
            messageService.GetLatestPublicMessage(Arg.Any<byte>(), Arg.Any<string>(), Arg.Any<bool>())
                .Returns(fixture.Create<GeneralLatestMessage>());
            messageService.GetLatestPrivateMessage(Arg.Any<int>(), Arg.Any<string>(), Arg.Any<bool>())
                .Returns(fixture.Create<GeneralLatestMessage>());
            messageService.CountUnreadPrivateMessage(Arg.Any<int>(), Arg.Any<string>())
                .Returns(fixture.Create<int>());
            messageService.GetMoveBetsMessage(Arg.Any<string>())
                .Returns(fixture.Create<GeneralMoveBetsMessage>());

            //// Act
            var act = messageHandler.Handle(new LatestMessageQuery(userSession, false));

            //// Assert
            Assert.IsInstanceOf(typeof(LatestMessageViewModel), act);
        }

        [Test]
        public void HandleUpdatePrivateAsRead_Always_CallServiceUpdate()
        {
            //// Arrange
            userSession.IsInternal.Returns(true);
            var query = new UpdatePrivateMessageAsReadQuery(userSession);

            //// Act
            messageHandler.Handle(query);

            //// Assert
            messageService.Received().UpdatePrivateAsRead(Arg.Any<UpdatePrivateMessageAsReadParam>());
        }

        [Test]
        public void HandleUpdatePrivateAsRead_Always_ReturnTrue()
        {
            //// Arrange
            userSession.IsInternal.Returns(true);
            var query = new UpdatePrivateMessageAsReadQuery(userSession);

            //// Act
            var act = messageHandler.Handle(query);

            //// Assert
            Assert.IsTrue(act);
        }        
    }
}