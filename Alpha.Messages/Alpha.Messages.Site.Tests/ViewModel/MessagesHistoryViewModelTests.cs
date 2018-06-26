namespace Alpha.Messages.Site.Tests.ViewModel
{
    using System;
    using System.Linq;
    using Alpha.Messages.Constants;
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
            var messageWrappers = FakeDataHelper.CreateMany<MessageHistoryWrapper>().ToList();
            var numberOfUnreadMessage = FakeDataHelper.Create<NumberOfUnreadMessage>();
            var startDate = DateTime.Today;

            // Act
            var viewModel = new MessagesHistoryViewModel(
                messageWrappers,
                startDate,
                DateTime.Today,
                MessageType.Normal,
                RoleId.Agent,
                numberOfUnreadMessage);

            // Assert
            Assert.AreEqual(startDate, viewModel.StartDate);
        }

        [Test]
        public void EndDate_Always_EqualsValueSetFromCtor()
        {
            // Arrange
            var messageWrappers = FakeDataHelper.CreateMany<MessageHistoryWrapper>().ToList();
            var numberOfUnreadMessage = FakeDataHelper.Create<NumberOfUnreadMessage>();
            var endDate = DateTime.Today;

            // Act
            var viewModel = new MessagesHistoryViewModel(
                messageWrappers,
                DateTime.Today,
                endDate,
                MessageType.Normal,
                RoleId.Agent,
                numberOfUnreadMessage);

            // Assert
            Assert.AreEqual(endDate, viewModel.EndDate);
        }

        [Test]
        public void MessageType_Always_EqualsValueSetFromCtor()
        {
            // Arrange
            var messageWrappers = FakeDataHelper.CreateMany<MessageHistoryWrapper>().ToList();
            var numberOfUnreadMessage = FakeDataHelper.Create<NumberOfUnreadMessage>();
            var msgType = MessageType.Normal;

            // Act
            var viewModel = new MessagesHistoryViewModel(
                messageWrappers,
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
            var messageWrappers = FakeDataHelper.CreateMany<MessageHistoryWrapper>().ToList();
            var numberOfUnreadMessage = FakeDataHelper.Create<NumberOfUnreadMessage>();
            // Act
            var viewModel = new MessagesHistoryViewModel(
                messageWrappers,
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
            var messageWrappers = FakeDataHelper.CreateMany<MessageHistoryWrapper>().ToList();
            var numberOfUnreadMessage = FakeDataHelper.Create<NumberOfUnreadMessage>();
            // Act
            var viewModel = new MessagesHistoryViewModel(
                messageWrappers,
                DateTime.Today,
                DateTime.Today,
                MessageType.Normal,
                roleId,
                numberOfUnreadMessage);

            // Assert
            Assert.AreEqual(userLevelName, viewModel.RoleName);
        }
    }
}
