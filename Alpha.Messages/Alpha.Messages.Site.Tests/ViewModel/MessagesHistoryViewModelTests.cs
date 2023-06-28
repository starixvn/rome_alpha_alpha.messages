namespace Alpha.Messages.Site.Tests.ViewModel
{
    using System;
    using System.Linq;
    using Alpha.Messages.Constants;
    using Alpha.Messages.Models;
    using Alpha.Messages.Site.Tests.Helpers;
    using Alpha.Messages.Site.ViewModels;
    using Models.Message;
    using NUnit.Framework;

    public class MessagesHistoryViewModelTests
    {
        [Test]
        public void StartDate_Always_EqualsValueSetFromCtor()
        {
            // Arrange
            var messages = FakeDataHelper.CreateMany<MessageHistory>().ToList();
            var report = new PagedReportData<MessageHistory>(
                 messages,
                 1,
                 50,
                 100);
            var numberOfUnreadMessage = FakeDataHelper.Create<NumberOfUnreadMessage>();
            var startDate = DateTime.Today;

            // Act
            var viewModel = new MessagesHistoryViewModel(
                report,
                DateTime.Today,
                Language.Default,
                startDate,
                DateTime.Today,
                MessageType.Normal,
                RoleId.Agent,
                numberOfUnreadMessage);

            // Assert
            Assert.AreEqual(startDate, viewModel.FromDate);
        }

        [Test]
        public void EndDate_Always_EqualsValueSetFromCtor()
        {
            // Arrange
            var messageWrappers = FakeDataHelper.CreateMany<MessageHistory>().ToList();
            var report = new PagedReportData<MessageHistory>(
                messageWrappers,
                1,
                50,
                100);
            var numberOfUnreadMessage = FakeDataHelper.Create<NumberOfUnreadMessage>();
            var endDate = DateTime.Today;

            // Act
            var viewModel = new MessagesHistoryViewModel(
                report,
                DateTime.Today,
                Language.Default,
                DateTime.Today,
                endDate,
                MessageType.Normal,
                RoleId.Agent,
                numberOfUnreadMessage);

            // Assert
            Assert.AreEqual(endDate, viewModel.ToDate);
        }

        [Test]
        public void MessageType_Always_EqualsValueSetFromCtor()
        {
            // Arrange
            var messageWrappers = FakeDataHelper.CreateMany<MessageHistory>().ToList();
            var report = new PagedReportData<MessageHistory>(
                messageWrappers,
                1,
                50,
                100);
            var numberOfUnreadMessage = FakeDataHelper.Create<NumberOfUnreadMessage>();
            var msgType = MessageType.Normal;

            // Act
            var viewModel = new MessagesHistoryViewModel(
                report,
                DateTime.Today,
                Language.Default,
                DateTime.Today,
                DateTime.Today,
                msgType,
                RoleId.Agent,
                numberOfUnreadMessage);

            // Assert
            Assert.AreEqual(msgType, viewModel.MessageType);
        }

        [TestCase(MessageType.Normal, 0)]
        [TestCase(MessageType.Special, 2)]
        [TestCase(MessageType.Personal, 1)]
        public void MessageTypeName_Always_ReturnValueByMessageType(int messageType, int messageTypeName)
        {
            // Arrange
            var messageWrappers = FakeDataHelper.CreateMany<MessageHistory>().ToList();
            var report = new PagedReportData<MessageHistory>(
                messageWrappers,
                1,
                50,
                100);
            var numberOfUnreadMessage = FakeDataHelper.Create<NumberOfUnreadMessage>();
            // Act
            var viewModel = new MessagesHistoryViewModel(
                report,
                DateTime.Today,
                Language.Default,
                DateTime.Today,
                DateTime.Today,
                messageType,
                RoleId.Agent,
                numberOfUnreadMessage);

            // Assert
            Assert.AreEqual(messageTypeName, viewModel.MessageType);
        }

        [TestCase(RoleId.Super, "Super")]
        [TestCase(RoleId.Master, "Master")]
        [TestCase(RoleId.Agent, "Agent")]
        public void RoleName_Always_ReturnValueByUserLevel(byte roleId, string userLevelName)
        {
            // Arrange
            var messageWrappers = FakeDataHelper.CreateMany<MessageHistory>().ToList();
            var report = new PagedReportData<MessageHistory>(
              messageWrappers,
              1,
              50,
              100);
            var numberOfUnreadMessage = FakeDataHelper.Create<NumberOfUnreadMessage>();
            // Act
            var viewModel = new MessagesHistoryViewModel(
                report,
                DateTime.Today,
                Language.Default,
                DateTime.Today,
                DateTime.Today,
                MessageType.Normal,
                roleId,
                numberOfUnreadMessage);

            // Assert
            Assert.AreEqual(userLevelName, viewModel.RoleName);
        }

        [TestCase(4, true)]
        [TestCase(3, true)]
        [TestCase(2, true)]
        public void IsNewMessage_Always_ReturnTrue(byte roleId, bool expected)
        {
            // Arrange
            var messageWrappers = FakeDataHelper.CreateMany<MessageHistory>().ToList();
            var report = new PagedReportData<MessageHistory>(
                messageWrappers,
                1,
                50,
                100);
            var numberOfUnreadMessage = FakeDataHelper.Create<NumberOfUnreadMessage>();
            var model = new MessagesHistoryViewModel(
                report,
                DateTime.Today,
                Language.Default,
                DateTime.Today,
                DateTime.Today,
                MessageType.Normal,
                roleId,
                numberOfUnreadMessage);

            // Act
            var act = model.IsNewMessage(DateTime.Now);

            // Assert
            Assert.AreEqual(expected, act);
        }
    }
}
