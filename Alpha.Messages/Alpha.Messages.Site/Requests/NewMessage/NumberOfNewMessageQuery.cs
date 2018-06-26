namespace Alpha.Messages.Site.Requests.NewMessage
{
    using Alpha.Announcements.Models;
    using MediatR;
    using Models.Message;
    using Params.Announcement;
    using Sunplus.Agent.Model.Entities;

    public class NumberOfNewMessageQuery : IRequest<NumberOfUnreadMessage>
    {
        private readonly IUserSession userSession;

        public NumberOfNewMessageQuery(IUserSession userSession)
        {
            this.userSession = userSession;
        }

        public IParam Specification { get; set; }
        
        public int UserId => userSession.CustId;

        public int SubAccId => userSession.SubAccId;

        public byte RoleId => userSession.RoleId;

        public int AdminId => userSession.AdminId;

        public bool IsAdminUser => AdminId != 0;

        public int CustId => userSession.CustId;

        public string Language => userSession.Language;

        public string UserName => userSession.UserName;

        public string Site => userSession.Site;
        
        public NumberOfNewAnnouncementsParams ConvertToParameters()
        {
            return new NumberOfNewAnnouncementsParams
            {
                AdminId = AdminId,
                CustId = CustId,
                UserId = UserId,
                Language = Language,
                RoleId = RoleId,
                Site = Site,
                SubAccId = SubAccId,
                UserName = UserName,
                SpecificationParamter = Specification
            };
        }
    }
}
