namespace Alpha.Messages.Site.Tests.Handlers
{
    using System.Threading.Tasks;
    using Alpha.Messages.Services;
    using Alpha.Messages.Site.Handlers;
    using Alpha.Messages.Site.Requests.BackupDomains;
    using Alpha.Messages.Site.Tests.Fixtures;
    using Alpha.Messages.Site.ViewModels;
    using NSubstitute;
    using NUnit.Framework;

    public class BackupDomainsHandlerTest
    {
        private UserSessionFixture fixtures;
        private BackupDomainsService service;
        private BackupDomainsHandler handler;

        [SetUp]
        public void SetUp()
        {
            fixtures = new UserSessionFixture();
            fixtures.UserSession.RoleId.Returns((byte)2);
            service = Substitute.For<BackupDomainsService>();
            handler = new BackupDomainsHandler(service);
        }

        [Test]
        public async Task Handle_Alway_CallServices()
        {
            // Arrange
            var query = new BackupDomainsQuery(fixtures.UserSession);

            // Act
            await handler.Handle(query);

            // Assert
            await service.Received().GetBackupDomains(Arg.Any<string>());
        }

        [Test]
        public async Task Handle_Always_ReturnCorrectViewModel()
        {
            // Arrange
            var query = new BackupDomainsQuery(fixtures.UserSession);

            // Act
            var act = await handler.Handle(query);

            // Assert
            Assert.IsInstanceOf(typeof(BackupDomainsViewModel), act);
        }
    }
}
