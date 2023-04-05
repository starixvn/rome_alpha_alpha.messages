namespace Alpha.Messages.Tests.Criteria.Feedback
{
    using Alpha.Messages.Criteria.Feedback;
    using NUnit.Framework;

    [TestFixture]
    public class GetFeedbackFunctionCriteriaTests
    {
        [Test]
        public void GetSettingKey_Always_ReturnsCorrectKey()
        {
            // Arrange
            var criteria = new GetFeedbackFunctionCriteria(102);

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.AreEqual("Age_FeedbackFunction_Sel", actualSettingKey);
        }

        [TestCase(-10, false)]
        [TestCase(0, false)]
        [TestCase(102, true)]
        public void Isvalid_Always_ReturnsCorrectExpectedValue(int functionId, bool expected)
        {
            // Arrange
            var criteria = new GetFeedbackFunctionCriteria(functionId);

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.AreEqual(expected, actualIsValid);
        }
    }
}
