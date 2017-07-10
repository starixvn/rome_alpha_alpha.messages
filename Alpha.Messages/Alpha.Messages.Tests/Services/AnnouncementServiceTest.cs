using Alpha.Messages.Models;
using Alpha.Messages.Params;
using Alpha.Messages.Services;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Messages.Tests.Services
{
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
            apiService.GetHotAnnouncements(Arg.Any<GetHotAnnouncementsParams>()).
                Returns(new List<HotAnnouncement>() { new HotAnnouncement() });

            dbService.GetHotAnnouncements(Arg.Any<GetHotAnnouncementsParams>()).
                Returns(new List<HotAnnouncement>() { new HotAnnouncement() });

            //// Act
            var act = service.GetHotAnnouncement(new GetHotAnnouncementsParams());

            //// Assert
            Assert.AreEqual(numberItem, act.Count());
        }

        [Test]
        public void GetHotAnnouncement_Always_CallApiAndDbService()
        {
            //// Arrange

            //// Act
            service.GetHotAnnouncement(new GetHotAnnouncementsParams());

            //// Assert
            apiService.GetHotAnnouncements(Arg.Any<GetHotAnnouncementsParams>()).Received();
            dbService.GetHotAnnouncements(Arg.Any<GetHotAnnouncementsParams>()).Received();
        }
    }
}
