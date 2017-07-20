using Alpha.Messages.Constants;
using Alpha.Messages.Models;
using Alpha.Messages.Params;
using Alpha.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alpha.Messages.Services
{
    public interface AnnouncementFormatService
    {
        IEnumerable<HotAnnouncement> Format(IEnumerable<HotAnnouncement> announcements, GetHotAnnouncementsParams message);

    }

    public class AnnouncementFormatServiceImpl : AnnouncementFormatService
    {
        public Dictionary<string, Func<string, GetHotAnnouncementsParams, string>> ReplacementCode { get; set; }        

        public AnnouncementFormatServiceImpl()
        {
            ReplacementCode = new Dictionary<string, Func<string, GetHotAnnouncementsParams, string>>();

            // Add Code & proccessed Function to replace
            ReplacementCode.Add("2222", ReplaceSiteMain);
        }

        public IEnumerable<HotAnnouncement> Format(IEnumerable<HotAnnouncement> announcements, GetHotAnnouncementsParams message)
        {
            foreach (var announcement in announcements)
            {
                var containsCode = ReplacementCode.ContainsKey(announcement.Code);
                if (containsCode)
                {
                    announcement.Content = ReplacementCode[announcement.Code](announcement.Content, message);
                }

                yield return announcement;
            }
        }

        private static string ReplaceSiteMain(string content, GetHotAnnouncementsParams param)
        {
            return content.Replace(TagMessage.SiteMain, param.IsMobileDevice ? MicroSites.MainUrl("~") : string.Empty);
        }        
    }
}