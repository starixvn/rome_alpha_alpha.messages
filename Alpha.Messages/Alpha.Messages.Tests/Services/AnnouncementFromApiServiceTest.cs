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
using RestSharp;
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
        private AnnouncementClient announcementClientImpl;
        private IRestClient restClient;
        private SpecificationService specificationService;
        private FanexCacheFixture cacheFixture;

        [SetUp]
        public void SetUp()
        {
            restClient = Substitute.For<RestClient>();
            announcementClientImpl = new AnnouncementClientImpl(restClient);
            announcementClient = Substitute.For<AnnouncementClient>();
            specificationService = Substitute.For<SpecificationService>();
            specificationService.IsSatisfied(Arg.Any<IEnumerable<ISpecification<IParam>>>(), Arg.Any<IParam>()).Returns(true);

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
            apiService = new AnnoucementFromApiServiceImpl(announcementClient, specificationService);
            announcementClient.GetAnnouncements(Arg.Any<QueryAnnouncementRequest>()).Returns(Enumerable.Empty<Announcement>());

            //// Act
            var act = apiService.GetPopupAnnouncements(new GetHotAnnouncementsParams());

            //// Assert
            announcementClient.Received().GetAnnouncements(Arg.Any<QueryAnnouncementRequest>());
        }

        [Test]
        public void GetPopupAnnouncement_AnnouncementNoSpec_Return2Announcement()
        {
            //// Arrange
            restClient.Execute(Arg.Any<IRestRequest>()).Returns(new RestResponse
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = AnnouncementFixture.AllContent
            });
            apiService = new AnnoucementFromApiServiceImpl(announcementClientImpl, specificationService);

            //// Act
            var act = apiService.GetPopupAnnouncements(new GetHotAnnouncementsParams { Language = "xxx" });

            //// Assert
            Assert.AreEqual(2, act.ToList().Count());
        }

        [Test]
        public void GetHeaderAnnouncement_NotExistedCache_CallApi()
        {
            //// Arrange
            apiService = new AnnoucementFromApiServiceImpl(announcementClient, specificationService);
            announcementClient.GetAnnouncements(Arg.Any<QueryAnnouncementRequest>()).Returns(Enumerable.Empty<Announcement>());

            //// Act
            var act = apiService.GetHeaderAnnouncements(new GetHotAnnouncementsParams());

            //// Assert
            announcementClient.Received().GetAnnouncements(Arg.Any<QueryAnnouncementRequest>());
        }

        [Test]
        public void GetHeaderAnnouncement_AnnouncementNoSpec_Return1Announcement()
        {
            //// Arrange
            restClient.Execute(Arg.Any<IRestRequest>()).Returns(new RestResponse
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = AnnouncementFixture.AllContent
            });
            apiService = new AnnoucementFromApiServiceImpl(announcementClientImpl, specificationService);

            //// Act
            var act = apiService.GetHeaderAnnouncements(new GetHotAnnouncementsParams { Language = "en-US" });

            //// Assert
            Assert.AreEqual(1, act.Count());
        }

        [Test]
        public void GetPopUpAnnouncement_NotFindingLangueage_ReturnDefaultAnnouncement()
        {
            //// Arrange
            restClient.Execute(Arg.Any<IRestRequest>()).Returns(new RestResponse
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = AnnouncementFixture.AllContent
            });
            apiService = new AnnoucementFromApiServiceImpl(announcementClientImpl, specificationService);

            //// Act
            var act = apiService.GetPopupAnnouncements(new GetHotAnnouncementsParams { Language = "ja-JP" }).ToList();

            //// Assert            
            Assert.AreEqual("<p>We are pleased to announce that you are allowed to transfer your Cash Balance to your downlines and vice versa, from now on.Moreover, the quantity of Yesterday Balance transfer for each member is now unlimited.</p><p>If you need further assistance, please contact our Customer Service Department.</p>",
                act[0].Content);
            Assert.AreEqual("xx", act[1].Content);
        }

        [Test]
        public void GetPopUpAnnouncement_2Announcement_SortByOrdinal()
        {
            //// Arrange
            restClient.Execute(Arg.Any<IRestRequest>()).Returns(new RestResponse
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = AnnouncementFixture.PopupContent
            });
            apiService = new AnnoucementFromApiServiceImpl(announcementClientImpl, specificationService);

            //// Act
            var act = apiService.GetPopupAnnouncements(new GetHotAnnouncementsParams { Language = "ja-JP" }).ToList();

            //// Assert            
            Assert.AreEqual("code1", act[0].Code);
            Assert.AreEqual("code2", act[1].Code);
            Assert.AreEqual("code3", act[2].Code);
        }
    }
}
