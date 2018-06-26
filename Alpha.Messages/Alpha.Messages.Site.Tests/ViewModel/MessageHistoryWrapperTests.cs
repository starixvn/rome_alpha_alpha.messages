using NUnit.Framework;
namespace Alpha.Messages.Site.Tests.ViewModel
{
    using System;
    using System.Collections.Generic;
    using Alpha.Messages.Constants;
    using Alpha.Messages.Models.Message;
    using Alpha.Messages.Site.Tests.Helpers;
    using Alpha.Messages.Site.ViewModels;
    using Models.Announcement;
    using NUnit.Framework;

    [TestFixture]
    public class MessageHistoryWrapperTests
    {
        [TestCase(Language.ChineseSimplified, "contentCS")]
        [TestCase(Language.ChineseTraditional, "contentCH")]
        [TestCase(Language.Thai, "contentTH")]
        [TestCase(Language.Japanese, "contentJP")]
        [TestCase(Language.Korean, "contentKO")]
        [TestCase(Language.Vietnamese, "contentVI")]
        [TestCase(Language.English, "contentEN")]
        public void Content_WhenCurrentLanguageContentIsNull_ReturnContentDefault(string lang, string contentParamName)
        {
            // Arrange
            var parameters = new Dictionary<string, dynamic>
            {
                [contentParamName] = string.Empty,
                ["contentEN"] = "en"
            };

            var message = FakeDataHelper.Create<MessageHistory>(parameters);
            var lastSeenAnnouncement = FakeDataHelper.Create<LastSeenAnnouncement>();
            // Act
            var viewModel = new MessageHistoryWrapper(message, lastSeenAnnouncement, lang);

            // Assert
            Assert.AreEqual("en", viewModel.Content);
        }

        [TestCase(Language.ChineseSimplified, "contentCS")]
        [TestCase(Language.ChineseTraditional, "contentCH")]
        [TestCase(Language.Thai, "contentTH")]
        [TestCase(Language.Japanese, "contentJP")]
        [TestCase(Language.Korean, "contentKO")]
        [TestCase(Language.Vietnamese, "contentVI")]
        [TestCase(Language.English, "contentEN")]
        public void Content_ByCurrentLanguage_ReturnContent(string lang, string contentParamName)
        {
            // Arrange
            var message = FakeDataHelper.Create<MessageHistory>(contentParamName, contentParamName);
            var lastSeenAnnouncement = FakeDataHelper.Create<LastSeenAnnouncement>();

            // Act
            var viewModel = new MessageHistoryWrapper(message, lastSeenAnnouncement, lang);

            // Assert
            Assert.AreEqual(contentParamName, viewModel.Content);
        }

        [Test]
        public void Content_HasFontMarkup_RemoveMarkup()
        {
            // Arrange
            var message = FakeDataHelper.Create<MessageHistory>("contentEN", "<font color=red>abc</font>");
            var lastSeenAnnouncement = FakeDataHelper.Create<LastSeenAnnouncement>();

            // Act
            var viewModel = new MessageHistoryWrapper(message, lastSeenAnnouncement, Language.English);

            // Assert
            Assert.AreEqual("abc", viewModel.Content);
        }

        [Test]
        public void Content_HasDoubleAtSymbol_ReplaceWithBetTransId()
        {
            // Arrange
            var message = FakeDataHelper.Create<MessageHistory>("contentEN", "abc@@");
            var lastSeenAnnouncement = FakeDataHelper.Create<LastSeenAnnouncement>();

            // Act
            var viewModel = new MessageHistoryWrapper(message, lastSeenAnnouncement, Language.English);

            // Assert
            Assert.AreEqual("abc" + viewModel.CurrentMessage.BetTransId, viewModel.Content);
        }

        [Test]
        public void HasRedColor_ContentHasFontMarkup_ReturnTrue()
        {
            // Arrange
            var message = FakeDataHelper.Create<MessageHistory>("contentEN", "<font color=red>abc</font>");
            var lastSeenAnnouncement = FakeDataHelper.Create<LastSeenAnnouncement>();

            // Act
            var viewModel = new MessageHistoryWrapper(message, lastSeenAnnouncement, Language.English);

            // Assert
            Assert.IsTrue(viewModel.HasRedColor);
        }

        [Test]
        public void HasRedColor_ContentHasNotFontMarkup_ReturnFalse()
        {
            // Arrange
            var message = FakeDataHelper.Create<MessageHistory>("contentEN", "abc");
            var lastSeenAnnouncement = FakeDataHelper.Create<LastSeenAnnouncement>();

            // Act
            var viewModel = new MessageHistoryWrapper(message, lastSeenAnnouncement, Language.English);

            // Assert
            Assert.IsFalse(viewModel.HasRedColor);
        }

        [Test]
        public void IsNewMessage_PersonalMessageAndNew_ReturnTrue()
        {
            // Arrange
            var message = FakeDataHelper.Create<MessageHistory>();
            message.MessageType = 1;
            message.LastUpdateTime = DateTime.Now;
            var lastSeenAnnouncement = FakeDataHelper.Create<LastSeenAnnouncement>();
            lastSeenAnnouncement.LastSeenPersonal = DateTime.Now.AddDays(-1);

            // Act
            var viewModel = new MessageHistoryWrapper(message, lastSeenAnnouncement, Language.English);

            // Assert
            Assert.IsTrue(viewModel.IsNewMessage);
        }

        [Test]
        public void IsNewMessage_PersonalMessageAndOld_ReturnFalse()
        {
            // Arrange
            var message = FakeDataHelper.Create<MessageHistory>();
            message.MessageType = 1;
            message.LastUpdateTime = DateTime.Now.AddDays(-1);
            var lastSeenAnnouncement = FakeDataHelper.Create<LastSeenAnnouncement>();
            lastSeenAnnouncement.LastSeenPersonal = DateTime.Now;

            // Act
            var viewModel = new MessageHistoryWrapper(message, lastSeenAnnouncement, Language.English);

            // Assert
            Assert.IsFalse(viewModel.IsNewMessage);
        }

        [Test]
        public void IsNewMessage_NormalMessageAndNew_ReturnTrue()
        {
            // Arrange
            var message = FakeDataHelper.Create<MessageHistory>();
            message.MessageType = 3;
            message.LastUpdateTime = DateTime.Now;
            message.IsSticky = false;
            var lastSeenAnnouncement = FakeDataHelper.Create<LastSeenAnnouncement>();
            lastSeenAnnouncement.LastSeenNormal = DateTime.Now.AddDays(-1);

            // Act
            var viewModel = new MessageHistoryWrapper(message, lastSeenAnnouncement, Language.English);

            // Assert
            Assert.IsTrue(viewModel.IsNewMessage);
        }

        [Test]
        public void IsNewMessage_NormalMessageAndOld_ReturnFalse()
        {
            // Arrange
            var message = FakeDataHelper.Create<MessageHistory>();
            message.MessageType = 3;
            message.LastUpdateTime = DateTime.Now.AddDays(-1);
            message.IsSticky = false;
            var lastSeenAnnouncement = FakeDataHelper.Create<LastSeenAnnouncement>();
            lastSeenAnnouncement.LastSeenNormal = DateTime.Now;

            // Act
            var viewModel = new MessageHistoryWrapper(message, lastSeenAnnouncement, Language.English);

            // Assert
            Assert.IsFalse(viewModel.IsNewMessage);
        }

        [Test]
        public void IsNewMessage_SpecialMessageAndNew_ReturnTrue()
        {
            // Arrange
            var message = FakeDataHelper.Create<MessageHistory>();
            message.MessageType = 3;
            message.LastUpdateTime = DateTime.Now;
            message.IsSticky = true;
            var lastSeenAnnouncement = FakeDataHelper.Create<LastSeenAnnouncement>();
            lastSeenAnnouncement.LastSeenSpecial = DateTime.Now.AddDays(-1);

            // Act
            var viewModel = new MessageHistoryWrapper(message, lastSeenAnnouncement, Language.English);

            // Assert
            Assert.IsTrue(viewModel.IsNewMessage);
        }

        [Test]
        public void IsNewMessage_SpecialMessageAndOld_ReturnFalse()
        {
            // Arrange
            var message = FakeDataHelper.Create<MessageHistory>();
            message.MessageType = 3;
            message.LastUpdateTime = DateTime.Now.AddDays(-1);
            message.IsSticky = true;
            var lastSeenAnnouncement = FakeDataHelper.Create<LastSeenAnnouncement>();
            lastSeenAnnouncement.LastSeenSpecial = DateTime.Now;

            // Act
            var viewModel = new MessageHistoryWrapper(message, lastSeenAnnouncement, Language.English);

            // Assert
            Assert.IsFalse(viewModel.IsNewMessage);
        }
    }
}
