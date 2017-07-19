using Alpha.Announcements.Services;
using Alpha.Messages.Client;
using Alpha.Messages.Services;
using Alpha.Messages.Tests.Fixtures;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Messages.Tests.Services
{
    public class AnnouncementFromApiServiceTest
    {
        private AnnouncementFromApiService apiService;
        private AnnouncementClient announcementClient;
        private SpecificationService specificationService;
        private FanexCacheFixture cacheFixture;

        [SetUp]
        public void SetUp()
        {
            announcementClient = Substitute.For<AnnouncementClient>();
            specificationService = Substitute.For<SpecificationService>();
            
            apiService = new AnnoucemtnFromApiServiceImpl(announcementClient, specificationService);
            cacheFixture = new FanexCacheFixture();
            
        }

        [Test]
        public void GetPopupAnnouncement_StateUnderTest_ExpectedBehaviour()
        {
            //// Arrange

            //// Act

            //// Assert

        }
    }
}
