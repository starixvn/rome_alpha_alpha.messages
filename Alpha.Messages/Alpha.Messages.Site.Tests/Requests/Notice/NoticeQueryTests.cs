#pragma warning disable 618, 612

namespace Alpha.Messages.Site.Tests.Requests.Dashboard.Notice
{
    using System;
    using Messages.Constants;
    using Messsages.Site.Requests.Notice;
    using NSubstitute;
    using NUnit.Framework;
    using Sunplus.Agent.Model.Entities;

    [TestFixture]
    public class NoticeMessageQueryTests
    {
        private IUserSession userSession;

        [SetUp]
        public void SetUp()
        {
            userSession = Substitute.For<IUserSession>();
        }

        [TestCase(RoleId.Super, "3000,3100")]
        [TestCase(RoleId.Master, "3000,3200")]
        [TestCase(RoleId.Agent, "3000,3300")]
        public void ParentSitesId_Always_ReturnCorrectByUserLevelId(byte userLevelId, string expectedParentSitesId)
        {
            // Arrange
            userSession.RoleId.Returns(userLevelId);

            // Act
            var query = new NoticeQuery(userSession, DateTime.Today, 10, false);

            // Assert
            Assert.AreEqual(expectedParentSitesId, query.ParentSites);
        }
    }
}