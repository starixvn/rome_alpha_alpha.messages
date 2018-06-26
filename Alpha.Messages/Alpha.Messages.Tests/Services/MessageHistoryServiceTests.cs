namespace Alpha.Messages.Tests.Services
{
    using System;
    using System.Collections.Specialized;
    using System.Linq;
    using Constants;
    using Fanex.Data.Repository;
    using Fixtures;
    using Messages.Configurations;
    using Messages.Criteria.Message;
    using Messages.DataAccesses;
    using Messages.Models.Announcement;
    using Messages.Models.Constants;
    using Messages.Models.Message;
    using Messages.Services;
    using NSubstitute;
    using NUnit.Framework;
    using Params;
    using Ploeh.AutoFixture;
    using Site.Tests.Helpers;
    using Assert = NUnit.Framework.Assert;

    [TestFixture]
    public class MessageHistoryServiceTests
    {
        private MessageHistoryService messageService;
        private AnnouncementService announcementService;
        private MessageDataAccess messageDataAccess;
        private Fixture fixture;
        private const string SiteSuperId = "234";
        private const string SiteMasterId = "2345";
        private const string SiteAgentId = "2346";
        [SetUp]
        public void SetUp()
        {
            messageDataAccess = Substitute.For<MessageDataAccess>();
            announcementService = Substitute.For<AnnouncementService>();
            messageService = new MessageHistoryServiceImpl(messageDataAccess, announcementService);
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
            announcementService.GetLastSeenAnnouncement(Arg.Any<int>()).Returns(lastSeenAnnouncement);
            announcementService.UpdateLastSeenAnnouncement(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(), false);

            //// Act
            messageService.GetMessagesHistory(
                new MessageHistoryParams(
                    123,
                    CustId.ValidId,
                    false,
                    "1",
                    1,
                    1,
                    DateTime.Today,
                    DateTime.Today,
                    limitedDate,
                    false,
                    personalType));

            //// Assert
            messageDataAccess.Received().GetMessagesHistory(Arg.Is<GetMessagesHistoryCriteria>(i => i.StartDate == limitedDate));
        }

        [Test]
        public void GetMessagesHistory_NoHasNewMessage_FromDateIsThreeDayAgo()
        {
            //// Arrange
            var personalType = 1;
            var threeDayAgo = DateTime.Now.AddDays(-3);
            var lastSeenAnnouncement = new LastSeenAnnouncement(123, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
            announcementService.GetLastSeenAnnouncement(Arg.Any<int>()).Returns(lastSeenAnnouncement);

            //// Act
            messageService.GetMessagesHistory(
                new MessageHistoryParams(
                    123,
                    CustId.ValidId,
                    false,
                    "1",
                    1,
                    1,
                    DateTime.MinValue,
                    DateTime.Now,
                    DateTime.MinValue,
                    false,
                    personalType));

            //// Assert
            messageDataAccess.Received().GetMessagesHistory(Arg.Is<GetMessagesHistoryCriteria>(i => i.StartDate.Date == threeDayAgo.Date));
        }

        [Test]
        public void GetMessagesHistory_HasNewMessage_FromDateIsNewReleaseDate()
        {
            //// Arrange
            var personalType = 1;
            var newReleaseDate = DateTime.Now.AddMonths(-3);
            var lastSeenAnnouncement = new LastSeenAnnouncement(123, newReleaseDate, newReleaseDate, newReleaseDate, newReleaseDate);
            announcementService.GetLastSeenAnnouncement(Arg.Any<int>()).Returns(lastSeenAnnouncement);

            //// Act
            messageService.GetMessagesHistory(
                new MessageHistoryParams(
                    321,
                    CustId.ValidId,
                    false,
                    "1",
                    1,
                    1,
                    DateTime.MinValue,
                    DateTime.Now,
                    DateTime.MinValue,
                    false,
                    personalType));

            //// Assert
            messageDataAccess.Received().GetMessagesHistory(Arg.Is<GetMessagesHistoryCriteria>(i => i.StartDate.Date == newReleaseDate.Date));
        }

        [Test]
        public void GetMessagesHistory_LongTimeNoLogin_FromDateIsSixMonthAgo()
        {
            //// Arrange
            var personalType = 1;
            var lastLoginDate = DateTime.Now.AddYears(-1);
            var sixMonthAgo = DateTime.Now.AddMonths(-6);
            var lastSeenAnnouncement = new LastSeenAnnouncement(123, lastLoginDate, lastLoginDate, lastLoginDate, lastLoginDate);
            announcementService.GetLastSeenAnnouncement(Arg.Any<int>()).Returns(lastSeenAnnouncement);

            //// Act
            messageService.GetMessagesHistory(
                new MessageHistoryParams(
                    321,
                    CustId.ValidId,
                    false,
                    "1",
                    1,
                    1,
                    DateTime.MinValue,
                    DateTime.Now,
                    DateTime.MinValue,
                    false,
                    personalType));

            //// Assert
            messageDataAccess.Received().GetMessagesHistory(Arg.Is<GetMessagesHistoryCriteria>(i => i.StartDate.Date == sixMonthAgo.Date));
        }
    }
}
