namespace Alpha.Messages.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Announcements.Models;
    using Announcements.Specs;
    using KellermanSoftware.CompareNetObjects;
    using Messages.Models.Announcement;
    using Messages.Services;
    using NUnit.Framework;
    using Params.Announcement;
    using Ploeh.AutoFixture;

    public class AnnouncementFormatServiceTest
    {
        private readonly Fixture fixture = new Fixture();
        private AnnouncementFormatServiceImpl formatService;
        private CompareLogic compareLogic;

        [SetUp]
        public void SetUp()
        {
            formatService = new AnnouncementFormatServiceImpl();
            compareLogic = new CompareLogic();
        }

        [Test]
        public void Format_NoMatchCode_NoReplaceContent()
        {
            //// Arrange
            var announcements = fixture.CreateMany<AlphaAnnouncement>();
            formatService.ReplacementCode = new Dictionary<string, Func<string, BaseAnnouncementsParams, IEnumerable<ISpecification<IParam>>, string>>();

            //// Act
            var act = formatService.Format(announcements, new BaseAnnouncementsParams());

            //// Assert
            Assert.True(compareLogic.Compare(act.ToList(), announcements.ToList()).AreEqual);
        }

        [Test]
        public void Format_MatchSiteMainCode_ReplaceSiteMain()
        {
            //// Arrange
            var content = fixture.Create<string>();
            var mainCodeAnnouncement = new AlphaAnnouncement(1, content + "{site-main}", "2222", "", DateTime.Now, DateTime.Now);
            var announcements = new List<AlphaAnnouncement> { mainCodeAnnouncement };

            //// Act
            var act = formatService.Format(announcements, new BaseAnnouncementsParams { IsMobileDevice = false });

            //// Assert
            Assert.IsFalse(act.Any(i => i.Content.Contains("{site-main}")));
        }
    }
}
