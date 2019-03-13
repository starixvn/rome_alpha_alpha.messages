namespace Alpha.Messages.Tests.Models.Message
{
    using Alpha.Messages.Models.Message;
    using NUnit.Framework;

    [TestFixture]
    public class GeneralMarqueeMessageTests
    {
        [Test]
        public void GeneraMarqueeMessage_Always_ReplaceDoubleAtCharacterByBettranId()
        {
            //// Arrange
            var expectedContent = "123razortesting marquee";
            var model = new GeneralMarqueeMessage(1, "@@razortesting marquee", "123", true, false, 1);

            //// Act
            var act = model.Content;

            //// Assert
            Assert.AreEqual(expectedContent, act);
        }
    }
}
