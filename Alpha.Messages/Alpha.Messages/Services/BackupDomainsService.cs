using System.Collections.Generic;
using System.Threading.Tasks;
using Alpha.Messages.Criteria.BackupDomains;
using Alpha.Messages.Models.BackupDomains;
using Fanex.Data.Repository;

namespace Alpha.Messages.Services
{
    public interface BackupDomainsService
    {
        Task<IEnumerable<BackupDomainModel>> GetBackupDomains(string siteLabel);
    }

    public class BackupDomainsServiceImpl : BackupDomainsService
    {
        private readonly IDynamicRepository dynamicRepository;

        public BackupDomainsServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public async Task<IEnumerable<BackupDomainModel>> GetBackupDomains(string siteLabel)
        {
            var criteria = new GetGetBackupDomainsCriteria(siteLabel);

            var data = await dynamicRepository.FetchAsync<BackupDomainModel>(criteria);

            return data;
        }
    }
}
