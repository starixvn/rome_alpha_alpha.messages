namespace Alpha.Messages.Site.Requests.MarqueeMessage
{
    using MediatR;
    using Sunplus.Agent.Model.BL.Definitions;
    using Sunplus.Agent.Model.Entities;
    using ViewModels;

    public class MarqueeMessageQuery : IRequest<MarqueeMessageViewModel>
    {
        private readonly bool? isTest;

        public MarqueeMessageQuery(IUserSession userSession)
        {
            CustId = userSession.CustId;
            CurrentLang = userSession.Language;
            RoleId = userSession.RoleId;
        }

        public MarqueeMessageQuery(
            IUserSession userSession,
            bool? isTest)
        {
            CustId = userSession.CustId;
            CurrentLang = userSession.Language;
            RoleId = userSession.RoleId;
            this.isTest = isTest;
        }

        public int CustId { get; }

        public bool IsTest
            => this.isTest == null ? AppSettings.LocalTest : this.isTest.Value; 

        public string CurrentLang { get; }

        public byte RoleId { get; }        
    }
}