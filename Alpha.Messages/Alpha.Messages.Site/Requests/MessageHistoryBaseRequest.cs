namespace Alpha.Messages.Site.Requests
{
    using Alpha.Announcements.Models;
    using Alpha.Messages.Constants;
    using Params.Announcement;
    using Sunplus.Agent.Model.Entities;

    public class MessageHistoryBaseRequest
    {
        protected readonly IUserSession userSession;

        public MessageHistoryBaseRequest(IUserSession userSession)
        {
            this.userSession = userSession;
        }

        public IParam Specification { get; set; }

        public int UserId => userSession.CustId;

        public string ParentSites => MessageConstants.ParentSitesByLevel[userSession.RoleId];

        public int SubAccId => userSession.SubAccId;

        public byte RoleId => userSession.RoleId;

        public int AdminId => userSession.AdminId;

        public bool IsAdminUser => AdminId != 0;

        public int CustId => userSession.CustId;

        public string Language => userSession.Language;

        public string UserName => userSession.UserName;

        public string Site => userSession.Site;

        virtual public BaseAnnouncementsParams ConvertToParameters()
            => new BaseAnnouncementsParams
            {
                AdminId = AdminId,
                CustId = CustId,
                Language = Language,
                RoleId = RoleId,
                Site = Site,
                SubAccId = SubAccId,
                UserName = UserName,
                SpecificationParamter = Specification,
                ParentSites = ParentSites
            };
    }
}