namespace Alpha.Messages.Tests.Criteria.Message
{
    using Alpha.Messages.Criteria.Message;
    using NUnit.Framework;

    public class HasLatestPrivateMessageCriteriaTests
    {
        [Test]
        public void GetSettingKey_Always_ReturnCorrectKey()
        {
            //// Arrange
            var criteria = new HasLatestPrivateMessageCriteria(263, false);
            var expectedKey = "Dot_Msg_HasPriMessage24";

            //// Act
            var act = criteria.GetSettingKey();

            //// Assert
            Assert.AreEqual(expectedKey, act);
        }

        [Test]
        public void IsValid_InvalidParameter_ReturnFalse()
        {
            //// Arrange
            var criteria = new HasLatestPrivateMessageCriteria(0, false);

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.IsFalse(act);
        }        

        [Test]
        public void IsTest_ValidParameter_ReturnValueInConstructor()
        {
            //// Arrange
            var valueInConstructor = true;
            var criteria = new HasLatestPrivateMessageCriteria(263, valueInConstructor);

            //// Act
            var act = criteria.IsTest;

            //// Assert
            Assert.AreEqual(valueInConstructor, act);
        }
    }
}
