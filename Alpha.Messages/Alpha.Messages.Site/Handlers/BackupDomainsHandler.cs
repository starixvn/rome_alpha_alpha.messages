namespace Alpha.Messages.Site.Handlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.Messages.Services;
    using Alpha.Messages.Site.Requests.BackupDomains;
    using Alpha.Messages.Site.ViewModels;
    using MediatR;

    public class BackupDomainsHandler : IAsyncRequestHandler<BackupDomainsQuery, BackupDomainsViewModel>
    {
        private readonly BackupDomainsService backupDomainsService;

        public BackupDomainsHandler(BackupDomainsService backupDomainsService)
        {
            this.backupDomainsService = backupDomainsService;
        }

        public async Task<BackupDomainsViewModel> Handle(BackupDomainsQuery message)
        {
            var domains = await backupDomainsService.GetBackupDomains(message.Site);
            var agencyDomains = new List<string>();
            var memberDomains = new List<string>();

            foreach (var item in domains)
            {
                if (item.IsAgency)
                {
                    agencyDomains.Add(item.Domain);
                }
                else
                {
                    memberDomains.Add(item.Domain);
                }
            }

            return new BackupDomainsViewModel(message.RoleId, agencyDomains, memberDomains);
        }
    }
}