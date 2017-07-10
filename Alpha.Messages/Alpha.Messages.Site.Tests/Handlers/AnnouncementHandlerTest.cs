using Alpha.Messages.Params;
using Alpha.Messages.Services;
using Alpha.Messages.Site.Handlers;
using Alpha.Messages.Site.Requests;
using Alpha.Messages.Site.Tests.Fixtures;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Messages.Site.Tests.Handlers
{
    public class AnnouncementHandlerTest
    {
        private AnnouncementService announcementService;
        private ProductService productService;
        private AnnouncementHandler handler;
        private UserSessionFixture fixture;
        [SetUp]
        public void SetUp()
        {
            announcementService = Substitute.For<AnnouncementService>();
            productService = Substitute.For<ProductService>();
            handler = new AnnouncementHandler(announcementService, productService);
            fixture = new UserSessionFixture();
        }

        [Test]
        public void Handle_Always_CallGetHotAnnouncment()
        {
            //// Arrange
            var query = new HotAnnouncementQuery(fixture.UserSession);            

            //// Act
            handler.Handle(query);

            //// Assert
            announcementService.Received().GetHotAnnouncement(Arg.Any<GetHotAnnouncementsParams>());
        }


    }
}
