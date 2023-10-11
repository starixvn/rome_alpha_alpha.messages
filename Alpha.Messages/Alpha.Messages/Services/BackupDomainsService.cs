namespace Alpha.Messages.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.Messages.Criteria.BackupDomains;
    using Alpha.Messages.Models.BackupDomains;
    using Fanex.Data.Repository;

    public interface BackupDomainsService
    {
        Task<IEnumerable<BackupDomainModel>> GetBackupDomains(string site);
    }

    public class BackupDomainsServiceImpl : BackupDomainsService
    {
        private readonly IDynamicRepository dynamicRepository;

        public BackupDomainsServiceImpl(IDynamicRepository dynamicRepository)
            => this.dynamicRepository = dynamicRepository;

        public Task<IEnumerable<BackupDomainModel>> GetBackupDomains(string site)
            => dynamicRepository.FetchAsync<BackupDomainModel>(new GetGetBackupDomainsCriteria(site));
    }
}
