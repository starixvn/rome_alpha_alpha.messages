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
    using Ploeh.AutoFixture;
    using Site.Requests.Announcement;

    public class PopupAnnouncementHandlerTest
    {
        private AnnouncementService announcementService;
        private AnnouncementFormatService formatService;
        private PopupAnnouncementHandler handler;
        private UserSessionFixture userSessionFixture;
        private Fixture fixture;

        [SetUp]
        public void SetUp()
        {
            announcementService = Substitute.For<AnnouncementService>();
            formatService = Substitute.For<AnnouncementFormatService>();
            handler = new PopupAnnouncementHandler(announcementService, formatService);
            userSessionFixture = new UserSessionFixture();
            userSessionFixture.UserSession.RoleId.Returns(RoleId.Agent);
            fixture = new Fixture();
        }

        [Test]
        public void Handle_Alway_CallServices()
        {
            //// Arrange
            var announcement = fixture.Create<AlphaAnnouncement>();
            var query = new PopupAnnouncementsQuery(userSessionFixture.UserSession);
            announcementService.GetPopUpAnnouncements(Arg.Any<PopupAnnouncementParams>())
                .Returns(announcement);

            //// Act
            handler.Handle(query);

            //// Assert
            announcementService.Received().GetPopUpAnnouncements(Arg.Any<PopupAnnouncementParams>());
            formatService.Received().Format(Arg.Any<AlphaAnnouncement>(), Arg.Any<PopupAnnouncementParams>());
        }

        [Test]
        public void Handle_Always_ReturnCorrectViewModel()
        {
            //// Arrange
            var announcement = fixture.Create<AlphaAnnouncement>();
            var query = new PopupAnnouncementsQuery(userSessionFixture.UserSession);            
            announcementService.GetPopUpAnnouncements(Arg.Any<PopupAnnouncementParams>())
                .Returns(announcement);
            formatService.Format(Arg.Any<AlphaAnnouncement>(), Arg.Any<PopupAnnouncementParams>()).Returns(announcement);

            //// Act
            var act = handler.Handle(query);

            //// Assert
            Assert.IsInstanceOf(typeof(AlphaAnnouncement), act);
        }
    }
}
