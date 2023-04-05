namespace Alpha.Messages.Site.Tests.Requests
{
    using Alpha.Messages.Site.Requests.Feedback;
    using NSubstitute;
    using NUnit.Framework;
    using Sunplus.Agent.Model.Entities;

    [TestFixture]
    public class AddFeedbackQueryTests
    {
        [Test]
        public void IsValid_RateAndMessageIsEmpty_ReturnFalse()
        {
            // Arrange
            IUserSession userSession = Substitute.For<IUserSession>();
            var query = new AddFeedbackQuery(userSession)
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
            IUserSession userSession = Substitute.For<IUserSession>();
            var query = new AddFeedbackQuery(userSession)
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
            IUserSession userSession = Substitute.For<IUserSession>();
            var query = new AddFeedbackQuery(userSession)
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
            IUserSession userSession = Substitute.For<IUserSession>();
            var query = new AddFeedbackQuery(userSession)
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
