namespace Alpha.Messages.Tests.Criteria.Customer
{
    using Alpha.Messages.Criteria.Feedback;
    using Alpha.Messages.Params.Feedback;
    using NUnit.Framework;

    [TestFixture]
    public class AddFeedbackCriteriaTests
    {
        [Test]
        public void GetSettingKey_Always_ReturnsCorrectKey()
        {
            // Arrange
            var param = new AddFeedbackParam(0, 0, string.Empty, 0, null, null);
            var criteria = new AddFeedbackCriteria(param);

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.AreEqual("Age_CustFeedbackIns", actualSettingKey);
        }

        [Test]
        public void IsValid_RateAndMessageAndXMLfbIsEmpty_ReturnFalse()
        {
            // Arrange
            var param = new AddFeedbackParam(0, 0, string.Empty, 0, null, null);
            var criteria = new AddFeedbackCriteria(param);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.False(actualValue);
        }

        [Test]
        public void IsValid_HasRateAndMessageIsEmpty_ReturnTrue()
        {
            // Arrange
            var param = new AddFeedbackParam(0, 0, string.Empty, 2, null, null);
            var criteria = new AddFeedbackCriteria(param);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.True(actualValue);
        }

        [Test]
        public void IsValid_RateIsZeroAndHasMessage_ReturnTrue()
        {
            // Arrange
            var param = new AddFeedbackParam(0, 0, "Message", 0, null, null);
            var criteria = new AddFeedbackCriteria(param);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.True(actualValue);
        }

        [Test]
        public void IsValid_RateIsZeroAndOverLimitMessage_ReturnFalse()
        {
            // Arrange
            var param = new AddFeedbackParam(0, 0, new string('*', 5000), 0, null, null);
            var criteria = new AddFeedbackCriteria(param);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.False(actualValue);
        }

        [Test]
        public void IsValid_HasXMLfb_ReturnTrue()
        {
            // Arrange
            var param = new AddFeedbackParam(0, 0, string.Empty, 0, null, "<root></root>");
            var criteria = new AddFeedbackCriteria(param);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.True(actualValue);
        }

        [Test]
        public void Constructor_Always_AssignsCorrectProperties()
        {
            // Arrange
            var param = new AddFeedbackParam(123, 456, "qwerty", 5, 102, "<root></root>");

            // Act
            var criteria = new AddFeedbackCriteria(param);

            // Assert
            Assert.True(123 == criteria.CustId
                && 456 == criteria.SubAccId
                && "qwerty" == criteria.Comment
                && 5 == criteria.Rate
                && 102 == criteria.FuncId
                && "<root></root>" == criteria.XMLfb);
        }
    }
}
