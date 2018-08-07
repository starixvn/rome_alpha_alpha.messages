namespace Alpha.Messages.Site.Tests.Handlers
{
    using System.Collections.Generic;
    using Alpha.Messages.Models.Announcement;
    using Alpha.Messages.Params.Announcement;
    using Alpha.Messages.Services;
    using Alpha.Messages.Site.Handlers;
    using Alpha.Messages.Site.Tests.Fixtures;
    using Messages.Constants;
    using NSubstitute;
    using NUnit.Framework;
    using Site.Requests.Announcement;
    using Site.ViewModels;

    public class AnnouncementHandlerTest
    {
        private AnnouncementService announcementService;
        private AnnouncementFormatService formatService;
        private NewMessageService messageService;
        private AnnouncementHandler handler;
        private UserSessionFixture fixture;
        [SetUp]
        public void SetUp()
        {
            announcementService = Substitute.For<AnnouncementService>();
            messageService = Substitute.For<NewMessageService>();
            formatService = Substitute.For<AnnouncementFormatService>();
            handler = new AnnouncementHandler(announcementService, formatService, messageService);
            fixture = new UserSessionFixture();
            fixture.UserSession.RoleId.Returns(RoleId.Agent);
        }

        [Test]
        public void Handle_Alway_CallServices()
        {
            //// Arrange
            var announcements = new List<AlphaAnnouncement>();
            var query = new AnnouncementsHistoryQuery(fixture.UserSession);


            //// Act
            handler.Handle(query);

            //// Assert
            messageService.Received().GetNumberOfUnreadMessage(Arg.Any<BaseAnnouncementsParams>());
            announcementService.Received().GetAnnouncementsHistory(Arg.Any<AnnouncementsHistoryParams>());
            formatService.Received().Format(Arg.Any<IEnumerable<AlphaAnnouncement>>(), Arg.Any<BaseAnnouncementsParams>());
        }

        [Test]
        public void Handle_Always_ReturnCorrectViewModel()
        {
            //// Arrange
            var announcements = new List<AlphaAnnouncement>();
            var query = new AnnouncementsHistoryQuery(fixture.UserSession);


            //// Act
            var act = handler.Handle(query);

            //// Assert
            Assert.IsInstanceOf(typeof(AnnouncementHistoryViewModel), act);
        }
    }
}
