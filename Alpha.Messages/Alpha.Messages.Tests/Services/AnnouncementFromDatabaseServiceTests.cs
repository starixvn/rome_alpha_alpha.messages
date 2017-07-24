using Alpha.Messages.Configurations;
using Alpha.Messages.Criteria;
using Alpha.Messages.Models;
using Alpha.Messages.Params;
using Alpha.Messages.Services;
using Fanex.Data.Repository;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Alpha.Messages.Tests.Services
{
    public class AnnouncementFromDatabaseServiceTests
    {
        private IDynamicRepository dynamicRepository;
        private ResourceManager resourceManager;
        private AnnouncementFromDatabaseServiceImpl service;

        [SetUp]
        public void SetUp()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            resourceManager = Substitute.For<ResourceManager>();
            service = new AnnouncementFromDatabaseServiceImpl(dynamicRepository, resourceManager);
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["NoticeTypes"] = "1_2,1,2,3"
            };
        }

        [Test]
        public void GetHotAnnouncements_UserIsNotAdmin_CallDynamicRepositoryToGetMessageTypes()
        {
            //// Arrange
            var parameters = new GetHotAnnouncementsParams { RoleId = 1 };
            dynamicRepository.Fetch<byte>(Arg.Any<GetNoticeListIsNoViewedCriteria>()).Returns(Enumerable.Empty<byte>());

            //// Act
            service.GetHotAnnouncements(parameters);

            //// Assert
            dynamicRepository.Received().Fetch<byte>(Arg.Any<GetNoticeListIsNoViewedCriteria>());
        }

        [Test]
        public void GetHotAnnouncements_NotUserAdminAndNotAgentAndContainFishing_ReturnFishing()
        {
            //// Arrange
            var parameters = new GetHotAnnouncementsParams { RoleId = 2 };
            dynamicRepository.Fetch<byte>(Arg.Any<GetNoticeListIsNoViewedCriteria>()).Returns(Enumerable.Empty<byte>());

            //// Act
            service.GetHotAnnouncements(parameters);

            //// Assert
            dynamicRepository.Received().Fetch<byte>(Arg.Any<GetNoticeListIsNoViewedCriteria>());
        }

        [Test]
        public void GetHotAnnouncements_HasFishingWordNotices_ReturnFishingWordNotices()
        {
            //// Arrange
            var parameters = new GetHotAnnouncementsParams
            {
                RoleId = 3,
                NoticeTypes = new string[] { "1_2", "1", "2", "3" } }
            ;
            dynamicRepository.Fetch<byte>(Arg.Any<GetNoticeListIsNoViewedCriteria>())
                .Returns(new List<byte> { 1, 2, 3, 4 });
            dynamicRepository.Fetch<int>(Arg.Any<InsertPersonalMessageCriteria>())
                .Returns(new List<int> { 100 });
            dynamicRepository.Fetch<bool>(Arg.Any<InsertPersonalMessageTargetCriteria>())
                .Returns(new List<bool> { true });
            dynamicRepository.Fetch<bool>(Arg.Any<UpdateViewStatus>())
                .Returns(new List<bool> { true });

            //// Act
            var act = service.GetHotAnnouncements(parameters);

            //// Assert
            Assert.AreEqual(1, act.Count());
        }
        [Test]
        public void GetHotAnnouncements_HasNotices_ReturnFishingWordNotices()
        {
            //// Arrange
            var parameters = new GetHotAnnouncementsParams
            {
                RoleId = 2,
                NoticeTypes = new string[] { "1_2", "1", "2", "3" }
            }
            ;
            dynamicRepository.Fetch<byte>(Arg.Any<GetNoticeListIsNoViewedCriteria>())
                .Returns(new List<byte> { 1, 2, 4 });
            dynamicRepository.Fetch<int>(Arg.Any<InsertPersonalMessageCriteria>())
                .Returns(new List<int> { 100 });
            dynamicRepository.Fetch<bool>(Arg.Any<InsertPersonalMessageTargetCriteria>())
                .Returns(new List<bool> { true });
            dynamicRepository.Fetch<bool>(Arg.Any<UpdateViewStatus>())
                .Returns(new List<bool> { true });

            //// Act
            var act = service.GetHotAnnouncements(parameters);

            //// Assert
            Assert.AreEqual(3, act.Count());
        }

    }
}
