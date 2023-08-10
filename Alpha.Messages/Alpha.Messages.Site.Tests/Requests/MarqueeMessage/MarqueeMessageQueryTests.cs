#pragma warning disable 618, 612

namespace Alpha.Messages.Site.Tests.Requests.Dashboard.Notice
{
    using NSubstitute;
    using NUnit.Framework;
    using Site.Requests.MarqueeMessage;
    using Sunplus.Agent.Model.Entities;

    [TestFixture]
    public class MarqueeMessageQueryTests
    {
        private IUserSession userSession;

        [SetUp]
        public void SetUp()
        {
            userSession = Substitute.For<IUserSession>();
        }

        [Test]
        public void RoleId_Always_ReturnCorrectInConstructor()
        {
            // Arrange
            byte roleIdInConstructor = 1;
            userSession.RoleId.Returns(roleIdInConstructor);

            // Act
            var query = new MarqueeMessageQuery(userSession);

            // Assert
            Assert.AreEqual(roleIdInConstructor, query.RoleId);
        }

        [Test]
        public void CustId_Always_ReturnCorrectInConstructor()
        {
            // Arrange
            var custId = 1;
            userSession.CustId.Returns(custId);

            // Act
            var query = new MarqueeMessageQuery(userSession);

            // Assert
            Assert.AreEqual(custId, query.CustId);
        }

        [Test]
        public void IsTest_Always_ReturnCorrectInConstructor()
        {
            // Arrange
            var isTest = true;

            // Act
            var query = new MarqueeMessageQuery(userSession, isTest);

            // Assert
            Assert.AreEqual(isTest, query.IsTest);
        }

        [Test]
        public void CurrentLang_Always_ReturnCorrectInConstructor()
        {
            // Arrange
            var currentLang = "EN";
            userSession.Language.Returns(currentLang);

            // Act
            var query = new MarqueeMessageQuery(userSession);

            // Assert
            Assert.AreEqual(currentLang, query.CurrentLang);
        }       
    }
}