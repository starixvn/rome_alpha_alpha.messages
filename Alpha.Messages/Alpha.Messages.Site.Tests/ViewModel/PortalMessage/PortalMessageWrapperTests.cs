using NUnit.Framework;
namespace Alpha.Messages.Site.Tests.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Helpers;
    using Messages.Constants;
    using Models.Message;
    using NUnit.Framework;
    using Ploeh.AutoFixture;
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

        [Test]
        public void Content_Always_ReplaceSpecialCharacters()
        {
            //// Arrange
            var message = FakeDataHelper.Create<PortalMessage>();            
            message.ContentEN = message.ContentEN +
                MessageConstants.ContentFontStartMarkup +
                "@@" +
                MessageConstants.ContentFontEndMarkup;

            var wrapper = new PortalMessageWrapper(message, Language.English);

            //// Act
            var act = wrapper.Content;

            //// Assert
            Assert.IsFalse(act.Contains("@@"));
            Assert.IsFalse(act.Contains(MessageConstants.ContentFontEndMarkup));
            Assert.IsFalse(act.Contains(MessageConstants.ContentFontStartMarkup));
            Assert.IsTrue(act.Contains(message.BetTransId));
        }

        public void ShortContent_ContentMoreThan100Length_ReduceLastCharacter(string lang)
        {
            // Arrange
            var noticeMessage = FakeDataHelper.Create<PortalMessage>();
            var fullContent = new string('a', 101);
            var expectedContent = $"{fullContent.Substring(0, fullContent.Length - 1)}...";
            noticeMessage.ContentEN = fullContent;

            // Act
            var viewModel = new PortalMessageWrapper(noticeMessage, Language.English);

            // Assert
            Assert.AreEqual(expectedContent, viewModel.ShortContent);
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
