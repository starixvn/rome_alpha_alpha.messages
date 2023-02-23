namespace Alpha.Messages.Site.Tests.Handlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.Messages.Models.BackupDomains;
    using Alpha.Messages.Services;
    using Alpha.Messages.Site.Handlers;
    using Alpha.Messages.Site.Requests.BackupDomains;
    using NSubstitute;
    using NUnit.Framework;

    public class BackupDomainsHandlerTest
    {
        private BackupDomainsService service;
        private BackupDomainsHandler handler;

        [SetUp]
        public void SetUp()
        {
            service = Substitute.For<BackupDomainsService>();
            handler = new BackupDomainsHandler(service);
        }

        [Test]
        public async Task Handle_Alway_CallServices()
        {
            // Arrange
            var query = new BackupDomainsQuery();

            // Act
            await handler.Handle(query);

            // Assert
            await service.Received().GetBackupDomains(Arg.Any<string>());
        }

        [Test]
        public async Task Handle_Always_ReturnCorrectViewModel()
        {
            // Arrange
            var query = new BackupDomainsQuery();

            // Act
            var act = await handler.Handle(query);

            // Assert
            Assert.IsInstanceOf(typeof(IEnumerable<BackupDomainModel>), act);
        }
    }
}
