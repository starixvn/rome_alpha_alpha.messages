using Alpha.Messages.Services;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;
using Alpha.Messages.Models;
using Alpha.Messages.Params;
using KellermanSoftware.CompareNetObjects;

namespace Alpha.Messages.Tests.Services
{
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
            var announcements = fixture.CreateMany<HotAnnouncement>();
            formatService.ReplacementCode = new Dictionary<string, Func<string, GetHotAnnouncementsParams, string>>();

            //// Act
            var act = formatService.Format(announcements, new GetHotAnnouncementsParams());

            //// Assert
            Assert.True(compareLogic.Compare(act.ToList(), announcements.ToList()).AreEqual);
        }

        [Test]
        public void Format_MatchSiteMainCode_ReplaceSiteMain()
        {
            //// Arrange
            var content = fixture.Create<string>();
            var mainCodeAnnouncement = new HotAnnouncement { Code = "2222", Content = "{site-main}" + content };
            var announcements = new List<HotAnnouncement> { mainCodeAnnouncement };

            //// Act
            var act = formatService.Format(announcements, new GetHotAnnouncementsParams { IsMobileDevice = false });

            //// Assert
            Assert.IsFalse(act.Any(i => i.Content.Contains("{site-main}")));
        }
    }
}
