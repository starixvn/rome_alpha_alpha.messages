namespace Alpha.Messages.Site.Handlers
{
    using MediatR;
    using Models.Announcement;
    using Params.Announcement;
    using Requests.Announcement;
    using Services;

    public class PopupAnnouncementHandler :
        IRequestHandler<PopupAnnouncementsQuery, AlphaAnnouncement>
    {
        private readonly AnnouncementService announcementService;
        private readonly AnnouncementFormatService formatAnnouncementService;

        public PopupAnnouncementHandler(
            AnnouncementService announcementService,
            AnnouncementFormatService formatService)
        {
            this.announcementService = announcementService;
            this.formatAnnouncementService = formatService;
        }

        public AlphaAnnouncement Handle(PopupAnnouncementsQuery message)
        {
            var parameters = (PopupAnnouncementParams)message.ConvertToParameters();
            var announcement = announcementService.GetPopUpAnnouncements(parameters);
            UpdatePopupIsShowedOnSession(message, announcement != null);            

            return formatAnnouncementService.Format(announcement, parameters);
        }

        private static void UpdatePopupIsShowedOnSession(PopupAnnouncementsQuery message, bool isShowed)
        {
            message.IsShowedPopup = isShowed;
        }
    }
}
