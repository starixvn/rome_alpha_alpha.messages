namespace Alpha.Messages.Site.Requests.Announcement
{
    using Constants;
    using MediatR;
    using Models.Announcement;
    using Params.Announcement;
    using AlphaPlus.User.Models;

    public class PopupAnnouncementQuery : MessageHistoryBaseRequest, IRequest<AlphaAnnouncement>
    {
        public PopupAnnouncementQuery(IUserSession userSession) : base(userSession)
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
