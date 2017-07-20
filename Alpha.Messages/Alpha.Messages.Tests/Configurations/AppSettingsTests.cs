
using Alpha.Messages.Configurations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Messages.Tests.Configurations
{
    public class AppSettingsTests
    {
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
        public void NoticeTypes_MissingNoticeTypes_ReturnEmptyList()
        {
            //// Arrange
            AppSettings.AppSettingValues = new NameValueCollection();

            //// Act
            var act = AppSettings.NoticeTypes;

            //// Assert
            Assert.False(act.Any());
        }

        [Test]
        public void SiteGroup_ExistedKey_ReturnValue()
        {
            //// Arrange
            var expectedValue = "xxx";
            AppSettings.AppSettingValues = new NameValueCollection()
            {
                ["SiteGroup"] = expectedValue
            };

            //// Act
            var act = AppSettings.SiteGroup;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }
    }
}
