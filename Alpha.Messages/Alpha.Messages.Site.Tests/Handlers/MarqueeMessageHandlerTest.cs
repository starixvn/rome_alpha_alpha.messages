namespace Alpha.Messages.Site.Tests.Handlers
{
    using System;
    using Messages.Constants;
    using Models.Message;
    using NSubstitute;
    using NUnit.Framework;
    using Ploeh.AutoFixture;
    using Services;
    using Site.Handlers;
    using Site.Requests.MarqueeMessage;
    using Site.ViewModels;
    using AlphaPlus.User.Models;

    [TestFixture]
    public class MarqueeMessageHandlerTest
    {
        private MarqueeMessageService messageService;
        private NewMessageService newMessageService;
        private MarqueeMessageHandler messageHandler;
        private IUserSession userSession;
        private Fixture fixture;

        [SetUp]
        public void SetUp()
        {
            messageService = Substitute.For<MarqueeMessageService>();
            newMessageService = Substitute.For<NewMessageService>();
            messageHandler = new MarqueeMessageHandler(messageService, newMessageService);

            userSession = Substitute.For<IUserSession>();
            userSession.Language.Returns(Language.English);
            userSession.CustId.Returns(263);
            userSession.RoleId.Returns(RoleId.Agent);
            userSession.GetProperty<DateTime>(SessionKey.CustomerDatelimit).Returns(DateTime.MinValue);
            fixture = new Fixture();
        }

        [Test]
        public void Handle_Always_Call3Methods()
        {
            //// Arrange
            messageService.GetLatestPublicMessage(Arg.Any<byte>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<string>())
                .Returns(fixture.Create<GeneralMarqueeMessage>());
            messageService.GetLatestPrivateMessage(Arg.Any<int>(), Arg.Any<string>(), Arg.Any<bool>())
                .Returns(fixture.Create<GeneralMarqueeMessage>());            
          

            //// Act
            messageHandler.Handle(new MarqueeMessageQuery(userSession, false));

            //// Assert
            messageService.Received().GetLatestPublicMessage(Arg.Any<byte>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<string>());
            messageService.Received().GetLatestPrivateMessage(Arg.Any<int>(), Arg.Any<string>(), Arg.Any<bool>());
        }

        [Test]
        public void HandleLatestMessageQuery_Always_ReturnViewModel()
        {
            //// Arrange
            messageService.GetLatestPublicMessage(Arg.Any<byte>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<string>())
                .Returns(fixture.Create<GeneralMarqueeMessage>());
            messageService.GetLatestPrivateMessage(Arg.Any<int>(), Arg.Any<string>(), Arg.Any<bool>())
                .Returns(fixture.Create<GeneralMarqueeMessage>());  

            //// Act
            var act = messageHandler.Handle(new MarqueeMessageQuery(userSession, false));

            //// Assert
            Assert.IsInstanceOf(typeof(MarqueeMessageViewModel), act);
        }
    }
}