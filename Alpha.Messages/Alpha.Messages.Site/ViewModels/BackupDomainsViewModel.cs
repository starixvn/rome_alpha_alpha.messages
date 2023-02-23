namespace Alpha.Messages.Site.ViewModels
{
    using System.Collections.Generic;
    using Alpha.Messages.Models.BackupDomains;
    using Alpha.Messages.Models.Constants;

    public class BackupDomainsViewModel
    {
        public BackupDomainsViewModel(byte roleId, IEnumerable<BackupDomainModel> domains)
        {
            RoleId = roleId;
            Domains = domains;
        }

        public byte RoleId { get; }

        public IEnumerable<BackupDomainModel> Domains { get; }

        public string RoleName => UserLevel.GetDisplayName(RoleId);
    }
}