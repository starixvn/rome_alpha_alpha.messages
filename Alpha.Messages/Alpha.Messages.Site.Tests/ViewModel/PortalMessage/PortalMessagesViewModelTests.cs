namespace Alpha.Messages.Site.Tests.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Helpers;
    using NUnit.Framework;
    using Site.ViewModels.PortalMessage;

    [TestFixture]
    public class PortalMessagesViewModelTests
    {
        [Test]
        public void LastMessagePostTime_NoticeMessagesHasNoData_ReturnMinDate()
        {
            // Arrange
            var noticeMessaseWrappers = new List<PortalMessageWrapper>();

            // Act
            var viewModel = new PortalMessagesViewModel(noticeMessaseWrappers);

            // Assert
            Assert.AreEqual(DateTime.MinValue, viewModel.LastMessagePostTime);
        }

        [Test]
        public void LastMessagePostTime_NoticeMessagesHasData_ReturnLastPostDate()
        {
            // Arrange
            var noticeMessaseWrappers = FakeDataHelper.CreateMany<PortalMessageWrapper>()
                .OrderByDescending(m => m.CurrentMessage.PostTime)
                .ToList();
            var lastPostTime = noticeMessaseWrappers.OrderByDescending(m => m.CurrentMessage.PostTime).Last().PostTime;

            // Act
            var viewModel = new PortalMessagesViewModel(noticeMessaseWrappers);

            // Assert
            Assert.AreEqual(lastPostTime, viewModel.LastMessagePostTime);
        }
    }
}