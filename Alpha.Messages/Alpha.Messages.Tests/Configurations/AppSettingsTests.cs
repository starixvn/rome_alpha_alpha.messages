namespace Alpha.Messages.Tests.Configurations
{
    using System.Collections.Specialized;
    using System.Configuration;
    using Messages.Configurations;
    using NUnit.Framework;

    public class AppSettingsTests
    {
        [Test]
        public void Verify_NoMissing_NoThrowException()
        {
            //// Arrange
            AppSettings.AppSettingValues = new NameValueCollection()
            {
                ["Environment"] = "xxx",
                ["StoredProcedurePath"] = "xxx"
            };

            //// Act && Assert
            Assert.DoesNotThrow(() => AppSettings.Verify());
        }

        [Test]
        public void Verify_MissingStoreProceducePatch_ThrowException()
        {
            //// Arrange
            AppSettings.AppSettingValues = new NameValueCollection()
            {
                ["Environment"] = "xxx"
            };

            //// Act && Assert
            Assert.Throws<ConfigurationErrorsException>(() => AppSettings.Verify());
        }

        [Test]
        public void Verify_MissingEnvironment_ThrowException()
        {
            //// Arrange
            AppSettings.AppSettingValues = new NameValueCollection()
            {
                ["StoredProcedurePath"] = "xxx"
            };

            //// Act && Assert
            Assert.Throws<ConfigurationErrorsException>(() => AppSettings.Verify());
        }

        [Test]
        public void SiteGroup_ExistedKey_ReturnValue()
        {
            //// Arrange
            var expectedValue = "xxx";
            AppSettings.AppSettingValues = new NameValueCollection()
            {
                ["SiteLabel"] = expectedValue
            };

            //// Act
            var act = AppSettings.SiteLabel;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void RemoteRefURL_ExistedKey_ReturnValue()
        {
            //// Arrange
            var expectedValue = "url";
            AppSettings.AppSettingValues = new NameValueCollection()
            {
                ["RemoteRefURL"] = expectedValue
            };

            //// Act
            var act = AppSettings.RemoteRefURL;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }
    }
}