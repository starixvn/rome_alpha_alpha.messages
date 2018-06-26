namespace Alpha.Messages.Site.Handlers
{
    using Constants;
    using MediatR;
    using Requests.Announcement;
    using Services;
    using ViewModels;

    public class AnnouncementHandler :
        IRequestHandler<AnnouncementsHistoryQuery, AnnouncementHistoryViewModel>
    {
        private readonly AnnouncementService announcementService;
        private readonly AnnouncementFormatService formatAnnouncementService;
        private readonly NewMessageService messageService;
        public AnnouncementHandler(
            AnnouncementService announcementService,
            AnnouncementFormatService formatService,
            NewMessageService messageService)
        {
            this.announcementService = announcementService;
            this.formatAnnouncementService = formatService;
            this.messageService = messageService;
        }
      
        public AnnouncementHistoryViewModel Handle(AnnouncementsHistoryQuery message)
        {
            var unreadMessage = messageService.GetNumberOfUnreadMessage(message.CustId);

            var parameters = message.ConvertToParameters();
            var announcements = announcementService.GetAnnouncementsHistory(parameters);
            var formatedAnnouncements = formatAnnouncementService.Format(announcements, parameters);            

            return new AnnouncementHistoryViewModel(
                formatedAnnouncements
                , parameters.FromDate
                , parameters.ToDate
                , MessageType.System
                , message.RoleId
                , unreadMessage);
        }
    }
}
