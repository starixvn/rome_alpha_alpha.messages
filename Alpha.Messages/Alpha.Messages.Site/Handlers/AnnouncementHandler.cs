namespace Alpha.Messages.Site.Handlers
{
    using Constants;
    using MediatR;
    using Params.Announcement;
    using Requests.Announcement;
    using Services;
    using ViewModels;

    public class AnnouncementHandler :
        IRequestHandler<AnnouncementsHistoryQuery, AnnouncementHistoryViewModel>
    {
        private readonly AnnouncementService announcementService;
        private readonly AnnouncementFormatService formatAnnouncementService;
        private readonly NewMessageService newMessageService;

        public AnnouncementHandler(
            AnnouncementService announcementService,
            AnnouncementFormatService formatService,
            NewMessageService newMessageService)
        {
            this.announcementService = announcementService;
            this.formatAnnouncementService = formatService;
            this.newMessageService = newMessageService;
        }

        public AnnouncementHistoryViewModel Handle(AnnouncementsHistoryQuery message)
        {
            var parameters = (AnnouncementsHistoryParams)message.ConvertToParameters();

            var announcements = announcementService.GetAnnouncementsHistory(parameters);
            var formatedAnnouncements = formatAnnouncementService.Format(announcements, parameters);

            var unreadMessage = newMessageService.GetNumberOfUnreadMessage(parameters);

            return new AnnouncementHistoryViewModel(
                formatedAnnouncements,
                parameters.FromDate,
                parameters.ToDate,
                MessageType.System,
                message.RoleId,
                unreadMessage);
        }
    }
}
