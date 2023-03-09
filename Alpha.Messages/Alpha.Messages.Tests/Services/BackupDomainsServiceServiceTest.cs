namespace Alpha.Messages.Tests.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.Messages.Criteria.BackupDomains;
    using Alpha.Messages.Models.BackupDomains;
    using Fanex.Data.Repository;
    using Messages.Services;
    using NSubstitute;
    using NUnit.Framework;
    using Ploeh.AutoFixture;

    public class BackupDomainsServiceTest
    {
        private readonly Fixture fixture = new Fixture();
        private IDynamicRepository dynamicRepository;
        private BackupDomainsServiceImpl service;

        [SetUp]
        public void SetUp()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            service = new BackupDomainsServiceImpl(dynamicRepository);
        }

        [Test]
        public async Task GetBackupDomains_Always_ReturnsDataFromRepository()
        {
            // Arrange
            dynamicRepository.FetchAsync<BackupDomainModel>(Arg.Any<GetGetBackupDomainsCriteria>())
                .Returns(new List<BackupDomainModel>());

            // Act
            var act = await service.GetBackupDomains(string.Empty);

            // Assert
            Assert.IsEmpty(act);
        }
    }
}
