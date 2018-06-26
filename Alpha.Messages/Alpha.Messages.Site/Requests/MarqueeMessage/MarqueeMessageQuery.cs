namespace Alpha.Messages.Site.Requests.MarqueeMessage
{
    using MediatR;
    using Sunplus.Agent.Model.BL.Definitions;
    using Sunplus.Agent.Model.Entities;
    using ViewModels;

    public class MarqueeMessageQuery : IRequest<MarqueeMessageViewModel>
    {
        public MarqueeMessageQuery(
            IUserSession userSession,
            bool? isTest = null)
        {
            CustId = userSession.CustId;
            CurrentLang = userSession.Language;
            RoleId = userSession.RoleId;
            IsTest = isTest ?? AppSettings.LocalTest;
        }

        public int CustId { get; }

        public bool IsTest { get; }

        public string CurrentLang { get; }

        public byte RoleId { get; }        
    }
}