namespace Alpha.Messages.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Models.Announcement;
    using Params.Announcement;

    public interface AnnouncementService
    {
        IEnumerable<HotAnnouncement> GetPopupAnnouncement(GetHotAnnouncementsParams getParams);
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

        public IEnumerable<HotAnnouncement> GetPopupAnnouncement(GetHotAnnouncementsParams getParams)
        {
            var announcementsFromApi = apiService.GetPopupAnnouncements(getParams);
            var firstDbAnnouncement = dataBaseSerivce.GetFirstHotAnnouncement(getParams);
            var announcementsFromDb = new List<HotAnnouncement>();

            if (firstDbAnnouncement != null)
            {
                announcementsFromDb.Add(firstDbAnnouncement);
            }

            return announcementsFromDb.Concat(announcementsFromApi);
        }
    }
}
