using Alpha.Announcements.Models;
using Alpha.Messages.Client.Requests;
using Alpha.Messages.Models;
using Alpha.Messages.Params;
using Alpha.Messages.Site.Requests;
using Alpha.Messages.Site.ViewModels;
using MediatR;
using Sunplus.Agent.Model.Entities;
using System.Collections.Generic;

namespace Alpha.Messages.Site.Requests
{
    public class HotAnnouncementQuery : IRequest<HotAnnouncementViewModel>
    {
#pragma warning disable CS0618 // 'IUserSession' is obsolete: 'Should rename to Sunplus.Agent.Model.Base'
        private readonly IUserSession userSession;
#pragma warning restore CS0618 // 'IUserSession' is obsolete: 'Should rename to Sunplus.Agent.Model.Base'
              
#pragma warning disable CS0618 // 'IUserSession' is obsolete: 'Should rename to Sunplus.Agent.Model.Base'
        public HotAnnouncementQuery(IUserSession userSession)
#pragma warning restore CS0618 // 'IUserSession' is obsolete: 'Should rename to Sunplus.Agent.Model.Base'
        {
            this.userSession = userSession;
        }

        public IParam Specification { get; set; }

        public bool IsMobileDevice { get; set; }

        public int UserId => userSession.UserId;

        public int SubAccId => userSession.SubAccId;

        public byte RoleId => userSession.RoleId;

        public int AdminId => userSession.AdminId;

        public int CustId => userSession.CustId;

        public string Language => userSession.Language;

        public string UserName => userSession.UserName;

        public string Site => userSession.Site;

        public string[] NoticeTypes { get; set; }

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
                NoticeTypes = NoticeTypes,
                IsMobileDevice = IsMobileDevice,
                SpecificationParamter = Specification
            };
        }
    }
}
