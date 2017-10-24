namespace Alpha.Messages.Tests.Models.Message
{
    using Messages.Models.Message;
    using NUnit.Framework;

    public class GeneralMoveBetsMessageTests
    {
        [Test]
        public void MoveBetsEndTime_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = "razor end time";
            var moveBets = new GeneralMoveBetsMessage(string.Empty, expectedValue);

            //// Act
            var act = moveBets.MoveBetsEndTime;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }
    }
}
