namespace Alpha.Messages.Site.Tests.Handlers
{
    using System;
    using System.Linq;
    using Alpha.Messages.Models;
    using Helpers;
    using KellermanSoftware.CompareNetObjects;
    using Messages.Constants;
    using Models.Announcement;
    using Models.Message;
    using NSubstitute;
    using NUnit.Framework;
    using Params;
    using Params.Announcement;
    using Ploeh.AutoFixture;
    using Services;
    using Site.Handlers;
    using Site.Requests.MessageHistory;
    using Site.ViewModels;
    using AlphaPlus.User.Models;

    [TestFixture]
    public class MessageHanlderTests
    {
        private MessageHistoryService messageService;
        private NewMessageService newMessageService;
        private AnnouncementService announcementService;
        private MessageHistoryHandler messageHandler;
#pragma warning disable CS0618 // Type or member is obsolete
        private IUserSession userSession;
#pragma warning restore CS0618 // Type or member is obsolete
        private Fixture fixture;

        [SetUp]
        public void SetUp()
        {
            messageService = Substitute.For<MessageHistoryService>();
            newMessageService = Substitute.For<NewMessageService>();
            announcementService = Substitute.For<AnnouncementService>();
            messageHandler = new MessageHistoryHandler(messageService, newMessageService, announcementService);

#pragma warning disable CS0618 // Type or member is obsolete
            userSession = Substitute.For<IUserSession>();
#pragma warning restore CS0618 // Type or member is obsolete
            userSession.Language.Returns(Language.English);
            userSession.CustId.Returns(263);
            userSession.RoleId.Returns(RoleId.Agent);
            userSession.GetProperty<DateTime>(SessionKey.CustomerDatelimit).Returns(DateTime.MinValue);
            fixture = new Fixture();
        }

        [Test]
        public void Handle_NotNullQuery_CallGetMessagesHistory()
        {
            // Arrange
            var messages = FakeDataHelper.CreateMany<MessageHistory>().ToList();
            var report = new PagedReportData<MessageHistory>(
                 messages,
                 1,
                 50,
                 100);

            messageService.GetMessagesHistory(Arg.Any<MessageHistoryParams>()).Returns(report);
            announcementService.GetLastSeenAnnouncement(Arg.Any<int>(), Arg.Any<int>())
                .Returns(FakeDataHelper.Create<LastSeenAnnouncement>());

            var query = new MessageHistoryQuery(
                userSession,
                1,
                DateTime.Today,
                DateTime.Today,
                false, 1, 100);

            // Act
            messageHandler.Handle(query);

            // Assert
            messageService
                .Received()
                .GetMessagesHistory(Arg.Is<MessageHistoryParams>(i => i.CustId == query.CustId
                && i.ParentSites == query.ParentSites
                && i.SpecialType == query.SpecialType
                && i.StartDate == query.StartDate
                && i.EndDate == query.EndDate
                && i.LimitedDate == query.LimitedDateToShowInfo
                && i.IsTest == query.IsTest
                && i.MessageType == query.MessageType));
        }

        [Test]
        public void HandleMessageQuery_NotNullQuery_ReturnMessagesViewModel()
        {
            // Arrange
            var compareLogic = new CompareLogic();
            UrlHelperTestSetUp.SetUp();
            var now = DateTime.Now;

            var messages = FakeDataHelper.CreateMany<MessageHistory>().ToList();
            var lastSeenAnnouncement = new LastSeenAnnouncement(263, now, now, now, now);
            var numberOfUnReadMessage = FakeDataHelper.Create<NumberOfUnreadMessage>();
            newMessageService.GetNumberOfUnreadMessage(Arg.Any<BaseAnnouncementsParams>()).Returns(numberOfUnReadMessage);
            var report = new PagedReportData<MessageHistory>(
                 messages,
                 1,
                 50,
                 100);
            messageService.GetMessagesHistory(Arg.Any<MessageHistoryParams>()).Returns(report);
            announcementService.GetLastSeenAnnouncement(Arg.Any<int>(), Arg.Any<int>()).Returns(lastSeenAnnouncement);

            var query = new MessageHistoryQuery(
                userSession,
                1,
                DateTime.Today,
                DateTime.Today,
                false, 1, 100);

            var expectedViewModel = new MessagesHistoryViewModel(
                report,
                now,
                Language.Default,
                query.StartDate,
                query.EndDate,
                query.MessageType,
                query.RoleId,
                numberOfUnReadMessage);

            // Act
            var viewModel = messageHandler.Handle(query);

            // Assert
            Assert.IsTrue(compareLogic.Compare(expectedViewModel, viewModel).AreEqual);
        }
    }
}