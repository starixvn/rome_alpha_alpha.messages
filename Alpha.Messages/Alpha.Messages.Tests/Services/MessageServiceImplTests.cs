namespace Alpha.Messages.Tests.Services
{
    using System;
    using System.Linq;
    using System.Collections.Specialized;
    using Constants;
    using Fanex.Data.Repository;
    using Fixtures;
    using Messages.Configurations;
    using Messages.Criteria.Message;
    using Messages.DataAccesses;
    using Messages.Models.Constants;
    using Messages.Models.Message;
    using Messages.Services;
    using NSubstitute;
    using NUnit.Framework;
    using Params.Message;
    using Ploeh.AutoFixture;
    using Site.Tests.Helpers;
    using Assert = NUnit.Framework.Assert;

    [TestFixture]
    public class MessageServiceImplTests
    {
        private MessageServiceImpl messageService;
        private IDynamicRepository dynamicRepository;
        private MessageDataAccess messageDataAccess;
        private Fixture fixture;
        private const string SiteSuperId = "234";
        private const string SiteMasterId = "2345";
        private const string SiteAgentId = "2346";
        [SetUp]
        public void SetUp()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            messageDataAccess = Substitute.For<MessageDataAccess>();
            messageService = new MessageServiceImpl(dynamicRepository, messageDataAccess);
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
        public void CountUnreadPrivateMessage_Always_CallGetOfDynamicRepository()
        {
            //// Arrange
            dynamicRepository.Get<int>(Arg.Any<CountPrivateMessageCriteria>()).Returns(1);

            //// Act
            messageService.CountUnreadPrivateMessage(263, "razor");

            //// Assert
            dynamicRepository.Received().Get<int>(Arg.Any<CountPrivateMessageCriteria>());
        }

        [Test]
        public void GetMoveBetsMessage_ExpiredCached_CallGetOfDynamicRepository()
        {
            //// Arrange
            var messageData = fixture.Create<MoveBetsMessage>();
            dynamicRepository.Get<MoveBetsMessage>(Arg.Any<GetMoveBetInformationCriteria>()).Returns(messageData);

            //// Act
            messageService.GetMoveBetsMessage("US-EN");

            //// Assert
            dynamicRepository.Received().Get<MoveBetsMessage>(Arg.Any<GetMoveBetInformationCriteria>());
        }

        [Test]
        public void GetMoveBetsMessage_EnglishLanguage_ReturnCorrectContent()
        {
            //// Arrange
            var now = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            var contentEN = "content EN";
            var messageData = new MoveBetsMessage(contentEN, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, now);
            dynamicRepository.Get<MoveBetsMessage>(Arg.Any<GetMoveBetInformationCriteria>()).Returns(messageData);

            //// Act
            var act = messageService.GetMoveBetsMessage("en-US");

            //// Assert
            Assert.AreEqual(contentEN, act.Content);
        }

        [Test]
        public void GetMoveBetsMessage_CHLanguage_ReturnCorrectContent()
        {
            //// Arrange
            var now = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            var contentCH = "content CH";
            var messageData = new MoveBetsMessage(string.Empty, string.Empty, contentCH, string.Empty, string.Empty, string.Empty, string.Empty, now);
            dynamicRepository.Get<MoveBetsMessage>(Arg.Any<GetMoveBetInformationCriteria>()).Returns(messageData);

            //// Act
            var act = messageService.GetMoveBetsMessage("zh-TW");

            //// Assert
            Assert.AreEqual(contentCH, act.Content);
        }

        [Test]
        public void GetMoveBetsMessage_CHContentIsEmpty_ReturnCSContent()
        {
            //// Arrange
            var now = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            var contentCS = "content CS";
            var messageData = new MoveBetsMessage(string.Empty, contentCS, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, now);
            dynamicRepository.Get<MoveBetsMessage>(Arg.Any<GetMoveBetInformationCriteria>()).Returns(messageData);

            //// Act
            var act = messageService.GetMoveBetsMessage("zh-TW");

            //// Assert
            Assert.AreEqual(contentCS, act.Content);
        }

        [Test]
        public void GetMoveBetsMessage_ContentCSIsEmpty_ReturnContentEN()
        {
            //// Arrange
            var contentEN = "content EN";
            var contentCS = string.Empty;
            var now = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            var messageData = new MoveBetsMessage(contentEN, contentCS, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, now);
            dynamicRepository.Get<MoveBetsMessage>(Arg.Any<GetMoveBetInformationCriteria>()).Returns(messageData);

            //// Act
            var act = messageService.GetMoveBetsMessage("zh-CN");

            //// Assert
            Assert.AreEqual(contentEN, act.Content);
        }

        [Test]
        public void GetLatestPublicMessage_ExpiredCached_CallGetOfDynamicRepository()
        {
            //// Arrange
            var messageData = fixture.Create<LatestMessage>();
            dynamicRepository.Get<LatestMessage>(Arg.Any<GetLatestPublicMessageCriteria>()).Returns(messageData);

            //// Act
            messageService.GetLatestPublicMessage(UserLevel.Agent, "en-US", true);

            //// Assert
            dynamicRepository.Received().Get<LatestMessage>(Arg.Any<GetLatestPublicMessageCriteria>());
        }

        [Test]
        public void GetLatestPublicMessage_ContentCSIsEmpty_ReturnContentEN()
        {
            var contentEN = "content EN";
            var contentCS = string.Empty;
            var messageData = fixture.Create<LatestMessage>();
            messageData.ContentEN = contentEN;
            messageData.ContentCS = contentCS;
            dynamicRepository.Get<LatestMessage>(Arg.Any<GetLatestPublicMessageCriteria>()).Returns(messageData);

            //// Act
            var act = messageService.GetLatestPublicMessage(1, "zh-CN", true);

            //// Assert
            Assert.AreEqual(contentEN, act.Content);
        }

        [Test]
        public void GetLatestPublicMessage_UserIsSuper_ReturnCorrectSite()
        {
            //// Arrange
            var messageData = fixture.Create<LatestMessage>();

            dynamicRepository.Get<LatestMessage>(Arg.Any<GetLatestPublicMessageCriteria>()).Returns(messageData);

            //// Act
            messageService.GetLatestPublicMessage(UserLevel.Super, "zh-CN", true);

            //// Assert
            dynamicRepository.Received().Get<LatestMessage>(Arg.Is<GetLatestPublicMessageCriteria>(i => i.Site1 == SiteSuperId));
        }

        [Test]
        public void GetLatestPublicMessage_UserIsMaster_ReturnCorrectSite()
        {
            //// Arrange
            var messageData = fixture.Create<LatestMessage>();

            dynamicRepository.Get<LatestMessage>(Arg.Any<GetLatestPublicMessageCriteria>()).Returns(messageData);

            //// Act
            messageService.GetLatestPublicMessage(UserLevel.Master, "zh-CN", true);

            //// Assert
            dynamicRepository.Received().Get<LatestMessage>(Arg.Is<GetLatestPublicMessageCriteria>(i => i.Site1 == SiteMasterId));
        }

        [Test]
        public void GetLatestPublicMessage_UserIsAgent_ReturnCorrectSite()
        {
            //// Arrange
            var messageData = fixture.Create<LatestMessage>();

            dynamicRepository.Get<LatestMessage>(Arg.Any<GetLatestPublicMessageCriteria>()).Returns(messageData);

            //// Act
            messageService.GetLatestPublicMessage(UserLevel.Agent, "zh-CN", true);

            //// Assert
            dynamicRepository.Received().Get<LatestMessage>(Arg.Is<GetLatestPublicMessageCriteria>(i => i.Site1 == SiteAgentId));
        }

        [Test]
        public void GetLatestPrivateMessage_HasMessage_CallGetOfDynamicRepository()
        {
            //// Arrange
            var messageData = fixture.Create<LatestMessage>();
            dynamicRepository.Get<LatestMessage>(Arg.Any<GetLatestPrivateMessageCriteria>()).Returns(messageData);
            dynamicRepository.Get<int>(Arg.Any<HasLatestPrivateMessageCriteria>()).Returns(1);

            //// Act
            messageService.GetLatestPrivateMessage(263, "en-US", true);

            //// Assert
            dynamicRepository.Received().Get<LatestMessage>(Arg.Any<GetLatestPrivateMessageCriteria>());
        }

        [Test]
        public void GetLatestPrivateMessage_HasNoMessage_NoCallGetOfDynamicRepository()
        {
            //// Arrange
            var messageData = fixture.Create<LatestMessage>();
            dynamicRepository.Get<LatestMessage>(Arg.Any<GetLatestPrivateMessageCriteria>()).Returns(messageData);
            dynamicRepository.Get<int>(Arg.Any<HasLatestPrivateMessageCriteria>()).Returns(0);

            //// Act
            messageService.GetLatestPrivateMessage(263, "en-US", true);

            //// Assert
            dynamicRepository.DidNotReceive().Get<LatestMessage>(Arg.Any<GetLatestPrivateMessageCriteria>());
        }

        [Test]
        public void GetLatestPrivateMessage_ContentCSIsEmpty_ReturnContentEN()
        {
            var contentEN = "content EN";
            var contentCS = string.Empty;
            var messageData = fixture.Create<LatestMessage>();
            messageData.ContentEN = contentEN;
            messageData.ContentCS = contentCS;
            dynamicRepository.Get<LatestMessage>(Arg.Any<GetLatestPrivateMessageCriteria>()).Returns(messageData);
            dynamicRepository.Get<int>(Arg.Any<HasLatestPrivateMessageCriteria>()).Returns(1);

            //// Act
            var act = messageService.GetLatestPrivateMessage(1, "zh-CN", true);

            //// Assert
            Assert.AreEqual(contentEN, act.Content);
        }

        [Test]
        public void UpdatePrivateAsRead_IsInteral_NotCallUpdate()
        {
            //// Arrange
            var param = new UpdatePrivateMessageAsReadParam(263, "razor", isInternal: true);

            //// Act
            messageService.UpdatePrivateAsRead(param);

            //// Assert
            dynamicRepository.DidNotReceive().Execute(Arg.Any<UpdatePrivateMessageAsReadCriteria>());
        }

        [Test]
        public void UpdatePrivateAsRead_IsNoInteral_CallUpdate()
        {
            //// Arrange
            var param = new UpdatePrivateMessageAsReadParam(263, "razor", isInternal: false);

            //// Act
            messageService.UpdatePrivateAsRead(param);

            //// Assert
            dynamicRepository.Received().Execute(Arg.Any<UpdatePrivateMessageAsReadCriteria>());
        }

        [Test]
        public void GetMessages_CustIdIsInvalidAndParentSitesIsNullOrEmpty_ReturnEmpty()
        {
            // Arrange
            var messages = messageService.GetMessagesHistory(
                CustId.InvalidId,
                string.Empty,
                1,
                1,
                DateTime.Today,
                DateTime.Today,
                false,
                1);

            // Act & Assert
            Assert.AreEqual(0, messages.Count());
        }

        [Test]
        public void GetMessages_CustIdIsValidAndParentSitesIsNotNullorEmpty_ReturnMessages()
        {
            // Arrange
            var messages = FakeDataHelper.CreateMany<Message>();

            messageDataAccess.GetMessagesHistory(Arg.Any<ICriteria>()).Returns(messages);

            // Act
            var actualMessages = messageService.GetMessagesHistory(
                CustId.ValidId,
                "1",
                1,
                1,
                DateTime.Today,
                DateTime.Today,
                false,
                1);

            // Assert
            Assert.AreEqual(messages, actualMessages);
        }

    }
}
