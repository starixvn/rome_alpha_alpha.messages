namespace Alpha.Messages.Tests.Criteria
{
    using Messages.Criteria.NewMessage;
    using NUnit.Framework;

    public class UpdateMessageAsReadOnUserCritriaTest
    {
        [Test]
        public void GetSettingKey_Always_ReturnCorrectKey()
        {
            //// Arrange
            var criteria = new UpdateMessageAsReadOnUserCritria(263, 1);
            var expectedKey = "Age_MessageNoticeUpdStatus";

            //// Act
            var act = criteria.GetSettingKey();

            //// Assert
            Assert.AreEqual(expectedKey, act);
        }

        [Test]
        public void IsValid_CustIdMoreThanZero_ReturnTrue()
        {
            //// Arrange
            var criteria = new UpdateMessageAsReadOnUserCritria(263, 1);

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.IsTrue(act);
        }

        [Test]
        public void IsViewed_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = true;
            var criteria = new UpdateMessageAsReadOnUserCritria(263, 1);

            //// Act
            var act = criteria.IsViewed;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void TypeId_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = 123;
            var criteria = new UpdateMessageAsReadOnUserCritria(263, expectedValue);

            //// Act
            var act = criteria.TypeId;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }
    }
}
