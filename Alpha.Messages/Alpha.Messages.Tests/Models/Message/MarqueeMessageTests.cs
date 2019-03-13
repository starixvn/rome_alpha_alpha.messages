namespace Alpha.Messages.Tests.Models.Message
{
    using Alpha.Messages.Models.Message;
    using NUnit.Framework;

    public class MarqueeMessageTests
    {
        [Test]
        public void MarqueeMessage_Always_SetPropertyInConstructor()
        {
            //// Arrange
            var expectedId = 123;
            var expectedBetTransId = "bettransid";
            var expectedIsBatch = true;
            var model = new MarqueeMessage(expectedId, "ContentEN", "ContentCS", "ContentCH", "ContentJP", "ContentTH", "ContentKO", "ContentVI", expectedIsBatch, expectedBetTransId, 321, false, 1);

            //// Act

            //// Assert
            Assert.AreEqual(expectedId, model.Id);
            Assert.AreEqual(expectedBetTransId, model.BetTransId);
            Assert.AreEqual(expectedIsBatch, model.IsBatch);
        }
    }
}
