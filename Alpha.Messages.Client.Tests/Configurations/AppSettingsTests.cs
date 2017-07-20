using Alpha.Messages.Client.Configurations;
using NUnit.Framework;
using System.Collections.Specialized;
using System.Configuration;

namespace Alpha.Messages.Client.Tests.Configurations
{
    public class AppSettingsTests
    {
        [Test]
        public void Verify_MissingAnnouncementApi_ThrowException()
        {
            //// Arrange
            AppSettings.AppSettingValues = new NameValueCollection();

            //// Act && Assert
            Assert.Throws<ConfigurationErrorsException>(() => AppSettings.Verify());
        }

        [Test]
        public void AnnouncementSecretKey_EmptyKey_ReturnDefaultValue()
        {
            //// Arrange
            AppSettings.AppSettingValues = new NameValueCollection();
            var defaultValue = "ZDlhMWFmMDgtZGQ0ZC00NWY0LTg1ODItNzVkNTdhOTNjOTEx";

            //// Act
            var act = AppSettings.AnnouncementSecretKey;

            //// Assert
            Assert.AreEqual(defaultValue, act);
        }

        [Test]
        public void AnnouncementSecretKey_ExistKey_ReturnValue()
        {
            //// Arrange
            var expectedValue = "xxxx";
            AppSettings.AppSettingValues = new NameValueCollection()
            {
                ["Announcement:SecretKey"] = expectedValue
            };            

            //// Act
            var act = AppSettings.AnnouncementSecretKey;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }
    }
}
