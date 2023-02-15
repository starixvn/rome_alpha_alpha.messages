namespace Alpha.Messages.Site.Tests.Requests
{
    using Alpha.Messages.Site.Requests.Feedback;
    using NUnit.Framework;

    [TestFixture]
    public class AddFeedbackQueryTests
    {
        [Test]
        public void IsValid_RateAndMessageIsEmpty_ReturnFalse()
        {
            // Arrange
            var query = new AddFeedbackQuery
            {
                Rate = 0,
                Message = string.Empty
            };

            // Act
            var actualValue = query.IsValid();

            // Assert
            Assert.False(actualValue);
        }

        [Test]
        public void IsValid_HasRateAndMessageIsEmpty_ReturnTrue()
        {
            // Arrange
            var query = new AddFeedbackQuery
            {
                Rate = 2,
                Message = string.Empty
            };

            // Act
            var actualValue = query.IsValid();

            // Assert
            Assert.True(actualValue);
        }

        [Test]
        public void IsValid_RateIsZeroAndHasMessage_ReturnTrue()
        {
            // Arrange
            var query = new AddFeedbackQuery
            {
                Rate = 0,
                Message = "Message"
            };

            // Act
            var actualValue = query.IsValid();

            // Assert
            Assert.True(actualValue);
        }

        [Test]
        public void IsValid_RateIsZeroAndOverLimitMessage_ReturnFalse()
        {
            // Arrange
            var query = new AddFeedbackQuery
            {
                Rate = 0,
                Message = new string('*', 5000)
            };

            // Act
            var actualValue = query.IsValid();

            // Assert
            Assert.False(actualValue);
        }
    }
}
