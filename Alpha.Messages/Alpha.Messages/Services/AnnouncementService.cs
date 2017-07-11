using Alpha.Announcements.Models;
using Alpha.Messages.Models;
using Alpha.Messages.Params;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Messages.Services
{
    public interface AnnouncementService
    {
        IEnumerable<HotAnnouncement> GetHotAnnouncement(GetHotAnnouncementsParams getParams);
    }

    public class AnnouncementServiceImpl : AnnouncementService
    {        
        private readonly AnnouncementFromApiService apiService;
        private readonly AnnouncementFromDatabaseService dataBaseSerivce;

        public AnnouncementServiceImpl(
            AnnouncementFromApiService apiService,
            AnnouncementFromDatabaseService dataBaseSerivce)
        {
            this.apiService = apiService;
            this.dataBaseSerivce = dataBaseSerivce;
        }

        public IEnumerable<HotAnnouncement> GetHotAnnouncement(GetHotAnnouncementsParams getParams)
        {            
            var announcementsFromApi = apiService.GetHotAnnouncements(getParams);
            var announcementsFromDb = dataBaseSerivce.GetHotAnnouncements(getParams);

            return announcementsFromDb.Concat(announcementsFromApi);
        }
    }
}
