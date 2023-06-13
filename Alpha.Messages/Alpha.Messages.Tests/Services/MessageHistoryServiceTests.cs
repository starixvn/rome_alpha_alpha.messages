namespace Alpha.Messages.Tests.Services
{
    using System;
    using System.Collections.Specialized;
    using Constants;
    using Fanex.Data.Repository;
    using Fixtures;
    using Messages.Configurations;
    using Messages.Criteria.Message;
    using Messages.Models.Announcement;
    using Messages.Models.Message;
    using Messages.Services;
    using NSubstitute;
    using NUnit.Framework;
    using Params;
    using Ploeh.AutoFixture;

    [TestFixture]
    public class MessageHistoryServiceTests
    {
        private MessageHistoryService messageService;
        private AnnouncementService announcementService;
        private IDynamicRepository dynamicRepository;
        private Fixture fixture;
        private const string SiteSuperId = "234";
        private const string SiteMasterId = "2345";
        private const string SiteAgentId = "2346";
        [SetUp]
        public void SetUp()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            announcementService = Substitute.For<AnnouncementService>();
            messageService = new MessageHistoryServiceImpl(announcementService, dynamicRepository);
            fixture = new Fixture();
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["Message:Super"] = SiteSuperId,
                ["Message:Master"] = SiteMasterId,
                ["Message:Agent"] = SiteAgentId
            };
            new FanexCacheFixture();
        }

        [Test]
        public void GetMessagesHistory_UserIsInLimitedDateRange_UseLimitedDate()
        {
            //// Arrange
            var personalType = 1;
            var oneYearAgo = DateTime.Now.AddYears(-1);
            var lastSeenAnnouncement = new LastSeenAnnouncement(123, oneYearAgo, oneYearAgo, oneYearAgo, oneYearAgo);
            var limitedDate = DateTime.Today.AddDays(1);
            announcementService.GetLastSeenAnnouncement(Arg.Any<int>(), Arg.Any<int>()).Returns(lastSeenAnnouncement);
            announcementService.UpdateLastSeenAnnouncement(Arg.Any<int>(), Arg.Any<int>(), false, Arg.Any<int>());

            //// Act
            messageService.GetMessagesHistory(
                new MessageHistoryParams(
                    CustId.ValidId,
                    0,
                    0,
                    "1",
                    1,
                    DateTime.Today,
                    DateTime.Today,
                    limitedDate,
                    false,
                    personalType,
                    null));

            //// Assert
            dynamicRepository.Received().FetchAsync<MessageHistory>(Arg.Is<GetMessagesHistoryCriteria>(i => i.StartDate == limitedDate));
        }

        [Test]
        public void GetMessagesHistory_NoHasNewMessage_FromDateIsThreeDayAgo()
        {
            //// Arrange
            var personalType = 1;
            var threeDayAgo = DateTime.Now.AddDays(-3);
            var lastSeenAnnouncement = new LastSeenAnnouncement(123, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
            announcementService.GetLastSeenAnnouncement(Arg.Any<int>(), Arg.Any<int>()).Returns(lastSeenAnnouncement);

            //// Act
            messageService.GetMessagesHistory(
                new MessageHistoryParams(
                    CustId.ValidId,
                    0,
                    0,
                    "1",
                    1,
                    DateTime.MinValue,
                    DateTime.Now,
                    DateTime.MinValue,
                    false,
                    personalType,
                    null));

            //// Assert
            dynamicRepository.Received().FetchAsync<MessageHistory>(Arg.Is<GetMessagesHistoryCriteria>(i => i.StartDate.Date == threeDayAgo.Date));
        }

        [Test]
        public void GetMessagesHistory_HasNewMessage_FromDateIsNewReleaseDate()
        {
            //// Arrange
            var personalType = 1;
            var newReleaseDate = DateTime.Now.AddMonths(-3);
            var lastSeenAnnouncement = new LastSeenAnnouncement(123, newReleaseDate, newReleaseDate, newReleaseDate, newReleaseDate);
            announcementService.GetLastSeenAnnouncement(Arg.Any<int>(), Arg.Any<int>()).Returns(lastSeenAnnouncement);

            //// Act
            messageService.GetMessagesHistory(
                new MessageHistoryParams(
                    CustId.ValidId,
                    0,
                    0,
                    "1",
                    1,
                    DateTime.MinValue,
                    DateTime.Now,
                    DateTime.MinValue,
                    false,
                    personalType,
                    null));

            //// Assert
            dynamicRepository.Received().FetchAsync<MessageHistory>(Arg.Is<GetMessagesHistoryCriteria>(i => i.StartDate.Date == newReleaseDate.Date));
        }

        [Test]
        public void GetMessagesHistory_LongTimeNoLogin_FromDateIsThreeMonthAgo()
        {
            //// Arrange
            var personalType = 1;
            var lastLoginDate = DateTime.Now.AddYears(-1);
            var threeMonthAgo = DateTime.Now.AddMonths(-3);
            var lastSeenAnnouncement = new LastSeenAnnouncement(123, lastLoginDate, lastLoginDate, lastLoginDate, lastLoginDate);
            announcementService.GetLastSeenAnnouncement(Arg.Any<int>(), Arg.Any<int>()).Returns(lastSeenAnnouncement);

            //// Act
            messageService.GetMessagesHistory(
                new MessageHistoryParams(
                    CustId.ValidId,
                    0,
                    0,
                    "1",
                    1,
                    DateTime.MinValue,
                    DateTime.Now,
                    DateTime.MinValue,
                    false,
                    personalType,
                    null));

            //// Assert
            dynamicRepository.Received().FetchAsync<MessageHistory>(Arg.Is<GetMessagesHistoryCriteria>(i => i.StartDate.Date == threeMonthAgo.Date));
        }
    }
}
