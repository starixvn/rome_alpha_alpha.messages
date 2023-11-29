namespace Alpha.Messages.Site.Requests.Announcement
{
    using System;
    using MediatR;
    using Params.Announcement;
    using AlphaPlus.User.Models;
    using ViewModels;

    public class AnnouncementsHistoryQuery : MessageHistoryBaseRequest, IRequest<AnnouncementHistoryViewModel>
    {
        public AnnouncementsHistoryQuery(IUserSession userSession) : base(userSession)
        {
            ToDate = DateTime.Now;
        }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        override public BaseAnnouncementsParams ConvertToParameters()
        {
            return new AnnouncementsHistoryParams
            {
                AdminId = AdminId,
                CustId = CustId,
                Language = Language,
                RoleId = RoleId,
                Site = Site,
                ParentSites = ParentSites,
                SubAccId = SubAccId,
                UserName = UserName,
                FromDate = FromDate,
                ToDate = ToDate.AddDays(1).AddSeconds(-1),
                SpecificationParamter = Specification
            };
        }
    }
}
