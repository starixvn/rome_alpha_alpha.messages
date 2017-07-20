using Alpha.Messages.Criteria;
using Alpha.Messages.Models;
using Alpha.Messages.Params;
using Alpha.Messages.Services;
using Fanex.Data.Repository;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Messages.Tests.Services
{
    public class AnnouncementFromDatabaseServiceTests
    {
        private IDynamicRepository dynamicRepository;
        private ResourceManager resourceManager;
        private AnnouncementFromDatabaseServiceImpl service;

        [SetUp]
        public void SetUp()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            resourceManager = Substitute.For<ResourceManager>();
            service = new AnnouncementFromDatabaseServiceImpl(dynamicRepository, resourceManager);
        }

        [Test]
        public void GetHotAnnouncements_UserIsNotAdmin_CallDynamicRepositoryToGetMessageTypes()
        {
            //// Arrange
            var parameters = new GetHotAnnouncementsParams { RoleId = 1};
            dynamicRepository.Fetch<byte>(Arg.Any<GetMessageTypeListCriteria>()).Returns(Enumerable.Empty<byte>());

            //// Act
            service.GetHotAnnouncements(parameters);

            //// Assert
            dynamicRepository.Received().Fetch<byte>(Arg.Any<GetMessageTypeListCriteria>());
        }
    }
}
