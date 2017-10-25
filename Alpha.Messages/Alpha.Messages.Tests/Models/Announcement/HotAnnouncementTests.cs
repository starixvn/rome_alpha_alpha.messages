namespace Alpha.Messages.Tests.Models.Announcement
{
    using Messages.Models.Announcement;
    using NUnit.Framework;

    class HotAnnouncementTests
    {
        [Test]
        public void Id_Always_ReturnExpectedValueInConstructor()
        {
            //// Arrange
            var expectedValue = "razor id";
            var model = new HotAnnouncement(expectedValue, string.Empty, string.Empty, string.Empty);

            //// Act
            var act = model.Id;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void Source_IsEmpty_ReturnEmpty()
        {
            //// Arrange
            var expectedValue = string.Empty;
            var model = new HotAnnouncement(string.Empty, string.Empty, string.Empty, expectedValue);

            //// Act
            var act = model.Source;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void Source_IsNotEmpty_ReturnAddingMessagePathPrefix()
        {
            //// Arrange
            var expectedValue = "razor source";
            var model = new HotAnnouncement(string.Empty, string.Empty, string.Empty, expectedValue);

            //// Act
            var act = model.Source;

            //// Assert
            Assert.AreEqual(expectedValue, act.Split('/')[1]);
        }
    }
}
