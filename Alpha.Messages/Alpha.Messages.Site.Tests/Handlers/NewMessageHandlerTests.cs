namespace Alpha.Messages.Site.Tests.Handlers
{
    using System.Collections.Generic;
    using Alpha.Messages.Models.Announcement;
    using Alpha.Messages.Params.Announcement;
    using Alpha.Messages.Services;
    using Alpha.Messages.Site.Handlers;
    using Alpha.Messages.Site.Tests.Fixtures;
    using Models.Message;
    using NSubstitute;
    using NUnit.Framework;
    using Ploeh.AutoFixture;
    using Site.Requests.NewMessage;

    public class NewMessageHandlerTests
    {
        private AnnouncementService announcementService;
        private NewMessageService messageService;
        private NewMessageHandler handler;
        private UserSessionFixture fixture;
        private readonly Fixture fixtureData = new Fixture();
        [SetUp]
        public void SetUp()
        {
            announcementService = Substitute.For<AnnouncementService>();
            messageService = Substitute.For<NewMessageService>();
            handler = new NewMessageHandler(announcementService, messageService);
            fixture = new UserSessionFixture();
        }

        [Test]
        public void Handle_Alway_CallServices()
        {
            //// Arrange
            var query = new NumberOfNewMessageQuery(fixture.UserSession);
            var unreadMessage = fixtureData.Create<NumberOfUnreadMessage>();
            messageService.GetNumberOfUnreadMessage(Arg.Any<int>()).Returns(unreadMessage);
            

            //// Act
            handler.Handle(query);

            //// Assert
            messageService.Received().MoveNewMessage(Arg.Any<int>(), Arg.Any<string>(), Arg.Any<int>(), Arg.Any<bool>());
            messageService.Received().GetNumberOfUnreadMessage(Arg.Any<int>());
            announcementService.Received().GetNumberOfNewAnnouncements(Arg.Any<NumberOfNewAnnouncementsParams>());
        }

        [Test]
        public void Handle_Always_ReturnCorrectViewModel()
        {
            //// Arrange
            var announcements = new List<AlphaAnnouncement>();
            var query = new NumberOfNewMessageQuery(fixture.UserSession);
            var unreadMessage = fixtureData.Create<NumberOfUnreadMessage>();
            messageService.GetNumberOfUnreadMessage(Arg.Any<int>()).Returns(unreadMessage);

            //// Act
            var act = handler.Handle(query);

            //// Assert
            Assert.IsInstanceOf(typeof(NumberOfUnreadMessage), act);
        }
    }
}
