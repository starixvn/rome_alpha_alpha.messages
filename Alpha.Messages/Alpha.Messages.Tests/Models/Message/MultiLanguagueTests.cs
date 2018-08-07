namespace Alpha.Messages.Tests.Models.Message
{
    using Constants;
    using Messages.Constants;
    using Messages.Models.Message;
    using NUnit.Framework;

    public class MultiLanguagueTests
    {
        [Test]
        public void GetContentByKey_Thai_ExpectedThaiText()
        {
            //// Arrange
            var expectedThaiText = "Thai....";
            var language = new MultiLanguague("EN", "CS", "CH", expectedThaiText, "JP", "KO", "VN");

            //// Act
            var act = language.GetContentByKey(Language.Thai);

            //// Assert
            Assert.AreEqual(expectedThaiText, act);
        }

        [Test]
        public void GetContentByKey_JP_ExpectedJPText()
        {
            //// Arrange
            var expectedJPText = "JP....";
            var language = new MultiLanguague("EN", "CS", "CH", "TH", expectedJPText, "KO", "VN");

            //// Act
            var act = language.GetContentByKey(Language.Japanese);

            //// Assert
            Assert.AreEqual(expectedJPText, act);
        }

        [Test]
        public void GetContentByKey_KO_ExpectedKOText()
        {
            //// Arrange
            var expectedKOText = "KO....";
            var language = new MultiLanguague("EN", "CS", "CH", "JP", "TH", expectedKOText, "VN");

            //// Act
            var act = language.GetContentByKey(Language.Korean);

            //// Assert
            Assert.AreEqual(expectedKOText, act);
        }

        [Test]
        public void GetContentByKey_VN_ExpectedVNText()
        {
            //// Arrange
            var expectedVNText = "Viet nam....";
            var language = new MultiLanguague("EN", "CS", "CH", "JP", "TH", "KO", expectedVNText);

            //// Act
            var act = language.GetContentByKey(Language.Vietnamese);

            //// Assert
            Assert.AreEqual(expectedVNText, act);
        }
    }
}
