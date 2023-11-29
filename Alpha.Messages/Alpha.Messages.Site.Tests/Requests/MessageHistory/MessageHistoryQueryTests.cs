namespace Alpha.Messages.Site.Tests.Requests
{
    using System;
    using Alpha.Messages.Constants;
    using NSubstitute;
    using NUnit.Framework;
    using Site.Requests.MessageHistory;
    using AlphaPlus.User.Models;

    [TestFixture]
    public class MessageHistoryQueryTests
    {
        private IUserSession userSession;

        [SetUp]
        public void SetUp()
        {
            userSession = Substitute.For<IUserSession>();
            userSession.RoleId.Returns(RoleId.Agent);
        }

        [Test]
        public void CustId_Always_EqualsValueSetFromCtor()
        {
            // Arrange
            var custId = 69;
            userSession.CustId.Returns(custId);

            // Act
            var query = new MessageHistoryQuery(
                userSession,
                MessageType.Normal,
                DateTime.Today,
                DateTime.Today,
                false, 1, 100);

            // Assert
            Assert.AreEqual(custId, query.CustId);
        }

        [TestCase(RoleId.Super, "3000,3100")]
        [TestCase(RoleId.Master, "3000,3200")]
        [TestCase(RoleId.Agent, "3000,3300")]
        public void ParentSites_Always_ReturnCorrectByUserLevelId(byte userLevelId, string expectedParentSitesId)
        {
            // Arrange
            userSession.RoleId.Returns(userLevelId);

            // Act
            var query = new MessageHistoryQuery(
                    userSession,
                    MessageType.Normal,
                    DateTime.Today,
                    DateTime.Today,
                    false, 1, 100);

            // Assert
            Assert.AreEqual(expectedParentSitesId, query.ParentSites);
        }

        [Test]
        public void MessageType_Always_EqualsValueSetFromCtor()
        {
            // Arrange
            var msgType = MessageType.Normal;

            // Act
            var query = new MessageHistoryQuery(
                userSession,
                msgType,
                DateTime.Today,
                DateTime.Today,
                false, 1, 100);

            // Assert
            Assert.AreEqual(msgType, query.MessageType);
        }

        [TestCase(MessageType.Normal, 0)]
        [TestCase(MessageType.Special, 1)]
        [TestCase(MessageType.Personal, 2)]
        public void SpecialType_Always_ReturnValueByMessageType(int messageType, int specialType)
        {
            // Act
            var query = new MessageHistoryQuery(
                userSession,
                messageType,
                DateTime.Today,
                DateTime.Today,
                false, 1, 100);

            // Assert
            Assert.AreEqual(specialType, query.SpecialType);
        }

        [TestCase(MessageType.Normal)]
        [TestCase(MessageType.Personal)]
        public void StartDate_WhenMessageTypeIsNotSpecial_EqualsValueSetFromCtor(int messageType)
        {
            // Arrange
            var startDate = DateTime.Today;

            // Act
            var query = new MessageHistoryQuery(
                userSession,
                messageType,
                startDate,
                DateTime.Today,
                false, 1, 100);

            // Assert
            Assert.AreEqual(startDate, query.StartDate);
        }

        [Test]
        public void StartDate_WhenMessageTypeIsSpecial_ReturnMinDateTimeValue()
        {
            // Arrange
            var startDate = new DateTime(1900, 1, 1);

            // Act
            var query = new MessageHistoryQuery(
                userSession,
                MessageType.Special,
                startDate,
                DateTime.Today,
                false, 1, 100);

            // Assert
            Assert.AreEqual(startDate, query.StartDate);
        }

        [Test]
        public void EndDate_Always_EqualsValueSetFromCtor()
        {
            // Arrange
            var endDate = DateTime.Today;

            // Act
            var query = new MessageHistoryQuery(
                userSession,
                MessageType.Normal,
                DateTime.Today,
                endDate,
                false, 1, 100);

            // Assert
            Assert.AreEqual(endDate.AddDays(1).AddTicks(-1), query.EndDate);
        }

        [Test]
        public void IsTest_Always_EqualsValueSetFromCtor()
        {
            // Arrange
            var isTest = false;

            // Act
            var query = new MessageHistoryQuery(
                userSession,
                MessageType.Normal,
                DateTime.Today,
                DateTime.Today,
                isTest, 1, 100);

            // Assert
            Assert.AreEqual(isTest, query.IsTest);
        }

        [Test]
        public void CurrentLang_Always_EqualsValueSetFromCtor()
        {
            // Arrange
            var currentLang = Language.English;
            userSession.Language.Returns(currentLang);

            // Act
            var query = new MessageHistoryQuery(
                userSession,
                MessageType.Normal,
                DateTime.Today,
                DateTime.Today,
                false, 1, 100);

            // Assert
            Assert.AreEqual(currentLang, query.Language);
        }

        [Test]
        public void RoleId_Always_EqualsValueSetFromCtor()
        {
            // Arrange
            userSession.RoleId.Returns(RoleId.Agent);

            // Act
            var query = new MessageHistoryQuery(
                userSession,
                MessageType.Normal,
                DateTime.Today,
                DateTime.Today,
                false, 1, 100);

            // Assert
            Assert.AreEqual(RoleId.Agent, query.RoleId);
        }
    }
}
