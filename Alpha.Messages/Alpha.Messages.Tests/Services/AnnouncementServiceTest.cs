namespace Alpha.Messages.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Announcements.Models;
    using Announcements.Services;
    using Client;
    using Client.Requests;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using KellermanSoftware.CompareNetObjects;
    using Messages.Configurations;
    using Messages.Criteria.Announcement;
    using Messages.Models.Announcement;
    using Messages.Services;
    using NSubstitute;
    using NUnit.Framework;
    using Params.Announcement;
    using Utils;

    public class AnnouncementServiceImplTest
    {
        private ICacheService cacheService;
        private AnnouncementClient announcementClient;
        private SpecificationService specificationService;
        private IDynamicRepository dynamicRepository;
        private AnnouncementService service;
        private CompareLogic compareLogic;

        [SetUp]
        public void SetUp()
        {
            cacheService = Substitute.For<ICacheService>();
            announcementClient = Substitute.For<AnnouncementClient>();
            specificationService = Substitute.For<SpecificationService>();
            dynamicRepository = Substitute.For<IDynamicRepository>();
            service = new AnnouncementServiceImpl(announcementClient, specificationService, dynamicRepository, cacheService);
            compareLogic = new CompareLogic();
        }

        [Test]
        public void UpdateLastSeenAnnouncement_Always_CallDynamicRepository()
        {
            //// Arrange
            dynamicRepository.Execute(Arg.Any<UpdateLastSeenAnnouncementCriteria>());

            //// Act
            service.UpdateLastSeenAnnouncement(121, 332, false, 0);

            //// Assert
            dynamicRepository.Received().Execute(Arg.Any<UpdateLastSeenAnnouncementCriteria>());
        }

        [Test]
        public void GetNumberOfNewAnnouncement_TodayHasNoNewMessage_ReturnZero()
        {
            //// Arrange
            announcementClient.GetAnnouncementsHistory(Arg.Any<AnnouncementHistoryRequest>()).Returns(Enumerable.Empty<Announcement>());
            var lastSeenAnnouncement = new LastSeenAnnouncement(123, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
            dynamicRepository.Get<LastSeenAnnouncement>(Arg.Any<GetLastSeenAnnouncementCriteria>()).Returns(lastSeenAnnouncement);

            //// Act
            var act = service.GetNumberOfNewAnnouncements(new BaseAnnouncementsParams());

            //// Assert
            Assert.Zero(act);
        }

        [Test]
        public void GetNumberOfNewAnnouncement_HasNewAnnouncementAndNoSee_ReturnNewAnnouncement()
        {
            //// Arrange
            var newAnnouncements = CreateAnnouncements(DateTime.Now.AddMinutes(-1));
            var lastSeenAnnouncement = new LastSeenAnnouncement(123, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(-1));
            announcementClient.GetAnnouncementsHistory(Arg.Any<AnnouncementHistoryRequest>()).Returns(newAnnouncements);
            dynamicRepository.Get<LastSeenAnnouncement>(Arg.Any<GetLastSeenAnnouncementCriteria>()).Returns(lastSeenAnnouncement);

            //// Act
            var act = service.GetNumberOfNewAnnouncements(new BaseAnnouncementsParams());

            //// Assert
            Assert.AreEqual(newAnnouncements.Count(), act);
        }

        [Test]
        public void GetNumberOfNewAnnouncement_HasNewAnnouncementAndSeen_ReturnZero()
        {
            //// Arrange
            var newAnnouncements = CreateAnnouncements(DateTime.Now.AddMinutes(-1));
            var lastSeenAnnouncement = new LastSeenAnnouncement(123, DateTime.Now.AddSeconds(10), DateTime.Now.AddSeconds(10), DateTime.Now.AddSeconds(10), DateTime.Now.AddSeconds(10));
            announcementClient.GetAnnouncementsHistory(Arg.Any<AnnouncementHistoryRequest>()).Returns(newAnnouncements);
            dynamicRepository.Get<LastSeenAnnouncement>(Arg.Any<GetLastSeenAnnouncementCriteria>()).Returns(lastSeenAnnouncement);

            //// Act
            var act = service.GetNumberOfNewAnnouncements(new BaseAnnouncementsParams());

            //// Assert
            Assert.Zero(act);
        }

        [Test]
        public void GetAnnouncementHistory_ReleaseDateTimeMoreThanNow_NotShow()
        {
            //// Arrange
            var today = DateTime.Now.AddMinutes(1);
            var announcements = CreateAnnouncements(today);
            var lastSeenAnnouncement = new LastSeenAnnouncement(123, today, today, today, today);
            dynamicRepository.Get<LastSeenAnnouncement>(Arg.Any<GetLastSeenAnnouncementCriteria>()).Returns(lastSeenAnnouncement);
            announcementClient.GetAnnouncementsHistory(Arg.Any<AnnouncementHistoryRequest>()).Returns(announcements);

            //// Act
            var act = service.GetAnnouncementsHistory(new AnnouncementsHistoryParams());

            //// Assert
            Assert.IsTrue(act.Count() == 0);
        }

        [Test]
        public void GetAnnouncementHistory_Always_OrderByDescendingByReleaseDate()
        {
            //// Arrange
            var today = DateTime.Now.AddMinutes(-1);
            var announcements = CreateAnnouncements(today);
            var expectedAnnouncements = new List<AlphaAnnouncement>
            {
                new AlphaAnnouncement(0,string.Empty,string.Empty,today.AddSeconds(1),today,null),
                new AlphaAnnouncement(0,string.Empty,string.Empty,today,today,null),
            };
            var lastSeenAnnouncement = new LastSeenAnnouncement(123, today, today, today, today);
            dynamicRepository.Get<LastSeenAnnouncement>(Arg.Any<GetLastSeenAnnouncementCriteria>()).Returns(lastSeenAnnouncement);
            announcementClient.GetAnnouncementsHistory(Arg.Any<AnnouncementHistoryRequest>()).Returns(announcements);

            //// Act
            var act = service.GetAnnouncementsHistory(new AnnouncementsHistoryParams());

            //// Assert
            Assert.IsTrue(compareLogic.Compare(expectedAnnouncements, act.ToList()).AreEqual);
        }

        [Test]
        public void GetAnnouncementHistory_Always_CallAnnouncementApi()
        {
            //// Arrange
            var lastSeenAnnouncement = new LastSeenAnnouncement(123, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
            dynamicRepository.Get<LastSeenAnnouncement>(Arg.Any<GetLastSeenAnnouncementCriteria>()).Returns(lastSeenAnnouncement);
            announcementClient.GetAnnouncementsHistory(Arg.Any<AnnouncementHistoryRequest>()).Returns(Enumerable.Empty<Announcement>());

            //// Act
            service.GetAnnouncementsHistory(new AnnouncementsHistoryParams());

            //// Assert
            announcementClient.GetAnnouncementsHistory(Arg.Any<AnnouncementHistoryRequest>());
        }

        [Test]
        public void GetAnnouncementHistory_OnceSearchAnnouncement_UpdateLastSeenSystem()
        {
            //// Arrange
            var lastSeenAnnouncement = new LastSeenAnnouncement(123, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
            dynamicRepository.Get<LastSeenAnnouncement>(Arg.Any<GetLastSeenAnnouncementCriteria>()).Returns(lastSeenAnnouncement);
            announcementClient.GetAnnouncementsHistory(Arg.Any<AnnouncementHistoryRequest>()).Returns(Enumerable.Empty<Announcement>());

            //// Act
            service.GetAnnouncementsHistory(new AnnouncementsHistoryParams());

            //// Assert
            dynamicRepository.Received().Execute(Arg.Any<UpdateLastSeenAnnouncementCriteria>());
        }

        [Test]
        public void GetAnnouncementHistory_FiveMonthNoLogin_FromDateIsFiveMonthAgo()
        {
            //// Arrange
            var fiveMonthAgo = DateTime.Now.AddMonths(-5);
            var newAnnouncements = CreateAnnouncements(DateTime.Now);

            var lastSeenAnnouncement = new LastSeenAnnouncement(123, fiveMonthAgo, fiveMonthAgo, fiveMonthAgo, fiveMonthAgo);
            dynamicRepository.Get<LastSeenAnnouncement>(Arg.Any<GetLastSeenAnnouncementCriteria>()).Returns(lastSeenAnnouncement);
            announcementClient.GetAnnouncementsHistory(Arg.Any<AnnouncementHistoryRequest>()).Returns(newAnnouncements);

            //// Act
            service.GetAnnouncementsHistory(new AnnouncementsHistoryParams());

            //// Assert
            announcementClient.Received().GetAnnouncementsHistory(Arg.Is<AnnouncementHistoryRequest>(i => i.FromDate.Date == fiveMonthAgo.Date));
        }

        [Test]
        public void GetAnnouncementHistory_OneYearNoLogin_FromDateIsSixMonthAgo()
        {
            //// Arrange
            var sixMonthAgo = DateTime.Now.AddMonths(-6);
            var newAnnouncements = CreateAnnouncements(DateTime.Now.AddMinutes(-1));

            var lastSeenAnnouncement = new LastSeenAnnouncement(123, DateTime.Now.AddYears(-1), DateTime.Now.AddYears(-1), DateTime.Now.AddYears(-1), DateTime.Now.AddYears(-1));
            dynamicRepository.Get<LastSeenAnnouncement>(Arg.Any<GetLastSeenAnnouncementCriteria>()).Returns(lastSeenAnnouncement);
            announcementClient.GetAnnouncementsHistory(Arg.Any<AnnouncementHistoryRequest>()).Returns(newAnnouncements);

            //// Act
            service.GetAnnouncementsHistory(new AnnouncementsHistoryParams());

            //// Assert
            announcementClient.Received().GetAnnouncementsHistory(Arg.Is<AnnouncementHistoryRequest>(i => i.FromDate.Date == sixMonthAgo.Date));
        }

        [Test]
        public void GetLastSeenAnnouncement_CannotFindCustomer_ReturnDefault()
        {
            //// Arrange
            SystemTime.Now = DateTime.Now;
            var defaultLastSeen = new LastSeenAnnouncement(123, SystemTime.Now, SystemTime.Now, SystemTime.Now, SystemTime.Now);
            LastSeenAnnouncement lastSeen = null;
            dynamicRepository.Get<LastSeenAnnouncement>(Arg.Any<GetLastSeenAnnouncementCriteria>()).Returns(lastSeen);

            //// Act
            var act = service.GetLastSeenAnnouncement(123, 123);

            //// Assert
            Assert.AreEqual(defaultLastSeen.CustId, act.CustId);
            Assert.AreEqual(defaultLastSeen.LastSeenNormal, act.LastSeenNormal);
            Assert.AreEqual(defaultLastSeen.LastSeenPersonal, act.LastSeenPersonal);
            Assert.AreEqual(defaultLastSeen.LastSeenSpecial, act.LastSeenSpecial);
            Assert.AreEqual(defaultLastSeen.LastSeenSystem, act.LastSeenSystem);
        }

        [Test]
        public void GetAnnouncements_Always_CallsAnnouncementClientGetAnnouncementsHistory()
        {
            // Arrange
            var fromTime = DateTime.Now;
            var toTime = DateTime.Now;
            var request = new AnnouncementHistoryRequest(fromTime, toTime, AppSettings.Environment, string.Empty);

            // Act
            service.GetAnnouncements(fromTime, toTime);

            // Assert
            announcementClient.Received().GetAnnouncementsHistory(
                Arg.Is<AnnouncementHistoryRequest>(
                    r => r.FromDate == request.FromDate
                        && r.ToDate == request.ToDate
                        && r.System == request.System
                        && r.Environment == request.Environment));
        }

        private IEnumerable<Announcement> CreateAnnouncements(DateTime dateTime)
            => new List<Announcement>
            {
                new Announcement
                {
                    ReleasedDate = dateTime,
                    Code = string.Empty,
                    Contents =new List<MessageContent> { new MessageContent { LanguageKey = "en-US" } }
                },
                new Announcement
                {
                    ReleasedDate = dateTime.AddSeconds(1),
                    Code = string.Empty,
                    Contents =new List<MessageContent> { new MessageContent { LanguageKey = "en-US" } }
                },
            };

        [Test]
        public void GetPopUpAnnouncements_Always_ReturnNull()
        {
            // Arrange
            var param = new PopupAnnouncementParams()
            {
                IsShowedPopup = true
            };

            // Act
            var act = service.GetPopUpAnnouncements(param);

            // Assert
            Assert.IsNull(act);
        }
    }
}