using Alpha.Announcements.Models;
using Alpha.Announcements.Services;
using Alpha.Announcements.Specs;
using Alpha.Messages.Client;
using Alpha.Messages.Client.Requests;
using Alpha.Messages.Configurations;
using Alpha.Messages.Params;
using Alpha.Messages.Services;
using Alpha.Messages.Tests.Fixtures;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Messages.Tests.Services
{
    public class AnnouncementFromApiServiceTest
    {
        private readonly Fixture fixture = new Fixture();
        private AnnouncementFromApiService apiService;
        private AnnouncementClient announcementClient;
        private SpecificationService specificationService;
        private FanexCacheFixture cacheFixture;

        [SetUp]
        public void SetUp()
        {
            announcementClient = Substitute.For<AnnouncementClient>();
            specificationService = Substitute.For<SpecificationService>();
            specificationService.IsSatisfied(Arg.Any<IEnumerable<ISpecification<IParam>>>(), Arg.Any<IParam>()).Returns(true);

            apiService = new AnnoucemtnFromApiServiceImpl(announcementClient, specificationService);
            cacheFixture = new FanexCacheFixture();
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["Environment"] = "local",
                ["SiteGroup"] = "ibc",
            };
        }

        [Test]
        public void GetPopupAnnouncement_NotExistedCache_CallApi()
        {
            //// Arrange            
            announcementClient.GetAnnouncements(Arg.Any<QueryAnnouncementRequest>()).Returns(Enumerable.Empty<Announcement>());

            //// Act
            var act = apiService.GetPopupAnnouncements(new GetHotAnnouncementsParams());

            //// Assert
            announcementClient.Received().GetAnnouncements(Arg.Any<QueryAnnouncementRequest>());
        }

        [Test]
        public void GetHeaderAnnouncement_NotExistedCache_CallApi()
        {
            //// Arrange
            announcementClient.GetAnnouncements(Arg.Any<QueryAnnouncementRequest>()).Returns(Enumerable.Empty<Announcement>());

            //// Act
            var act = apiService.GetHeaderAnnouncements(new GetHotAnnouncementsParams());

            //// Assert
            announcementClient.Received().GetAnnouncements(Arg.Any<QueryAnnouncementRequest>());
        }
    }
}
