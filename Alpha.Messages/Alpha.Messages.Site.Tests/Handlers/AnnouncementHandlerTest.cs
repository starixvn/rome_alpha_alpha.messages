namespace Alpha.Messages.Site.Tests.Handlers
{
    using System.Collections.Generic;
    using Alpha.Messages.Models.Announcement;
    using Alpha.Messages.Params.Announcement;
    using Alpha.Messages.Services;
    using Alpha.Messages.Site.Handlers;
    using Alpha.Messages.Site.Requests;
    using Alpha.Messages.Site.Tests.Fixtures;
    using NSubstitute;
    using NUnit.Framework;

    public class AnnouncementHandlerTest
    {
        private AnnouncementService announcementService;
        private AnnouncementFromApiService announcementApiService;
        private AnnouncementFormatService formatService;
        private AnnouncementHandler handler;
        private UserSessionFixture fixture;
        [SetUp]
        public void SetUp()
        {
            announcementService = Substitute.For<AnnouncementService>();
            formatService = Substitute.For<AnnouncementFormatService>();
            announcementApiService = Substitute.For<AnnouncementFromApiService>();
            handler = new AnnouncementHandler(announcementService, formatService, announcementApiService);
            fixture = new UserSessionFixture();
        }

        [Test]
        public void Handle_HeaderAnnouncement_CallGetHotAnnouncement()
        {
            //// Arrange
            var query = new PopupAnnouncementQuery(fixture.UserSession, false);

            //// Act
            handler.Handle(query);

            //// Assert
            announcementService.Received().GetPopupAnnouncement(Arg.Any<GetHotAnnouncementsParams>());
        }

        [Test]
        public void Handle_HeaderAnnouncement_CallFormatService()
        {
            //// Arrange
            var announcements = new List<HotAnnouncement>();
            var query = new PopupAnnouncementQuery(fixture.UserSession, false);
            announcementService.GetPopupAnnouncement(Arg.Any<GetHotAnnouncementsParams>()).Returns(announcements);

            //// Act
            handler.Handle(query);

            //// Assert
            formatService.Received().Format(announcements, Arg.Any<GetHotAnnouncementsParams>());
        }

        [Test]
        public void Handle_HeaderAnnouncement_CallHeaderHotAnnouncementService()
        {
            //// Arrange
            var query = new HeaderAnnouncementQuery(fixture.UserSession);

            //// Act
            handler.Handle(query);

            //// Assert
            announcementApiService.Received().GetHeaderAnnouncements(Arg.Any<GetHotAnnouncementsParams>());
        }
    }
}
