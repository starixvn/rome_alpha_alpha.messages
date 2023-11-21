namespace Alpha.Messages.Models.BackupDomains
{
    public class BackupDomainModel
    {
        public BackupDomainModel()
        {
        }

        public BackupDomainModel(string domain, bool isAgency)
        {
            Domain = domain;
            IsAgency = isAgency;
        }

        public string Domain { get; }

        public bool IsAgency { get; }
    }
}