namespace Alpha.Messages.Site.ViewModels
{
    using System.Collections.Generic;
    using Alpha.Messages.Models.Constants;

    public class BackupDomainsViewModel
    {
        public BackupDomainsViewModel(
            byte roleId,
            IEnumerable<string> agencyDomains,
            IEnumerable<string> memberDomains)
        {
            RoleName = UserLevel.GetDisplayName(roleId);
            AgencyDomains = agencyDomains;
            MemberDomains = memberDomains;
        }

        public string RoleName { get; }

        public IEnumerable<string> AgencyDomains { get; }

        public IEnumerable<string> MemberDomains { get; }
    }
}