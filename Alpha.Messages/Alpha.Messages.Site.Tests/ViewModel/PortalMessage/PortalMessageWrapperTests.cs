namespace Alpha.Messages.Site.Tests.ViewModels
{
    using System.Collections.Generic;
    using Helpers;
    using Messages.Constants;
    using Models.Message;
    using NUnit.Framework;
    using Site.ViewModels;

    [TestFixture]
    public class PortalMessageWrapperTests
    {
        [TestCase(1, false, "private")]
        [TestCase(1, true, "special")]
        [TestCase(3, true, "special")]
        [TestCase(3, false, "public")]
        [TestCase(4, false, "")]
        public void MessageType_ByMessageTypeAndIsSticky_ReturnTypeText(byte msgType, bool isSticky, string expectedType)
        {
            // Arrange
            var parameters = new Dictionary<string, dynamic>();
            parameters.Add("type", msgType);
            parameters.Add("isSticky", isSticky);

            var noticeMessage = FakeDataHelper.Create<PortalMessage>(parameters);

            // Act
            var viewModel = new PortalMessageWrapper(noticeMessage, string.Empty);

            // Assert
            Assert.AreEqual(expectedType, viewModel.MessageType);
        }

        [TestCase(Language.ChineseSimplified, "contentCS")]
        [TestCase(Language.ChineseTraditional, "contentCH")]
        [TestCase(Language.Thai, "contentTH")]
        [TestCase(Language.Japanese, "contentJP")]
        [TestCase(Language.Korean, "contentKO")]
        [TestCase(Language.Vietnamese, "contentVI")]
        [TestCase(Language.English, "contentEN")]
        public void Content_WhenCurrentLanguageContentIsNull_ReturnContentDefault(string lang, string contentPropName)
        {
            // Arrange
            var noticeMessage = FakeDataHelper.Create<PortalMessage>(contentPropName, string.Empty);

            // Act
            var viewModel = new PortalMessageWrapper(noticeMessage, lang);

            // Assert
            Assert.AreEqual(noticeMessage.ContentEN, viewModel.Content);
        }

        [TestCase(Language.ChineseSimplified, "CS")]
        [TestCase(Language.ChineseTraditional, "CH")]
        [TestCase(Language.Thai, "TH")]
        [TestCase(Language.Japanese, "JP")]
        [TestCase(Language.Korean, "KO")]
        [TestCase(Language.Vietnamese, "VI")]
        [TestCase(Language.English, "EN")]
        public void Content_ByCurrentLanguage_ReturnContent(string lang, string expectedContent)
        {
            // Arrange
            var noticeMessage = FakeDataHelper.Create<PortalMessage>("content" + expectedContent, expectedContent);

            // Act
            var viewModel = new PortalMessageWrapper(noticeMessage, lang);

            // Assert
            Assert.AreEqual(expectedContent, viewModel.Content);
        }

        [TestCase(Language.ChineseSimplified, "CS")]
        [TestCase(Language.ChineseTraditional, "CH")]
        [TestCase(Language.Thai, "TH")]
        [TestCase(Language.Japanese, "JP")]
        [TestCase(Language.Korean, "KO")]
        [TestCase(Language.Vietnamese, "VI")]
        [TestCase(Language.English, "EN")]
        public void ShortContent_Always_ReturnData(string lang, string expectedShortContent)
        {
            // Arrange
            var noticeMessage = FakeDataHelper.Create<PortalMessage>("content" + expectedShortContent, expectedShortContent);

            // Act
            var viewModel = new PortalMessageWrapper(noticeMessage, lang);

            // Assert
            Assert.AreEqual(expectedShortContent + "...", viewModel.ShortContent);
        }

        [Test]
        public void GetStyleMessage_NoHighlight_ReturenEmpty()
        {
            // Arrange
            var noticeMessage = FakeDataHelper.Create<PortalMessage>("contentEN", "content message");
            var viewModel = new PortalMessageWrapper(noticeMessage, Language.English);

            // Act
            var act = viewModel.GetStyleMessage;

            // Assert
            Assert.IsEmpty(act);
        }

        [Test]
        public void GetStyleMessage_Highlight_ReturnRedText()
        {
            // Arrange
            var noticeMessage = FakeDataHelper.Create<PortalMessage>("contentEN", "content message has </font>");
            var viewModel = new PortalMessageWrapper(noticeMessage, Language.English);

            // Act
            var act = viewModel.GetStyleMessage;

            // Assert
            Assert.AreEqual("text-red", act);
        }

        [Test]
        public void Content_HasFontTag_RemoveFontTag()
        {
            // Arrange
            var noticeMessage = FakeDataHelper.Create<PortalMessage>("contentEN", "content message <font color=red>has</font>");
            var viewModel = new PortalMessageWrapper(noticeMessage, Language.English);

            // Act
            var act = viewModel.Content;

            // Assert
            Assert.AreEqual("content message has", act);
        }

        [Test]
        public void ShortContent_HasFontTag_RemoveFontTag()
        {
            // Arrange
            var noticeMessage = FakeDataHelper.Create<PortalMessage>("contentEN", "content message <font color=red>has</font>");
            var viewModel = new PortalMessageWrapper(noticeMessage, Language.English);

            // Act
            var act = viewModel.ShortContent;

            // Assert
            Assert.AreEqual("content message has...", act);
        }
    }
}
