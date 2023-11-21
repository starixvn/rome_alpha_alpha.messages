namespace Alpha.Messages.Site.Tests.Controllers
{
    using System.Threading.Tasks;
    using Alpha.Announcements.Models;
    using Alpha.Messages.Site.Controllers;
    using Alpha.Messages.Site.Requests.BackupDomains;
    using Alpha.Messages.Site.Tests.Fixtures;
    using KellermanSoftware.CompareNetObjects;
    using MediatR;
    using NSubstitute;
    using NUnit.Framework;

    public class BackupDomainsControllerTest
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
        public async Task Index_Always_SendQueryToMediator()
        {
            // Arrange
            var query = new BackupDomainsQuery(fixtures.UserSession);
            var controller = new BackupDomainsController(mediator);

            // Act
            await controller.Index(query);

            // Assert
            await mediator.Received().SendAsync(query);
        }
    }
}
