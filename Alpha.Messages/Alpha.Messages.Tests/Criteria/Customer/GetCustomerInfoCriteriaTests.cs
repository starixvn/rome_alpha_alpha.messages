namespace Alpha.Messages.Tests.Criteria.Customer
{
    using Alpha.Messages.Criteria.Customer;
    using NUnit.Framework;

    public class GetCustomerInfoCriteriaTests
    {
        [Test]
        public void GetSettingKey_Always_ReturnsCorrectKey()
        {
            // Arrange
            var expectedSettingKey = "Age_GetCustomerInfo";
            var criteria = new GetCustomerInfoCriteria(1);

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.AreEqual(expectedSettingKey, actualSettingKey);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(int.MinValue)]
        public void IsValid_InvalidCustId_ReturnsFalse(int custId)
        {
            // Arrange
            var criteria = new GetCustomerInfoCriteria(custId);

            // Act & Assert
            Assert.IsFalse(criteria.IsValid());
        }

        [Test]
        public void IsValid_ValidCustId_ReturnsTrue()
        {
            // Arrange
            var criteria = new GetCustomerInfoCriteria(1);

            // Act & Assert
            Assert.IsTrue(criteria.IsValid());
        }
    }
}