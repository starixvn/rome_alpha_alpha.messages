namespace Alpha.Messages.Site.Requests.Message
{
    using MediatR;
    using Sunplus.Agent.Model.BL.Definitions;
    using Sunplus.Agent.Model.Entities;
    using ViewModels.Message;

    public class LatestMessageQuery : IRequest<LatestMessageViewModel>
    {
        public LatestMessageQuery(
            IUserSession userSession,
            bool? isTest = null)
        {
            CustId = userSession.CustId;
            CurrentLang = userSession.Language;
            RoleId = userSession.RoleId;
            UserName = userSession.UserName;
            IsTest = isTest ?? AppSettings.LocalTest;
        }

        public int CustId { get; }

        public bool IsTest { get; }

        public string CurrentLang { get; }

        public byte RoleId { get; }

        public string UserName { get; }
    }
}