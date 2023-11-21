namespace Alpha.Messages.Site.Requests.BackupDomains
{
    using Alpha.Messages.Site.ViewModels;
    using MediatR;
    using Sunplus.Agent.Model.Entities;

    public class BackupDomainsQuery : IAsyncRequest<BackupDomainsViewModel>
    {
        public BackupDomainsQuery(IUserSession userSession)
        {
            Site = userSession.Site;
            RoleId = userSession.RoleId;
        }

        public string Site { get; }

        public byte RoleId { get; }
    }
}