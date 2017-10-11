namespace Alpha.Messages.Tests.Criteria
{
    using Alpha.Messages.Criteria.Announcement;
    using NUnit.Framework;

    public class UpdateViewStatusTest
    {
        [Test]
        public void GetSettingKey_Always_ReturnCorrectKey()
        {
            //// Arrange
            var criteria = new UpdateViewStatus(263, true, 1);
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
            var criteria = new UpdateViewStatus(263, true, 1);

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
            var criteria = new UpdateViewStatus(263, expectedValue, 1);

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
            var criteria = new UpdateViewStatus(263, true, expectedValue);

            //// Act
            var act = criteria.TypeId;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }
    }
}
