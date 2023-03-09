using System.Collections.Generic;
using Alpha.Messages.Models.BackupDomains;
using MediatR;

namespace Alpha.Messages.Site.Requests.BackupDomains
{
    public class BackupDomainsQuery : IAsyncRequest<IEnumerable<BackupDomainModel>>
    {
        public string SiteLabel { get; set; }
    }
}