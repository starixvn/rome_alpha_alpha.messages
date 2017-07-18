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

namespace Alpha.Messages.Site.Handlers
{
    public class AnnouncementHandler :
         IRequestHandler<HotAnnouncementQuery, HotAnnouncementViewModel>
    {        
        private readonly AnnouncementService announcementService;
        private readonly AnnouncementFormatService formatAnnouncementService;        

        public AnnouncementHandler(
            AnnouncementService announcementService, 
            AnnouncementFormatService formatService)
        {
            this.announcementService = announcementService;
            this.formatAnnouncementService = formatService;
        }

        public HotAnnouncementViewModel Handle(HotAnnouncementQuery message)
        {
            var parameters = message.ConvertToParameters();            

            var announcements = announcementService.GetHotAnnouncement(parameters);
            var formatedAnnouncements = formatAnnouncementService.Format(announcements, parameters);

            var cookiesId = Encryption.PearsonEncode(message.UserName + message.UserId + message.SubAccId);

            return new HotAnnouncementViewModel(formatedAnnouncements, cookiesId);
        }        
    }
}