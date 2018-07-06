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
            // Act
            var viewModel = new MessageHistoryWrapper(message, DateTime.Now, lang);

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

            // Act
            var viewModel = new MessageHistoryWrapper(message, DateTime.Now, lang);

            // Assert
            Assert.AreEqual(contentParamName, viewModel.Content);
        }

        [Test]
        public void Content_HasFontMarkup_RemoveMarkup()
        {
            // Arrange
            var message = FakeDataHelper.Create<MessageHistory>("contentEN", "<font color=red>abc</font>");            

            // Act
            var viewModel = new MessageHistoryWrapper(message, DateTime.Now, Language.English);

            // Assert
            Assert.AreEqual("abc", viewModel.Content);
        }

        [Test]
        public void Content_HasDoubleAtSymbol_ReplaceWithBetTransId()
        {
            // Arrange
            var message = FakeDataHelper.Create<MessageHistory>("contentEN", "abc@@");

            // Act
            var viewModel = new MessageHistoryWrapper(message, DateTime.Now, Language.English);

            // Assert
            Assert.AreEqual("abc" + viewModel.CurrentMessage.BetTransId, viewModel.Content);
        }

        [Test]
        public void HasRedColor_ContentHasFontMarkup_ReturnTrue()
        {
            // Arrange
            var message = FakeDataHelper.Create<MessageHistory>("contentEN", "<font color=red>abc</font>");

            // Act
            var viewModel = new MessageHistoryWrapper(message, DateTime.Now, Language.English);

            // Assert
            Assert.IsTrue(viewModel.HasRedColor);
        }

        [Test]
        public void HasRedColor_ContentHasNotFontMarkup_ReturnFalse()
        {
            // Arrange
            var message = FakeDataHelper.Create<MessageHistory>("contentEN", "abc");

            // Act
            var viewModel = new MessageHistoryWrapper(message, DateTime.Now, Language.English);

            // Assert
            Assert.IsFalse(viewModel.HasRedColor);
        }

        [Test]
        public void IsNewMessage_PersonalMessageAndNew_ReturnTrue()
        {
            // Arrange
            var message = FakeDataHelper.Create<MessageHistory>();
            message.MessageType = 1;
            message.PostTime = DateTime.Now;

            // Act
            var viewModel = new MessageHistoryWrapper(message, DateTime.Now.AddDays(-1), Language.English);

            // Assert
            Assert.IsTrue(viewModel.IsNewMessage);
        }

        [Test]
        public void IsNewMessage_PersonalMessageAndOld_ReturnFalse()
        {
            // Arrange
            var message = FakeDataHelper.Create<MessageHistory>();
            message.MessageType = 1;
            message.PostTime = DateTime.Now.AddDays(-1);

            // Act
            var viewModel = new MessageHistoryWrapper(message, DateTime.Now, Language.English);

            // Assert
            Assert.IsFalse(viewModel.IsNewMessage);
        }

        [Test]
        public void IsNewMessage_NormalMessageAndNew_ReturnTrue()
        {
            // Arrange
            var message = FakeDataHelper.Create<MessageHistory>();
            message.MessageType = 3;
            message.PostTime = DateTime.Now;
            message.IsSticky = false;

             // Act
             var viewModel = new MessageHistoryWrapper(message, DateTime.Now.AddDays(-1), Language.English);

            // Assert
            Assert.IsTrue(viewModel.IsNewMessage);
        }

        [Test]
        public void IsNewMessage_NormalMessageAndOld_ReturnFalse()
        {
            // Arrange
            var message = FakeDataHelper.Create<MessageHistory>();
            message.MessageType = 3;
            message.PostTime = DateTime.Now.AddDays(-1);
            message.IsSticky = false;

            // Act
            var viewModel = new MessageHistoryWrapper(message, DateTime.Now, Language.English);

            // Assert
            Assert.IsFalse(viewModel.IsNewMessage);
        }

        [Test]
        public void IsNewMessage_SpecialMessageAndNew_ReturnTrue()
        {
            // Arrange
            var message = FakeDataHelper.Create<MessageHistory>();
            message.MessageType = 3;
            message.PostTime = DateTime.Now;
            message.IsSticky = true;

            // Act
            var viewModel = new MessageHistoryWrapper(message, DateTime.Now.AddDays(-1), Language.English);

            // Assert
            Assert.IsTrue(viewModel.IsNewMessage);
        }

        [Test]
        public void IsNewMessage_SpecialMessageAndOld_ReturnFalse()
        {
            // Arrange
            var message = FakeDataHelper.Create<MessageHistory>();
            message.MessageType = 3;
            message.PostTime = DateTime.Now.AddDays(-1);
            message.IsSticky = true;

            // Act
            var viewModel = new MessageHistoryWrapper(message, DateTime.Now, Language.English);

            // Assert
            Assert.IsFalse(viewModel.IsNewMessage);
        }
    }
}
