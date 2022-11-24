namespace Alpha.Messages.Tests.Criteria.Message
{
    using Alpha.Messages.Criteria.Message;
    using NUnit.Framework;

    public class GetLatestPrivateMessageCriteriaTests
    {
        [Test]
        public void GetSettingKey_Always_ReturnCorrectKey()
        {
            //// Arrange
            var criteria = new GetLatestPrivateMessageCriteria(263, false);
            var expectedKey = "Age_Msg_GetTopPriMessage24";

            //// Act
            var act = criteria.GetSettingKey();

            //// Assert
            Assert.AreEqual(expectedKey, act);
        }

        [Test]
        public void IsValid_InvalidParameter_ReturnFalse()
        {
            //// Arrange
            var criteria = new GetLatestPrivateMessageCriteria(0, false);

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.IsFalse(act);
        }

        [Test]
        public void IsValid_ValidParameter_ReturnTrue()
        {
            //// Arrange
            var criteria = new GetLatestPrivateMessageCriteria(263, false);

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.IsTrue(act);
        }

        [Test]
        public void GetLatestPrivateMessageCriteria_AlwaysSetLatestMessageIdEqual0()
        {
            //// Arrange
            var criteria = new GetLatestPrivateMessageCriteria(263, false);

            //// Act
            var act = criteria.LatestMessageId;

            //// Assert
            Assert.Zero(act);
        }

        [Test]
        public void IsTest_Always_ReturnValueInConstructor()
        {
            //// Arrange
            var valueInConstructor = true;
            var criteria = new GetLatestPrivateMessageCriteria(263, valueInConstructor);

            //// Act
            var act = criteria.IsTest;

            //// Assert
            Assert.AreEqual(valueInConstructor, act);
        }        
    }
}
