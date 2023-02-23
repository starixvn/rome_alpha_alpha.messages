using System.Collections.Generic;
using System.Threading.Tasks;
using Alpha.Messages.Models.BackupDomains;
using Alpha.Messages.Services;
using Alpha.Messages.Site.Requests.BackupDomains;
using MediatR;

namespace Alpha.Messages.Site.Handlers
{
    public class BackupDomainsHandler
        : IAsyncRequestHandler<BackupDomainsQuery, IEnumerable<BackupDomainModel>>
    {
        private readonly BackupDomainsService service;

        public BackupDomainsHandler(BackupDomainsService service)
        {
            this.service = service;
        }

        public async Task<IEnumerable<BackupDomainModel>> Handle(BackupDomainsQuery message)
            => await service.GetBackupDomains(message.SiteLabel);
    }
}