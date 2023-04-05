namespace Alpha.Messages.Tests.Criteria.Customer
{
    using Alpha.Messages.Criteria.Feedback;
    using NUnit.Framework;

    [TestFixture]
    public class CheckExistsCriteriaTests
    {
        [Test]
        public void GetSettingKey_Always_ReturnsCorrectKey()
        {
            // Arrange
            var criteria = new CheckExistsCriteria(0, 0, 1);

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.AreEqual("Age_CustFeedbackCheckExists", actualSettingKey);
        }

        [TestCase(-1, false)]
        [TestCase(0, false)]
        [TestCase(123, true)]
        public void IsValid_CustId_ReturnsCorrectExpectedValue(int custId, bool expected)
        {
            // Arrange
            var criteria = new CheckExistsCriteria(custId, 0, 102);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.AreEqual(expected, actualValue);
        }

        [TestCase(-1, false)]
        [TestCase(0, true)]
        [TestCase(456, true)]
        public void IsValid_SubAccId_ReturnsCorrectExpectedValue(int subAccId, bool expected)
        {
            // Arrange
            var criteria = new CheckExistsCriteria(123, subAccId, 102);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.AreEqual(expected, actualValue);
        }

        [TestCase(-10, false)]
        [TestCase(0, false)]
        [TestCase(102, true)]
        public void IsValid_FunctionId_ReturnsCorrectExpectedValue(int functionId, bool expected)
        {
            // Arrange
            var criteria = new CheckExistsCriteria(123, 456, functionId);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.AreEqual(expected, actualValue);
        }

        [Test]
        public void Constructor_Always_AssignsCorrectProperties()
        {
            // Arrange & Act
            var criteria = new CheckExistsCriteria(123, 456, 102) { IsExists = true };

            // Assert
            Assert.IsTrue(criteria.IsExists);
        }
    }
}
