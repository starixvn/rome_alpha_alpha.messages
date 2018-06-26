namespace Alpha.Messages.Tests.Services
{
    using System;
    using System.Collections.Specialized;
    using Fanex.Data.Repository;
    using Fixtures;
    using Messages.Configurations;
    using Messages.Constants;
    using Messages.Criteria.Message;
    using Messages.Models.Constants;
    using Messages.Models.Message;
    using Messages.Services;
    using NSubstitute;
    using NUnit.Framework;
    using Ploeh.AutoFixture;
    using Assert = NUnit.Framework.Assert;

    [TestFixture]
    public class MarqueeMessageServiceTest
    {
        private MarqueeMessageService messageService;
        private IDynamicRepository dynamicRepository;
        private Fixture fixture;
        private const string SiteSuperId = "234";
        private const string SiteMasterId = "2345";
        private const string SiteAgentId = "2346";
        [SetUp]
        public void SetUp()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            messageService = new MarqueeMessageServiceImpl(dynamicRepository);
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
            var messageData = new MoveBetsMessage(contentEN, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, now);
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
            var messageData = new MoveBetsMessage(string.Empty, string.Empty, string.Empty, contentCH, string.Empty, string.Empty, string.Empty, string.Empty, now);
            dynamicRepository.Get<MoveBetsMessage>(Arg.Any<GetMoveBetInformationCriteria>()).Returns(messageData);

            //// Act
            var act = messageService.GetMoveBetsMessage("zh-TW");

            //// Assert
            Assert.AreEqual(contentCH, act.Content);
        }

        [Test]
        public void GetMoveBetsMessage_CSContentIsNOTEmpty_ReturnCSContent()
        {
            //// Arrange
            var now = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            var contentCS = "content CS";
            var messageData = new MoveBetsMessage(string.Empty, contentCS, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, now);
            dynamicRepository.Get<MoveBetsMessage>(Arg.Any<GetMoveBetInformationCriteria>()).Returns(messageData);

            //// Act
            var act = messageService.GetMoveBetsMessage(Language.ChineseSimplified);

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
            var messageData = new MoveBetsMessage(contentEN, contentCS, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, now);
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
            var messageData = fixture.Create<MarqueeMessage>();
            dynamicRepository.Get<MarqueeMessage>(Arg.Any<GetLatestPublicMessageCriteria>()).Returns(messageData);

            //// Act
            messageService.GetLatestPublicMessage(UserLevel.Agent, "en-US", true);

            //// Assert
            dynamicRepository.Received().Get<MarqueeMessage>(Arg.Any<GetLatestPublicMessageCriteria>());
        }

        [Test]
        public void GetLatestPublicMessage_ContentCSIsEmpty_ReturnContentEN()
        {
            var contentEN = "content EN";
            var contentCS = string.Empty;
            var messageData = fixture.Create<MarqueeMessage>();
            messageData.ContentEN = contentEN;
            messageData.ContentCS = contentCS;
            dynamicRepository.Get<MarqueeMessage>(Arg.Any<GetLatestPublicMessageCriteria>()).Returns(messageData);

            //// Act
            var act = messageService.GetLatestPublicMessage(1, "zh-CN", true);

            //// Assert
            Assert.AreEqual(contentEN, act.Content);
        }

        [Test]
        public void GetLatestPublicMessage_UserIsSuper_ReturnCorrectSite()
        {
            //// Arrange
            var messageData = fixture.Create<MarqueeMessage>();

            dynamicRepository.Get<MarqueeMessage>(Arg.Any<GetLatestPublicMessageCriteria>()).Returns(messageData);

            //// Act
            messageService.GetLatestPublicMessage(UserLevel.Super, "zh-CN", true);

            //// Assert
            dynamicRepository.Received().Get<MarqueeMessage>(Arg.Is<GetLatestPublicMessageCriteria>(i => i.Site1 == SiteSuperId));
        }

        [Test]
        public void GetLatestPublicMessage_UserIsMaster_ReturnCorrectSite()
        {
            //// Arrange
            var messageData = fixture.Create<MarqueeMessage>();

            dynamicRepository.Get<MarqueeMessage>(Arg.Any<GetLatestPublicMessageCriteria>()).Returns(messageData);

            //// Act
            messageService.GetLatestPublicMessage(UserLevel.Master, "zh-CN", true);

            //// Assert
            dynamicRepository.Received().Get<MarqueeMessage>(Arg.Is<GetLatestPublicMessageCriteria>(i => i.Site1 == SiteMasterId));
        }

        [Test]
        public void GetLatestPublicMessage_UserIsAgent_ReturnCorrectSite()
        {
            //// Arrange
            var messageData = fixture.Create<MarqueeMessage>();

            dynamicRepository.Get<MarqueeMessage>(Arg.Any<GetLatestPublicMessageCriteria>()).Returns(messageData);

            //// Act
            messageService.GetLatestPublicMessage(UserLevel.Agent, "zh-CN", true);

            //// Assert
            dynamicRepository.Received().Get<MarqueeMessage>(Arg.Is<GetLatestPublicMessageCriteria>(i => i.Site1 == SiteAgentId));
        }

        [Test]
        public void GetLatestPrivateMessage_HasMessage_CallGetOfDynamicRepository()
        {
            //// Arrange
            var messageData = fixture.Create<MarqueeMessage>();
            dynamicRepository.Get<MarqueeMessage>(Arg.Any<GetLatestPrivateMessageCriteria>()).Returns(messageData);
            dynamicRepository.Get<int>(Arg.Any<HasLatestPrivateMessageCriteria>()).Returns(1);

            //// Act
            messageService.GetLatestPrivateMessage(263, "en-US", true);

            //// Assert
            dynamicRepository.Received().Get<MarqueeMessage>(Arg.Any<GetLatestPrivateMessageCriteria>());
        }

        [Test]
        public void GetLatestPrivateMessage_HasNoMessage_NoCallGetOfDynamicRepository()
        {
            //// Arrange
            var messageData = fixture.Create<MarqueeMessage>();
            dynamicRepository.Get<MarqueeMessage>(Arg.Any<GetLatestPrivateMessageCriteria>()).Returns(messageData);
            dynamicRepository.Get<int>(Arg.Any<HasLatestPrivateMessageCriteria>()).Returns(0);

            //// Act
            messageService.GetLatestPrivateMessage(263, Language.English, true);

            //// Assert
            dynamicRepository.DidNotReceive().Get<MarqueeMessage>(Arg.Any<GetLatestPrivateMessageCriteria>());
        }

        [Test]
        public void GetLatestPrivateMessage_ContentCSIsEmpty_ReturnContentEN()
        {
            var contentEN = "content EN";
            var contentCS = string.Empty;
            var messageData = fixture.Create<MarqueeMessage>();
            messageData.ContentEN = contentEN;
            messageData.ContentCS = contentCS;
            dynamicRepository.Get<MarqueeMessage>(Arg.Any<GetLatestPrivateMessageCriteria>()).Returns(messageData);
            dynamicRepository.Get<int>(Arg.Any<HasLatestPrivateMessageCriteria>()).Returns(1);

            //// Act
            var act = messageService.GetLatestPrivateMessage(1, Language.ChineseSimplified, true);

            //// Assert
            Assert.AreEqual(contentEN, act.Content);
        }   
    }
}
