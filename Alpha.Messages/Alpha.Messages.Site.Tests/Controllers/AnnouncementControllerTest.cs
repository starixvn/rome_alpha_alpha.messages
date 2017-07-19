using Alpha.Announcements.Models;
using Alpha.Messages.Site.Controllers;
using Alpha.Messages.Site.Requests;
using Alpha.Messages.Site.Tests.Fixtures;
using KellermanSoftware.CompareNetObjects;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Messages.Site.Tests.Controllers
{
    public class AnnouncementControllerTest
    {
        private UserSessionFixture fixtures;
        private IMediator mediator;
        private IParam specificationParam;
        private CompareLogic compareLogic;

        [SetUp]
        public void SetUp()
        {
            fixtures = new UserSessionFixture();
            mediator = NSubstitute.Substitute.For<IMediator>();
            specificationParam = Substitute.For<IParam>();
            compareLogic = new CompareLogic();
        }

        [Test]
        public void PopupAnnouncement_Always_SetSpecificationToQuery()
        {
            //// Arrange
            var query = new PopupAnnouncementQuery(fixtures.UserSession);
            var controller = new AnnouncementController(mediator, specificationParam);

            //// Act
            controller.PopupAnnouncement(query);

            //// Assert
            Assert.True(compareLogic.Compare(specificationParam, query.Specification).AreEqual);
        }

        [Test]
        public void PopupAnnouncement_Always_SendQueryToMediator()
        {
            //// Arrange
            var query = new PopupAnnouncementQuery(fixtures.UserSession);
            var controller = new AnnouncementController(mediator, specificationParam);

            //// Act
            controller.PopupAnnouncement(query);

            //// Assert
            mediator.Received().Send(query);
        }

        [Test]
        public void HeaderAnnouncement_Always_SendQueryToMediator()
        {
            //// Arrange
            var query = new HeaderAnnouncementQuery(fixtures.UserSession);
            var controller = new AnnouncementController(mediator, specificationParam);

            //// Act
            controller.HeaderAnnouncement(query);

            //// Assert
            mediator.Received().Send(query);
        }
    }
}
