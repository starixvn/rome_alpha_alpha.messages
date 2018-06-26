namespace Alpha.Messages.Site.Tests.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Helpers;
    using KellermanSoftware.CompareNetObjects;
    using Messages.Constants;
    using Models.Announcement;
    using Models.Message;
    using NSubstitute;
    using NUnit.Framework;
    using Params;
    using Ploeh.AutoFixture;
    using Services;
    using Site.Handlers;
    using Site.Requests.MessageHistory;
    using Site.ViewModels;
    using Sunplus.Agent.Model.Entities;

    [TestFixture]
    public class MessageHanlderTests
    {
        private MessageHistoryService messageService;
        private NewMessageService newMessageService;
        private AnnouncementService announcementService;
        private MessageHistoryHandler messageHandler;
        private IUserSession userSession;
        private Fixture fixture;

        [SetUp]
        public void SetUp()
        {
            messageService = Substitute.For<MessageHistoryService>();
            newMessageService = Substitute.For<NewMessageService>();
            announcementService = Substitute.For<AnnouncementService>();
            messageHandler = new MessageHistoryHandler(messageService, newMessageService, announcementService);

            userSession = Substitute.For<IUserSession>();
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
            messageService.GetMessagesHistory(Arg.Any<MessageHistoryParams>()).Returns(messages);

            var query = new MessageHistoryQuery(
                userSession,
                1,
                DateTime.Today,
                DateTime.Today,
                false);

            // Act
            messageHandler.Handle(query);

            // Assert
            messageService
                .Received()
                .GetMessagesHistory(Arg.Is<MessageHistoryParams>(i => i.CustId == query.CustId
                && i.ParentSites == query.ParentSites
                && i.SpecialType == query.SpecialType
                && i.TotalMessage == query.TotalMessage
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

            var messages = FakeDataHelper.CreateMany<MessageHistory>().ToList();
            var lastSeenAnnouncement = FakeDataHelper.Create<LastSeenAnnouncement>();
            var numberOfUnReadMessage = FakeDataHelper.Create<NumberOfUnreadMessage>();
            newMessageService.GetNumberOfUnreadMessage(Arg.Any<int>()).Returns(numberOfUnReadMessage);
            messageService.GetMessagesHistory(Arg.Any<MessageHistoryParams>()).Returns(messages);
            announcementService.GetLastSeenAnnouncement(Arg.Any<int>()).Returns(lastSeenAnnouncement);

            var query = new MessageHistoryQuery(
                userSession,
                1,
                DateTime.Today,
                DateTime.Today,
                false);
            var messageWrappers = new List<MessageHistoryWrapper>(messages.Select(m => new MessageHistoryWrapper(m, lastSeenAnnouncement, query.CurrentLang)));

            var expectedViewModel = new MessagesHistoryViewModel(
                messageWrappers,
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