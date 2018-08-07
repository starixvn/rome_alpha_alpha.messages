namespace Alpha.Messages.Site.Tests.ViewModel
{
    using System;
    using System.Collections.Generic;
    using Alpha.Messages.Constants;
    using Alpha.Messages.Models.Announcement;
    using Alpha.Messages.Models.Constants;
    using Alpha.Messages.Models.Message;
    using Alpha.Messages.Site.Tests.Helpers;
    using Alpha.Messages.Site.ViewModels;
    using KellermanSoftware.CompareNetObjects;
    using NUnit.Framework;

    public class AnnouncementHistoryViewModelTests
    {
        private CompareLogic compareLogic;

        [SetUp]
        public void SetUp()
        {
            compareLogic = new CompareLogic();
        }

        [Test]
        public void Announcements_Always_SetOnConstructor()
        {
            //// Arrange
            var announcements = new List<AlphaAnnouncement>
            {
                new AlphaAnnouncement(12,"content","code","source",DateTime.Now,DateTime.Now,null)
            };
            var unreadMessage = FakeDataHelper.Create<NumberOfUnreadMessage>();
            var viewModel = new AnnouncementHistoryViewModel(announcements, DateTime.Now, DateTime.Now, MessageType.Normal, UserLevel.Agent, unreadMessage);

            //// Act
            var act = viewModel.Announcements;

            //// Assert
            Assert.IsTrue(compareLogic.Compare(announcements, act).AreEqual);
        }

        [Test]
        public void FromDate_Always_SetOnConstructor()
        {
            //// Arrange
            var announcements = new List<AlphaAnnouncement>
            {
                new AlphaAnnouncement(12,"content","code","source",DateTime.Now,DateTime.Now,null)
            };
            var unreadMessage = FakeDataHelper.Create<NumberOfUnreadMessage>();
            var fromDate = DateTime.Now.AddDays(-1);
            var viewModel = new AnnouncementHistoryViewModel(announcements, fromDate, DateTime.Now, MessageType.Normal, UserLevel.Agent, unreadMessage);

            //// Act
            var act = viewModel.FromDate;

            //// Assert
            Assert.IsTrue(compareLogic.Compare(fromDate, act).AreEqual);
        }

        [Test]
        public void ToDate_Always_SetOnConstructor()
        {
            //// Arrange
            var announcements = new List<AlphaAnnouncement>
            {
                new AlphaAnnouncement(12,"content","code","source",DateTime.Now,DateTime.Now,null)
            };
            var unreadMessage = FakeDataHelper.Create<NumberOfUnreadMessage>();
            var toDate = DateTime.Now.AddDays(-1);
            var viewModel = new AnnouncementHistoryViewModel(announcements, DateTime.Now, toDate, MessageType.Normal, UserLevel.Agent, unreadMessage);

            //// Act
            var act = viewModel.ToDate;

            //// Assert
            Assert.IsTrue(compareLogic.Compare(toDate, act).AreEqual);
        }
    }
}
