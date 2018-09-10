namespace Alpha.Messages.Site.Requests.Announcement
{
    using Constants;
    using MediatR;
    using Models.Announcement;
    using Params.Announcement;
    using Sunplus.Agent.Model.Entities;

    public class PoupAnnouncementQuery : MessageHistoryBaseRequest, IRequest<AlphaAnnouncement>
    {
        public PoupAnnouncementQuery(IUserSession userSession) : base(userSession)
        {            
        }

        
        public bool IsShowedPopup
        {
            get
            {
                return userSession.GetProperty<bool>(SessionKey.ShowedPopupAnnouncement);
            }
            set
            {        
                userSession.SetProperty(SessionKey.ShowedPopupAnnouncement, value);
            }
        }

        public override BaseAnnouncementsParams ConvertToParameters()
            => new PopupAnnouncementParams
            {
                AdminId = AdminId,
                CustId = CustId,
                Language = Language,
                RoleId = RoleId,
                Site = Site,
                SubAccId = SubAccId,
                UserName = UserName,
                SpecificationParamter = Specification,
                ParentSites = ParentSites,
                IsShowedPopup = IsShowedPopup
            };        
    }
}
