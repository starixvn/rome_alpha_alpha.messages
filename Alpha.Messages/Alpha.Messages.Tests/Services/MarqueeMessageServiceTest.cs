namespace Alpha.Messages.Tests.Services
{
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
    using System.Collections.Specialized;
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
        public void GetLatestPublicMessage_ExpiredCached_CallGetOfDynamicRepository()
        {
            //// Arrange
            var messageData = fixture.Create<MarqueeMessage>();
            dynamicRepository.Get<MarqueeMessage>(Arg.Any<GetLatestPublicMessageCriteria>()).Returns(messageData);

            //// Act
            messageService.GetLatestPublicMessage(UserLevel.Agent, "en-US", true, null);

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
            var act = messageService.GetLatestPublicMessage(UserLevel.Agent, "zh-CN", true, null);

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
            messageService.GetLatestPublicMessage(UserLevel.Super, "zh-CN", true, null);

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
            messageService.GetLatestPublicMessage(UserLevel.Master, "zh-CN", true, null);

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
            messageService.GetLatestPublicMessage(UserLevel.Agent, "zh-CN", true, null);

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
