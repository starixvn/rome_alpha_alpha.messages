namespace Alpha.Messages.Tests.Criteria.Customer
{
    using Alpha.Messages.Criteria.Feedback;
    using NUnit.Framework;

    [TestFixture]
    public class CheckExistsCriteriaTests
    {
        [Test]
        public void IsValid_CustIdIsZero_ReturnFalse()
        {
            // Arrange
            var criteria = new CheckExistsCriteria(0,0);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.False(actualValue);
        }

        [Test]
        public void IsValid_CustIdIsNegative_ReturnFalse()
        {
            // Arrange
            var criteria = new CheckExistsCriteria(-1, 0);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.False(actualValue);
        }

        [Test]
        public void IsValid_CustIdIsPositive_ReturnTrue()
        {
            // Arrange
            var criteria = new CheckExistsCriteria(1, 0);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.True(actualValue);
        }
    }
}
