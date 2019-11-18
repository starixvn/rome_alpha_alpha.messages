using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Alpha.Messages.Tests.Models.Announcement
{
    using System;
    using Messages.Constants;
    using Messages.Models.Announcement;
    using NUnit.Framework;

    public class LastSeenAnnouncementTests
    {
        [Test]
        public void GetLastSeenByType_NormalType_ReturnNormalSeen()
        {
            //// Arrange
            var normalSeen = DateTime.Now.AddDays(-1);
            var model = new LastSeenAnnouncement(123, normalSeen, DateTime.Now, DateTime.Now, DateTime.Now);

            //// Act
            var act = model.GetLastSeenByType(MessageType.Normal);

            //// Assert
            Assert.AreEqual(normalSeen, act);
        }

        [Test]
        public void GetLastSeenByType_PersornalType_ReturnPersonalSeen()
        {
            //// Arrange
            var personalSeen = DateTime.Now.AddDays(-1);
            var model = new LastSeenAnnouncement(123, DateTime.Now, personalSeen, DateTime.Now, DateTime.Now);

            //// Act
            var act = model.GetLastSeenByType(MessageType.Personal);

            //// Assert
            Assert.AreEqual(personalSeen, act);
        }

        [Test]
        public void GetLastSeenByType_SpecialType_ReturnSpecialSeen()
        {
            //// Arrange
            var specialSeen = DateTime.Now.AddDays(-1);
            var model = new LastSeenAnnouncement(123, DateTime.Now, DateTime.Now, specialSeen, DateTime.Now);

            //// Act
            var act = model.GetLastSeenByType(MessageType.Special);

            //// Assert
            Assert.AreEqual(specialSeen, act);
        }

        [Test]
        public void GetLastSeenByType_SystemType_ReturnSystemSeen()
        {
            //// Arrange
            var systemSeen = DateTime.Now.AddDays(-1);
            var model = new LastSeenAnnouncement(123, DateTime.Now, DateTime.Now, DateTime.Now, systemSeen);

            //// Act
            var act = model.GetLastSeenByType(MessageType.System);

            //// Assert
            Assert.AreEqual(systemSeen, act);
        }

        [Test]
        public void GetLastSeen_DefaultCtor_ReturnSystemSeen()
        {
            //// Expected
            var expected = DateTime.MinValue;

            //// Arrange
            var model = new LastSeenAnnouncement();

            var act = model.GetLastSeenByType(MessageType.System);

            //// Assert
            Assert.AreEqual(expected, act);
        }

        [Test]
        public void GetLastSeen_DefaultCtor_ReturnNormalSeen()
        {
            //// Expected
            var expected = DateTime.MinValue;

            //// Arrange
            var model = new LastSeenAnnouncement();

            var act = model.GetLastSeenByType(MessageType.Normal);

            //// Assert
            Assert.AreEqual(expected, act);
        }

        [Test]
        public void GetLastSeen_DefaultCtor_ReturnSpecialSeen()
        {
            //// Expected
            var expected = DateTime.MinValue;

            //// Arrange
            var model = new LastSeenAnnouncement();

            var act = model.GetLastSeenByType(MessageType.Special);

            //// Assert
            Assert.AreEqual(expected, act);
        }

        [Test]
        public void GetLastSeen_DefaultCtor_ReturnPersonalSeen()
        {
            //// Expected
            var expected = DateTime.MinValue;

            //// Arrange
            var model = new LastSeenAnnouncement();

            var act = model.GetLastSeenByType(MessageType.Personal);

            //// Assert
            Assert.AreEqual(expected, act);
        }
    }
}
