using System.Linq;
using Alpha.Messages.Constants;
using Alpha.Messages.Services;
using Alpha.Messages.Site.Requests;
using Alpha.Messages.Site.ViewModels;
using Alpha.Web.Helpers;
using MediatR;
using Sunplus.Agent.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using Alpha.Messages.Models;

namespace Alpha.Messages.Site.Handlers
{
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

            var announcements = announcementService.GetHotAnnouncement(parameters);
            var formatedAnnouncements = formatAnnouncementService.Format(announcements, parameters);

            var cookiesId = Encryption.PearsonEncode(message.UserName + message.UserId + message.SubAccId);

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