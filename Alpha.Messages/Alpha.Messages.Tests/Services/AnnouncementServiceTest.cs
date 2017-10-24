namespace Alpha.Messages.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Messages.Models.Announcement;
    using Messages.Services;
    using NSubstitute;
    using NUnit.Framework;
    using Params.Announcement;

    public class AnnouncementServiceImplTest
    {
        private AnnouncementFromApiService apiService;
        private AnnouncementFromDatabaseService dbService;
        private AnnouncementService service;

        [SetUp]
        public void SetUp()
        {
            apiService = Substitute.For<AnnouncementFromApiService>();
            dbService = Substitute.For<AnnouncementFromDatabaseService>();
            service = new AnnouncementServiceImpl(apiService, dbService);
        }

        [Test]
        public void GetHotAnnouncement_ApiAndDbHasAnnouncement_ReturnConcatApiAndDb()
        {
            //// Arrange
            var numberItem = 2;
            apiService.GetPopupAnnouncements(Arg.Any<GetHotAnnouncementsParams>()).
                Returns(new List<HotAnnouncement>() { new HotAnnouncement() });

            dbService.GetFirstHotAnnouncement(Arg.Any<GetHotAnnouncementsParams>()).
                Returns(new HotAnnouncement());

            //// Act
            var act = service.GetPopupAnnouncement(new GetHotAnnouncementsParams());

            //// Assert
            Assert.AreEqual(numberItem, act.Count());
        }

        [Test]
        public void GetHotAnnouncement_DBAnnouncementEmpty_ReturnApiAnnouncement()
        {
            //// Arrange
            HotAnnouncement firstAnnouncement = null;
            apiService.GetPopupAnnouncements(Arg.Any<GetHotAnnouncementsParams>()).
                Returns(new List<HotAnnouncement>() { new HotAnnouncement() });

            dbService.GetFirstHotAnnouncement(Arg.Any<GetHotAnnouncementsParams>()).
                Returns(firstAnnouncement);

            //// Act
            var act = service.GetPopupAnnouncement(new GetHotAnnouncementsParams());

            //// Assert
            Assert.AreEqual(1, act.Count());
        }

        [Test]
        public void GetHotAnnouncement_DBAnnouncementNoEmpty_ReturnApiAnnouncement()
        {
            //// Arrange            
            apiService.GetPopupAnnouncements(Arg.Any<GetHotAnnouncementsParams>()).
                Returns(new List<HotAnnouncement>() { new HotAnnouncement() });

            dbService.GetFirstHotAnnouncement(Arg.Any<GetHotAnnouncementsParams>()).
                Returns(new HotAnnouncement());

            //// Act
            var act = service.GetPopupAnnouncement(new GetHotAnnouncementsParams());

            //// Assert
            Assert.AreEqual(2, act.Count());
        }
        [Test]
        public void GetHotAnnouncement_Always_Call2Services()
        {
            //// Arrange            
            apiService.GetPopupAnnouncements(Arg.Any<GetHotAnnouncementsParams>()).
                Returns(new List<HotAnnouncement>() { new HotAnnouncement() });

            dbService.GetFirstHotAnnouncement(Arg.Any<GetHotAnnouncementsParams>()).
                Returns(new HotAnnouncement());

            //// Act
            var act = service.GetPopupAnnouncement(new GetHotAnnouncementsParams());

            //// Assert
            
            apiService.Received().GetPopupAnnouncements(Arg.Any<GetHotAnnouncementsParams>());
            dbService.Received().GetFirstHotAnnouncement(Arg.Any<GetHotAnnouncementsParams>());
        }
    }
}
