namespace Alpha.Messages.Tests.Models.Message
{
    using System;
    using Alpha.Messages.Site.Tests.Helpers;
    using NUnit.Framework;
    using MessageModel = Alpha.Messages.Models.Message.Message;

    [TestFixture]
    public class MessageTests
    {
        [Test]
        public void Id_Always_SetValueFromCtor()
        {
            // Arrange
            int id = 1;

            // Act
            var message = FakeDataHelper.Create<MessageModel>("id", id);

            // Assert
            Assert.AreEqual(id, message.Id);
        }

        [Test]
        public void ContentEN_Always_SetValueFromCtor()
        {
            // Arrange
            string contentEN = "value";

            // Act
            var message = FakeDataHelper.Create<MessageModel>("contentEN", contentEN);

            // Assert
            Assert.AreEqual(contentEN, message.ContentEN);
        }

        [Test]
        public void ContentCS_Always_SetValueFromCtor()
        {
            // Arrange
            string contentCS = "value";

            // Act
            var message = FakeDataHelper.Create<MessageModel>("contentCS", contentCS);

            // Assert
            Assert.AreEqual(contentCS, message.ContentCS);
        }

        [Test]
        public void ContentCH_Always_SetValueFromCtor()
        {
            // Arrange
            string contentCH = "value";

            // Act
            var message = FakeDataHelper.Create<MessageModel>("contentCH", contentCH);

            // Assert
            Assert.AreEqual(contentCH, message.ContentCH);
        }

        [Test]
        public void ContentJP_Always_SetValueFromCtor()
        {
            // Arrange
            string contentJP = "value";

            // Act
            var message = FakeDataHelper.Create<MessageModel>("contentJP", contentJP);

            // Assert
            Assert.AreEqual(contentJP, message.ContentJP);
        }

        [Test]
        public void ContentTH_Always_SetValueFromCtor()
        {
            // Arrange
            string contentTH = "value";

            // Act
            var message = FakeDataHelper.Create<MessageModel>("contentTH", contentTH);

            // Assert
            Assert.AreEqual(contentTH, message.ContentTH);
        }

        [Test]
        public void ContentIT_Always_SetValueFromCtor()
        {
            // Arrange
            string contentIT = "value";

            // Act
            var message = FakeDataHelper.Create<MessageModel>("contentIT", contentIT);

            // Assert
            Assert.AreEqual(contentIT, message.ContentIT);
        }

        [Test]
        public void ContentKO_Always_SetValueFromCtor()
        {
            // Arrange
            string contentKO = "value";

            // Act
            var message = FakeDataHelper.Create<MessageModel>("contentKO", contentKO);

            // Assert
            Assert.AreEqual(contentKO, message.ContentKO);
        }

        [Test]
        public void ContentES_Always_SetValueFromCtor()
        {
            // Arrange
            string contentES = "value";

            // Act
            var message = FakeDataHelper.Create<MessageModel>("contentES", contentES);

            // Assert
            Assert.AreEqual(contentES, message.ContentES);
        }

        [Test]
        public void PostTime_Always_SetValueFromCtor()
        {
            // Arrange
            DateTime postTime = DateTime.Today;

            // Act
            var message = FakeDataHelper.Create<MessageModel>("postTime", postTime);

            // Assert
            Assert.AreEqual(postTime, message.PostTime);
        }

        [Test]
        public void CreateTime_Always_SetValueFromCtor()
        {
            // Arrange
            DateTime createTime = DateTime.Today;

            // Act
            var message = FakeDataHelper.Create<MessageModel>("createTime", createTime);

            // Assert
            Assert.AreEqual(createTime, message.CreateTime);
        }

        [Test]
        public void LastUpdateTime_Always_SetValueFromCtor()
        {
            // Arrange
            DateTime lastUpdateTime = DateTime.Today;

            // Act
            var message = FakeDataHelper.Create<MessageModel>("lastUpdateTime", lastUpdateTime);

            // Assert
            Assert.AreEqual(lastUpdateTime, message.LastUpdateTime);
        }

        [Test]
        public void MessageType_Always_SetValueFromCtor()
        {
            // Arrange
            byte messageType = 1;

            // Act
            var message = FakeDataHelper.Create<MessageModel>("messageType", messageType);

            // Assert
            Assert.AreEqual(messageType, message.MessageType);
        }

        [Test]
        public void IsSticky_Always_SetValueFromCtor()
        {
            // Arrange
            bool isSticky = false;

            // Act
            var message = FakeDataHelper.Create<MessageModel>("isSticky", isSticky);

            // Assert
            Assert.AreEqual(isSticky, message.IsSticky);
        }

        [Test]
        public void IsTest_Always_SetValueFromCtor()
        {
            // Arrange
            bool isTest = false;

            // Act
            var message = FakeDataHelper.Create<MessageModel>("isTest", isTest);

            // Assert
            Assert.AreEqual(isTest, message.IsTest);
        }

        [Test]
        public void MatchId_Always_SetValueFromCtor()
        {
            // Arrange
            int matchId = 1;

            // Act
            var message = FakeDataHelper.Create<MessageModel>("matchId", matchId);

            // Assert
            Assert.AreEqual(matchId, message.MatchId);
        }

        [Test]
        public void IsBatch_Always_SetValueFromCtor()
        {
            // Arrange
            bool isBatch = false;

            // Act
            var message = FakeDataHelper.Create<MessageModel>("isBatch", isBatch);

            // Assert
            Assert.AreEqual(isBatch, message.IsBatch);
        }

        [Test]
        public void CreatorId_Always_SetValueFromCtor()
        {
            // Arrange
            int creatorId = 1;

            // Act
            var message = FakeDataHelper.Create<MessageModel>("creator", creatorId);

            // Assert
            Assert.AreEqual(creatorId, message.CreatorId);
        }

        [Test]
        public void LastUpdatorId_Always_SetValueFromCtor()
        {
            // Arrange
            int lastUpdator = 1;

            // Act
            var message = FakeDataHelper.Create<MessageModel>("lastUpdator", lastUpdator);

            // Assert
            Assert.AreEqual(lastUpdator, message.LastUpdator);
        }

        [Test]
        public void UserId_Always_SetValueFromCtor()
        {
            // Arrange
            int userId = 1;

            // Act
            var message = FakeDataHelper.Create<MessageModel>("userId", userId);

            // Assert
            Assert.AreEqual(userId, message.UserId);
        }

        [Test]
        public void CreatorName_Always_SetValueFromCtor()
        {
            // Arrange
            string creatorName = "user";

            // Act
            var message = FakeDataHelper.Create<MessageModel>("creatorName", creatorName);

            // Assert
            Assert.AreEqual(creatorName, message.CreatorName);
        }

        [Test]
        public void LastUpdatorName_Always_SetValueFromCtor()
        {
            // Arrange
            string lastUpdateName = "user";

            // Act
            var message = FakeDataHelper.Create<MessageModel>("lastUpdateName", lastUpdateName);

            // Assert
            Assert.AreEqual(lastUpdateName, message.LastUpdateName);
        }

        [Test]
        public void UserName_Always_SetValueFromCtor()
        {
            // Arrange
            string userName = "user";

            // Act
            var message = FakeDataHelper.Create<MessageModel>("userName", userName);

            // Assert
            Assert.AreEqual(userName, message.UserName);
        }

        [Test]
        public void BettransId_Always_SetValueFromCtor()
        {
            // Arrange
            string bettransId = "1";

            // Act
            var message = FakeDataHelper.Create<MessageModel>("bettransId", bettransId);

            // Assert
            Assert.AreEqual(bettransId, message.BetTransId);
        }

        [Test]
        public void RowNum_Always_SetValueFromCtor()
        {
            // Arrange
            long rowNum = 1;

            // Act
            var message = FakeDataHelper.Create<MessageModel>("rowNum", rowNum);

            // Assert
            Assert.AreEqual(rowNum, message.RowNum);
        }
    }
}
