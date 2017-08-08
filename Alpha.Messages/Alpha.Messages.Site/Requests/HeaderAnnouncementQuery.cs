using Alpha.Announcements.Models;
using Alpha.Messages.Models;
using Alpha.Messages.Params;
using MediatR;
using Sunplus.Agent.Model.Entities;
using System.Collections.Generic;

namespace Alpha.Messages.Site.Requests
{
    public class HeaderAnnouncementQuery : IRequest<IEnumerable<HotAnnouncement>>
    {
        private readonly IUserSession userSession;

        public HeaderAnnouncementQuery(IUserSession userSession)
        {
            this.userSession = userSession;
        }

        public IParam Specification { get; set; }
        
        public int UserId => userSession.CustId;

        public int SubAccId => userSession.SubAccId;

        public byte RoleId => userSession.RoleId;

        public int AdminId => userSession.AdminId;

        public int CustId => userSession.CustId;

        public string Language => userSession.Language;

        public string UserName => userSession.UserName;

        public string Site => userSession.Site;
        
        public GetHotAnnouncementsParams ConvertToParameters()
        {
            return new GetHotAnnouncementsParams
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
