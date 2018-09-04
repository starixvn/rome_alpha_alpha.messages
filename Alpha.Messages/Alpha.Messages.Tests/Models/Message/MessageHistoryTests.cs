using NUnit.Framework;
namespace Alpha.Messages.Tests.Models.Message
{
    using System;
    using Messages.Models.Message;
    using NUnit.Framework;
    using Site.Tests.Helpers;

    [TestFixture]
    public class MessageHistoryTests
    {
        [Test]
        public void Id_Always_SetValueFromCtor()
        {
            // Arrange
            int id = 1;

            // Act
            var message = FakeDataHelper.Create<MessageHistory>("id", id);

            // Assert
            Assert.AreEqual(id, message.Id);
        }

        [Test]
        public void ContentEN_Always_SetValueFromCtor()
        {
            // Arrange
            string contentEN = "value";

            // Act
            var message = FakeDataHelper.Create<MessageHistory>("contentEN", contentEN);

            // Assert
            Assert.AreEqual(contentEN, message.ContentEN);
        }

        [Test]
        public void ContentCS_Always_SetValueFromCtor()
        {
            // Arrange
            string contentCS = "value";

            // Act
            var message = FakeDataHelper.Create<MessageHistory>("contentCS", contentCS);

            // Assert
            Assert.AreEqual(contentCS, message.ContentCS);
        }

        [Test]
        public void ContentCH_Always_SetValueFromCtor()
        {
            // Arrange
            string contentCH = "value";

            // Act
            var message = FakeDataHelper.Create<MessageHistory>("contentCH", contentCH);

            // Assert
            Assert.AreEqual(contentCH, message.ContentCH);
        }

        [Test]
        public void ContentJP_Always_SetValueFromCtor()
        {
            // Arrange
            string contentJP = "value";

            // Act
            var message = FakeDataHelper.Create<MessageHistory>("contentJP", contentJP);

            // Assert
            Assert.AreEqual(contentJP, message.ContentJP);
        }

        [Test]
        public void ContentTH_Always_SetValueFromCtor()
        {
            // Arrange
            string contentTH = "value";

            // Act
            var message = FakeDataHelper.Create<MessageHistory>("contentTH", contentTH);

            // Assert
            Assert.AreEqual(contentTH, message.ContentTH);
        }
        
        [Test]
        public void ContentKO_Always_SetValueFromCtor()
        {
            // Arrange
            string contentKO = "value";

            // Act
            var message = FakeDataHelper.Create<MessageHistory>("contentKO", contentKO);

            // Assert
            Assert.AreEqual(contentKO, message.ContentKO);
        }
       
        [Test]
        public void PostTime_Always_SetValueFromCtor()
        {
            // Arrange
            DateTime postTime = DateTime.Today;

            // Act
            var message = FakeDataHelper.Create<MessageHistory>("postTime", postTime);

            // Assert
            Assert.AreEqual(postTime, message.PostTime);
        }
      
        [Test]
        public void MessageType_Always_SetValueFromCtor()
        {
            // Arrange
            byte messageType = 1;

            // Act
            var message = FakeDataHelper.Create<MessageHistory>("messageType", messageType);

            // Assert
            Assert.AreEqual(messageType, message.MessageType);
        }

        [Test]
        public void IsSticky_Always_SetValueFromCtor()
        {
            // Arrange
            bool isSticky = false;

            // Act
            var message = FakeDataHelper.Create<MessageHistory>("isSticky", isSticky);

            // Assert
            Assert.AreEqual(isSticky, message.IsSticky);
        }
        
        [Test]
        public void BettransId_Always_SetValueFromCtor()
        {
            // Arrange
            string bettransId = "1";

            // Act
            var message = FakeDataHelper.Create<MessageHistory>("bettransId", bettransId);

            // Assert
            Assert.AreEqual(bettransId, message.BetTransId);
        }

        [Test]
        public void RowNum_Always_SetValueFromCtor()
        {
            // Arrange
            long rowNum = 1;

            // Act
            var message = FakeDataHelper.Create<MessageHistory>("rowNum", rowNum);

            // Assert
            Assert.AreEqual(rowNum, message.RowNum);
        }

        [Test]
        public void HasRedColor_RedColor_ReturnTrue()
        {
            //// Arrange
            var message = FakeDataHelper.Create<MessageHistory>("contentEN", "<font color=red>abc</font>");

            //// Act
            var act = message.HasRedColor;

            //// Assert
            Assert.IsTrue(act);
        }

        [Test]
        public void Content_Always_ReplaceSpecialCharacters()
        {
            //// Arrange
            var message = FakeDataHelper.Create<MessageHistory>("ContentEN", "<font color=red>@@abc</font>");

            //// Act
            var act = message.Content("en-US");

            //// Assert
            Assert.IsFalse(act.Contains("@@"));
            Assert.IsFalse(act.Contains("<font color=red>"));
            Assert.IsFalse(act.Contains("</font>"));
        }
    }
}
