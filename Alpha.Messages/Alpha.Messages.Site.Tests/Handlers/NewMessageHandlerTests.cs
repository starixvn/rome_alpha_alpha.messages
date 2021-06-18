namespace Alpha.Messages.Site.Tests.Handlers
{
    using System.Collections.Generic;
    using Alpha.Messages.Models.Announcement;
    using Alpha.Messages.Params.Announcement;
    using Alpha.Messages.Services;
    using Alpha.Messages.Site.Handlers;
    using Alpha.Messages.Site.Tests.Fixtures;
    using Messages.Constants;
    using Models.Message;
    using NSubstitute;
    using NUnit.Framework;
    using Ploeh.AutoFixture;
    using Site.Requests.NewMessage;

    public class NewMessageHandlerTests
    {
        private NewMessageService messageService;
        private NewMessageHandler handler;
        private UserSessionFixture fixture;
        private readonly Fixture fixtureData = new Fixture();
        [SetUp]
        public void SetUp()
        {
            messageService = Substitute.For<NewMessageService>();
            handler = new NewMessageHandler(messageService);
            fixture = new UserSessionFixture();
            fixture.UserSession.RoleId.Returns(RoleId.Agent);
        }

        [Test]
        public void Handle_Alway_CallServices()
        {
            //// Arrange
            var query = new NumberOfNewMessageQuery(fixture.UserSession);
            var unreadMessage = fixtureData.Create<NumberOfUnreadMessage>();
            messageService.CheckNewUnreadMessage(Arg.Any<NumberOfNewMessageParams>()).Returns(unreadMessage);
            

            //// Act
            handler.Handle(query);

            //// Assert
            // messageService.Received().MoveNewMessage(Arg.Any<int>(), Arg.Any<string>(), Arg.Any<int>(), Arg.Any<bool>());
            messageService.Received().CheckNewUnreadMessage(Arg.Any<NumberOfNewMessageParams>());            
        }

        [Test]
        public void Handle_Always_ReturnCorrectViewModel()
        {
            //// Arrange
            var announcements = new List<AlphaAnnouncement>();
            var query = new NumberOfNewMessageQuery(fixture.UserSession);
            var unreadMessage = fixtureData.Create<NumberOfUnreadMessage>();
            messageService.CheckNewUnreadMessage(Arg.Any<NumberOfNewMessageParams>()).Returns(unreadMessage);

            //// Act
            var act = handler.Handle(query);

            //// Assert
            Assert.IsInstanceOf(typeof(NumberOfUnreadMessage), act);
        }
    }
}
