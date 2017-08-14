using Alpha.Messages.Criteria;
using Alpha.Messages.Models;
using NSubstitute;
using NUnit.Framework;
using System.Globalization;

namespace Alpha.Messages.Tests.Criteria
{
    public class InsertPersonalMessageCriteriaTest
    {
        private ResourceManager resourceManager;

        [SetUp]
        public void SetUp()
        {
            resourceManager = Substitute.For<ResourceManager>();
            resourceManager.GetString(Arg.Any<string>(), Arg.Any<CultureInfo>()).Returns("testing");
        }

        [Test]
        public void GetSettingKey_Always_ReturnCorrectKey()
        {
            //// Arrange
            var criteria = new InsertPersonalMessageCriteria("vn-VI", resourceManager);
            var expectedKey = "Dot_Msg_SetMessage";

            //// Act
            var act = criteria.GetSettingKey();

            //// Assert
            Assert.AreEqual(expectedKey, act);
        }

        [Test]
        public void IsValid_Always_ReturnTrue()
        {
            //// Arrange
            var criteria = new InsertPersonalMessageCriteria("vn-VI", resourceManager);

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.True(act);
        }
    }
}
