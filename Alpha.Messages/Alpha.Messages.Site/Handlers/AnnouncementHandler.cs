namespace Alpha.Messages.Site.Handlers
{
    using System.Collections.Generic;
    using MediatR;
    using Models.Announcement;
    using Models;
    using Requests;
    using Services;
    using Sunplus.Agent.Utilities;
    using ViewModels;

    public class AnnouncementHandler :
         IRequestHandler<PopupAnnouncementQuery, HotAnnouncementViewModel>,
        IRequestHandler<HeaderAnnouncementQuery, IEnumerable<HotAnnouncement>>
    {
        private readonly AnnouncementService announcementService;
        private readonly AnnouncementFromApiService announcementApiService;
        private readonly AnnouncementFormatService formatAnnouncementService;

        public AnnouncementHandler(
            AnnouncementService announcementService,
            AnnouncementFormatService formatService,
            AnnouncementFromApiService announcementApiService)
        {
            this.announcementService = announcementService;
            this.formatAnnouncementService = formatService;
            this.announcementApiService = announcementApiService;
        }

        public HotAnnouncementViewModel Handle(PopupAnnouncementQuery message)
        {
            var parameters = message.ConvertToParameters();

            var announcements = announcementService.GetPopupAnnouncement(parameters);
            var formatedAnnouncements = formatAnnouncementService.Format(announcements, parameters);

            var cookiesId = Encryption.PearsonEncode(message.UserName + message.CustId + message.SubAccId);

            return new HotAnnouncementViewModel(formatedAnnouncements, cookiesId);
        }

        public IEnumerable<HotAnnouncement> Handle(HeaderAnnouncementQuery message)
        {
            var parameters = message.ConvertToParameters();

            var announcements = announcementApiService.GetHeaderAnnouncements(parameters);

            return announcements;
        }
    }
}