using Fanex.Data.Repository;

namespace Alpha.Messages.Criteria.BackupDomains
{
    public class GetGetBackupDomainsCriteria : CriteriaBase
    {
        public GetGetBackupDomainsCriteria(string siteLabel)
        {
            SiteLabel = siteLabel;
        }

        public string SiteLabel { get; set; }

        public override string GetSettingKey()
           => "Age_DomainListSel";

        public override bool IsValid()
            => true;
    }
}
