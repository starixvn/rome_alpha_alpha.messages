using Alpha.Announcements.Models;
using Alpha.Messages.Client.Requests;
using Alpha.Messages.Configurations;
using Alpha.Messages.Models;
using Alpha.Messages.Params;
using Alpha.Messages.Site.Requests;
using Alpha.Messages.Site.ViewModels;
using MediatR;
using Sunplus.Agent.Model.Entities;
using System.Collections.Generic;
using System.Web;

namespace Alpha.Messages.Site.Requests
{
    public class PopupAnnouncementQuery : IRequest<HotAnnouncementViewModel>
    {
        private readonly IUserSession userSession;

        public PopupAnnouncementQuery(IUserSession userSession, bool? isMobileDevice)
        {
            this.userSession = userSession;
            IsMobileDevice = isMobileDevice ?? HttpContext.Current.Request.Browser.IsMobileDevice;
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

        public string[] NoticeTypes { get; set; } = AppSettings.NoticeTypes;

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
