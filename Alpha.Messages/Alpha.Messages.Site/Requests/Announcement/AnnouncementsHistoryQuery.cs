namespace Alpha.Messages.Site.Requests.Announcement
{
    using System;
    using Alpha.Announcements.Models;
    using MediatR;
    using Params.Announcement;
    using Sunplus.Agent.Model.Entities;
    using ViewModels;

    public class AnnouncementsHistoryQuery : IRequest<AnnouncementHistoryViewModel>
    {
        private readonly IUserSession userSession;

        public AnnouncementsHistoryQuery(IUserSession userSession)
        {
            this.userSession = userSession;
            ToDate = DateTime.Now;
        }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public IParam Specification { get; set; }
        
        public int UserId => userSession.CustId;

        public int SubAccId => userSession.SubAccId;

        public byte RoleId => userSession.RoleId;

        public int AdminId => userSession.AdminId;        

        public int CustId => userSession.CustId;

        public string Language => userSession.Language;

        public string UserName => userSession.UserName;

        public string Site => userSession.Site;
        
        public AnnouncementsHistoryParams ConvertToParameters()
        {
            return new AnnouncementsHistoryParams
            {
                AdminId = AdminId,
                CustId = CustId,
                UserId = UserId,
                Language = Language,
                RoleId = RoleId,
                Site = Site,
                SubAccId = SubAccId,
                UserName = UserName,
                FromDate = FromDate,
                ToDate = ToDate,
                SpecificationParamter = Specification
            };
        }
    }
}
