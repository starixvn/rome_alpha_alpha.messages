namespace Alpha.Messages.Site.Tests.ViewModel
{
    using System.Linq;
    using Alpha.Messages.Models.Announcement;
    using Alpha.Messages.Site.ViewModels;
    using NUnit.Framework;
    using Ploeh.AutoFixture;

    public class HotAnnouncementViewModelTests
    {
        private Fixture fixture;
        [SetUp]
        public void SetUp()
        {
            fixture = new Fixture();
        }

        [Test]
        public void CookiesId_Always_CorrectValue()
        {
            //// Arrange
            var expectedValue = "Razor11212";
            var viewModel = new HotAnnouncementViewModel(null, expectedValue);

            //// Act
            var act = viewModel.CookiesId;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void Announcements_Always_CorrectValue()
        {
            //// Arrange
            var expectedValue = fixture.CreateMany<HotAnnouncement>();
            var viewModel = new HotAnnouncementViewModel(expectedValue, string.Empty);

            //// Act
            var act = viewModel.Announcements;

            //// Assert
            Assert.AreEqual(expectedValue.Count(), act.Count());
        }
    }
}
