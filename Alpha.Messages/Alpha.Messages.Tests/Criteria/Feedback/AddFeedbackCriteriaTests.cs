namespace Alpha.Messages.Tests.Criteria.Customer
{
    using Alpha.Messages.Criteria.Feedback;
    using NUnit.Framework;

    [TestFixture]
    public class AddFeedbackCriteriaTests
    {
        [Test]
        public void IsValid_RateAndMessageIsEmpty_ReturnFalse()
        {
            // Arrange
            var criteria = new AddFeedbackCriteria(0, 0, string.Empty, 0);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.False(actualValue);
        }

        [Test]
        public void IsValid_HasRateAndMessageIsEmpty_ReturnTrue()
        {
            // Arrange
            var criteria = new AddFeedbackCriteria(0, 0, string.Empty, 2);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.True(actualValue);
        }

        [Test]
        public void IsValid_RateIsZeroAndHasMessage_ReturnTrue()
        {
            // Arrange
            var criteria = new AddFeedbackCriteria(0, 0, "Message", 0);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.True(actualValue);
        }

        [Test]
        public void IsValid_RateIsZeroAndOverLimitMessage_ReturnFalse()
        {
            // Arrange
            var criteria = new AddFeedbackCriteria(0, 0, new string('*', 5000), 0);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.False(actualValue);
        }
    }
}
