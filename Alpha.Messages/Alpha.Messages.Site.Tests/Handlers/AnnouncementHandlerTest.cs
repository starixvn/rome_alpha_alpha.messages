using Alpha.Messages.Models;
using Alpha.Messages.Params;
using Alpha.Messages.Services;
using Alpha.Messages.Site.Handlers;
using Alpha.Messages.Site.Requests;
using Alpha.Messages.Site.Tests.Fixtures;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Messages.Site.Tests.Handlers
{
    public class AnnouncementHandlerTest
    {
        private AnnouncementService announcementService;
        private AnnouncementFormatService formatService;
        private AnnouncementHandler handler;
        private UserSessionFixture fixture;
        [SetUp]
        public void SetUp()
        {
            announcementService = Substitute.For<AnnouncementService>();
            formatService = Substitute.For<AnnouncementFormatService>();
            handler = new AnnouncementHandler(announcementService, formatService);
            fixture = new UserSessionFixture();
        }

        [Test]
        public void Handle_Always_CallGetHotAnnouncement()
        {
            //// Arrange
            var query = new HotAnnouncementQuery(fixture.UserSession);            

            //// Act
            handler.Handle(query);

            //// Assert
            announcementService.Received().GetHotAnnouncement(Arg.Any<GetHotAnnouncementsParams>());
        }

        [Test]
        public void Handle_Always_CallFormatService()
        {
            //// Arrange
            var announcements = new List<HotAnnouncement>();            
            var query = new HotAnnouncementQuery(fixture.UserSession);
            announcementService.GetHotAnnouncement(Arg.Any<GetHotAnnouncementsParams>()).Returns(announcements);

            //// Act
            handler.Handle(query);

            //// Assert
            formatService.Received().Format(announcements, Arg.Any<GetHotAnnouncementsParams>());
        }

    }
}
